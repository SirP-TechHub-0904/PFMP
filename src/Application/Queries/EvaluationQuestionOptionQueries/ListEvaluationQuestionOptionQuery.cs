using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.EvaluationQuestionOptionQueries
{
    public sealed class ListEvaluationQuestionOptionQuery : IRequest<List<EvaluationQuestionOption>>
    {
        public class ListEvaluationQuestionOptionQueryHandler : IRequestHandler<ListEvaluationQuestionOptionQuery, List<EvaluationQuestionOption>>
        {
            private readonly IEvaluationQuestionOptionRepository _repository;

            public ListEvaluationQuestionOptionQueryHandler(IEvaluationQuestionOptionRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<EvaluationQuestionOption>> Handle(ListEvaluationQuestionOptionQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAllAsync();
                return list.ToList();
            }
        }
    }

}
