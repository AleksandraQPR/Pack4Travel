namespace Pack4Travel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class equipements
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public equipements()
        {
            equipementTags = new HashSet<equipementTags>();
            items = new HashSet<items>();
        }

        [Key]
        public int idEquipement { get; set; }

        [Required]
        [StringLength(30)]
        public string equipementName { get; set; }

        public int idGroup { get; set; }

        public int? idOwner { get; set; }

        [Required]
        [StringLength(1)]
        public string privateStatus { get; set; }

        public int five_stars { get; set; }

        public int four_stars { get; set; }

        public int three_stars { get; set; }

        public int two_stars { get; set; }

        public int one_star { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipementTags> equipementTags { get; set; }

        public virtual userInfo userInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<items> items { get; set; }
    }
}
