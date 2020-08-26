using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_ANSPRICING.Models;
using API_ANSPRICING.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("{StationId}/edit/{TagId}")]
        public IActionResult EditTag(Guid StationId, Guid TagId, [FromBody]Tag tag)
        {
            IActionResult response = Unauthorized();
            tag.StationId = StationId;
            tag.id = TagId;

            Tag result = StationManager.EditTag(tag);
            if (result != null)
            {
                response = Ok(new { tag = tag });
            }
            else
            {
                response = BadRequest(new { tag = "some error" });
            }

            return response;
        }

    }
}