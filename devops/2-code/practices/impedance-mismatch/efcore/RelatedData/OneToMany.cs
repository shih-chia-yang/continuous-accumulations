using System.Globalization;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace efcore.RelatedData
{
    public class OneToMany
    {
        public static void Run()
        {
            using (var context =new ClassifiedAdContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                AddingGraphOfEntities();
                AddingRelatedEntity();
            }
        }

        private static void AddingGraphOfEntities()
        {
            #region AddingGraphOfEntities
            using (var context = new ClassifiedAdContext())
            {
                var rootId = Guid.NewGuid();
                var classifiedAd = new ClassifiedAd(Guid.NewGuid(), "one to many");
                classifiedAd.AddPicture(new Picture(800,600,"https://google.com.tw",1));
                classifiedAd.AddPicture(new Picture(801,601,"https://yahoo.com.tw",2));
                classifiedAd.AddPicture(new Picture(802,602,"https://facebook.com.tw",3));
                context.ClassifiedAds.Add(classifiedAd);
                context.SaveChanges();
            }
            #endregion
        }

        private static void AddingRelatedEntity()
        {
            #region AddingRelatedEntity
            using (var context = new ClassifiedAdContext())
            {
                var classifiedAd = context.ClassifiedAds.Include(b => b.Pictures).First();
                var picture = new Picture(803, 603, "https://yuntech.edu.tw",classifiedAd.Pictures.Count()+1);
                classifiedAd.AddPicture(picture);
                context.SaveChanges();
            }
            #endregion
        }
    }
}