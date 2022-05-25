using Microsoft.AspNetCore.Mvc;

using SigmaBooking.Core.IServices;
using SigmaBooking.Core.Models;
using SigmaBooking.WebApi.Dtos;

namespace SigmaBooking.WebApi.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class CredentialsController : ControllerBase
    {
        private readonly ICredentialsService _credentialsService;
        
        public CredentialsController(ICredentialsService service)
        {
            _credentialsService = service;
        }

        [HttpPost]
        public ActionResult<CredentialsDTO> CreateCredentials([FromBody] CredentialsDTO dto)
        {
            var credentialsFromDto = new CredentialsModel
            {
                Credentials = dto.Credentials
            };
        
            try
            {
                var newCredentials = _credentialsService.CreateCredentials(credentialsFromDto);
                return Created($"https://localhost:7026/api/credentials/{newCredentials.Id}", newCredentials);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<CredentialsDTO> getCredentials(string id)
        {
            try
            {
                var credentials = _credentialsService.GetCredentials(id);
                return Ok(new CredentialsDTO
                {
                    Id = credentials.Id,
                    Credentials = credentials.Credentials
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public void DeleteCredentials(string id)
        {
            _credentialsService.DeleteCredentials(id);
        }
    }