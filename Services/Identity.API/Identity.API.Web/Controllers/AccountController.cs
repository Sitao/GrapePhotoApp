﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Identity.API.SignIn;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : AbpController
    {
        private ISignInService _signInService;
        public AccountController(SignInService signInService)
        {
            _signInService = signInService;
        }
     
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

 
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public bool Post([FromBody]UserDto user)
        {
            if (_signInService.SignIn(user) != null)
            {
                return true;
            }
            return false;
        }


       


        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



    }
}