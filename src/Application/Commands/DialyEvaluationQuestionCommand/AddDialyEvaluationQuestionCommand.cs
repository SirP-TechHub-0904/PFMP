using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyEvaluationQuestionCommand
{
    public sealed class AddDialyEvaluationQuestionCommand : IRequest<long>
    {
        public AddDialyEvaluationQuestionCommand(DialyEvaluationQuestion dialyEvaluationQuestion)
        {
            DialyEvaluationQuestion = dialyEvaluationQuestion;
        }

        public DialyEvaluationQuestion DialyEvaluationQuestion { get; set; }


    }

    public class AddDialyEvaluationQuestionCommandHandler : IRequestHandler<AddDialyEvaluationQuestionCommand, long>
    {
        private readonly IDialyEvaluationQuestionRepository _dialyEvaluationQuestionRepository;

        public AddDialyEvaluationQuestionCommandHandler(IDialyEvaluationQuestionRepository dialyEvaluationQuestionRepository)
        {
            _dialyEvaluationQuestionRepository = dialyEvaluationQuestionRepository;
        }

        public async Task<long> Handle(AddDialyEvaluationQuestionCommand request, CancellationToken cancellationToken)
        {

            await _dialyEvaluationQuestionRepository.AddAsync(request.DialyEvaluationQuestion);

            return request.DialyEvaluationQuestion.Id;
        }
    }
}
