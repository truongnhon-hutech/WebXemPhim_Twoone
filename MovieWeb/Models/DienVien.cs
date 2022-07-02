namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DienVien")]
    public partial class DienVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DienVien()
        {
            DienVienDongPhims = new HashSet<DienVienDongPhim>();
        }

        [Key]
        public int MaDienVien { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDienVien { get; set; }

        [StringLength(200)]
        public string HinhAnhDienVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DienVienDongPhim> DienVienDongPhims { get; set; }
        public List<Phim> phims = new List<Phim>();

    }
}
