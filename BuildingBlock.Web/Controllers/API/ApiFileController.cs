using BuildingBlock.Defs;
using BuildingBlock.FilePersistence;
using BuildingBlock.Model;
using BuildingBlock.Repository.Contracts;
using BuildingBlock.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using static BuildingBlock.Defs.Enums;

namespace BuildingBlock.Web.Controllers
{
    public class ApiFileController : EPApiController
    {
        public ApiFileController(IMainUow uow, Utils.Utils utils)
        {
            Uow = uow;
            Utils = utils;
        }

        /*
        /// <summary>
        ///   Get all photos
        /// </summary>
        /// <returns></returns>
        [ActionName("RelatedToObject")]
        public async Task<List<FileViewModel>> Get(int fileType, int fileSubType, int relatedId)
        {
            switch (fileType)
            {
                case (int)Enums.FileType.Photo:
                    List<PhotoViewModel> photos = new List<PhotoViewModel>();

                    switch(fileSubType)
                    {
                        case (int)Enums.FileSubType.ProductPhoto:
                            await Task.Factory.StartNew(() =>
                            {
                                List<Photo> photoList = Uow.Photos.Get((int)Enums.FileSubType.ProductPhoto, relatedId).ToList();
                                photos = photoList.Select(p => new PhotoViewModel(p)).ToList();

                                return photos;
                            });


                            break;

                    }

                    break;
            }

            return new List<FileViewModel>();
        }

        /// <summary>
        ///   Delete photo
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int fileType, int id)
        {
            var result = new FileActionResult
            {
                Successful = false,
                Message = "error"
            };

            switch (fileType)
            {
                case (int)Enums.FileType.Photo:
                    Uow.Photos.Delete(id);

                    Uow.Commit();

                    result = new FileActionResult
                    {
                        Successful = true,
                        Message = string.Format("Photo Id: {0} deleted successfully", id)
                    };

                    return Ok(new { message = result.Message });
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        ///   Add a photo
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Add(int fileType, int fileSubType, int relatedId)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }
            try
            {
                FilePersistenceSystemType filePersistenceSystemType = (FilePersistenceSystemType)(Defs.Utils.ReadAppSetting<int>("FileStorageType", (int)FilePersistenceSystemType.Local));

                IPersistenceSystem persistenceSystem = PersitenceSistemFactory.GetPersistenseSystem(filePersistenceSystemType, (FileType)fileType, (FileSubType)fileSubType, relatedId.ToString());

                MultipartFormDataStreamProvider provider = persistenceSystem.MultipartFormProvider;
                await Request.Content.ReadAsMultipartAsync(provider);

                List<object> fileList = new List<object>();

                foreach(MultipartFileData fileData in provider.FileData)
                {
                    int index = Convert.ToInt32(Path.GetFileNameWithoutExtension(fileData.LocalFileName));

                    switch (fileType)
                    {
                        case (int)Enums.FileType.Photo:

                            Photo photo = new Photo()
                            {
                                Index = index,
                                RelatedId = relatedId,
                                SubTypeId = fileSubType,
                                Title = index.ToString(),
                                Alt = index.ToString(),
                                FileName = Path.GetFileName(fileData.LocalFileName)
                            };

                            fileList.Add(photo);

                            break;
                    }

                }

                List<FileViewModel> fileViewModelList = new List<FileViewModel>();

                switch (fileType)
                {
                    case (int)Enums.FileType.Photo:
                        foreach(object photo in fileList)
                        {
                            Uow.Photos.Add((Photo)photo);
                            fileViewModelList.Add(new PhotoViewModel((Photo)photo));
                        }

                        break;
                }

                Uow.Commit();

                return Ok(new { Message = "Files uploaded ok", Files = fileViewModelList });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    */

    }


    public class FileActionResult
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
    }









    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private string workingPath = string.Empty;
        private Utils.Utils utils;

        public CustomMultipartFormDataStreamProvider(string rootPath, Utils.Utils utils) : base(rootPath)
        {
            this.workingPath = rootPath;
            this.utils = utils;
        }

        public CustomMultipartFormDataStreamProvider(string rootPath, int bufferSize, Utils.Utils utils) : base(rootPath, bufferSize)
        {
            this.workingPath = rootPath;
            this.utils = utils;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string originalName = headers.ContentDisposition.FileName.Trim('"').Replace("&", "and");
            var name = NextFileName(originalName);

            return name;
        }

        private string NextFileName(string fileName)
        {
            string result = utils.GetNextIndexedFileName(workingPath, fileName, true);

            return result;
        }
    }


}
