using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UniversityWebApp.Repository
{
    public class SampleData:DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        protected override void Seed(MyDbContext context)
        {
            
        }

    }
}