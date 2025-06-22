using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionOptionCommand
{
    public sealed class AddEvaluationQuestionOptionCommand : IRequest
    {
        public AddEvaluationQuestionOptionCommand(EvaluationQuestionOption trainingTest)
        {
            EvaluationQuestionOption = trainingTest;
        }

        public EvaluationQuestionOption EvaluationQuestionOption { get; set; }


    }

    public class AddEvaluationQuestionOptionCommandHandler : IRequestHandler<AddEvaluationQuestionOptionCommand>
    {
        private readonly IEvaluationQuestionOptionRepository _EvaluationQuestionOptionRepository;

        public AddEvaluationQuestionOptionCommandHandler(IEvaluationQuestionOptionRepository EvaluationQuestionOptionRepository)
        {
            _EvaluationQuestionOptionRepository = EvaluationQuestionOptionRepository;
        }

        public async Task Handle(AddEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            await _EvaluationQuestionOptionRepository.AddAsync(request.EvaluationQuestionOption);


        }
    }
}
