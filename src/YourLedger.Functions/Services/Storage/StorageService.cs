using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Storage.V1;
using Newtonsoft.Json;
using YourLedger.Functions.Services.Storage.Exceptions;
using YourLedger.Functions.Services.Storage.Interface;
using System.Linq;
namespace YourLedger.Functions.Services.Storage
{
    public class StorageService<T> : IStorageService<T>
    {
        private readonly StorageClient _client;
        private readonly string _bucketName;
        private readonly string _fileType;
        public StorageService(StorageClient client, string bucketName, string fileType)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _bucketName = !string.IsNullOrEmpty(bucketName) ? bucketName : throw new ArgumentNullException(nameof(bucketName));
            _fileType = !string.IsNullOrEmpty(fileType) ? fileType : throw new ArgumentNullException(nameof(fileType));
        }

        public async Task UploadFile(string fileName, T jsonFile)
        {
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            if(jsonFile == null)
                throw new ArgumentNullException(nameof(jsonFile));

            try
            {
                var stream = new MemoryStream(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(jsonFile)));
                await _client.UploadObjectAsync(_bucketName, fileName ,_fileType, stream);
            }
            catch(Exception ex)
            {
                throw new StorageServiceException("Error when uploading file", ex);
            }
        }

        public async Task<T> GetFile(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            try
            {
                using(var ms = new MemoryStream())
                {
                    await _client.DownloadObjectAsync(_bucketName, fileName, ms);
                    using(StreamReader reader = new StreamReader(ms))
                    {
                        ms.Position = 0; //return the memory postion back to 0 just in case
                        return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
                    }           
                }
            }
            catch(Exception ex)
            {
                throw new StorageServiceException("Error when getting file", ex);
            }
        }

        public async Task<bool> FileExists(string fileName)
        {
            if(string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            
            try
            {
                var objectsInBucket = _client.ListObjects(_bucketName);
                return objectsInBucket.FirstOrDefault(x=>x.Name == fileName) != null ? true : false; 
            }
            catch(Exception ex)
            {
                throw new StorageServiceException("Error when trying to get objects list from gcp bucket", ex);
            }
        }   
    }
}