﻿using System;
using System.Collections.Generic;

namespace WebApplication3.Models
{
    public partial class ItemMarkupHistory
    {
        public int ItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Markup { get; set; }
        public bool Sale { get; set; }

        public virtual Item Item { get; set; } = null!;
    }
}
