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
                var classifiedAd = new ClassifiedAd
                {
                    OwnerId = Guid.NewGuid(),
                    Title = "one to many",
                    Pictures= new List<Picture>(){
                        new Picture
                        {
                            Width = 800,
                            Height = 600,
                            Location = "https://google.com.tw"
                        },      
                        new Picture
                        {
                            Width = 801,
                            Height = 601,
                            Location = "https://yahoo.com.tw"
                        },new Picture
                        {
                            Width = 802,
                            Height = 602,
                            Location = "https://facebook.com.tw"
                        }
                    }
                };
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
                var picture = new Picture { 
                    Width=803,Height=603,Location="https://yuntech.edu.tw"};

                classifiedAd.Pictures.Add(picture);
                context.SaveChanges();
            }
            #endregion
        }
    }
}