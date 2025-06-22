using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingImageCommand
{
      public sealed class DeleteTrainingImageCommand : IRequest
    {
        public DeleteTrainingImageCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingImageCommandHandler : IRequestHandler<DeleteTrainingImageCommand>
    {
        private readonly ITrainingImageRepository _repository;

        public DeleteTrainingImageCommandHandler(ITrainingImageRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingImageCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
