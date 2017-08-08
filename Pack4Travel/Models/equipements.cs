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
        [Display(Name = "Nazwa listy")]
        [StringLength(30)]
        public string equipementName { get; set; }

        public int idGroup { get; set; }

        public string Id { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Prywatny Y/N")]
        public string privateStatus { get; set; }

        [Display(Name = "Œrednia ocena")]
        public decimal average
        {
            get
            {
                var sum = 0.0m + five_stars + four_stars + three_stars + two_stars + one_star;
                if (sum == 0)
                {
                    return 0;
                }
                var wages = 0.0m + five_stars * 5 + four_stars * 4 + three_stars * 3 + two_stars * 2 + one_star;
                return (wages / sum);
            }
        }

        [Display(Name = "5 gwiazdek")]
        public int five_stars { get; set; }

        [Display(Name = "4 gwiazki")]
        public int four_stars { get; set; }

        [Display(Name = "3 gwiazdki")]
        public int three_stars { get; set; }

        [Display(Name = "2 gwiazdki")]
        public int two_stars { get; set; }

        [Display(Name = "1 gwiazdka")]
        public int one_star { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<equipementTags> equipementTags { get; set; }

        //public virtual ApplicationUser Id{ get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<items> items { get; set; }
    }
}
