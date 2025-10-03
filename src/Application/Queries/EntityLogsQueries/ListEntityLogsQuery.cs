using Domain.Models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.EntityLogsQueries
{
    public sealed class ListEntityLogsQuery : IRequest<ListEntityLogsResult>
    {
        public int PageNumber { get; set; } = 1;      // Default page
        public int PageSize { get; set; } = 10;       // Default page size
        public string? SearchTerm { get; set; }       // Optional search term
    }

    public class ListEntityLogsResult
    {
        public List<EntityLogging> Logs { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class ListEntityLogsQueryHandler : IRequestHandler<ListEntityLogsQuery, ListEntityLogsResult>
    {
        private readonly AppDbContext _context;

        public ListEntityLogsQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListEntityLogsResult> Handle(ListEntityLogsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.EntityLoggings.AsQueryable();

            // 🔍 Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                string search = request.SearchTerm.ToLower();
                query = query.Where(x =>
                    (x.Entity != null && x.Entity.ToLower().Contains(search)) ||
                    (x.SpecificChanges != null && x.SpecificChanges.ToLower().Contains(search)) ||
                    (x.LoggedInUserEmail != null && x.LoggedInUserEmail.ToLower().Contains(search))
                );
            }

            // 📊 Count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // 🗓 Order by Date (descending)
            query = query.OrderByDescending(x => x.Date);

            // 📄 Apply pagination
            int skip = (request.PageNumber - 1) * request.PageSize;
            var logs = await query
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new ListEntityLogsResult
            {
                Logs = logs,
                TotalCount = totalCount
            };
        }
    }
}
