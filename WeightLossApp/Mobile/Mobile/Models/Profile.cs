using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public long Exp { get; set; }
        public int MemberId { get; set; }
        public int InventoryId { get; set; }
        public string Settings { get; set; }
    }
}
