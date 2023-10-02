using saga.orchestrator.domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saga.orchestrator.application.Consumers
{
    public interface IConsumerEventHandler
    {
        Task On(CreatedSaleEvent @event);
    }
}
