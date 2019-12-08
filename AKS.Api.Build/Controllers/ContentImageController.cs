using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using AKS.Infrastructure.Enums;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using AKS.Infrastructure.DTO;
using AKS.Api.Build.Data;
using Microsoft.Extensions.Configuration;
using AKS.Common;

namespace AKS.Api.Build.Controllers
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ContentImageController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;
        private readonly string[] _supportedMimeTypes =
        {
            "image/png",
            "image/jpeg",
            "image/jpg",
            "image/gif",
            "image/bmp",
            "image/webp",
            "image/tiff"
        };
        public ContentImageController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{projectId}/{topicId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid projectId, Guid topicId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentImages, $"{projectId}/{topicId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }
            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }

        [HttpPost, RequestSizeLimit(2 * 1024 * 1024)]
        [Route("api/[controller]/{projectId}/{topicId}/{*slug}")]
        public async Task<IActionResult> PostImage(Guid projectId, Guid topicId, string slug, IFormFile upload)
        {
            if (!_supportedMimeTypes.Contains(upload.ContentType.ToLower()))
            {
                throw new UnsupportedContentTypeException($"{upload.ContentType} Unsupported file type");
            }

            var stream = upload.OpenReadStream();

            var document = new Document()
            {
                Content = stream,
                ContentType = upload.ContentType,
                Name = upload.FileName,
                ProjectId = projectId,
                TopicId = topicId
            };

            var key = $"{projectId}/{topicId}/{slug}";

            await _fileStorage.UploadDocument(FileStorageType.ContentImages, key, document);

            var response = new CKEUploadSuccess($"{ConfigSettings.ThisApiBaseUrl}ContentImage/{projectId}/{topicId}/{slug}");

            return Ok(response);
        }
    }
}