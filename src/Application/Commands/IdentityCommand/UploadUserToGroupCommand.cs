using Domain.DTOs;
using Domain.Models;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Application.Commands.IdentityCommand
{
    public sealed class UploadUserToGroupCommand : IRequest<bool>
    {
        public UploadUserToGroupCommand(IFormFile excelFile, long groupId)
        {
            ExcelFile = excelFile;
            GroupId = groupId;
        }

        public IFormFile ExcelFile { get; set; }
        public long GroupId { get; set; }

    }

    public class UploadUserToGroupCommandHandler : IRequestHandler<UploadUserToGroupCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ILogger<UploadUserToGroupCommandHandler> _logger;
        public UploadUserToGroupCommandHandler(IMediator mediator, UserManager<AppUser> userManager, AppDbContext context, ILogger<UploadUserToGroupCommandHandler> logger)
        {
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(UploadUserToGroupCommand request, CancellationToken cancellationToken)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var failedLogs = new List<string>();

            try
            {
                using var stream = request.ExcelFile.OpenReadStream();
                using var package = new ExcelPackage(stream);
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null) return false;

                int rowCount = worksheet.Dimension.Rows;
                for (int row = 3; row <= rowCount; row++) // start at 3: header is row 2
                {
                    try
                    {
                        var state = worksheet.Cells[row, 2].Text.Trim();
                        var lga = worksheet.Cells[row, 3].Text.Trim();
                        var fullName = worksheet.Cells[row, 4].Text.Trim();
                        var rawPhone = worksheet.Cells[row, 5].Text ?? "";
                        var phone = rawPhone
                            .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .FirstOrDefault()?
                            .Trim() ?? "";

                        var rawEmail = worksheet.Cells[row, 6].Text?.Trim() ?? "";
                        var email = string.IsNullOrWhiteSpace(rawEmail)
                            ? $"{Guid.NewGuid()}@demo.com"
                            : rawEmail.Replace(" ", "");

                        var columnOne = worksheet.Cells[row, 7].Text.Trim();  // ICT Savvy
                        var columnTwo = worksheet.Cells[row, 8].Text.Trim();  // LGCFN AVAILABLE
                        var columnThree = worksheet.Cells[row, 9].Text.Trim(); // LGCFN FUNCTIONAL
                         
                        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                        {
                            failedLogs.Add($"Row {row}: Missing email or phone.");
                            continue;
                        }

                        if (await _userManager.FindByEmailAsync(email) != null ||
                            _userManager.Users.Any(u => u.PhoneNumber == phone))
                        {
                            failedLogs.Add($"Row {row}: Duplicate email or phone.");
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(fullName))
                        {
                            failedLogs.Add($"Row {row}: Missing full name.");
                            continue;
                        }

                        string cleanName = Regex.Replace(fullName, @"[^\w\s]", "");
                        string[] nameParts = cleanName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        var user = new AppUser
                        {
                            UserName = email,
                            Email = email,
                            PhoneNumber = phone,
                            FirstName = nameParts.ElementAtOrDefault(0),
                            MiddleName = nameParts.ElementAtOrDefault(1),
                            LastName = nameParts.ElementAtOrDefault(2),
                            CurrentState = state,
                            CurrentLga = "",
                            Address = lga,
                            PlaceOfWork = "",
                            Role = "Facilitator", // default if needed
                            UserStatus = Domain.Models.EnumStatus.UserStatus.Active,
                            Date = DateTime.UtcNow.AddHours(1),
                            TempPass = ""
                        };

                        var result = await _userManager.CreateAsync(user, Guid.NewGuid().ToString()); // Replace with real password policy
                        if (!result.Succeeded)
                        {
                            failedLogs.Add($"Row {row}: Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                            continue;
                        }
                        bool alreadyInGroup = await _context.UserCategoryGroups
    .Include(g => g.User)
    .AnyAsync(g =>
        g.UserCategoryId == request.GroupId &&
        (g.User.Email == email || g.User.PhoneNumber == phone));

                        if (alreadyInGroup)
                        {
                            failedLogs.Add($"Row {row}: User with phone/email already in this group.");
                            continue;
                        }
                        var group = new UserCategoryGroup
                        {
                            UserId = user.Id,
                            UserCategoryId = request.GroupId,
                            ColumnOne = columnOne,
                            ColumnTwo = columnTwo,
                            ColumnThree = columnThree,
                            ColumnFour = null,
                            Date = DateTime.UtcNow.AddHours(1)

                        };

                        await _context.UserCategoryGroups.AddAsync(group);
                    }
                    catch (Exception ex)
                    {
                        failedLogs.Add($"Row {row}: Exception - {ex.Message}");
                    }
                }

                //            for (int row = 3; row <= rowCount; row++) // start at 3: header is row 2
                //            {
                //                try
                //                {
                //                    var state = worksheet.Cells[row, 2].Text.Trim();
                //                    var lga = worksheet.Cells[row, 3].Text.Trim();
                //                    var fullName = worksheet.Cells[row, 4].Text.Trim();
                //                    var rawPhone = worksheet.Cells[row, 5].Text ?? "";
                //                    var phone = rawPhone
                //                        .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                //                        .FirstOrDefault()?
                //                        .Trim() ?? "";

                //                    var rawEmail = worksheet.Cells[row, 6].Text?.Trim() ?? "";
                //                    var email = string.IsNullOrWhiteSpace(rawEmail)
                //                        ? $"{Guid.NewGuid()}@demo.com"
                //                        : rawEmail.Replace(" ", "");

                //                    var columnOne = worksheet.Cells[row, 7].Text.Trim();  // ICT Savvy
                //                    var columnTwo = worksheet.Cells[row, 8].Text.Trim();  // LGCFN AVAILABLE
                //                    var columnThree = worksheet.Cells[row, 9].Text.Trim(); // LGCFN FUNCTIONAL

                //                    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
                //                    {
                //                        failedLogs.Add($"Row {row}: Missing email or phone.");
                //                        continue;
                //                    }

                //                    if (await _userManager.FindByEmailAsync(email) != null ||
                //                        _userManager.Users.Any(u => u.PhoneNumber == phone))
                //                    {
                //                        failedLogs.Add($"Row {row}: Duplicate email or phone.");
                //                        continue;
                //                    }

                //                    if (string.IsNullOrWhiteSpace(fullName))
                //                    {
                //                        failedLogs.Add($"Row {row}: Missing full name.");
                //                        continue;
                //                    }

                //                    string cleanName = Regex.Replace(fullName, @"[^\w\s]", "");
                //                    string[] nameParts = cleanName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                //                    var user = new AppUser
                //                    {
                //                        UserName = email,
                //                        Email = email,
                //                        PhoneNumber = phone,
                //                        FirstName = nameParts.ElementAtOrDefault(0),
                //                        MiddleName = nameParts.ElementAtOrDefault(1),
                //                        LastName = nameParts.ElementAtOrDefault(2),
                //                        CurrentState = state,
                //                        CurrentLga = lga,
                //                        PlaceOfWork = "",
                //                        Role = "Facilitator", // default if needed
                //                        UserStatus = Domain.Models.EnumStatus.UserStatus.Active,
                //                        Date = DateTime.UtcNow.AddHours(1),
                //                        TempPass = ""
                //                    };

                //                    var result = await _userManager.CreateAsync(user, Guid.NewGuid().ToString()); // Replace with real password policy
                //                    if (!result.Succeeded)
                //                    {
                //                        failedLogs.Add($"Row {row}: Failed to create user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                //                        continue;
                //                    }
                //                    bool alreadyInGroup = await _context.UserCategoryGroups
                //.Include(g => g.User)
                //.AnyAsync(g =>
                //    g.UserCategoryId == request.GroupId &&
                //    (g.User.Email == email || g.User.PhoneNumber == phone));

                //                    if (alreadyInGroup)
                //                    {
                //                        failedLogs.Add($"Row {row}: User with phone/email already in this group.");
                //                        continue;
                //                    }
                //                    var group = new UserCategoryGroup
                //                    {
                //                        UserId = user.Id,
                //                        UserCategoryId = request.GroupId,
                //                        ColumnOne = columnOne,
                //                        ColumnTwo = columnTwo,
                //                        ColumnThree = columnThree,
                //                        ColumnFour = null,
                //                        Date = DateTime.UtcNow.AddHours(1)

                //                    };

                //                    await _context.UserCategoryGroups.AddAsync(group);
                //                }
                //                catch (Exception ex)
                //                {
                //                    failedLogs.Add($"Row {row}: Exception - {ex.Message}");
                //                }
                //            }

                await _context.SaveChangesAsync();

                foreach (var fail in failedLogs)
                    _logger.LogWarning(fail);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Upload failed completely.");
                return false;
            }
        }
    }

}