using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common
{
    public class AppInsightsInitializer : ITelemetryInitializer
    {
        private readonly string _tags;
        public AppInsightsInitializer(string tags)
        {
            _tags = tags;
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.GlobalProperties["tags"] = _tags;
        }
    }
}
