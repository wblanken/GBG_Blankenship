// Copyright (c) 2016 Will Blankenship, All Rights Reserved.

using System.Configuration;
using System.Data.Linq;

namespace GBG.Entities
{
   internal static class DbContext
   {
      public static DataContext GetDataContext()
      {
         return new DataContext(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
      }
   }
}