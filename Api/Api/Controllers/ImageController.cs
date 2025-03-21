using Api.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController: ControllerBase
{
    private readonly IImageRepository _imageRepository;

    public ImageController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    [HttpGet]
   
    public async Task<IActionResult> GetImages()
    {
        var images = await _imageRepository.GetImages();
        
        if(!ModelState.IsValid)return BadRequest(ModelState);
        
        return Ok(images);
    }
}