using System;

namespace marketplace.domain.events
{
    public class PictureAdded
    {
        public Guid ClassifiedAdId { get; set; }
        public Guid PictureId { get; set; }
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public PictureAdded(Guid classifiedAdId,Guid pictureId,string url,int height,int width)
        {
            ClassifiedAdId = classifiedAdId;
            PictureId = pictureId;
            Url = url;
            Height = height;
            Width = width;
        }
    }
}