﻿// <auto-generated />
using System;
using API_DSCS2_WEBBANGIAY.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API_DSCS2_WEBBANGIAY.Migrations
{
    [DbContext(typeof(ShoesEcommereContext))]
    [Migration("20220610091232_init_db_v2")]
    partial class init_db_v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.BoSuuTap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Slug")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .HasColumnName("slug")
                        .IsFixedLength(true);

                    b.Property<string>("TenBoSuuTap")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("Id");

                    b.ToTable("BoSuuTap");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ChiTietHoaDon", b =>
                {
                    b.Property<int>("IdHoaDon")
                        .HasColumnType("int")
                        .HasColumnName("_id_HoaDon");

                    b.Property<string>("MasanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int?>("DonGia")
                        .HasColumnType("int");

                    b.Property<int?>("Soluong")
                        .HasColumnType("int")
                        .HasColumnName("soluong");

                    b.HasKey("IdHoaDon", "MasanPham")
                        .HasName("pk_CTHD");

                    b.HasIndex("MasanPham");

                    b.ToTable("ChiTietHoaDon");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ChiTietSale", b =>
                {
                    b.Property<int>("IdSale")
                        .HasColumnType("int")
                        .HasColumnName("_id_sale");

                    b.Property<string>("MaSanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int?>("Giamgia")
                        .HasColumnType("int")
                        .HasColumnName("giamgia");

                    b.HasKey("IdSale", "MaSanPham")
                        .HasName("pk_cts");

                    b.HasIndex("MaSanPham");

                    b.ToTable("ChiTietSale");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.DanhMuc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .HasColumnName("slug")
                        .IsFixedLength(true);

                    b.Property<string>("TenDanhMuc")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HoaDon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("Giamgia")
                        .HasColumnType("money")
                        .HasColumnName("giamgia");

                    b.Property<int?>("IdKh")
                        .HasColumnType("int")
                        .HasColumnName("_id_KH");

                    b.Property<decimal>("Phiship")
                        .HasColumnType("decimal(18,0)")
                        .HasColumnName("phiship");

                    b.Property<string>("TenTaiKhoan")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .IsFixedLength(true);

                    b.Property<decimal>("Thanhtien")
                        .HasColumnType("money")
                        .HasColumnName("thanhtien");

                    b.Property<decimal?>("TienNhan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal?>("TienThoiLai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("TenTaiKhoan");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.KhachHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .IsFixedLength(true);

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .IsFixedLength(true);

                    b.Property<bool?>("Gioitinh")
                        .HasColumnType("bit")
                        .HasColumnName("gioitinh");

                    b.Property<DateTime?>("Ngaysinh")
                        .HasColumnType("date")
                        .HasColumnName("ngaysinh");

                    b.Property<string>("Sdt")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<string>("TenKhachHang")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .IsFixedLength(true);

                    b.HasKey("Id");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.MauSac", b =>
                {
                    b.Property<string>("MaMau")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .IsFixedLength(true);

                    b.Property<string>("TenMau")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("MaMau")
                        .HasName("pk_mausac");

                    b.ToTable("MauSac");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.MauSacDetail", b =>
                {
                    b.Property<string>("MaSanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<string>("MaMauSac")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .IsFixedLength(true);

                    b.HasKey("MaSanPham", "MaMauSac")
                        .HasName("pk_MauSac_Detail");

                    b.HasIndex("MaMauSac");

                    b.ToTable("MauSac_Detail");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ReviewStar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Avr")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("float")
                        .HasColumnName("avr")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("BaSao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ba_sao")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("BonSao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("bon_sao")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("HaiSao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("hai_sao")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("MasanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int?>("MotSao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("mot_sao")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("NamSao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("nam_sao")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.HasIndex("MasanPham");

                    b.ToTable("reviewStar");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Sale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Dsc")
                        .HasColumnType("ntext");

                    b.Property<DateTime?>("NgayBatDat")
                        .HasColumnType("date");

                    b.Property<DateTime?>("NgayKetThuc")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SanPham", b =>
                {
                    b.Property<string>("MaSanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdBst")
                        .HasColumnType("int")
                        .HasColumnName("_id_BST");

                    b.Property<int?>("IdDm")
                        .HasColumnType("int")
                        .HasColumnName("_id_DM");

                    b.Property<string>("Img")
                        .HasColumnType("text")
                        .HasColumnName("img");

                    b.Property<string>("Slug")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .HasColumnName("slug")
                        .IsFixedLength(true);

                    b.Property<int>("SoLuongNhap")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuongTon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MaSanPham")
                        .HasName("pk_sanpham");

                    b.HasIndex("IdBst");

                    b.HasIndex("IdDm");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Size1")
                        .HasColumnType("int")
                        .HasColumnName("size");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SizeDetail", b =>
                {
                    b.Property<string>("MaSanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int>("IdSizes")
                        .HasColumnType("int")
                        .HasColumnName("_id_sizes");

                    b.HasKey("MaSanPham", "IdSizes")
                        .HasName("pk_size_detail");

                    b.HasIndex("IdSizes");

                    b.ToTable("Size_Detail");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", b =>
                {
                    b.Property<string>("TenTaiKhoan")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .IsFixedLength(true);

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("char(30)")
                        .IsFixedLength(true);

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IdKh")
                        .HasColumnType("int")
                        .HasColumnName("_id_KH");

                    b.Property<string>("MatKhau")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("char(15)")
                        .IsFixedLength(true);

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int?>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("TenTaiKhoan")
                        .HasName("pk_TK");

                    b.HasIndex("IdKh");

                    b.ToTable("TaiKhoan");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ChiTietHoaDon", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.HoaDon", "IdHoaDonNavigation")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("IdHoaDon")
                        .HasConstraintName("fk_ChiTietHoaDon_HD")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MasanPhamNavigation")
                        .WithMany("ChiTietHoaDons")
                        .HasForeignKey("MasanPham")
                        .HasConstraintName("fk_ChiTietHoaDon_KH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdHoaDonNavigation");

                    b.Navigation("MasanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ChiTietSale", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.Sale", "IdSaleNavigation")
                        .WithMany("ChiTietSales")
                        .HasForeignKey("IdSale")
                        .HasConstraintName("fk_cts_Sale")
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MaSanPhamNavigation")
                        .WithMany("ChiTietSales")
                        .HasForeignKey("MaSanPham")
                        .HasConstraintName("fk_cts_SP")
                        .IsRequired();

                    b.Navigation("IdSaleNavigation");

                    b.Navigation("MaSanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HoaDon", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", "TenTaiKhoanNavigation")
                        .WithMany("HoaDons")
                        .HasForeignKey("TenTaiKhoan")
                        .HasConstraintName("fk_HD_TK");

                    b.Navigation("TenTaiKhoanNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.MauSacDetail", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.MauSac", "MaMauSacNavigation")
                        .WithMany("MauSacDetails")
                        .HasForeignKey("MaMauSac")
                        .HasConstraintName("fk_detail_mausac_sizes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MaSanPhamNavigation")
                        .WithMany("MauSacDetails")
                        .HasForeignKey("MaSanPham")
                        .HasConstraintName("fk_detail_mausac_sanpham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaMauSacNavigation");

                    b.Navigation("MaSanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.ReviewStar", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MasanPhamNavigation")
                        .WithMany("ReviewStars")
                        .HasForeignKey("MasanPham")
                        .HasConstraintName("fk_reviewStar_sanpham")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("MasanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SanPham", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.BoSuuTap", "IdBstNavigation")
                        .WithMany("SanPhams")
                        .HasForeignKey("IdBst")
                        .HasConstraintName("fk_sanpham_BST")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.DanhMuc", "IdDmNavigation")
                        .WithMany("SanPhams")
                        .HasForeignKey("IdDm")
                        .HasConstraintName("fk_sanpham_danhmuc")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IdBstNavigation");

                    b.Navigation("IdDmNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SizeDetail", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.Size", "IdSizesNavigation")
                        .WithMany("SizeDetails")
                        .HasForeignKey("IdSizes")
                        .HasConstraintName("fk_detail_sanpham_sizes")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MaSanPhamNavigation")
                        .WithMany("SizeDetails")
                        .HasForeignKey("MaSanPham")
                        .HasConstraintName("fk_detail_sanpham_sanpham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdSizesNavigation");

                    b.Navigation("MaSanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.KhachHang", "IdKhNavigation")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("IdKh")
                        .HasConstraintName("fk_TaiKhoan_KH")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("IdKhNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.BoSuuTap", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.DanhMuc", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HoaDon", b =>
                {
                    b.Navigation("ChiTietHoaDons");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.KhachHang", b =>
                {
                    b.Navigation("TaiKhoans");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.MauSac", b =>
                {
                    b.Navigation("MauSacDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Sale", b =>
                {
                    b.Navigation("ChiTietSales");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("ChiTietSales");

                    b.Navigation("MauSacDetails");

                    b.Navigation("ReviewStars");

                    b.Navigation("SizeDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Size", b =>
                {
                    b.Navigation("SizeDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", b =>
                {
                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}