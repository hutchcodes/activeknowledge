using AKS.AppCore.DTO;
using AKS.AppCore.Enums;
using System.Threading.Tasks;

namespace AKS.AppCore.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<Document?> GetDocument(FileStorageType fileStorageType, string key);

        Task UploadDocument(FileStorageType fileStorageType, string key, Document uploadedFile);
    }
}
