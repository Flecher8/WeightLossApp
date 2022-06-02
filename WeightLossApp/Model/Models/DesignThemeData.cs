using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class DesignThemeData
    {
        public DesignThemeData()
        {
            AvailableDesignTheme = new HashSet<AvailableDesignTheme>();
            Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public byte[] IconImage { get; set; }
        public string BaseColor { get; set; }
        public string AccentColor { get; set; }
        public string SecondaryColor { get; set; }

        public virtual ICollection<AvailableDesignTheme> AvailableDesignTheme { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
