using System;

namespace efcore.RelatedData
{
    public class Picture
    {
        public Guid PictureId{ get; set; }
        public int Width{ get; set;}
        public int Height { get; set;}
        public string Location { get; set;}
        public int Order { get; set; }

        public Picture(int width,int height,string location,int order)
        {
            PictureId = Guid.NewGuid();
            Width = width;
            Height = height;
            Location = location;
            Order = order;
        }
        // public Guid ClassifiedAdId{ get; set; }
        // public ClassifiedAd ClassifiedAd { get; set; }

    }
}