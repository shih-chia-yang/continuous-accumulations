using System.Collections;
using System;
using System.Collections.Generic;

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

        public List<Picture> Pictures { get; set; }

    }
}