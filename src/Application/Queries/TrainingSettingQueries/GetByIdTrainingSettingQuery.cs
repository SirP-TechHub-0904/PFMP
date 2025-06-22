using Application.Validators;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingSettingQueries
{
        public sealed class GetByIdTrainingSettingQuery : IRequest<TrainingSetting>
    {
        public GetByIdTrainingSettingQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingSettingQueryHandler : IRequestHandler<GetByIdTrainingSettingQuery, TrainingSetting>
        {

            private readonly ITrainingSettingRepository _repository;

            public GetByIdTrainingSettingQueryHandler(ITrainingSettingRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingSetting> Handle(GetByIdTrainingSettingQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingSetting data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
