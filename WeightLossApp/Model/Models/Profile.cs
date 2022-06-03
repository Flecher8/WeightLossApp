using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Profile
    {
        public Profile()
        {
            AchievementAcquirement = new HashSet<AchievementAcquirement>();
            Payment = new HashSet<Payment>();
            ProfileSetting = new HashSet<ProfileSetting>();
            Schedule = new HashSet<Schedule>();
            UserMedicine = new HashSet<UserMedicine>();
        }

        public int Id { get; set; }
        public long Exp { get; set; }
        public int MemberId { get; set; }
        public int InventoryId { get; set; }
        public string Settings { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Member Member { get; set; }
        public virtual ICollection<AchievementAcquirement> AchievementAcquirement { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<ProfileSetting> ProfileSetting { get; set; }
        public virtual ICollection<Schedule> Schedule { get; set; }
        public virtual ICollection<UserMedicine> UserMedicine { get; set; }
    }
}
