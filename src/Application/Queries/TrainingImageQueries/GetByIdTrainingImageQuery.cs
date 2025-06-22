using Application.Validators;
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
        public sealed class GetByIdTrainingImageQuery : IRequest<TrainingImage>
    {
        public GetByIdTrainingImageQuery(long id)
        {
            Id = id;
        }

        public long Id { get; }

        // Handler
        public class GetByIdTrainingImageQueryHandler : IRequestHandler<GetByIdTrainingImageQuery, TrainingImage>
        {

            private readonly ITrainingImageRepository _repository;

            public GetByIdTrainingImageQueryHandler(ITrainingImageRepository repository)
            {
                _repository = repository;
            }
            public async Task<TrainingImage> Handle(GetByIdTrainingImageQuery request, CancellationToken cancellationToken)
            {
                request.ThrowIfNull(nameof(request));


                TrainingImage data = await _repository.GetByIdAsync(request.Id);

                return data;
            }
        }
    }

}
