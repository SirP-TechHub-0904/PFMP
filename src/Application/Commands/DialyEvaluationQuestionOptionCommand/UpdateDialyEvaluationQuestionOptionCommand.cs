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
     public sealed class UpdateDialyEvaluationQuestionOptionCommand : IRequest
    {
        public UpdateDialyEvaluationQuestionOptionCommand(DialyEvaluationQuestionOption movie)
        {
            DialyEvaluationQuestionOption = movie;
        }

        public DialyEvaluationQuestionOption DialyEvaluationQuestionOption { get; set; }


    }

    public class UpdateDialyEvaluationQuestionOptionCommandHandler : IRequestHandler<UpdateDialyEvaluationQuestionOptionCommand>
    {
        private readonly IDialyEvaluationQuestionOptionRepository _repository;

        public UpdateDialyEvaluationQuestionOptionCommandHandler(IDialyEvaluationQuestionOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDialyEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            await _repository.UpdateAsync(request.DialyEvaluationQuestionOption);
        }
    }
}
