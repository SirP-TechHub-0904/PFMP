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
    public sealed class ListTrainingImageQuery : IRequest<List<TrainingImage>>
    {
        public class ListTrainingImageQueryHandler : IRequestHandler<ListTrainingImageQuery, List<TrainingImage>>
        {
            private readonly ITrainingImageRepository _repository;

            public ListTrainingImageQueryHandler(ITrainingImageRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingImage>> Handle(ListTrainingImageQuery request, CancellationToken cancellationToken)
            {
                return await _repository.GetAllAsync();

            }
        }
    }

}
