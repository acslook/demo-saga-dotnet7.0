using saga.orchestrator.domain.Common;
using saga.orchestrator.domain.Entities;
using saga.orchestrator.domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saga.orchestrator.domain.Events
{
    public class CreatedSaleEvent : BaseEvent
    {
        public Sale Sale { get; set; }
    }
}
