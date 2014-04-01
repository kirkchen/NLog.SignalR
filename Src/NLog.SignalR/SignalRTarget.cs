using Microsoft.AspNet.SignalR;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.SignalR
{
    [Target("SignalR")]
    public class SignalRTarget : TargetWithLayout
    {
        public IHubContext HubContext { get; set; }

        public SignalRTarget()
            : this(GlobalHost.ConnectionManager.GetHubContext<LogHub>())
        {
        }

        public SignalRTarget(IHubContext hubContext)
        {
            this.HubContext = hubContext;
        }

        protected override void Write(LogEventInfo logEvent)
        {
            string log = this.Layout.Render(logEvent);

            this.SendMessageViaHub(log, logEvent);
        }

        private void SendMessageViaHub(string log, LogEventInfo logEvent)
        {
            this.HubContext.Clients.All.Log(log, logEvent);
        }        
    }
}
