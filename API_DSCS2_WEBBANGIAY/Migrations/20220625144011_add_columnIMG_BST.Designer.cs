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
    [Migration("20220625144011_add_columnIMG_BST")]
    partial class add_columnIMG_BST
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

                    b.Property<string>("Img")
                        .HasColumnType("text");

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

                    b.Property<decimal?>("DonGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Soluong")
                        .HasColumnType("int")
                        .HasColumnName("soluong");

                    b.HasKey("IdHoaDon", "MasanPham")
                        .HasName("pk_CTHD");

                    b.HasIndex(new[] { "MasanPham" }, "IX_ChiTietHoaDon_MasanPham");

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

                    b.HasIndex(new[] { "MaSanPham" }, "IX_ChiTietSale_MaSanPham");

                    b.ToTable("ChiTietSale");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.DanhMuc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GioiTinhCode")
                        .HasColumnType("int")
                        .HasColumnName("GioiTinh_Code");

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

                    b.HasIndex(new[] { "GioiTinhCode" }, "IX_DanhMuc_GioiTinh_Code");

                    b.ToTable("DanhMuc");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.GioiTinh", b =>
                {
                    b.Property<int>("GioitinhCode")
                        .HasColumnType("int")
                        .HasColumnName("gioitinh_code");

                    b.Property<string>("GioitinhText")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("gioitinh_text");

                    b.HasKey("GioitinhCode")
                        .HasName("pk_gioitinh");

                    b.ToTable("GioiTinh");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HinhAnh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .HasColumnType("char(255)")
                        .HasColumnName("file_name");

                    b.HasKey("Id");

                    b.ToTable("HinhAnh");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HinhAnhSanPham", b =>
                {
                    b.Property<string>("MaSanPham")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

                    b.Property<int>("IdHinhAnh")
                        .HasColumnType("int")
                        .HasColumnName("_id_HinhAnh");

                    b.HasKey("MaSanPham", "IdHinhAnh")
                        .HasName("pk_hinhanh_sanpham");

                    b.HasIndex(new[] { "IdHinhAnh" }, "IX_HinhAnh_SanPham__id_HinhAnh");

                    b.ToTable("HinhAnh_SanPham");
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

                    b.HasIndex(new[] { "TenTaiKhoan" }, "IX_HoaDon_TenTaiKhoan");

                    b.ToTable("HoaDon");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.KhachHang", b =>
                {
                    b.Property<string>("Sdt")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .IsFixedLength(true);

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

                    b.Property<int?>("GiamGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("Gioitinh")
                        .HasColumnType("bit")
                        .HasColumnName("gioitinh");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("_id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Ngaysinh")
                        .HasColumnType("date")
                        .HasColumnName("ngaysinh");

                    b.Property<string>("TenKhachHang")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal?>("TienThanhToan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Sdt")
                        .HasName("pk_KH");

                    b.ToTable("KhachHang");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.MauSac", b =>
                {
                    b.Property<string>("MaMau")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("char(20)")
                        .HasDefaultValueSql("('')")
                        .IsFixedLength(true);

                    b.Property<string>("TenMau")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValueSql("(N'')");

                    b.HasKey("MaMau")
                        .HasName("pk_mausac");

                    b.ToTable("MauSac");
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

                    b.HasIndex(new[] { "MasanPham" }, "IX_reviewStar_MasanPham");

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

                    b.Property<int>("Giamgia")
                        .HasColumnType("int");

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

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("money");

                    b.Property<decimal>("GiaDaGiam")
                        .HasColumnType("money");

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

                    b.Property<string>("Mota")
                        .HasColumnType("ntext");

                    b.Property<string>("Slug")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("char(50)")
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("MaSanPham")
                        .HasName("pk_sanpham");

                    b.HasIndex(new[] { "IdBst" }, "IX_SanPham__id_BST");

                    b.HasIndex(new[] { "IdDm" }, "IX_SanPham__id_DM");

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

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SoLuongDetails", b =>
                {
                    b.Property<string>("maMau")
                        .HasColumnType("char(20)");

                    b.Property<string>("maSanPham")
                        .HasColumnType("char(10)");

                    b.Property<int>("_idSize")
                        .HasColumnType("int");

                    b.Property<int?>("Soluong")
                        .HasColumnType("int");

                    b.Property<int>("_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("maMau", "maSanPham", "_idSize");

                    b.HasIndex("_idSize");

                    b.HasIndex("maSanPham");

                    b.ToTable("SoLuongDetails");
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

                    b.Property<string>("MatKhau")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("char(15)")
                        .IsFixedLength(true);

                    b.Property<int?>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("SdtKh")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("char(10)")
                        .HasColumnName("SdtKH")
                        .IsFixedLength(true);

                    b.HasKey("TenTaiKhoan")
                        .HasName("pk_TK");

                    b.HasIndex("SdtKh");

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MaSanPhamNavigation")
                        .WithMany("ChiTietSales")
                        .HasForeignKey("MaSanPham")
                        .HasConstraintName("fk_cts_SP")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdSaleNavigation");

                    b.Navigation("MaSanPhamNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.DanhMuc", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.GioiTinh", "GioiTinhCodeNavigation")
                        .WithMany("DanhMucs")
                        .HasForeignKey("GioiTinhCode")
                        .HasConstraintName("fk_DM_GT")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("GioiTinhCodeNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HinhAnhSanPham", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.HinhAnh", "IdHinhAnhNavigation")
                        .WithMany("HinhAnhSanPhams")
                        .HasForeignKey("IdHinhAnh")
                        .HasConstraintName("fk_cthd_hinhanh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "MaSanPhamNavigation")
                        .WithMany("HinhAnhSanPhams")
                        .HasForeignKey("MaSanPham")
                        .HasConstraintName("fk_cthd_sanpham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdHinhAnhNavigation");

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

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SoLuongDetails", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.Size", "IdSizeNavigation")
                        .WithMany("SoLuongDetails")
                        .HasForeignKey("_idSize")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.MauSac", "IdMauSacNavigation")
                        .WithMany("SoLuongDetails")
                        .HasForeignKey("maMau")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.SanPham", "IdSanPhamNavigation")
                        .WithMany("SoLuongDetails")
                        .HasForeignKey("maSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdMauSacNavigation");

                    b.Navigation("IdSanPhamNavigation");

                    b.Navigation("IdSizeNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", b =>
                {
                    b.HasOne("API_DSCS2_WEBBANGIAY.Models.KhachHang", "SdtKhNavigation")
                        .WithMany("TaiKhoans")
                        .HasForeignKey("SdtKh")
                        .HasConstraintName("fk_TaiKhoan_KH");

                    b.Navigation("SdtKhNavigation");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.BoSuuTap", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.DanhMuc", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.GioiTinh", b =>
                {
                    b.Navigation("DanhMucs");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.HinhAnh", b =>
                {
                    b.Navigation("HinhAnhSanPhams");
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
                    b.Navigation("SoLuongDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Sale", b =>
                {
                    b.Navigation("ChiTietSales");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.SanPham", b =>
                {
                    b.Navigation("ChiTietHoaDons");

                    b.Navigation("ChiTietSales");

                    b.Navigation("HinhAnhSanPhams");

                    b.Navigation("ReviewStars");

                    b.Navigation("SoLuongDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.Size", b =>
                {
                    b.Navigation("SoLuongDetails");
                });

            modelBuilder.Entity("API_DSCS2_WEBBANGIAY.Models.TaiKhoan", b =>
                {
                    b.Navigation("HoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
