using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.Models.EnumStatus;

namespace Application.Commands.DialyUserEvaluationCommand
{
    public sealed class UpdateDialyUserEvaluationListCommand : IRequest
    {
        public UpdateDialyUserEvaluationListCommand(List<(long questionId, string answer, string userId, long trainingId)> testeData)
        {
            TesteData = testeData;
        }

        public List<(long questionId, string answer, string userId, long trainingId)> TesteData { get; set; }
    }

    public class UpdateDialyUserEvaluationListCommandHandler : IRequestHandler<UpdateDialyUserEvaluationListCommand>
    {
        private readonly IDialyUserEvaluationRepository _repository;

        public UpdateDialyUserEvaluationListCommandHandler(IDialyUserEvaluationRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateDialyUserEvaluationListCommand request, CancellationToken cancellationToken)
        {
            await _repository.DialyUserEvaluationSubmit(request.TesteData);
        }
    }

}
