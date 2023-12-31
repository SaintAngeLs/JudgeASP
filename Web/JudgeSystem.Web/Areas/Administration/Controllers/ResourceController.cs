﻿using System.Collections.Generic;
using System.Threading.Tasks;

using JudgeSystem.Common;
using JudgeSystem.Services.Data;
using JudgeSystem.Web.ViewModels.Resource;
using JudgeSystem.Web.InputModels.Resource;
using JudgeSystem.Web.Filters;
using JudgeSystem.Services;
using JudgeSystem.Web.Dtos.Resource;

using Microsoft.AspNetCore.Mvc;

namespace JudgeSystem.Web.Areas.Administration.Controllers
{
    public class ResourceController : AdministrationBaseController
	{
		private readonly IResourceService resourceService;
        private readonly ILessonService lessonService;
        // private readonly IAzureStorageService azureStorageService;
        private readonly IAwsS3Service awsS3Service;
        private readonly IValidationService validationService;

        public ResourceController(
            IResourceService resourceService, 
            ILessonService lessonService,
            // IAzureStorageService azureStorageService,
            IAwsS3Service awsS3Service,
            IValidationService validationService)
		{
			this.resourceService = resourceService;
            this.lessonService = lessonService;
            // this.azureStorageService = azureStorageService;
            this.awsS3Service = awsS3Service;
            this.validationService = validationService;
        }

        public IActionResult Create() => View();

        [ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Create(ResourceInputModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
            if(!validationService.IsValidFileExtension(model.File.FileName))
            {
                ModelState.AddModelError(nameof(ResourceInputModel.File), 
                    string.Format(ErrorMessages.UnsupportedFileFormat, System.IO.Path.GetExtension(model.File.FileName)));
                return View(model);
            }

            using(System.IO.Stream stream = model.File.OpenReadStream())
            {
                //string filePath = await azureStorageService.Upload(stream, model.File.FileName, model.Name);
                string filePath = await awsS3Service.Upload(stream, model.File.FileName, "judgesystembucket");
                await resourceService.CreateResource(model, filePath);
            }

            return RedirectToAction("Details", "Lesson", new { id = model.LessonId, model.PracticeId });
		}

		public IActionResult LessonResources(int lessonId, int practiceId)
		{
			IEnumerable<ResourceViewModel> resources = resourceService.LessonResources(lessonId);
            var model = new AllResourcesViewModel { Resources = resources, LessonId = lessonId, PracticeId = practiceId };
            return View(model);
		}

		public async Task<IActionResult> Edit(int id)
		{
            ResourceEditInputModel model = await resourceService.GetById<ResourceEditInputModel>(id);
			return View(model);
		}

        [ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Edit(ResourceEditInputModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
            if (model.File != null && !validationService.IsValidFileExtension(model.File.FileName))
            {
                ModelState.AddModelError(nameof(ResourceEditInputModel.File),
                    string.Format(ErrorMessages.UnsupportedFileFormat, System.IO.Path.GetExtension(model.File.Name)));
                return View(model);
            }

            ResourceDto resource = await resourceService.GetById<ResourceDto>(model.Id);

			if(model.File != null)
			{
                using(System.IO.Stream stream = model.File.OpenReadStream())
                {
                    string filePath = await awsS3Service.Upload(stream, model.File.FileName, "judgesystembucket");
                    await awsS3Service.Delete(resource.FilePath, "judgesystembucket");
                    await resourceService.Update(model, filePath);
                }
			}
            else
            {
                await resourceService.Update(model);
            }

            int practiceId = lessonService.GetPracticeId(resource.LessonId);
			return RedirectToAction(nameof(LessonResources), "Resource", new { lessonId = resource.LessonId, practiceId });
		}

        [EndpointExceptionFilter]
		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
            ResourceDto resource = await resourceService.Delete(id);
            await awsS3Service.Delete(resource.FilePath, "judgesystembucket");

			return Content(string.Format(InfoMessages.SuccessfullyDeletedMessage, resource.Name));
		}



        //private readonly IResourceService resourceService;
        //private readonly ILessonService lessonService;
        //private readonly IAzureStorageService azureStorageService;
        //private readonly IValidationService validationService;

        //public ResourceController(
        //    IResourceService resourceService,
        //    ILessonService lessonService,
        //    IAzureStorageService azureStorageService,
        //    IValidationService validationService)
        //{
        //    this.resourceService = resourceService;
        //    this.lessonService = lessonService;
        //    this.azureStorageService = azureStorageService;
        //    this.validationService = validationService;
        //}

        //public IActionResult Create() => View();

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> Create(ResourceInputModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    if (!validationService.IsValidFileExtension(model.File.FileName))
        //    {
        //        ModelState.AddModelError(nameof(ResourceInputModel.File),
        //            string.Format(ErrorMessages.UnsupportedFileFormat, System.IO.Path.GetExtension(model.File.FileName)));
        //        return View(model);
        //    }

        //    using (System.IO.Stream stream = model.File.OpenReadStream())
        //    {
        //        string filePath = await azureStorageService.Upload(stream, model.File.FileName, model.Name);
        //        await resourceService.CreateResource(model, filePath);
        //    }

        //    return RedirectToAction("Details", "Lesson", new { id = model.LessonId, model.PracticeId });
        //}

        //public IActionResult LessonResources(int lessonId, int practiceId)
        //{
        //    IEnumerable<ResourceViewModel> resources = resourceService.LessonResources(lessonId);
        //    var model = new AllResourcesViewModel { Resources = resources, LessonId = lessonId, PracticeId = practiceId };
        //    return View(model);
        //}

        //public async Task<IActionResult> Edit(int id)
        //{
        //    ResourceEditInputModel model = await resourceService.GetById<ResourceEditInputModel>(id);
        //    return View(model);
        //}

        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public async Task<IActionResult> Edit(ResourceEditInputModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    if (model.File != null && !validationService.IsValidFileExtension(model.File.FileName))
        //    {
        //        ModelState.AddModelError(nameof(ResourceEditInputModel.File),
        //            string.Format(ErrorMessages.UnsupportedFileFormat, System.IO.Path.GetExtension(model.File.Name)));
        //        return View(model);
        //    }

        //    ResourceDto resource = await resourceService.GetById<ResourceDto>(model.Id);

        //    if (model.File != null)
        //    {
        //        using (System.IO.Stream stream = model.File.OpenReadStream())
        //        {
        //            string filePath = await azureStorageService.Upload(stream, model.File.FileName, resource.Name);
        //            await azureStorageService.Delete(resource.FilePath);
        //            await resourceService.Update(model, filePath);
        //        }
        //    }
        //    else
        //    {
        //        await resourceService.Update(model);
        //    }

        //    int practiceId = lessonService.GetPracticeId(resource.LessonId);
        //    return RedirectToAction(nameof(LessonResources), "Resource", new { lessonId = resource.LessonId, practiceId });
        //}

        //[EndpointExceptionFilter]
        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    ResourceDto resource = await resourceService.Delete(id);
        //    await azureStorageService.Delete(resource.FilePath);

        //    return Content(string.Format(InfoMessages.SuccessfullyDeletedMessage, resource.Name));
        //}
    }
}
