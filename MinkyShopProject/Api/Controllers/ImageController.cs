using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MinkyShopProject.Admin.Shared;
using System.Text;
using System.IO;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<ImageController> logger;

        public ImageController(IWebHostEnvironment env,
            ILogger<ImageController> logger)
        {
            this.env = env;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<List<UploadResult>>> UploadImage(List<IFormFile> files)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1024 * 1024 * 1024;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new();

            foreach (var file in files)
            {
                var image = file.FileName.Split(".")[1];
                if (image != "jpg" && image != "png" && image != "webp" && image != "jpeg")
                {
                    return new NotFoundResult();
                }

                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Guid.NewGuid() + "." + image;
                            var path = Path.Combine(env.ContentRootPath, "wwwroot/images/",
                                trustedFileNameForFileStorage);

                            if (!Directory.Exists(env.ContentRootPath + "wwwroot/images/"))
                            {
                                Directory.CreateDirectory(env.ContentRootPath + "wwwroot/images/");
                            }

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                        }
                        catch (IOException ex)
                        {
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadResults);
        }

        [HttpPost("khachhang")]
        public async Task<ActionResult<List<UploadResult>>> UploadImageKhach(List<IFormFile> files)
        {
            var maxAllowedFiles = 3;
            long maxFileSize = 1024 * 1024 * 1024;
            var filesProcessed = 0;
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            List<UploadResult> uploadResults = new();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                var trustedFileNameForDisplay =
                    WebUtility.HtmlEncode(untrustedFileName);

                if (filesProcessed < maxAllowedFiles)
                {
                    if (file.Length == 0)
                    {
                        logger.LogInformation("{FileName} length is 0 (Err: 1)",
                            trustedFileNameForDisplay);
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        logger.LogInformation("{FileName} of {Length} bytes is " +
                            "larger than the limit of {Limit} bytes (Err: 2)",
                            trustedFileNameForDisplay, file.Length, maxFileSize);
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            trustedFileNameForFileStorage = Guid.NewGuid() + "." + file.ContentType.Split("/")[1];
                            var path = Path.Combine(env.ContentRootPath, "wwwroot/images/khach",
                                trustedFileNameForFileStorage);

                            if (!Directory.Exists(env.ContentRootPath + "wwwroot/images/khach"))
                            {
                                Directory.CreateDirectory(env.ContentRootPath + "wwwroot/images/khach");
                            }

                            await using FileStream fs = new(path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            logger.LogInformation("{FileName} saved at {Path}",
                                trustedFileNameForDisplay, path);
                            uploadResult.Uploaded = true;
                            uploadResult.StoredFileName = trustedFileNameForFileStorage;
                        }
                        catch (IOException ex)
                        {
                            logger.LogError("{FileName} error on upload (Err: 3): {Message}",
                                trustedFileNameForDisplay, ex.Message);
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    filesProcessed++;
                }
                else
                {
                    logger.LogInformation("{FileName} not uploaded because the " +
                        "request exceeded the allowed {Count} of files (Err: 4)",
                        trustedFileNameForDisplay, maxAllowedFiles);
                    uploadResult.ErrorCode = 4;
                }

                uploadResults.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadResults);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> Get()
        {
            var resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");

            List<string> files = new List<string>();

            DirectoryInfo d = new DirectoryInfo(env.ContentRootPath + "wwwroot/images/"); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles().Where(c => c.Extension.EndsWith(".webp") || c.Extension.EndsWith(".png") || c.Extension.EndsWith(".jpg")).ToArray(); //Getting Text files

            foreach (FileInfo file in Files)
            {
                files.Add(resourcePath + "/images/" + file.Name);
            }

            return files;
        }
    }
}
