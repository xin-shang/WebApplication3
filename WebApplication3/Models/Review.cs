using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int CustomerId { get; set; }
        public DateTime ReviewDate { get; set; }
        public int? ItemId { get; set; }
        public int Rating { get; set; }
        public string ReviewDescription { get; set; } = null!;

        public virtual Customer Customer { get; set; } = null!;
        public virtual Item? Item { get; set; }
    }
}
