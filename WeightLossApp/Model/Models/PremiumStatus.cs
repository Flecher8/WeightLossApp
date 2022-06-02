using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class PremiumStatus
    {
        public PremiumStatus()
        {
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }
        public int? PremiumSubscriptionId { get; set; }

        public virtual PremiumSubscription PremiumSubscription { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
