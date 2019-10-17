using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using AKS.Infrastructure.Enums;
using AKS.Infrastructure.Interfaces;

namespace AKS.Api.Build
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ProjectImageController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;

        public ProjectImageController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{projectId}/{*slug}"), HttpGet]
        public async Task<IActionResult> GetImage(Guid projectId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ProjectImages, $"{projectId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }



    }
}