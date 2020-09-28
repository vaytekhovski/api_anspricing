using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ANSPRICING.Models;
using API_ANSPRICING.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API_ANSPRICING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouterController : ControllerBase
    {
        private StationManager StationManager;

        public RouterController(StationManager StationManager)
        {
            this.StationManager = StationManager;
        }


        [HttpPost("update")]
        public IActionResult EditTag([FromBody]string json)
        {
            Tag tag = JsonConvert.DeserializeObject<Tag>(json);
            IActionResult response = Unauthorized();

            try
            {
                var result = StationManager.EditTag(tag);
                response = Ok(new { result = result });
            }
            catch (NullReferenceException e)
            {
                response = BadRequest(e.Message);
            }
            return response;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("It work");
        }

    }
}