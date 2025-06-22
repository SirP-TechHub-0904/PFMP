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
    public sealed class TrainingSettingRepository : Repository<TrainingSetting>, ITrainingSettingRepository
    {
        private readonly AppDbContext _context;

        public TrainingSettingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TrainingSetting>> GetAll(long id)
        {
            var list = await _context.TrainingSettings.Where(x => x.TrainingId == id).ToListAsync();
            return list;
        }
    }
}
