using AWS.Dto.AwsDtos;
using AWS.Services;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TrainingSettingCommand
{
      public sealed class AddTrainingSettingCommand : IRequest
    {
        public AddTrainingSettingCommand(TrainingSetting trainingSetting)
        {
            TrainingSetting = trainingSetting;
        }

        public TrainingSetting TrainingSetting { get; set; }
 
    }

    public class AddTrainingSettingCommandHandler : IRequestHandler<AddTrainingSettingCommand>
    {
        private readonly ITrainingSettingRepository _trainingSettingRepository;

        public AddTrainingSettingCommandHandler(ITrainingSettingRepository trainingSettingRepository)
        {
            _trainingSettingRepository = trainingSettingRepository;
        }

        public async Task Handle(AddTrainingSettingCommand request, CancellationToken cancellationToken)
        {
 
            await _trainingSettingRepository.AddAsync(request.TrainingSetting);


        }
    }
}
