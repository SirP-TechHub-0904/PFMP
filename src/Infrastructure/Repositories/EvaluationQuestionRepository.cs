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

    public sealed class EvaluationQuestionRepository : Repository<EvaluationQuestion>, IEvaluationQuestionRepository
    {
        private readonly AppDbContext _context;

        public EvaluationQuestionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<EvaluationQuestion>> GetAllListAsync()
        {
            var list = await _context.EvaluationQuestions
                .Include(x => x.EvaluationQuestionCategory).ToListAsync();
            return list;
        }


        public async Task<List<EvaluationQuestion>> GetAllListByTrainingIdAsync(long id)
        {
            try
            {

                var list = await _context.EvaluationQuestions
                    .Include(x => x.EvaluationQuestionCategory)
                    .Include(x => x.EvaluationQuestionOptions)
                    .Where(x => x.TrainingId == id && x.Publish).ToListAsync();
                return list;

            }
            catch (Exception c)
            {
                
                return null;
            }
        }

        public async Task<EvaluationQuestion> GetQuestionByIdAsync(long id)
        {
            var data = await _context.EvaluationQuestions
                   .Include(x => x.EvaluationQuestionCategory)
                   .Include(x => x.EvaluationQuestionOptions)
                   .FirstOrDefaultAsync(x => x.Id == id);
            return data;
        }
    }
}
