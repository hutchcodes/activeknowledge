using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using AKS.AppCore.Enums;
using AKS.AppCore.Interfaces;

namespace AKS.Share.Web.Api
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ContentDocumentController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;

        public ContentDocumentController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [Route("api/[controller]/{projectId}/{topicId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid projectId, Guid topicId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentDocuments, $"{projectId}/{topicId}/{slug}");

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;
        }
    }
}