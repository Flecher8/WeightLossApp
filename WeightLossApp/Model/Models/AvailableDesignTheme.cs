using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class AvailableDesignTheme
    {
        public int InventoryId { get; set; }
        public int DesignThemeId { get; set; }

        public virtual DesignThemeData DesignTheme { get; set; }
        public virtual Inventory Inventory { get; set; }
    }
}
