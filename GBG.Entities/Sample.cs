// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace GBG.Entities
{
   [Table(Name = "Sample")]
   public class Sample
   {
      private int _id;
      private string _barCode;
      private DateTime _createdAt;
      private int _createdBy;
      private int _statusId;
      private EntityRef<User> _user;
      private EntityRef<Status> _status;

      [Column(IsPrimaryKey = true, Storage = "_id", IsDbGenerated = true)]
      public int Id
      {
         get { return _id; }
         set { _id = value; }
      }

      [Column(Storage = "_barCode")]
      public string BarCode
      {
         get { return _barCode; }
         set { _barCode = value; }
      }

      [Column(Storage = "_createdAt")]
      public DateTime CreatedAt
      {
         get { return _createdAt; }
         set { _createdAt = value; }
      }

      [Column(Storage = "_createdBy")]
      public int CreatedBy
      {
         get { return _createdBy; }
         set { _createdBy = value; }
      }

      [Column(Storage = "_statusId")]
      public int StatusId
      {
         get { return _statusId; }
         set { _statusId = value; }
      }

      // Disabling because the objects are disposed and need to be manually queried.
      // [Association(Name = "FK_Sample_User", Storage = "_user", ThisKey = "CreatedBy", IsForeignKey = true)]
      public User User
      {
         get { return _user.Entity; }
         set { _user.Entity = value; }
      }

      // Disabling because the objects are disposed and need to be manually queried.
      // [Association(Name = "FK_Sample_Status", Storage = "_status", ThisKey = "StatusId", IsForeignKey = true)]
      public Status Status
      {
         get { return _status.Entity; }
         set { _status.Entity = value; }
      }
   }
}