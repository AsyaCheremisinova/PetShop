namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.ComponentModel;

    public partial class Shopping_Basket : INotifyPropertyChanged
    {
        [Key]
        public int id_basket
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged("id_basket");
            }
        }
        private int id;

        public int inventory_number { get; set; }

        public int customer_id { get; set; }

        public int number { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
