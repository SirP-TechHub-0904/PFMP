using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Infrastructure.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public sealed class ServerRepository : Repository<Server>, IServerRepository
    {
        private readonly AppDbContext _context;

        public ServerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Server>> GetAllByUserId()
        {
            var data = await _context.Servers.ToListAsync();
            return data;
        }
    }
}
