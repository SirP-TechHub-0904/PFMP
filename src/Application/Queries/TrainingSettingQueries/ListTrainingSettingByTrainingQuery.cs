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
    public sealed class ListTrainingSettingByTrainingQuery : IRequest<List<TrainingSetting>>
    {
        public long TrainingId { get; set; }

        public ListTrainingSettingByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListTrainingSettingByTrainingQueryHandler : IRequestHandler<ListTrainingSettingByTrainingQuery, List<TrainingSetting>>
        {
            private readonly ITrainingSettingRepository _repository;

            public ListTrainingSettingByTrainingQueryHandler(ITrainingSettingRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingSetting>> Handle(ListTrainingSettingByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.TrainingId);

            }
        }
    }
}
