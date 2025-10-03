using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        //{
        //}
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(
    DbContextOptions<AppDbContext> options,
    IHttpContextAccessor httpContextAccessor = null)
    : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public DbSet<ProfileCategory> ProfileCategories { get; set; }
        public DbSet<ProfileCategoryList> ProfileCategoryLists { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<DialyActivity> DialyActivities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleTopic> ModuleTopics { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TestCategory> TestCategories { get; set; }
        public DbSet<TestSheet> TestSheets { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingCategory> TrainingCategories { get; set; }
        public DbSet<TrainingFacilitator> TrainingFacilitators { get; set; }
        public DbSet<TrainingParticipant> TrainingParticipants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<TrainingTest> TrainingTests { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<EvaluationQuestion> EvaluationQuestions { get; set; }
        public DbSet<DialyUserEvaluation> DialyUserEvaluations { get; set; }
        public DbSet<EvaluationQuestionCategory> EvaluationQuestionCategories { get; set; }
        public DbSet<DialyEvaluationQuestion> DialyEvaluationQuestions { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<UserCategoryGroup> UserCategoryGroups { get; set; }
        public DbSet<TrainingImage> TrainingImages { get; set; }
        public DbSet<TrainingSetting> TrainingSettings { get; set; }

        public DbSet<EmailList> EmailLists { get; set; }
        public DbSet<EmailProject> EmailProjects { get; set; }
        public DbSet<EmailResponseStatus> EmailResponseStatuses { get; set; }
        public DbSet<EmailSendingStatus> EmailSendingStatuses { get; set; }
        public DbSet<EmailGroup> EmailGroups { get; set; }
        public DbSet<GroupSendingProject> GroupSendingProjects { get; set; }
        public DbSet<Server> Servers { get; set; }


        // Add this to your AppDbContext class
        public DbSet<EntityLogging> EntityLoggings { get; set; }
        public override int SaveChanges()
        {
            LogEntityChanges();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            LogEntityChanges();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void LogEntityChanges()
        {
            var userEmail = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.Email) ?? "System";
            var now = DateTime.UtcNow;

            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in entries)
            {
                // 🔸 Handle role changes
                if (entry.Entity is IdentityUserRole<string> userRole)
                {
                    var roleName = Roles.FirstOrDefault(r => r.Id == userRole.RoleId)?.Name;
                    var targetUser = Users.FirstOrDefault(u => u.Id == userRole.UserId);

                    var action = entry.State switch
                    {
                        EntityState.Added => "added",
                        EntityState.Deleted => "removed",
                        _ => "changed"
                    };

                    var changeSummary = $"{userEmail} {action} {targetUser?.Email} {(action == "removed" ? "from" : "to")} role {roleName}";

                    EntityLoggings.Add(new EntityLogging
                    { 
                        Entity = "IdentityUserRole",
                        SpecificChanges = changeSummary,
                        Date = now,
                        LoggedInUserEmail = userEmail
                    });

                    continue;
                }

                // 📝 Handle normal entity CRUD
                if (entry.Entity is EntityLogging) continue;

                var entityName = entry.Entity.GetType().Name;
                string summary;

                if (entry.State == EntityState.Added)
                {
                    summary = $"{userEmail} created a new {entityName}";
                }
                else if (entry.State == EntityState.Deleted)
                {
                    summary = $"{userEmail} deleted a {entityName}";
                }
                else // Modified
                {
                    var modifiedProps = entry.Properties.Where(p => p.IsModified).ToList();
                    var changes = modifiedProps
                        .Select(p => $"{p.Metadata.Name}: {p.OriginalValue} → {p.CurrentValue}")
                        .ToList();

                    summary = $"{userEmail} updated {entityName}: " + string.Join(", ", changes);
                }

                EntityLoggings.Add(new EntityLogging
                { 
                    Entity = entityName,
                    SpecificChanges = summary,
                    Date = now,
                    LoggedInUserEmail = userEmail
                });
            }
        }







         

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Add any custom model configurations here
        }
    }
}
