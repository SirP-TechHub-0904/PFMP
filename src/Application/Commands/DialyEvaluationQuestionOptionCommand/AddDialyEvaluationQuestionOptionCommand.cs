using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DialyEvaluationQuestionOptionCommand
{
    public sealed class AddDialyEvaluationQuestionOptionCommand : IRequest
    {
        public AddDialyEvaluationQuestionOptionCommand(DialyEvaluationQuestionOption trainingTest)
        {
            DialyEvaluationQuestionOption = trainingTest;
        }

        public DialyEvaluationQuestionOption DialyEvaluationQuestionOption { get; set; }


    }

    public class AddDialyEvaluationQuestionOptionCommandHandler : IRequestHandler<AddDialyEvaluationQuestionOptionCommand>
    {
        private readonly IDialyEvaluationQuestionOptionRepository _DialyEvaluationQuestionOptionRepository;

        public AddDialyEvaluationQuestionOptionCommandHandler(IDialyEvaluationQuestionOptionRepository DialyEvaluationQuestionOptionRepository)
        {
            _DialyEvaluationQuestionOptionRepository = DialyEvaluationQuestionOptionRepository;
        }

        public async Task Handle(AddDialyEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            await _DialyEvaluationQuestionOptionRepository.AddAsync(request.DialyEvaluationQuestionOption);


        }
    }
}
