using Api.Identity;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPublisherRepository _publisherRepository;

        public PublisherController(IMapper mapper, IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }
        
        [Authorize(Policy = IdentityData.AdminPolicyName)]
        [HttpPost]
        public async Task<IActionResult> CreatePublisher([FromBody]PublisherDto publisherDto)
        {
            
            if(publisherDto.Royalty < 0) return BadRequest(new { message = "royalty can not be negative" });
            
            if(String.IsNullOrWhiteSpace(publisherDto.Name)) return BadRequest(new { message = "Name is required" });
            
            var validpublisher = await _publisherRepository.GetPublisherByName(publisherDto.Name);
            
            if(validpublisher != null) return BadRequest(new { message = "This name is already in use" });
            
            if(publisherDto.Description.Length < 8) return BadRequest(new { message = "Description msut be at least 8 characters" });
            
            
            publisherDto.Created_on = DateTime.Now; 
            
            var publisher = _mapper.Map<Publisher>(publisherDto);
            
            await _publisherRepository.CreatePublisher(publisher);
            
            return Ok(publisher);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var publishers = await _publisherRepository.GetAllPublishers();
            return Ok(publishers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);
            
            if(publisher == null) return NotFound();
            
            return Ok(publisher);
        }

        [Authorize(Policy = IdentityData.AdminPolicyName)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherDto publisherDto)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);
            
            if(publisher == null) return NotFound();
            
            if(publisherDto.Royalty == null ) publisherDto.Royalty = publisherDto.Royalty;
            else if(publisherDto.Royalty < 0) return BadRequest(new { message = "royalty can not be negative" });
            
            if(String.IsNullOrWhiteSpace(publisherDto.Name)) publisherDto.Name = publisherDto.Name;
            
            var validpublisher = await _publisherRepository.GetPublisherByName(publisherDto.Name);
            
            if(validpublisher != null) return BadRequest(new { message = "This name is already in use" });

            if (publisherDto.Description == "") publisherDto.Description = publisher.Description; 
            else if(publisherDto.Description.Length < 8) return BadRequest(new { message = "Description msut be at least 8 characters" });

            publisherDto.Created_on = publisher.Created_on;
            
            await _publisherRepository.UpdatePublisher(_mapper.Map(publisherDto, publisher));
            
            return Ok(new {message = "Publisher updated successfully!"});
        }

        [Authorize(Policy = IdentityData.AdminPolicyName)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);
            if(publisher == null) return NotFound();
            await _publisherRepository.DeletePublisher(publisher);
            return Ok(new {message = "Publisher deleted successfully!"});
        }
    }
}
