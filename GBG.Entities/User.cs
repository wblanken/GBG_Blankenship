// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Data.Linq.Mapping;

namespace GBG.Entities
{
   [Table(Name = "User")]
   public class User
   {
      private int _id;
      private string _firstName;
      private string _lastName;

      [Column(IsPrimaryKey = true, Storage = "_id", IsDbGenerated = true)]
      public int Id
      {
         get { return _id; }
         set { _id = value; }
      }

      [Column(Storage = "_firstName")]
      public string FirstName
      {
         get { return _firstName; }
         set { _firstName = value; }
      }

      [Column(Storage = "_lastName")]
      public string LastName
      {
         get { return _lastName; }
         set { _lastName = value; }
      }
   }
}