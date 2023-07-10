using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using Web_API.Models.Domain;
using Web_API.Models.DTO;
using Web_API.Repositories;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        //POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        { 
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //Convert DTO to Domain Model
                var imageDomainModel = new Image 
                { 
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName  = request.FileName,
                    FileDescription = request.FileDescription,
                    
                };

                // User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        { 
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtentions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extention");
            }

            if(request.File.Length > 10485760)
            { 
                ModelState.AddModelError("file", "File size more then 10MB, PLease upload a smaller size file.");
            }
        }
    }
}
