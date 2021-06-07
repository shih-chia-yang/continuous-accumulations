using System;

namespace marketplace.domain.events.ClassifiedAdEvents
{
    public class PictureResized
    {
        public Guid PictureId { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public PictureResized(Guid id,int height,int width)
        {
            PictureId = id;
            Height = height;
            Width = width;
        }
    }
}