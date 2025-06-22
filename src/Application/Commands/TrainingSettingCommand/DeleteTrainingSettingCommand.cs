using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingSettingCommand
{
      public sealed class DeleteTrainingSettingCommand : IRequest
    {
        public DeleteTrainingSettingCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteTrainingSettingCommandHandler : IRequestHandler<DeleteTrainingSettingCommand>
    {
        private readonly ITrainingSettingRepository _repository;

        public DeleteTrainingSettingCommandHandler(ITrainingSettingRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTrainingSettingCommand request, CancellationToken cancellationToken)
        {

            var movie = await _repository.GetByIdAsync(request.Id);

            await _repository.RemoveAsync(movie);

        }
    }
}
