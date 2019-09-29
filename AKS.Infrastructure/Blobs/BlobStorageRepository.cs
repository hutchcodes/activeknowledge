using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using AKS.AppCore.DTO;
using AKS.AppCore.Enums;
using AKS.AppCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Blobs
{
    public class BlobStorageRepository : IFileStorageRepository
    {
        private readonly IConfiguration _config;

        public BlobStorageRepository(IConfiguration config)
        {
            _config = config;
        }

        private CloudBlobContainer GetCloudBlobContainer(string? containerName)
        {
            var blobStorageConnection = _config.GetValue<string>("AppSettings:BlobStorageConnection");

            var cloudStorageAccount = CloudStorageAccount.Parse(blobStorageConnection);

            var client = cloudStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference(containerName);

            return container;
        }

        public async Task<Document?> GetDocument(FileStorageType fileStorageType, string key)
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

            if(blob.Metadata.TryGetValue(FileMetaData.CustomerId.GetStringValue(), out var customerId))
            {
                doc.CustomerId = Guid.Parse(customerId);
            }

            if (blob.Metadata.TryGetValue(FileMetaData.ProjectId.GetStringValue(), out var projectId))
            {
                doc.ProjectId = Guid.Parse(projectId);
            }

            if (blob.Metadata.TryGetValue(FileMetaData.TopicId.GetStringValue(), out var topicId))
            {
                doc.TopicId = Guid.Parse(topicId);
            }

            return doc;
        }

        public async Task UploadDocument(FileStorageType fileStorageType, string key, Document uploadedFile)
        {
            var container = GetCloudBlobContainer(fileStorageType.GetStringValue());

            CloudBlockBlob blockBlobImage = container.GetBlockBlobReference(key.ToLower());
            blockBlobImage.Properties.ContentType = uploadedFile.ContentType;
            if (uploadedFile.CustomerId.HasValue)
            {
                blockBlobImage.Metadata.Add(FileMetaData.CustomerId.GetStringValue(), uploadedFile.CustomerId.ToString());
            }
            if (uploadedFile.ProjectId.HasValue)
            {
                blockBlobImage.Metadata.Add(FileMetaData.ProjectId.GetStringValue(), uploadedFile.ProjectId.ToString());
            }
            if (uploadedFile.TopicId.HasValue)
            {
                blockBlobImage.Metadata.Add(FileMetaData.TopicId.GetStringValue(), uploadedFile.TopicId.ToString());
            }

            await blockBlobImage.UploadFromStreamAsync(uploadedFile.Content);
        }
    }
}
