using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using AKS.AppCore.DTO;
using AKS.AppCore.Enums;
using AKS.AppCore.Interfaces;

namespace AKS.Api
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ContentDocumentController : ControllerBase
    {
        private IFileStorageRepository _fileStorage;
        private readonly string[] _supportedMimeTypes =
        {
            "image/png",
            "image/jpeg",
            "image/jpg",
            "application/pdf",
            "application/doc",
            "application/docx",
            "application/xls",
            "application/xlsx",
            "application/ppt",
            "application/pptx",
        };

        public ContentDocumentController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{projectId}/{topicId}/{*slug}")]
        public async Task<IActionResult> GetContentDocument(Guid projectId, Guid topicId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentDocuments, $"{projectId}/{topicId}/{slug}");

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }

        [HttpPost]
        [Route("api/[controller]/{projectId}/{topicId}/{*slug}")]
        public async Task<IActionResult> UploadContentDocument(Guid projectId, Guid topicId, string slug, IFormFile file)
        {
            if (!_supportedMimeTypes.Contains(file.ContentType.ToLower()))
            {
                throw new UnsupportedContentTypeException("Unsupported file type");
            }


            var stream = file.OpenReadStream();

            var document = new Document()
            {
                Content = stream,
                ContentType = file.ContentType,
                Name = file.FileName,
                ProjectId = projectId,
                TopicId = topicId
            };

            var key = $"{projectId}/{topicId}/{slug}";

            await _fileStorage.UploadDocument(FileStorageType.ContentDocuments, key, document);

            document.Content = null;
            return Ok(document);
        }
    }
}