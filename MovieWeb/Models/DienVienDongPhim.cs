namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DienVienDongPhim")]
    public partial class DienVienDongPhim
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhim { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDienVien { get; set; }

        [StringLength(50)]
        public string TenVaiDien { get; set; }

        public virtual DienVien DienVien { get; set; }

        public virtual Phim Phim { get; set; }
    }
}
