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
    public sealed class TrainingImageRepository : Repository<TrainingImage>, ITrainingImageRepository
    {
        private readonly AppDbContext _context;

        public TrainingImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<TrainingImage>> GetAll(long id)
        {
            var list = await _context.TrainingImages.Where(x => x.TrainingId == id).ToListAsync();
            return list;
        }
    }
}
