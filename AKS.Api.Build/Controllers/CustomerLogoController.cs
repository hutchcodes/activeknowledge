using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using AKS.Api.Build.Helpers;
using AKS.Infrastructure.DTO;
using AKS.Infrastructure.Enums;
using AKS.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace AKS.Api.Build
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerLogoController : ControllerBase
    {
        private readonly IFileStorageRepository _fileStorage;
        private readonly string[] _supportedMimeTypes = { "image/png", "image/jpeg", "image/jpg" };

        public CustomerLogoController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet]
        [Route("api/[controller]/{customerId}/{*slug}")]
        public async Task<IActionResult> GetImage(Guid customerId, string slug)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.CustomerLogos, $"{customerId}/{slug}");

            if (doc == null)
            {
                return NotFound();
            }

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;         
        }

        private class UploadedFile
        {
            //Since JsonConvert.SerializeObject couldn't serialize the stream object I used byte[] instead
            public byte[] Stream { get; set; } = Array.Empty<byte>();
            public string FileName { get; set; } = "";

            public string ContentType { get; set; } = "";
        }

        [HttpPost]
        [Route("api/[controller]/{customerId}/{*slug}")]
        public async Task<IActionResult> AddImage(Guid customerId, string slug, IFormFile file)
        {
            if (!_supportedMimeTypes.Contains(file.ContentType.ToLower()))
            {
                throw new UnsupportedContentTypeException("Only PNG and JPG are supported.");
            }


            var stream = file.OpenReadStream();

            var document = new Document()
            {
                Content = stream,
                ContentType = file.ContentType,
                Name = file.FileName,
                CustomerId = customerId
            };

            var key = $"{customerId}/{slug}";

            await _fileStorage.UploadDocument(FileStorageType.CustomerLogos, key, document);

            return Ok(key);
            
        }
    }
}