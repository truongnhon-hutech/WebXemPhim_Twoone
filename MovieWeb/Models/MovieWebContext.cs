using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MovieWeb.Models
{
    public partial class MovieWebContext : DbContext
    {
        public MovieWebContext()
            : base("name=MovieWebContext")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<DanhGiaPhim> DanhGiaPhims { get; set; }
        public virtual DbSet<DaoDien> DaoDiens { get; set; }
        public virtual DbSet<DatNuoc> DatNuocs { get; set; }
        public virtual DbSet<DienVien> DienViens { get; set; }
        public virtual DbSet<DienVienDongPhim> DienVienDongPhims { get; set; }
        public virtual DbSet<GoiDichVu> GoiDichVus { get; set; }
        public virtual DbSet<HashTag> HashTags { get; set; }
        public virtual DbSet<NgonNgu> NgonNgus { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<PhieuDangKy> PhieuDangKies { get; set; }
        public virtual DbSet<Phim> Phims { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.TaiKhoanAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.MatKhauAdmin)
                .IsUnicode(false);

            modelBuilder.Entity<DaoDien>()
                .HasMany(e => e.Phims)
                .WithMany(e => e.DaoDiens)
                .Map(m => m.ToTable("DaoDienCuaPhim").MapLeftKey("MaDaoDien").MapRightKey("MaPhim"));

            modelBuilder.Entity<DienVien>()
                .Property(e => e.HinhAnhDienVien)
                .IsUnicode(false);

            modelBuilder.Entity<GoiDichVu>()
                .Property(e => e.GiaGoiDV)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HashTag>()
                .HasMany(e => e.Phims)
                .WithMany(e => e.HashTags)
                .Map(m => m.ToTable("HashTagCuaPhim").MapLeftKey("MaHashTag").MapRightKey("MaPhim"));

            modelBuilder.Entity<NgonNgu>()
                .HasMany(e => e.Phims)
                .WithOptional(e => e.NgonNgu)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.GioiTinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.TaiKhoan)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.HinhAnhNguoiDung)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.PhieuDangKies)
                .WithOptional(e => e.NguoiDung)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhieuDangKy>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Phim>()
                .Property(e => e.HinhBanner)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.LinkPhim)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.LinkPhimVip)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .Property(e => e.TrailerPhim)
                .IsUnicode(false);

            modelBuilder.Entity<Phim>()
                .HasMany(e => e.DanhGiaPhims)
                .WithRequired(e => e.Phim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phim>()
                .HasMany(e => e.DienVienDongPhims)
                .WithRequired(e => e.Phim)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Phim>()
                .HasMany(e => e.TheLoais)
                .WithMany(e => e.Phims)
                .Map(m => m.ToTable("TheLoaiCuaPhim").MapLeftKey("MaPhim").MapRightKey("MaTheLoai"));
        }
    }
}
