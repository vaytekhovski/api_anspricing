﻿using System;
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


        [HttpPost("update")]
        public IActionResult EditTag([FromBody]string Json)
        {
            IActionResult response = Unauthorized();

            var result = StationManager.EditTag(Json);
            response = Ok(new { result = result });

            return response;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("It work");
        }

    }
}