﻿// <auto-generated />
using System;
using JudgeSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JudgeSystem.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210922081004_UserValidationsAndAddedColumn")]
    partial class UserValidationsAndAddedColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JudgeSystem.Data.Models.AllowedIpAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("AllowedIpAddresses");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.AllowedIpAddressContest", b =>
                {
                    b.Property<int>("AllowedIpAddressId");

                    b.Property<int>("ContestId");

                    b.HasKey("AllowedIpAddressId", "ContestId");

                    b.HasIndex("ContestId");

                    b.ToTable("AllowedIpAddressContests");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsFromActiveDirectory");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("StudentId");

                    b.Property<string>("Surname")
                        .HasMaxLength(100);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("StudentId")
                        .IsUnique()
                        .HasFilter("[StudentId] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<DateTime>("EndTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LessonId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LessonId");

                    b.ToTable("Contests");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("OrderBy");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.ExecutedTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Error");

                    b.Property<int>("ExecutionResultType");

                    b.Property<bool>("IsCorrect");

                    b.Property<double>("MemoryUsed");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Output");

                    b.Property<int>("SubmissionId");

                    b.Property<int>("TestId");

                    b.Property<double>("TimeUsed");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.HasIndex("TestId");

                    b.ToTable("ExecutedTests");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .HasMaxLength(1500);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("SenderId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("SenderId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LessonPassword");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("OrderBy");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Practice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LessonId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LessonId")
                        .IsUnique();

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Problem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("AllowedMemoryInMegaBytes");

                    b.Property<int>("AllowedMinCodeDifferenceInPercentage");

                    b.Property<int>("AllowedTimeInMilliseconds");

                    b.Property<byte[]>("AutomatedTestingProject");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsExtraTask");

                    b.Property<int>("LessonId");

                    b.Property<int>("MaxPoints");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("OrderBy");

                    b.Property<int>("SubmissionType");

                    b.Property<int>("TestingStrategy")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.Property<int>("TimeIntervalBetweenSubmissionInSeconds");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("LessonId");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<int>("LessonId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("OrderBy");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClassNumber");

                    b.Property<int>("ClassType");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivationKeyHash")
                        .IsRequired();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsActivated");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("NumberInCalss");

                    b.Property<int>("SchoolClassId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualPoints");

                    b.Property<byte[]>("Code")
                        .IsRequired();

                    b.Property<byte[]>("CompilationErrors");

                    b.Property<int?>("ContestId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int?>("PracticeId");

                    b.Property<int>("ProblemId");

                    b.Property<DateTime>("SubmisionDate");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("PracticeId");

                    b.HasIndex("ProblemId");

                    b.HasIndex("UserId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InputData");

                    b.Property<bool>("IsTrialTest");

                    b.Property<int>("OrderBy");

                    b.Property<string>("OutputData")
                        .IsRequired();

                    b.Property<int>("ProblemId");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.UserContest", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("ContestId");

                    b.HasKey("UserId", "ContestId");

                    b.HasIndex("ContestId");

                    b.ToTable("UserContests");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.UserPractice", b =>
                {
                    b.Property<int>("PracticeId");

                    b.Property<string>("UserId");

                    b.HasKey("PracticeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPractices");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.AllowedIpAddressContest", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.AllowedIpAddress", "AllowedIpAddress")
                        .WithMany("Contests")
                        .HasForeignKey("AllowedIpAddressId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.Contest", "Contest")
                        .WithMany("AllowedIpAddresses")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Student", "Student")
                        .WithOne("User")
                        .HasForeignKey("JudgeSystem.Data.Models.ApplicationUser", "StudentId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Contest", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Lesson", "Lesson")
                        .WithMany("Contests")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.ExecutedTest", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Submission", "Submission")
                        .WithMany("ExecutedTests")
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.Test", "Test")
                        .WithMany("ExecutedTests")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Feedback", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Lesson", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Course", "Course")
                        .WithMany("Lessons")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Practice", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Lesson", "Lesson")
                        .WithOne("Practice")
                        .HasForeignKey("JudgeSystem.Data.Models.Practice", "LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Problem", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Lesson", "Lesson")
                        .WithMany("Problems")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Resource", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Lesson", "Lesson")
                        .WithMany("Resources")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Student", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Submission", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Contest", "Contest")
                        .WithMany("Submissions")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.Practice", "Practice")
                        .WithMany("Submissions")
                        .HasForeignKey("PracticeId");

                    b.HasOne("JudgeSystem.Data.Models.Problem", "Problem")
                        .WithMany("Submissions")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("Submissions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.Test", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Problem", "Problem")
                        .WithMany("Tests")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.UserContest", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Contest", "Contest")
                        .WithMany("UserContests")
                        .HasForeignKey("ContestId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("UserContests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("JudgeSystem.Data.Models.UserPractice", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.Practice", "Practice")
                        .WithMany("UserPractices")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser", "User")
                        .WithMany("UserPractices")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("JudgeSystem.Data.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
