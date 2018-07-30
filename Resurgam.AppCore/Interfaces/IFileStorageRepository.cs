using Resurgam.AppCore.DTO;
using Resurgam.AppCore.Enums;
using System.Threading.Tasks;

namespace Resurgam.AppCore.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<Document> GetDocument(FileStorageType fileStorageType, string key);
    }
}
