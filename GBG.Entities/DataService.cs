// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace GBG.Entities
{
   public static class DataService
   {
      #region Read Methods

      /// <summary>
      /// Gets all users in the database
      /// </summary>
      public static IList<User> GetUsers()
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            return db.GetTable<User>().ToList();
         }
      }

      /// <summary>
      /// Get a specified user by Id
      /// </summary>
      /// <param name="userId">User's Id</param>
      public static User GetUser(int userId)
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            return db.GetTable<User>().SingleOrDefault(s => s.Id == userId);
         }
      }

      /// <summary>
      /// Gets all statuses in the database
      /// </summary>
      public static IList<Status> GetStatuses()
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            return db.GetTable<Status>().ToList();
         }
      }

      /// <summary>
      /// Get a specified status by Id
      /// </summary>
      /// <param name="statusId">User's Id</param>
      public static Status GetStatus(int statusId)
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            return db.GetTable<Status>().SingleOrDefault(s => s.Id == statusId);
         }
      }

      /// <summary>
      /// Gets all samples in the database, including their user and status.
      /// </summary>
      public static IList<Sample> GetSamples()
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            var samples = db.GetTable<Sample>();

            PopulateUserAndStatus(samples, db);

            return samples.ToList();
         }
      }

      /// <summary>
      /// Gets samples entered by a specific user
      /// </summary>
      /// <param name="userId">The Id of the user to filter on</param>
      public static IList<Sample> GetSamplesByUser(int userId)
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            var samples = db.GetTable<Sample>().Where(w => w.CreatedBy == userId);
            PopulateUserAndStatus(samples, db);
            return samples.ToList();
         }
      }

      /// <summary>
      /// Gets samples that are in a specific status
      /// </summary>
      /// <param name="statusId">Te Id of the status to filter on</param>
      public static IList<Sample> GetSamplesByStatus(int statusId)
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            var samples = db.GetTable<Sample>().Where(w => w.StatusId == statusId);
            PopulateUserAndStatus(samples, db);
            return samples.ToList();
         }
      }

      /// <summary>
      /// Gets samples entered by user name
      /// </summary>
      /// <param name="userName">Portion of a user's name</param>
      public static IList<Sample> GetSamplesByUserName(string userName)
      {
         using (var db = DbContext.GetDataContext())
         {
            db.Log = Console.Out;

            var filteredUserTable = db.GetTable<User>().Where(w => w.FirstName.Contains(userName) || w.LastName.Contains(userName));

            var samples = new List<Sample>();

            foreach (var user in filteredUserTable)
            {
               samples.AddRange(GetSamplesByUser(user.Id));
            }

            return samples;
         }
      }

      # endregion Read Methods

      #region Write Methods

      /// <summary>
      /// Create a new sample in the database
      /// </summary>
      /// <param name="sample">Sample data</param>
      /// <exception cref="ArgumentNullException">Throws argument exception if the user or status data are null</exception>
      public static void AddSample(Sample sample)
      {
         using (var db = DbContext.GetDataContext())
         {
            if (null == sample.User)
            {
               throw new ArgumentNullException(nameof(sample.User), "The sample user data is invalid");
            }

            if (null == sample.Status)
            {
               throw new ArgumentNullException(nameof(sample.Status), "The sample status data is invalid");
            }

            sample.CreatedAt = DateTime.Now;

            db.GetTable<Sample>().InsertOnSubmit(sample);
            db.SubmitChanges();
         }
      }

      #endregion Write Methods


      #region Helper Methods

      /// <summary>
      /// Populate a collection of samples' status and user properties.
      /// </summary>
      /// <param name="samples">Collection of samples</param>
      /// <param name="db">Data Context</param>
      private static void PopulateUserAndStatus(IEnumerable<Sample> samples, DataContext db)
      {
         foreach (var sample in samples)
         {
            sample.User = db.GetTable<User>().SingleOrDefault(s => s.Id == sample.CreatedBy);
            sample.Status = db.GetTable<Status>().SingleOrDefault(s => s.Id == sample.StatusId);
         }
      }

      #endregion Helper Methods

   }
}