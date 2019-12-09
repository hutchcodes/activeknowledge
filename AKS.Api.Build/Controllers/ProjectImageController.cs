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
using AKS.Common;

namespace AKS.Api.Build
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ProjectImageController : ControllerBase
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

        public ProjectImageController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{projectId}/{imageId}/{*slug}"), HttpGet]
        public async Task<IActionResult> GetImage(Guid projectId, Guid imageId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ProjectImages, $"{projectId}/{imageId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }

        [HttpPost, RequestSizeLimit(2 * 1024 * 1024)]
        [Route("api/[controller]/{projectId}")]
        public async Task<IActionResult> PostImage(Guid projectId, IFormFile upload)
        {
            if (!_supportedMimeTypes.Contains(upload.ContentType.ToLower()))
            {
                throw new UnsupportedContentTypeException($"{upload.ContentType} Unsupported file type");
            }

            var stream = upload.OpenReadStream();

            var imageId = Guid.NewGuid();

            var document = new Document()
            {
                Content = stream,
                ContentType = upload.ContentType,
                Name = upload.FileName,
                ProjectId = projectId,
                DocumentId = imageId
            };

            var key = $"{projectId}/{imageId}/{upload.FileName}";

            await _fileStorage.UploadDocument(FileStorageType.ProjectImages, key, document);

            var response = new CKEUploadSuccess($"{ConfigSettings.ThisApiBaseUrl}ContentImage/{projectId}/{imageId}/{upload.FileName}");

            return Ok(response);
        }
    }
}