using AKS.Infrastructure.DTO;
using AKS.Infrastructure.Enums;
using System.Threading.Tasks;

namespace AKS.Infrastructure.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<Document?> GetDocument(FileStorageType fileStorageType, string key);

        Task UploadDocument(FileStorageType fileStorageType, string key, Document uploadedFile);
    }
}
