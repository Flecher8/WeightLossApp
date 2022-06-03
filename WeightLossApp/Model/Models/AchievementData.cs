using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class AchievementData
    {
        public AchievementData()
        {
            AchievementAcquirement = new HashSet<AchievementAcquirement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardExperience { get; set; }
        public string ImgName { get; set; }

        public virtual ICollection<AchievementAcquirement> AchievementAcquirement { get; set; }
    }
}
