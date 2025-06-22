using Application.Validators;
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
    public sealed class GetByIdEvaluationQuestionOptionQuery : IRequest<EvaluationQuestionOption>
    {
        public GetByIdEvaluationQuestionOptionQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdEvaluationQuestionOptionQueryHandler : IRequestHandler<GetByIdEvaluationQuestionOptionQuery, EvaluationQuestionOption>
        {

            private readonly IEvaluationQuestionOptionRepository _repository;

            public GetByIdEvaluationQuestionOptionQueryHandler(IEvaluationQuestionOptionRepository repository)
            {
                _repository = repository;
            }
            public async Task<EvaluationQuestionOption> Handle(GetByIdEvaluationQuestionOptionQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                EvaluationQuestionOption data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
