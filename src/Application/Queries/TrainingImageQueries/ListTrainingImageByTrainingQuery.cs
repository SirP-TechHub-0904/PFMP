using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingImageQueries
{ 
    public sealed class ListTrainingImageByTrainingQuery : IRequest<List<TrainingImage>>
    {
        public long TrainingId { get; set; }

        public ListTrainingImageByTrainingQuery(long trainingId)
        {
            TrainingId = trainingId;
        }

        public class ListTrainingImageByTrainingQueryHandler : IRequestHandler<ListTrainingImageByTrainingQuery, List<TrainingImage>>
        {
            private readonly ITrainingImageRepository _repository;

            public ListTrainingImageByTrainingQueryHandler(ITrainingImageRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingImage>> Handle(ListTrainingImageByTrainingQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAll(request.TrainingId);

            }
        }
    }
}
