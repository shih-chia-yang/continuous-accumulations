using System;
using System.Collections.Generic;
using marketplace.domain.kernel;

namespace marketplace.domain.entities
{
    public class PictureSize:ValueObject
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }

        internal PictureSize(){}
        public PictureSize(int width, int height)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Picture width must be a positive number");
            if (height < 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Picture width must be a positive number");
            Width = width;
            Height = height;
        }

        public static implicit operator string(PictureSize size) =>
            $"{size.Width},{size.Height}";

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Width;
            yield return Height;
        }
    }   
}