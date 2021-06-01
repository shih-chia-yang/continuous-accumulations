using System.Collections;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efcore.RelatedData
{
    public class ClassifiedAd
    {
        public Guid ClassifiedAdId{ get; set; }

        public Guid OwnerId{ get; set; }

        public string Title{ get; set; }

        public string Text { get; set; }

        public string CurrencyCode { get; set; }

        public bool InUse { get; set; }

        public int DecimalPlace { get; set; }

        public decimal Amount{ get; set; }

        public int State { get; set; }

        public Guid ApproveBy { get; set; }

        private List<Picture> _picture;

        public IEnumerable<Picture> Pictures => _picture.AsReadOnly();

        protected ClassifiedAd()
        {
            _picture = new List<Picture>();
        }

        public ClassifiedAd(Guid ownerId,string title):this()
        {
            ClassifiedAdId = Guid.NewGuid();
            OwnerId = ownerId;
            Title = title;
        }
        public void AddPicture(Picture pic)
        {
            _picture.Add(pic);
        }

    }
}