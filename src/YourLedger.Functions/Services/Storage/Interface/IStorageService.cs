using System.Threading.Tasks;

namespace YourLedger.Functions.Services.Storage.Interface
{
    public interface IStorageService<T>
    {
        Task UploadFile(string fileName, T jsonFile);
        Task<T> GetFile(string fileName);
        Task<bool> FileExists(string fileName);
    }
}