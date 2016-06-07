// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using GBG.Entities;

namespace GBG.Web.Controllers
{
   [RoutePrefix("api/samples")]
   public class SamplesController : ApiController
   {
      [HttpGet]
      public IEnumerable<Sample> GetAllSamples()
      {
         return DataService.GetSamples();
      }

      [Route("{id:int}")]
      [HttpGet]
      public IHttpActionResult GetSample(int id)
      {
         var sample = DataService.GetSamples().SingleOrDefault(s => s.Id == id);
         if (null == sample)
         {
            return NotFound();
         }
         return Ok(sample);
      }

      [Route("user/{userId:int}")]
      [HttpGet]
      public IEnumerable<Sample> GetSamplesByUsers(int userId)
      {
         return DataService.GetSamplesByUser(userId);
      }

      [Route("user/{userString:alpha}")]
      [HttpGet]
      public IEnumerable<Sample> GetSamplesByUserName(string userString)
      {
         return DataService.GetSamplesByUserName(userString);
      }

      [Route("status/{statusId:int}")]
      [HttpGet]
      public IEnumerable<Sample> GetSamplesByStatus(int statusId)
      {
         return DataService.GetSamplesByStatus(statusId);
      }

      [Route("user/{userString:alpha}/status/{statusId:int}")]
      public IEnumerable<Sample> GetSamplesByStatusAndName(string userString, int statusId)
      {
         var samples = DataService.GetSamplesByUserName(userString);

         return samples.Where(w => w.StatusId == statusId);
      }

      [HttpPost]
      public IHttpActionResult Post([FromBody]Sample sampleData)
      {
         try
         {
            DataService.AddSample(sampleData);
            return Ok();
         }
         catch (Exception e)
         {
            return BadRequest(e.Message);
         }
      }
   }
}