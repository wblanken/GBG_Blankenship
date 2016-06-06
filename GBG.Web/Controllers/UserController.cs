// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Collections.Generic;
using System.Web.Http;
using GBG.Entities;

namespace GBG.Web.Controllers
{
   public class UserController : ApiController
   {
      [HttpGet]
      public IEnumerable<User> GetAllUsers()
      {
         return DataService.GetUsers();
      }
   }
}