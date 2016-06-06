// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Collections.Generic;
using System.Web.Http;
using GBG.Entities;

namespace GBG.Web.Controllers
{
   public class StatusController : ApiController
   {
      [HttpGet]
      public IEnumerable<Status> GetAllStatuses()
      {
         return DataService.GetStatuses();
      }
   }
}