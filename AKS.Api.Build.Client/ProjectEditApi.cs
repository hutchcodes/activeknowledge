using AKS.Common;
using AKS.Common.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKS.Api.Build.Client
{
    public class ProjectEditApi
    {
        private readonly HttpClient _http;

        public ProjectEditApi(HttpClient http)
        {
            _http = http;
        }        

        public async Task<ProjectEdit> GetProject(Guid projectId)
        {
            var project = await _http.GetJsonAsync<ProjectEdit>($"api/project/{projectId}");

            return project;
        }

        public async Task<ProjectEdit> UpdateProject(ProjectEdit projectEdit)
        {
            var project = await _http.PostJsonAsync<ProjectEdit>($"api/project", projectEdit);

            return project;
        }
    }
}
