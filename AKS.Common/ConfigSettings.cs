using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Common
{
    public static class ConfigSettings
    {
        private static ApiType _thisApiType;
        public static string BuildApiBaseUrl { get; set; } = "";
        public static string ViewApiBaseUrl { get; set; } = "";
        public static string ThisApiBaseUrl
        {
            get
            {
                switch (_thisApiType)
                {
                    case ApiType.Build:
                        return BuildApiBaseUrl;
                    case ApiType.View:
                        return ViewApiBaseUrl;
                    default:
                        return "";
                }
            }
        }
        public static void LoadConfigs(IConfiguration configuration, ApiType apiType)
        {
            _thisApiType = apiType;
            BuildApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSBuildApiBaseUrl");
            ViewApiBaseUrl = configuration.GetValue<string>("AppSettings:AKSViewApiBaseUrl");
        }

        public enum ApiType
        {
            Build,
            View
        }
    }
}
