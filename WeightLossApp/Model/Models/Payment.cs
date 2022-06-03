using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string CreditCard { get; set; }
        public decimal Sum { get; set; }
        public string PaymentPurpose { get; set; }
        public string BoughtItem { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
