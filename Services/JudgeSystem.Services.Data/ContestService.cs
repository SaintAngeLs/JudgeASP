﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using JudgeSystem.Common;
using JudgeSystem.Common.Exceptions;
using JudgeSystem.Data.Common.Repositories;
using JudgeSystem.Data.Models;
using JudgeSystem.Services.Mapping;
using JudgeSystem.Web.Dtos.Submission;
using JudgeSystem.Web.Infrastructure.Pagination;
using JudgeSystem.Web.InputModels.Contest;
using JudgeSystem.Web.ViewModels.Contest;
using JudgeSystem.Web.ViewModels.Problem;
using JudgeSystem.Web.ViewModels.Student;

using Microsoft.EntityFrameworkCore;

namespace JudgeSystem.Services.Data
{
    public class ContestService : IContestService
	{
		public const int ResultsPerPage = 15;

		private readonly IDeletableEntityRepository<Contest> repository;
		private readonly IRepository<UserContest> userContestRepository;
        private readonly IRepository<AllowedIpAddressContest> allowedIpAddressContestRepository;
        private readonly ILessonService lessonService;
        private readonly IProblemService problemService;
        private readonly ISubmissionService submissionService;
        private readonly IPaginationService paginationService;

        public ContestService(
            IDeletableEntityRepository<Contest> repository,
			IRepository<UserContest> userContestRepository,
            IRepository<AllowedIpAddressContest> allowedIpAddressContestRepository,
            ILessonService lessonService,
            IProblemService problemService,
            ISubmissionService submissionService,
            IPaginationService paginationService)
		{
			this.repository = repository;
			this.userContestRepository = userContestRepository;
            this.allowedIpAddressContestRepository = allowedIpAddressContestRepository;
            this.lessonService = lessonService;
            this.problemService = problemService;
            this.submissionService = submissionService;
            this.paginationService = paginationService;
        }

		public async Task<bool> AddUserToContestIfNotAdded(string userId, int contestId)
		{
			if(userContestRepository.All().SingleOrDefault(uc => uc.UserId == userId && uc.ContestId == contestId) != null)
			{
				return false;
			}

			await userContestRepository.AddAsync(new UserContest { UserId = userId, ContestId = contestId });
			return true;
		}

        public async Task Create(ContestCreateInputModel contestCreateInputModel)
		{
            Contest contest = contestCreateInputModel.To<Contest>();
			await repository.AddAsync(contest);
		}

		public IEnumerable<ActiveContestViewModel> GetActiveContests()
		{
            var now = DateTime.Now;

            var contests = repository.All()
                .Where(c => c.StartTime < now && c.EndTime > now)
                .To<ActiveContestViewModel>()
                .ToList();

            return contests;
        }

		public async Task<T> GetById<T>(int contestId)
		{
            Contest contest = await repository.FindAsync(contestId);
			return contest.To<T>();
		}

		public IEnumerable<ContestBreifInfoViewModel> GetActiveAndFollowingContests()
		{
			var followingContests = repository.All()
				.Where(c => c.EndTime > DateTime.Now)
				.To<ContestBreifInfoViewModel>()
				.ToList();

			return followingContests;
		}

		public IEnumerable<PreviousContestViewModel> GetPreviousContests(int passedDays)
		{
			var contests = repository.All()
                .Where(c => c.EndTime < DateTime.Now && c.EndTime >= DateTime.Now.AddDays(-passedDays))
                .To<PreviousContestViewModel>()
                .ToList();

			return contests;
		}
        public bool IsActive(int contestId)
        {
            var now = DateTime.Now;

            return repository.All().Any(x => x.Id == contestId && x.StartTime < now && x.EndTime > now);
        }


        public async Task Update(ContestEditInputModel model)
		{
            Contest contest = await repository.FindAsync(model.Id);
            contest.Name = model.Name;
			contest.StartTime = model.StartTime;
			contest.EndTime = model.EndTime;

			await repository.UpdateAsync(contest);
		}

		public async Task Delete(int id)
		{
            Contest contest = await repository.FindAsync(id);
            await repository.DeleteAsync(contest);
		}

		public IEnumerable<ContestViewModel> GetAllConests(int page)
		{
			var contests = repository.All()
				.OrderByDescending(c => c.StartTime)
				.Skip((page - 1) * GlobalConstants.ContestsPerPage)
				.Take(GlobalConstants.ContestsPerPage)
				.To<ContestViewModel>()
				.ToList();

			return contests;
		}

		public int GetNumberOfPages()
		{
			int numberOfContests = repository.All().Count();
			return (int)Math.Ceiling((double)numberOfContests / GlobalConstants.ContestsPerPage);
		}

