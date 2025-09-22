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
    public sealed class DialyEvaluationQuestionRepository : Repository<DialyEvaluationQuestion>, IDialyEvaluationQuestionRepository
    {
        private readonly AppDbContext _context;

        public DialyEvaluationQuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DialyEvaluationQuestion>> GetAllByDialyActivity(long trainingId)
        {
            var list = await _context.DialyEvaluationQuestions
                .Include(x=>x.DialyEvaluationQuestionOptions)
                .Where(x=>x.DialyActivityId == trainingId).ToListAsync();
            return list;
        }
    }
}
