namespace MovieWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int MaAdmin { get; set; }

        [Required]
        [StringLength(50)]
        public string TaiKhoanAdmin { get; set; }

        [Required]
        [StringLength(50)]
        public string MatKhauAdmin { get; set; }

        [StringLength(50)]
        public string TenAdmin { get; set; }

        [StringLength(100)]
        public string EmailAdmin { get; set; }

        public DateTime? NgayKhoiTao { get; set; }
    }
}
