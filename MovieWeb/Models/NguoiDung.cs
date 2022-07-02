namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiDung")]
    public partial class NguoiDung
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiDung()
        {
            DanhGiaPhims = new HashSet<DanhGiaPhim>();
            PhieuDangKies = new HashSet<PhieuDangKy>();
        }

        [Key]
        public int MaNguoiDung { get; set; }
        public List<Phim> DaXem = new List<Phim>();
        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(1)]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string SDT { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string TaiKhoan { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhau { get; set; }

        [StringLength(200)]
        public string HinhAnhNguoiDung { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhGiaPhim> DanhGiaPhims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuDangKy> PhieuDangKies { get; set; }
    }
}
