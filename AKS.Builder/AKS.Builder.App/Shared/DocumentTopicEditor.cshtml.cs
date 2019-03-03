using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components;
using RestSharp;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;

namespace AKS.Builder.App.Shared
{
    public class DocumentTopicEditorModel : ComponentBase
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }

        protected ElementRef _fileUploader { get; set; }

        protected Uri _baseUri = new Uri($"https://localhost:44341/api");

        [Inject]
        private IFileReaderService fileReaderService { get; set; }

        protected async Task UploadFile()
        {
            this.StateHasChanged();
            foreach(var file in await fileReaderService.CreateReference(_fileUploader).EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();

                using (var stream = await file.OpenReadAsync())
                {
                    var bufferSize = 4096;
                    var buffer = new byte[bufferSize];
                    int count;
                    var memStream = new MemoryStream();
                    while ((count = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, count);
                    }

                    var anotherStream = memStream.ToArray();

                    var foo = new MemoryStream(anotherStream);
                    //memStream.CopyTo(anotherStream);

                    await UploadToApi(fileInfo, foo);
                }
            }
            this.StateHasChanged();
        }

        private async Task UploadToApi(IFileInfo fileInfo, Stream fileStream)
        {
            
            RestClient client = new RestClient(_baseUri);

            var req = new RestRequest($"ContentDocument/{Topic.ProjectId}/{Topic.TopicId}/{fileInfo.Name}", Method.POST, DataFormat.None);

            var fileStreamAction = new Action<Stream>(x => { fileStream.CopyTo(x); });
            req.AddFile("file", fileStreamAction, fileInfo.Name, fileInfo.Size, fileInfo.Type);

            try
            {
                var result = await client.ExecuteTaskAsync<Resurgam.AppCore.DTO.Document>(req);

                var doc = result.Data;
                Topic.DocumentName = doc.Name;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
