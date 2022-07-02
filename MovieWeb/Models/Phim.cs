namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Phim")]
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            DanhGiaPhims = new HashSet<DanhGiaPhim>();
            DienVienDongPhims = new HashSet<DienVienDongPhim>();
            DaoDiens = new HashSet<DaoDien>();
            HashTags = new HashSet<HashTag>();
            TheLoais = new HashSet<TheLoai>();
        }

        [Key]
        public int MaPhim { get; set; }

        [Required]
        [StringLength(100)]
        public string TenPhim { get; set; }

        public DateTime? NgaySanXuat { get; set; }

        public string NoiDungPhim { get; set; }

        public string NoiDungPhimTomTat { get; set; }

        public double? DiemIMDB { get; set; }

        public int? LuotXem { get; set; }

        public int? ThoiLuong { get; set; }

        [StringLength(200)]
        public string HinhBanner { get; set; }

        [StringLength(200)]
        public string LinkPhim { get; set; }

        [StringLength(200)]
        public string LinkPhimVip { get; set; }

        [StringLength(200)]
        public string TrailerPhim { get; set; }

        public int? MaNuoc { get; set; }

        public int? MaNgonNgu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaPhim> DanhGiaPhims { get; set; }

        public virtual DatNuoc DatNuoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DienVienDongPhim> DienVienDongPhims { get; set; }

        public virtual NgonNgu NgonNgu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DaoDien> DaoDiens { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HashTag> HashTags { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheLoai> TheLoais { get; set; }
        public List<DienVien> dienViens = new List<DienVien>();
        [Display(Name = "S? sao")]
        public virtual double? SoSaoTrungBinh
        {
            get
            {
                if (DanhGiaPhims.Count == 0)
                    return 0;
                else
                    return (int)Math.Round((double)DanhGiaPhims.Average(x => x.SoSao));
            }
        }
        [Display(Name = "S? l??t ? nh gi ")]
        public virtual int SoLuotDanhGia
        {
            get
            {
                return DanhGiaPhims.Count;
            }
        }
    }
}
