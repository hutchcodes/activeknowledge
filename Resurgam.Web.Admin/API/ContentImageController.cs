using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Resurgam.AppCore.Enums;
using Resurgam.AppCore.Interfaces;

namespace Resurgam.Web.Admin.API
{
    //[RoutePrefix("api/[controller]")]
    [ApiController]
    public class ContentImageController : ControllerBase
    {
        private IFileStorageRepository _fileStorage;

        public ContentImageController(IFileStorageRepository fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [Route("api/[controller]/{customerId}")]
        public async Task<IActionResult> GetImage(int customerId)
        {
            var doc = await _fileStorage.GetDocument(FileStorageType.ContentImages, $"{customerId}/portland.jpg");

            var file = File(doc.Content, doc.ContentType, doc.LastModified, new EntityTagHeaderValue(doc.ETag));
            return file;         
        }
    }
}