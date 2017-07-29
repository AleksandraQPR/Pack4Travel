namespace Pack4Travel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class items
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public items()
        {
            equipements = new HashSet<equipements>();
        }

        [Key]
        public int idItem { get; set; }

        [Required]
        [StringLength(30)]
        public string itemName { get; set; }

        public int idGroup { get; set; }

        public string Id { get; set; }

        [Required]
        [StringLength(1)]
        public string privateStatus { get; set; }

        public virtual itemGroup itemGroup { get; set; }

        //public virtual ApplicationUser Id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipements> equipements { get; set; }
    }
}
