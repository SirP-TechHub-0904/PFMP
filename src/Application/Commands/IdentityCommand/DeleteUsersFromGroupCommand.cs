using Domain.Models;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.IdentityCommand
{
    public sealed class DeleteUsersFromGroupCommand : IRequest<bool>
    {
        public long GroupId { get; }

        public DeleteUsersFromGroupCommand(long groupId)
        {
            GroupId = groupId;
        }
    }
    public class DeleteUsersFromGroupCommandHandler : IRequestHandler<DeleteUsersFromGroupCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<DeleteUsersFromGroupCommandHandler> _logger;

        public DeleteUsersFromGroupCommandHandler(
            AppDbContext context,
            UserManager<AppUser> userManager,
            ILogger<DeleteUsersFromGroupCommandHandler> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUsersFromGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userGroups = await _context.UserCategoryGroups
                    .Include(x => x.User)
                    .Where(x => x.UserCategoryId == request.GroupId)
                    .ToListAsync(cancellationToken);

                int batchSize = 200;
                int processed = 0;
                var deletedUserGroupIds = new List<long>();

                foreach (var userGroup in userGroups)
                {
                    var user = userGroup.User;

                    try
                    {
                        // Remove user from roles
                        var roles = await _userManager.GetRolesAsync(user);
                        if (roles.Any())
                        {
                            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
                            if (!removeRolesResult.Succeeded)
                            {
                                _logger.LogWarning("Failed to remove roles from user {Email}: {Errors}", user.Email,
                                    string.Join(", ", removeRolesResult.Errors.Select(e => e.Description)));
                                continue;
                            }
                        }

                        // Delete user account
                        var deleteResult = await _userManager.DeleteAsync(user);
                        if (!deleteResult.Succeeded)
                        {
                            _logger.LogWarning("Failed to delete user {Email}: {Errors}", user.Email,
                                string.Join(", ", deleteResult.Errors.Select(e => e.Description)));
                            continue;
                        }

                        // Mark UserCategoryGroup for manual deletion
                        deletedUserGroupIds.Add(userGroup.Id);
                        processed++;

                        // Every 50 records, remove and save
                        if (processed % batchSize == 0)
                        {
                            await DeleteUserCategoryGroupsAsync(deletedUserGroupIds, cancellationToken);
                            deletedUserGroupIds.Clear();
                            _logger.LogInformation("Processed {Count} users...", processed);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error processing user {Email}", user?.Email);
                    }
                }

                // Final cleanup
                if (deletedUserGroupIds.Any())
                {
                    await DeleteUserCategoryGroupsAsync(deletedUserGroupIds, cancellationToken);
                    _logger.LogInformation("Final batch of {Count} processed.", deletedUserGroupIds.Count);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Batch deletion failed for group {GroupId}", request.GroupId);
                return false;
            }
        }

        private async Task DeleteUserCategoryGroupsAsync(List<long> ids, CancellationToken cancellationToken)
        {
            var toRemove = await _context.UserCategoryGroups
                .Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);

            _context.UserCategoryGroups.RemoveRange(toRemove);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
