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
     public sealed class UpdateEvaluationQuestionOptionCommand : IRequest
    {
        public UpdateEvaluationQuestionOptionCommand(EvaluationQuestionOption movie)
        {
            EvaluationQuestionOption = movie;
        }

        public EvaluationQuestionOption EvaluationQuestionOption { get; set; }


    }

    public class UpdateEvaluationQuestionOptionCommandHandler : IRequestHandler<UpdateEvaluationQuestionOptionCommand>
    {
        private readonly IEvaluationQuestionOptionRepository _repository;

        public UpdateEvaluationQuestionOptionCommandHandler(IEvaluationQuestionOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.EvaluationQuestionOption);
        }
    }
}
