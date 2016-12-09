//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Photography.DatabaseFirst
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lens
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lens()
        {
            this.Equipments = new HashSet<Equipment>();
        }
    
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
