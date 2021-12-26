namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Type")]
    public partial class Type
    {
        [Key]
        public int id_type { get; set; }

        public int type_id { get; set; }

        public int animal_id { get; set; }

        public virtual Animals Animals { get; set; }

        public virtual Product_Type Product_Type { get; set; }
    }
}
