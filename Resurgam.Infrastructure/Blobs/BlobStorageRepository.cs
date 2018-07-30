using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Resurgam.AppCore.DTO;
using Resurgam.AppCore.Enums;
using Resurgam.AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Resurgam.Infrastructure.Blobs
{
    public class BlobStorageRepository : IFileStorageRepository
    {
        IConfiguration _config;

        public BlobStorageRepository(IConfiguration config)
        {
            _config = config;
        }

        private CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            var blobStorageConnection = _config.GetValue<string>("AppSettings:BlobStorageConnection");

            var cloudStorageAccount = CloudStorageAccount.Parse(blobStorageConnection);

            var client = cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

            return container;
        }

        public async Task<Document> GetDocument(FileStorageType fileStorageType, string key)
        {
            
            var container = GetCloudBlobContainer(fileStorageType.GetStringValue());
            var blob = container.GetBlockBlobReference(key.ToLower());

            var blobExists = await blob.ExistsAsync();
            if (!blobExists)
            {
                return null;
            }

            var blobStream = await blob.OpenReadAsync();

            var doc = new Document()
            {
                Content = blobStream,
                ETag = blob.Properties.ETag,
                ContentType = blob.Properties.ContentType,
                LastModified = blob.Properties.LastModified,
                Name = blob.Name,
            };

            if(blob.Metadata.TryGetValue("CustomerId", out string customerId))
            {
                doc.CustomerId = int.Parse(customerId);
            }

            if (blob.Metadata.TryGetValue("ProjectId", out string projectId))
            {
                doc.ProjectId = int.Parse(projectId);
            }

            return doc;
        }
    }
}
