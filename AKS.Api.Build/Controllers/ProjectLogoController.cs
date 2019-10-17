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
    public class ProjectLogoController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;

        public ProjectLogoController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{projectId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid projectId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ProjectLogos, $"{projectId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;         
        }
    }
}