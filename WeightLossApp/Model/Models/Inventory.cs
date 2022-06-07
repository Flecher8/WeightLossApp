using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Inventory
    {
        public Inventory()
        {
            AvailableDesignTheme = new HashSet<AvailableDesignTheme>();
            Profile = new HashSet<Profile>();
        }

        public int Id { get; set; }
        public int? PremiumStatusId { get; set; }
        public int? CurrentDesignThemeId { get; set; }
        public string ImageName { get; set; }

        public virtual DesignThemeData CurrentDesignTheme { get; set; }
        public virtual PremiumStatus PremiumStatus { get; set; }
        public virtual ICollection<AvailableDesignTheme> AvailableDesignTheme { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
