namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoiDichVu")]
    public partial class GoiDichVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoiDichVu()
        {
            PhieuDangKies = new HashSet<PhieuDangKy>();
        }

        [Key]
        public int MaGoiDV { get; set; }

        [StringLength(250)]
        public string TenGoiDV { get; set; }

        [Column(TypeName = "money")]
        public decimal? GiaGoiDV { get; set; }

        public string MoTaGoiDichVu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDangKy> PhieuDangKies { get; set; }
    }
}