        public ContestAllResultsViewModel GetContestReults(int contestId, int page, int entitiesPerPage)
        {
            var contest = repository.All()
                .Include(c => c.Lesson.Problems)
                .Include(c => c.UserContests)
                    .ThenInclude(uc => uc.User)
                    .ThenInclude(u => u.Student)
                .Include(c => c.UserContests)
                    .ThenInclude(uc => uc.User)
                    .ThenInclude(u => u.Submissions)
                .FirstOrDefault(c => c.Id == contestId);

            Validator.ThrowEntityNotFoundExceptionIfEntityIsNull(contest, nameof(Contest));

            ContestAllResultsViewModel model = new ContestAllResultsViewModel()
            {
                Id = contest.Id,
                Name = contest.Name,
                Problems = contest.Lesson.Problems
                    .OrderBy(p => p.CreatedOn)
                    .Select(p => new ContestProblemViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        IsExtraTask = p.IsExtraTask,
                        MaxPoints = p.MaxPoints
                    })
                    .ToList(),
                ContestResults = contest.UserContests
                    .Where(u => u.User.StudentId != null && u.User.Student.SchoolClass != null)
                    .Select(uc => new ContestResultViewModel
                    {
                        UserId = uc.User.Id,
                        Student = new StudentBreifInfoViewModel
                        {
                            ClassNumber = uc.User.Student.SchoolClass?.ClassNumber ?? default,
                            ClassType = uc.User.Student.SchoolClass?.ClassType.ToString(),
                            FullName = uc.User.Student.FullName,
                            NumberInCalss = uc.User.Student.NumberInCalss
                        },
                        PointsByProblem = uc.User.Submissions
                            .Where(s => s.ContestId == contestId)
                            .GroupBy(s => s.ProblemId)
                            .ToDictionary(s => s.Key, x => x.Max(s => s.ActualPoints))
                    })
                    .OrderByDescending(cr => cr.Total)
                    .ThenBy(cr => cr.Student.ClassNumber)
                    .ThenBy(cr => cr.Student.ClassType)
                    .ThenBy(cr => cr.Student.NumberInCalss)
                    .Skip((page - 1) * entitiesPerPage)
                    .Take(entitiesPerPage)
                    .ToList(),
            };

            return model;
        }


        public int GetContestResultsPagesCount(int contestId, int entitiesPerPage)
        {
            ThrowEntityNotFoundExceptionIfContestDoesNotExist(contestId);

            int count = repository.All()
                .Include(c => c.UserContests)
                .ThenInclude(uc => uc.User)
                .FirstOrDefault(c => c.Id == contestId)
                .UserContests
                .Where(uc => uc.User.StudentId != null)
                .Count();

            return paginationService.CalculatePagesCount(count, entitiesPerPage);
        }

        public async Task<int> GetLessonId(int contestId)
        {
            Contest contest = await repository.FindAsync(contestId);
            return contest.LessonId;
        }

        public async Task<ContestSubmissionsViewModel> GetContestSubmissions(int contestId, string userId, int? problemId, int page, string baseUrl)
        {
            int baseProblemId = 0;
            Contest contest = await repository.FindAsync(contestId);
            int lessonId = contest.LessonId;
            if (problemId.HasValue)
            {
                baseProblemId = problemId.Value;
            }
            else
            {
                baseProblemId = lessonService.GetFirstProblemId(lessonId) ?? baseProblemId;
            }

            IEnumerable<SubmissionResult> submissions = submissionService.GetUserSubmissionsByProblemIdAndContestId(contestId, baseProblemId, userId, page, GlobalConstants.SubmissionsPerPage);
            string problemName = problemService.GetProblemName(baseProblemId);

            int submissionsCount = submissionService.GetSubmissionsCountByProblemIdAndContestId(baseProblemId, contestId, userId);

            var paginationData = new PaginationData
            {
                CurrentPage = page,
                NumberOfPages = paginationService.CalculatePagesCount(submissionsCount, GlobalConstants.SubmissionsPerPage),
                Url = baseUrl + $"{GlobalConstants.QueryStringDelimiter}{GlobalConstants.ProblemIdKey}={baseProblemId}{GlobalConstants.QueryStringDelimiter}{GlobalConstants.PageKey}=" + "{0}"
            };

            var model = new ContestSubmissionsViewModel
            {
                ProblemName = problemName,
                Submissions = submissions,
                LessonId = lessonId,
                UrlPlaceholder = baseUrl + $"{GlobalConstants.QueryStringDelimiter}{GlobalConstants.ProblemIdKey}=" + "{0}",
                PaginationData = paginationData,
                ContestName = contest.Name,
                UserId = userId
            };
            return model;
        }

        //public bool IsActive(int contestId) => repository.All().Any(x => x.Id == contestId && x.IsActive);
        
        private void ThrowEntityNotFoundExceptionIfContestDoesNotExist(int contestId)
        {
            if (!repository.All().Any(x => x.Id == contestId))
            {
                throw new EntityNotFoundException(nameof(Contest));
            }
        }

        public Task AddAllowedIpAddress(ContestAllowedIpAddressesInputModel model, int id) => 
            allowedIpAddressContestRepository.AddAsync(new AllowedIpAddressContest
            {
                AllowedIpAddressId = model.IpAddressId,
                ContestId = id
            });

        public async Task RemoveAllowedIpAddress(int contestId, int ipAddressId)
        {
            AllowedIpAddressContest entry = allowedIpAddressContestRepository.All()
                .FirstOrDefault(x => x.AllowedIpAddressId == ipAddressId && x.ContestId == contestId);
            await allowedIpAddressContestRepository.DeleteAsync(entry);
        }

        public bool IsRestricted(int contestId, string ip) => 
            repository.All()
                .Include(c => c.AllowedIpAddresses)
                .ThenInclude(a => a.AllowedIpAddress)
                .Where(c => c.Id == contestId && c.AllowedIpAddresses.Any())
                .Any(c => !c.AllowedIpAddresses.Any(a => a.AllowedIpAddress.Value == ip));
    }
}
