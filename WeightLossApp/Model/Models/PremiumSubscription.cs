using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class PremiumSubscription
    {
        public PremiumSubscription()
        {
            PremiumStatus = new HashSet<PremiumStatus>();
        }

        public int Id { get; set; }
        public int? NumberOfDays { get; set; }

        public virtual ICollection<PremiumStatus> PremiumStatus { get; set; }
    }
}
