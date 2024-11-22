using Microsoft.Extensions.Logging;
using MediatR;
using System;

namespace AwesomeGIC.Bank.Application.Common
{
    public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        protected readonly IAwesomeGICBankDBContext _context;
        protected readonly ILogger<TRequest>? _logger;

        public BaseRequestHandler(IAwesomeGICBankDBContext context)
        {
            _context = context;
        }
        public BaseRequestHandler(IAwesomeGICBankDBContext context, ILogger<TRequest> logger)
        {
            _context = context;
            _logger = logger;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

        //protected async Task<ICollection<Domain.Entities.ActionType>> GetAllActionTypes(CancellationToken cancellationToken)
        //{
        //    var actionTypes = await _context.ActionTypes.ToListAsync(cancellationToken);
        //    return actionTypes;
        //}

        //protected async Task<ICollection<Domain.Entities.ActionStatus>> GetAllActionStatuses(CancellationToken cancellationToken)
        //{
        //    var actionStatuses = await _context.ActionStatuses.ToListAsync(cancellationToken);
        //    return actionStatuses;
        //}
    }
}
