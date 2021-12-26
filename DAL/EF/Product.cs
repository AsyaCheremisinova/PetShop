namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_line = new HashSet<Order_line>();
            Shopping_Basket = new HashSet<Shopping_Basket>();
        }

        [Key]
        public int inventory_number { get; set; }

        [Required]
        [StringLength(250)]
        public string product_name { get; set; }

        [StringLength(1000)]
        public string description { get; set; }

        public bool availability { get; set; }

        public decimal cost { get; set; }

        public decimal? weight { get; set; }

        public int type_id { get; set; }

        public int animal_id { get; set; }

        public int product_quantity { get; set; }

        [StringLength(250)]
        public string picture { get; set; }

        public virtual Animals Animals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_line> Order_line { get; set; }

        public virtual Product_Type Product_Type { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shopping_Basket> Shopping_Basket { get; set; }
    }
}
