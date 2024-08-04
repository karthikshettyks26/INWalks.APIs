using INWalks.APIs.Models.Domain;
using INWalks.APIs.Models.DTO;
using INWalks.APIs.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace INWalks.APIs.Controllers
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

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //Convert Dto to domain model
                var imageDomainModel = new Image()
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length
                };

                //Use repository to upload image
                await imageRepository.UploadAsync(imageDomainModel);

                return Ok(imageDomainModel);

            }
            else 
                return BadRequest(ModelState);

        }


        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(imageUploadRequestDto.File.Length > 1048576) //10 bytes
            {
                ModelState.AddModelError("file", "File size more than 10MB,please upload smaller sized file!");
            }
        }
    }
}
