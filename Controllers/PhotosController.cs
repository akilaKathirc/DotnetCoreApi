using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication2.Data;
using WebApplication2.Helpers;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Security.Claims;
using WebApplication2.Models;
using WebApplication2.Dtos;

namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]/{userID}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _CloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IDatingRepository repo,IMapper mapper,IOptions<CloudinarySettings> CloudinaryConfig)
        {
            _repo = repo;
            _mapper = mapper;
            _CloudinaryConfig = CloudinaryConfig;

            Account account = new Account(
             _CloudinaryConfig.Value.CloudName,
             _CloudinaryConfig.Value.ApiKey,
              _CloudinaryConfig.Value.ApiSecret);

             _cloudinary = new Cloudinary(account);
        }

        [HttpGet("{id}", Name ="GetPhoto")]
        public async Task<IActionResult> GetPhotos(int id)
        {
            var photoFromRepo =await _repo.GetPhotos(id);
            var photo = _mapper.Map<PhotoToReturnDTO>(photoFromRepo);
            return Ok(photo);
        }




        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userID, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var userFromRepo = _repo.GetUser(userID).Result;

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();
            if(file.Length> 0) {
            using(var stream = file.OpenReadStream())
                {
                    var UploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    uploadResult = _cloudinary.Upload(UploadParams);

                }
            }

            photoForCreationDto.url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicID = uploadResult.PublicId;

            var photos = _mapper.Map<Photos>(photoForCreationDto);

            if(!userFromRepo.Photos.Any(u => u.isMain)) {
                photos.isMain = true;
            }
            userFromRepo.Photos.Add(photos);

         
            if (await _repo.SaveAll())
            {
                var photosToReturn = _mapper.Map<PhotoToReturnDTO>(photos);
                return CreatedAtRoute("GetPhoto",new { UserId = userID, id = photos.ID }, photosToReturn);
            }
            return BadRequest();
        }
            
        [HttpPost("{photoId}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userID, int photoId)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = _repo.GetUser(userID).Result;

            if (!userFromRepo.Photos.Any(p => p.ID == photoId))
                  return Unauthorized();

            var photoFromRepo = await _repo.GetPhotos(photoId);
            if (photoFromRepo.isMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = await _repo.GetMainPhotoForUser(userID);
            currentMainPhoto.isMain = false;

           
            photoFromRepo.isMain = true;

            if (await _repo.SaveAll())
                return NoContent();

            return BadRequest("Could not set photo to main");
        }

        [HttpDelete("{photoId}/delete")]

        public async Task<IActionResult> DeletePhoto(int userID, int photoId)
        {
            if (userID != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = _repo.GetUser(userID).Result;

            if (!userFromRepo.Photos.Any(p => p.ID == photoId))
                return Unauthorized();

            var PhotofromRepo = await _repo.GetPhotos(photoId); 
            if (PhotofromRepo.isMain == true)
                return BadRequest("User can't delete main photo");

            if(PhotofromRepo.PublicID != null)
            {
                var deleteparams = new DeletionParams(PhotofromRepo.PublicID);

                var cloudResult = _cloudinary.Destroy(deleteparams);

                if (cloudResult.Result == "ok")
                {
                    _repo.Delete(PhotofromRepo);
                }
            }
            else if(PhotofromRepo.PublicID == null)
            {
                _repo.Delete(PhotofromRepo);
            }

         

            if (await _repo.SaveAll())
                return Ok();

            return BadRequest("Failed to delete");
        }

        }
}