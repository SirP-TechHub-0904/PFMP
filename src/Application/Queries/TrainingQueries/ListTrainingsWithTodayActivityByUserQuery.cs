using Domain.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.TrainingQueries
{
    public sealed class ListTrainingsWithTodayActivityByUserQuery : IRequest<List<TrainingAndDailyActivitiesByUserIdDto>>
    {
        public string UserId { get; }

        public ListTrainingsWithTodayActivityByUserQuery(string userId)
        {
            UserId = userId;
        }
    }
    public class ListTrainingsWithTodayActivityByUserQueryHandler
       : IRequestHandler<ListTrainingsWithTodayActivityByUserQuery, List<TrainingAndDailyActivitiesByUserIdDto>>
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IDialyActivityRepository _dialyActivityRepository;

        public ListTrainingsWithTodayActivityByUserQueryHandler(
            ITrainingRepository trainingRepository,
            IDialyActivityRepository dialyActivityRepository)
        {
            _trainingRepository = trainingRepository;
            _dialyActivityRepository = dialyActivityRepository;
        }

        public async Task<List<TrainingAndDailyActivitiesByUserIdDto>> Handle(
            ListTrainingsWithTodayActivityByUserQuery request,
            CancellationToken cancellationToken)
        {
            var today = DateTime.UtcNow.Date; // or DateTime.Now.Date if local time is preferred

            // Get trainings where user is facilitator or participant
            var trainings = await _trainingRepository.GetAllTrainingsByUserId(request.UserId);
            // filter trainings still active
            trainings = trainings.Where(t => t.TrainingStatus == Domain.Models.EnumStatus.TrainingStatus.Active).ToList();
            var result = new List<TrainingAndDailyActivitiesByUserIdDto>();

            foreach (var training in trainings)
            {
                var activity = await _dialyActivityRepository.GetActivityByTrainingIdAndDate(training.TrainingId, today);

                var trainingDto = new TrainingAndDailyActivitiesByUserIdDto
                {
                    UserId = request.UserId,
                    TrainingId = training.TrainingId,
                    TrainingTitle = training.TrainingTitle,
                    TrainingDate = training.TrainingDate.ToString("dd MMM yyyy"),
                    TrainingState = training.TrainingState,
                    TrainingLGA = training.TrainingLGA,
                    DailyActivityDto = new List<DailyActivityDto>()
                };

                if (activity != null)
                {
                    trainingDto.DailyActivityDto.Add(new DailyActivityDto
                    {
                        Id = activity.Id,
                        Title = activity.Title,
                        Date = activity.Date
                    });
                }

                result.Add(trainingDto);
            }

            return result;
        }
    }
}
