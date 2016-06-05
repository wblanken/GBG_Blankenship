// Copyright (c) 2016 LDARtools, Inc.  All Rights Reserved.

using System;

namespace GBG.Entities
{
   public class Sample
   {
      public int Id { get; set; }
      public string BarCode { get; set; }
      public DateTime CreatedAt { get; set; }
      public int CreatedBy { get; set; }
      public int StatusId { get; set; }
   }
}