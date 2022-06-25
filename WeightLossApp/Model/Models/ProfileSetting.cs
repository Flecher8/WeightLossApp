using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class ProfileSetting
    {
        public int SettingId { get; set; }
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual SettingData Setting { get; set; }
    }
}
