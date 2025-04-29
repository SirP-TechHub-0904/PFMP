using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingTestQueries
{
    public sealed class ListTrainingTestByCategoryQuery : IRequest<List<TrainingTest>>
    {
        public long Id { get; set; }

        public ListTrainingTestByCategoryQuery(long id)
        {
            Id = id;
        }

        public class ListTrainingTestByCategoryQueryHandler : IRequestHandler<ListTrainingTestByCategoryQuery, List<TrainingTest>>
        {
            private readonly ITrainingTestRepository _repository;

            public ListTrainingTestByCategoryQueryHandler(ITrainingTestRepository repository)
            {
                _repository = repository;
            }

            public async Task<List<TrainingTest>> Handle(ListTrainingTestByCategoryQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAllTrainingTestByCategory(request.Id);
                return list;
            }
        }
    }

}
