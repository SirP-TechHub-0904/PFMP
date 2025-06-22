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
    public sealed class ListTrainingSettingQuery : IRequest<List<TrainingSetting>>
    {
        public class ListTrainingSettingQueryHandler : IRequestHandler<ListTrainingSettingQuery, List<TrainingSetting>>
        {
            private readonly ITrainingSettingRepository _repository;

            public ListTrainingSettingQueryHandler(ITrainingSettingRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingSetting>> Handle(ListTrainingSettingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
