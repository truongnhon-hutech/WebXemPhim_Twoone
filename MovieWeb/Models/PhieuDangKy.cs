namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuDangKy")]
    public partial class PhieuDangKy
    {
        [Key]
        public int MaPhieuDangKy { get; set; }

        public int? MaGoiDV { get; set; }

        public int? MaNguoiDung { get; set; }

        public DateTime NgayThanhToan { get; set; }

        public DateTime NgayHetHan { get; set; }

        public double? KhuyenMai { get; set; }

        [Column(TypeName = "money")]
        public decimal? ThanhTien { get; set; }

        public virtual GoiDichVu GoiDichVu { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
