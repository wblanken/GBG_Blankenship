// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Data.Linq.Mapping;

namespace GBG.Entities
{
   [Table(Name = "Status")]
   public class Status
   {
      private int _id;
      private string _message;

      [Column(IsPrimaryKey = true, Storage = "_id", IsDbGenerated = true)]
      public int Id
      {
         get { return _id; }
         set { _id = value; }
      }

      [Column(Name = "Status", Storage = "_message")]
      public string Message
      {
         get { return _message; }
         set { _message = value; }
      }
   }
}