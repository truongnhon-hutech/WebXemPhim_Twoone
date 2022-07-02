namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhGiaPhim")]
    public partial class DanhGiaPhim
    {
        [Key]
        public int MaDanhGia { get; set; }

        public int MaPhim { get; set; }

        public int MaNguoiDung { get; set; }

        public DateTime? ThoiGianBinhLuan { get; set; }

        public string NoiDungBinhLuan { get; set; }

        public int? SoSao { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }

        public virtual Phim Phim { get; set; }
    }
}
