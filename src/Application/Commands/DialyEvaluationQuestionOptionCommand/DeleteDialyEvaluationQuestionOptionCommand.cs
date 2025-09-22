using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionOptionCommand
{
      public sealed class DeleteDialyEvaluationQuestionOptionCommand : IRequest
    {
        public DeleteDialyEvaluationQuestionOptionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteDialyEvaluationQuestionOptionCommandHandler : IRequestHandler<DeleteDialyEvaluationQuestionOptionCommand>
    {
        private readonly IDialyEvaluationQuestionOptionRepository _repository;

        public DeleteDialyEvaluationQuestionOptionCommandHandler(IDialyEvaluationQuestionOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteDialyEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
