using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Resurgam.AppCore.Enums;
using Resurgam.AppCore.Interfaces;

namespace Resurgam.Admin.Api
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ProjectLogoController : ControllerBase
    {
        private IFileStorageRepository _fileStorage;
        const string ContainerName = "projectlogos";

        public ProjectLogoController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [Route("api/[controller]/{projectId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid projectId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ProjectLogos, $"{projectId}/{slug}");

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;         
        }
    }
}