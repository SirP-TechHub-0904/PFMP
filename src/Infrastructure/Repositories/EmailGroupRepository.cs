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
      public sealed class EmailGroupRepository : Repository<EmailGroup>, IEmailGroupRepository
    {
        private readonly AppDbContext _context;

        public EmailGroupRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EmailGroup> GetEmailGroupById(long id)
        {
            return await _context.EmailGroups
               .Include(x => x.Emails)
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetEmailsCountByUserId()
        {
            return await _context.EmailGroups.CountAsync();
        }

        public async Task<List<EmailGroup>> ListEmailGroupsByUserId()
        {
            return await _context.EmailGroups
                .Include(x=>x.Emails).ToListAsync();
        }
    }
}
