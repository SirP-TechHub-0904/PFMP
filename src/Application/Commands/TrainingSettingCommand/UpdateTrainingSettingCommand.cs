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
    public sealed class UpdateTrainingSettingCommand : IRequest
    {
        public UpdateTrainingSettingCommand(TrainingSetting trainingSetting)
        {
            TrainingSetting = trainingSetting;
        }

        public TrainingSetting TrainingSetting { get; set; }
 


    }

    public class UpdateTrainingSettingCommandHandler : IRequestHandler<UpdateTrainingSettingCommand>
    {
        private readonly ITrainingSettingRepository _repository;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateTrainingSettingCommandHandler(ITrainingSettingRepository repository, IConfiguration config, IStorageService storageService)
        {
            _repository = repository;
            _config = config;
            _storageService = storageService;
        }

        public async Task Handle(UpdateTrainingSettingCommand request, CancellationToken cancellationToken)
        {
 

            await _repository.UpdateAsync(request.TrainingSetting);
        }
    }
}
