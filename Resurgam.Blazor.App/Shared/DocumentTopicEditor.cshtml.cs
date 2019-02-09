using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using RestSharp;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.ViewModels;

namespace Resurgam.Blazor.App.Shared
{
    public class DocumentTopicEditorModel : BlazorComponent
    {
        [Parameter]
        protected TopicEditViewModel Topic { get; set; }

        protected ElementRef _fileUploader { get; set; }

        [Inject]
        private IFileReaderService fileReaderService { get; set; }

        protected async Task UploadFile()
        {
            this.StateHasChanged();
            foreach(var file in await fileReaderService.CreateReference(_fileUploader).EnumerateFilesAsync())
            {
                var fileInfo = await file.ReadFileInfoAsync();

                using (var stream = await file.CreateMemoryStreamAsync())
                {
                    await UploadToApi(fileInfo, stream);

                    //var bufferSize = 4096;
                    //var buffer = new byte[bufferSize];
                    //int count;
                    //while((count = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    //{

                    //}
                }
            }
            this.StateHasChanged();
        }

        private async Task UploadToApi(IFileInfo fileInfo, Stream fileStream)
        {
            var baseUri = new Uri($"https://localhost:44341/api");
            RestClient client = new RestClient(baseUri);

            var req = new RestRequest($"ContentDocument/{Topic.ProjectId}/{Topic.TopicId}/{fileInfo.Name}", Method.POST, DataFormat.None);

            var fileStreamAction = new Action<Stream>(x => { fileStream.CopyTo(x); });
            req.AddFile("file", fileStreamAction, fileInfo.Name, fileInfo.Size, fileInfo.Type);

            try
            {
                var result = await client.ExecuteTaskAsync(req);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
