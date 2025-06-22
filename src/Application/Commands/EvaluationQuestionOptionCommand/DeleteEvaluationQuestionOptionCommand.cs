using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EvaluationQuestionOptionCommand
{
      public sealed class DeleteEvaluationQuestionOptionCommand : IRequest
    {
        public DeleteEvaluationQuestionOptionCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEvaluationQuestionOptionCommandHandler : IRequestHandler<DeleteEvaluationQuestionOptionCommand>
    {
        private readonly IEvaluationQuestionOptionRepository _repository;

        public DeleteEvaluationQuestionOptionCommandHandler(IEvaluationQuestionOptionRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteEvaluationQuestionOptionCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
