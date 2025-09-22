using Application.DTO;
using Application.Queries.DashboardQueries;
using Application.Queries.EmailGroupQueries;
using Application.Queries.EmailListQueries;
using Application.Queries.EmailProjectQueries;
using Application.Queries.EmailResponseStatusQueries;
using Application.Queries.EmailSendingStatusQueries;
using Application.Queries.GroupSendingProjectQueries;
using Application.Queries.ServerQueries;
using AWS.Services;
using Domain.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PostmarkEmailService;
using XYZSMS.Services;


namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("AppConnection") ?? throw new InvalidOperationException("Connection string 'MovieConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));
            //
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));


            //services.AddMediatR(typeof(ListTrainingWithDetailsQueryHandler).Assembly);
            services.AddTransient<PostmarkClient>(_ => new PostmarkClient(configuration.GetSection("PostmarkSettings")["ServerToken"]));
            // Register StorageService
            services.AddTransient<IStorageService, StorageService>();

            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddTransient<IAttendanceRepository, AttendanceRepository>();
            services.AddTransient<IDialyActivityRepository, DialyActivityRepository>();
            services.AddTransient<IEducationRepository, EducationRepository>();
            services.AddTransient<IExperienceRepository, ExperienceRepository>();
            services.AddTransient<IModuleRepository, ModuleRepository>();
            services.AddTransient<IModuleTopicRepository, ModuleTopicRepository>();
            services.AddTransient<ISponsorRepository, SponsorRepository>();
            services.AddTransient<ITestCategoryRepository, TestCategoryRepository>();
            services.AddTransient<ITestSheetRepository, TestSheetRepository>();
            services.AddTransient<ITrainingRepository, TrainingRepository>();
            services.AddTransient<ITrainingFacilitatorRepository, TrainingFacilitatorRepository>();
            services.AddTransient<ITrainingParticipantRepository, TrainingParticipantRepository>();
            services.AddTransient<IProfileCategoryListRepository, ProfileCategoryListRepository>();
            services.AddTransient<IProfileCategoryRepository, ProfileCategoryRepository>();
            services.AddTransient<ICertificateRepository, CertificateRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();
            services.AddTransient<ITrainingTestRepository, TrainingTestRepository>();
            services.AddTransient<ITrainingTestOptionRepository, TrainingTestOptionRepository>();
            services.AddTransient<IUserTestRepository, UserTestRepository>();
            services.AddTransient<IEvaluationQuestionRepository, EvaluationQuestionRepository>();
            services.AddTransient<IDialyUserEvaluationRepository, DialyUserEvaluationRepository>();
            services.AddTransient<IEvaluationQuestionCategoryRepository, EvaluationQuestionCategoryRepository>();
            services.AddTransient<IDialyEvaluationQuestionRepository, DialyEvaluationQuestionRepository>();
            services.AddTransient<ITimeTableRepository, TimeTableRepository>();
            services.AddTransient<IGalleryRepository, GalleryRepository>();
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            services.AddTransient<ITrainingCategoryRepository, TrainingCategoryRepository>();
            services.AddTransient<ITrainingSettingRepository, TrainingSettingRepository>();
            services.AddTransient<ITrainingImageRepository, TrainingImageRepository>();
            services.AddTransient<IEvaluationQuestionOptionRepository, EvaluationQuestionOptionRepository>();
            services.AddTransient<IDialyEvaluationQuestionOptionRepository, DialyEvaluationQuestionOptionRepository>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;


            });


            services.AddTransient<IEmailListRepository, EmailListRepository>();
            services.AddTransient<IEmailProjectRepository, EmailProjectRepository>();
            services.AddTransient<IEmailResponseStatusRepository, EmailResponseStatusRepository>();
            services.AddTransient<IEmailSendingStatusRepository, EmailSendingStatusRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IEmailGroupRepository, EmailGroupRepository>();
            services.AddTransient<IGroupSendingProjectRepository, GroupSendingProjectRepository>();
            

            //Register Queries and Handlers
             services.AddTransient<IRequestHandler<GetDashboardQuery, AdminDashboardDto>, GetDashboardQuery.GetDashboardQueryHandler>();
            services.AddTransient<IRequestHandler<ListProjectByUserId, List<EmailProject>>, ListProjectByUserId.ListProjectByUserIdHandler>();
            services.AddTransient<IRequestHandler<GetByIdEmailProjectQuery, EmailProject>, GetByIdEmailProjectQuery.GetByIdEmailProjectQueryHandler>();

            services.AddTransient<IRequestHandler<GetByIdEmailGroupQuery, EmailGroup>, GetByIdEmailGroupQuery.GetByIdEmailGroupQueryHandler>();
            services.AddTransient<IRequestHandler<ListAllByUserIdEmailGroupQuery, List<EmailGroup>>, ListAllByUserIdEmailGroupQuery.ListAllByUserIdEmailGroupQueryHandler>();


            services.AddTransient<IRequestHandler<ListAllByUserIdEmailListQuery, List<EmailList>>, ListAllByUserIdEmailListQuery.ListAllByUserIdEmailListQueryHandler>();
            services.AddTransient<IRequestHandler<ListByGroupIdEmailListQuery, List<EmailList>>, ListByGroupIdEmailListQuery.ListByGroupIdEmailListQueryHandler>();
            services.AddTransient<IRequestHandler<ListByUserIdServerQuery, List<Server>>, ListByUserIdServerQuery.ListByUserIdServerQueryHandler>();
            services.AddTransient<IRequestHandler<ListGroupSendingProjectByProjectIdQuery, List<GroupSendingProject>>, ListGroupSendingProjectByProjectIdQuery.ListGroupSendingProjectByProjectIdQueryHandler>();
            services.AddTransient<IRequestHandler<ListByUserIdEmailSendingStatusQuery, IEnumerable<EmailSendingStatus>>, ListByUserIdEmailSendingStatusQuery.ListByUserIdEmailSendingStatusQueryHandler>();
            services.AddTransient<IRequestHandler<ListByQueryEmailResponseStatusQuery, IEnumerable<EmailResponseStatus>>, ListByQueryEmailResponseStatusQuery.ListByQueryEmailResponseStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetTotalCountEmailSendingStatusQuery, int>, GetTotalCountEmailSendingStatusQuery.GetTotalCountEmailSendingStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetTotalCountEmailResponseStatusQuery, int>, GetTotalCountEmailResponseStatusQuery.GetTotalCountEmailResponseStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetByIdEmailSendingStatusQuery, EmailSendingStatus>, GetByIdEmailSendingStatusQuery.GetByIdEmailSendingStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetEmailListIdbyMessageIdQuery, WebHookUpdateIds>, GetEmailListIdbyMessageIdQuery.GetEmailListIdbyMessageIdQueryHandler>();
            services.AddTransient<IRequestHandler<ListAllEmailResponseByMessageIdQuery, MailInfoDto>, ListAllEmailResponseByMessageIdQuery.ListAllEmailResponseByMessageIdQueryHandler>();



            return services;
        }
    }
}
