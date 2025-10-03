using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EntityLogsQueries
{
    public sealed class GetByIdEntityLogsQuery : IRequest<EntityLogging>
    {
        public GetByIdEntityLogsQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdEntityLogsQueryHandler : IRequestHandler<GetByIdEntityLogsQuery, EntityLogging>
        {

            private readonly AppDbContext _context;

            public GetByIdEntityLogsQueryHandler(AppDbContext context)
            {
                _context = context;
            }

            public async Task<EntityLogging> Handle(GetByIdEntityLogsQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                EntityLogging data = await _context.EntityLoggings.FindAsync(request.Id);

                return data;
            }
        }
    }

}
