using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HistoryGenPin.Areas.Admin.Data.DatabaseContext
{
    public partial class GenPinContext : DbContext
    {
        public GenPinContext()
        {
        }

        public GenPinContext(DbContextOptions<GenPinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<ActivityHistory> ActivityHistory { get; set; }
        public virtual DbSet<CardHistory> CardHistory { get; set; }
        public virtual DbSet<ConnectAgent> ConnectAgent { get; set; }
        public virtual DbSet<Dobhistory> Dobhistory { get; set; }
        public virtual DbSet<EndCallHistory> EndCallHistory { get; set; }
        public virtual DbSet<ErrorLog> ErrorLog { get; set; }
        public virtual DbSet<GenPinHistory> GenPinHistory { get; set; }
        public virtual DbSet<Gttthistory> Gttthistory { get; set; }
        public virtual DbSet<HistoryIvr> HistoryIvr { get; set; }
        public virtual DbSet<Loan320210412> Loan320210412 { get; set; }
        public virtual DbSet<PhoneHistory> PhoneHistory { get; set; }
        public virtual DbSet<StatusResult> StatusResult { get; set; }
        public virtual DbSet<TblActionLogs> TblActionLogs { get; set; }
        public virtual DbSet<TblRoleModules> TblRoleModules { get; set; }
        public virtual DbSet<TblRoleUsers> TblRoleUsers { get; set; }
        public virtual DbSet<TblRoles> TblRoles { get; set; }
        public virtual DbSet<TblUsers> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-5LPGDMC\\SQLEXPRESS01;Database=GenPin;user=sa;password=123@123a;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ActivityHistory>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(256);

                entity.Property(e => e.Type)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CardHistory>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(256);
            });

            modelBuilder.Entity<ConnectAgent>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(256);
            });

            modelBuilder.Entity<Dobhistory>(entity =>
            {
                entity.ToTable("DOBHistory");

                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Note).HasMaxLength(256);
            });

            modelBuilder.Entity<EndCallHistory>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(256);
            });

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.Property(e => e.Api)
                    .HasColumnName("API")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ErrMessage).HasMaxLength(256);

                entity.Property(e => e.Parameter).HasMaxLength(128);
            });

            modelBuilder.Entity<GenPinHistory>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ExecutionChannel)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gttthistory>(entity =>
            {
                entity.ToTable("GTTTHistory");

                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Gttt)
                    .HasColumnName("GTTT")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Note)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<HistoryIvr>(entity =>
            {
                entity.HasKey(e => e.CallId);

                entity.ToTable("HistoryIVR");

                entity.Property(e => e.CallId)
                    .HasColumnName("CallID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CardNo)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Conditicon).HasMaxLength(64);

                entity.Property(e => e.ContractNumber)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName).HasMaxLength(128);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ExecutionChannel)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Executor).HasMaxLength(128);

                entity.Property(e => e.Gttt)
                    .HasColumnName("GTTT")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Loan320210412>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LOAN_3_20210412");

                entity.Property(e => e.ChrgCmp)
                    .HasColumnName("CHRG_CMP")
                    .HasColumnType("numeric(25, 4)");

                entity.Property(e => e.IntCmp)
                    .HasColumnName("INT_CMP")
                    .HasColumnType("numeric(25, 4)");

                entity.Property(e => e.LoanAccount)
                    .IsRequired()
                    .HasColumnName("LOAN_ACCOUNT")
                    .HasMaxLength(16);

                entity.Property(e => e.PrinCmp)
                    .HasColumnName("PRIN_CMP")
                    .HasColumnType("numeric(25, 4)");

                entity.Property(e => e.RepShdlDate).HasColumnName("REP_SHDL_DATE");

                entity.Property(e => e.TotalFlow)
                    .HasColumnName("TOTAL_FLOW")
                    .HasColumnType("numeric(25, 4)");
            });

            modelBuilder.Entity<PhoneHistory>(entity =>
            {
                entity.Property(e => e.CallSession)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusResult>(entity =>
            {
                entity.Property(e => e.StatusName).HasMaxLength(128);
            });

            modelBuilder.Entity<TblActionLogs>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblActionLogs");

                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Tên hành động");

                entity.Property(e => e.ActionType)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasComment("Loại hành động(Audo/Manual)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasComment("Biến Guid");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Tên user");
            });

            modelBuilder.Entity<TblRoleModules>(entity =>
            {
                entity.ToTable("tblRoleModules");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblRoleUsers>(entity =>
            {
                entity.ToTable("tblRoleUsers");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblRoles>(entity =>
            {
                entity.ToTable("tblRoles");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Ngày tạo");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasComment("Mô tả");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian chỉnh sửa");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(150)
                    .HasComment("Tên quyền");
            });

            modelBuilder.Entity<TblUsers>(entity =>
            {
                entity.ToTable("tblUsers");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasComment("Người tạo");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian tạo");

                entity.Property(e => e.IsDelete)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Trạng thái xóa tài khoản");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Thời gian sửa");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasComment("mật khẩu mã hóa MD5");

                entity.Property(e => e.Status).HasComment("Trạng thái hoạt động của tài khoản");

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasComment("Tên user đăng nhập");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
