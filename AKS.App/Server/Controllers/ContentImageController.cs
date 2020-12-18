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
using Microsoft.AspNetCore.Authorization;

namespace AKS.Api.Build.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
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
        [Route("api/[controller]/{projectId}/{topicId}/{imageId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid projectId, Guid topicId, Guid imageId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentImages, $"{projectId}/{topicId}/{imageId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }

        [HttpPost, RequestSizeLimit(2 * 1024 * 1024)]
        [Route("api/[controller]/{projectId}/{topicId}")]
        public async Task<IActionResult> PostImage(Guid projectId, Guid topicId, IFormFile upload)
        {
            if (!_supportedMimeTypes.Contains(upload.ContentType.ToLower()))
            {
                throw new UnsupportedContentTypeException($"{upload.ContentType} Unsupported file type");
            }

            var stream = upload.OpenReadStream();

            var imageId = Guid.NewGuid();
            var document = new Document()
            {
                DocumentId = imageId,
                Content = stream,
                ContentType = upload.ContentType,
                Name = upload.FileName,
                ProjectId = projectId,
                TopicId = topicId
            };

            var key = $"{projectId}/{topicId}/{imageId}/{upload.FileName}";

            await _fileStorage.UploadDocument(FileStorageType.ContentImages, key, document);

            var response = new CKEUploadSuccess($"{ConfigSettings.ThisApiBaseUrl}ContentImage/{projectId}/{topicId}/{imageId}/{upload.FileName}");

            return Ok(response);
        }
    }
}