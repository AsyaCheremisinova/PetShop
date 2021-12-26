namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order_line
    {
        [Key]
        public int order_line_id { get; set; }

        public decimal order_line_cost { get; set; }

        public int inventory_number { get; set; }

        public int order_id { get; set; }

        public int number { get; set; }
        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
