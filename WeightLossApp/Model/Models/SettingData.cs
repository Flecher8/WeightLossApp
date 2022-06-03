using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class SettingData
    {
        public SettingData()
        {
            ProfileSetting = new HashSet<ProfileSetting>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProfileSetting> ProfileSetting { get; set; }
    }
}
