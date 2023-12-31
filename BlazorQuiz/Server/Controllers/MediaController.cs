﻿using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Services;
using BlazorQuiz.Shared.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace BlazorQuiz.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : BaseController
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService )
        {
            _mediaService = mediaService;
        }


        [HttpGet("{guid}")]
        public async Task<IActionResult> GetMedia(Guid guid)
        {

            var media = await _mediaService.GetMediaByIdAsync(guid);

            MediaViewModel returnMedia = new()
            {
                Type = media.Type,
                Path = media.Path
            };

            return Ok(returnMedia);
        }

        [HttpPost]
        public async Task<IActionResult> UploadMediaAsync([FromForm] IFormFile file)
        {
            //Upload video or image

            int maxMb = 13;
            long megaByte = 1024 * 1024;

            long maxAllowedSizeInBytes = maxMb * megaByte;
            string[] permittedFileTypes = { ".jpg", ".jpeg", ".png", ".gif", ".mp4" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (file.Length > maxAllowedSizeInBytes)
            {
                return BadRequest("File size exceeds the allowable limit.");
            }

            if (string.IsNullOrEmpty(extension) || !permittedFileTypes.Contains(extension))
            {
                return BadRequest("Invalid file type. Submitted filetype: " + file.ContentType);
            }



            var newMedia = await _mediaService.UploadMediaAsync(file, UserId);

            return Ok(newMedia);
        }

    }
}
