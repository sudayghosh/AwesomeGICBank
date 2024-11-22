using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Common
{
    public abstract class BaseRequest<TResponse> : IRequest<TResponse>
    {
        public Guid Id { get; set; }
    }

}
