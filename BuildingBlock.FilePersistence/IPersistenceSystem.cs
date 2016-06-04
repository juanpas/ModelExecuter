using System.IO;
using System.Net.Http;

namespace BuildingBlock.FilePersistence
{
    public interface IPersistenceSystem
    {
        MultipartFormDataStreamProvider MultipartFormProvider
        {
            get;
        }

        string GetNextIndexedFileName(string fileName, bool createFolder);

        FileUploadResult SaveFile(string fileName, Stream fileContent);
        FileUploadResult SaveFileFromURL(string sourceURL, string fileName);

        Stream GetFile(string fileName);
    }
}