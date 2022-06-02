using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class AchievementAcquirement
    {
        public int ProfileId { get; set; }
        public int AchievementDataId { get; set; }
        public int ProgressPersantage { get; set; }

        public virtual AchievementData AchievementData { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
