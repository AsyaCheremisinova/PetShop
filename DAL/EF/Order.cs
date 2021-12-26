namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Order_line = new HashSet<Order_line>();
        }

        [Key]
        public int order_id { get; set; }

        public decimal total_cost { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public int customer_id { get; set; }

        public int? pick_up_point_id { get; set; }

        public int? status_id { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_line> Order_line { get; set; }

        public virtual Puck_Up_Point Puck_Up_Point { get; set; }

        public virtual Status Status { get; set; }
    }
}
