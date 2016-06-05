// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GBG.Entities.Test
{
   [TestClass]
   public class DataServiceTest
   {
      [TestMethod]
      public void TestGetUsers()
      {
         var users = DataService.GetUsers();
         Assert.IsNotNull(users);
         Assert.IsTrue(users.Count >= 10); // We know the seed data has 10 users.
      }

      [TestMethod]
      public void TestGetUser()
      {
         var user = DataService.GetUser(0);
         Assert.IsNotNull(user);
      }

      [TestMethod]
      public void TestGetStatues()
      {
         var statuses = DataService.GetStatuses();
         Assert.IsNotNull(statuses);
         Assert.IsTrue(statuses.Count >= 3); // We know the seed data has 3 statuses.
      }

      [TestMethod]
      public void TestGetStatus()
      {
         var status = DataService.GetStatus(0);
         Assert.IsNotNull(status);
      }

      [TestMethod]
      public void TestGetSamples()
      {
         var samples = DataService.GetSamples();
         Assert.IsNotNull(samples);
         Assert.IsTrue(samples.Count >= 100); // We know the seed data has 100 samples.
         Assert.IsNotNull(samples[0].Status);
         Assert.IsNotNull(samples[0].User);
      }

      [TestMethod]
      public void TestGetSamplesByUser()
      {
         var samples = DataService.GetSamplesByUser(0);
         Assert.IsNotNull(samples);
         Assert.IsTrue(samples.All(a => a.CreatedBy == 0));
      }

      [TestMethod]
      public void TestGetSamplesByStatus()
      {
         var samples = DataService.GetSamplesByStatus(0);
         Assert.IsNotNull(samples);
         Assert.IsTrue(samples.All(a => a.StatusId == 0));
      }

      [TestMethod]
      public void TestGetSamplesByUserName()
      {
         const string testName = "Kristine";

         var samples = DataService.GetSamplesByUserName(testName);
         Assert.IsNotNull(samples);
         Assert.IsTrue(samples.All(a => a.User.FirstName.Contains(testName) || a.User.LastName.Contains(testName)));
      }

      [TestMethod]
      public void TestAddSample()
      {
         Sample newSample = null;

         try
         {
            var sample = new Sample()
            {
               CreatedAt = DateTime.Now,
               BarCode = "111222333",
               CreatedBy = 0,
               StatusId = 0,
            };

            sample.User = DataService.GetUser(sample.CreatedBy);
            sample.Status = DataService.GetStatus(sample.StatusId);

            DataService.AddSample(sample);

            var samples = DataService.GetSamples();
            newSample = samples.SingleOrDefault(s => s.BarCode == sample.BarCode);
            Assert.IsNotNull(newSample);
            Assert.IsTrue(newSample.StatusId == sample.StatusId);
            Assert.IsTrue(newSample.CreatedBy == sample.CreatedBy);
         }
         finally
         {
            if (null != newSample)
            {
               var queryString = $"DELETE FROM [dbo].[Sample] WHERE [Id]={newSample.Id}";
               var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

               // Cleanup added data from the db 
               using (var connection = new SqlConnection(connectionString))
               {
                  var command = new SqlCommand(queryString, connection);
                  connection.Open();
                  command.ExecuteNonQuery();
               }
            }
         }
      }
   }
}