using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using AKS.AppCore.Enums;
using AKS.AppCore.Interfaces;

namespace AKS.App.Build.Api.Controllers
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ContentImageController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;

        public ContentImageController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{customerId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid customerId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentImages, $"{customerId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }
            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }
    }
}