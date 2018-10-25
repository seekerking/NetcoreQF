using Microsoft.EntityFrameworkCore;

namespace DBContextEF.Models
{
    /// <summary>
    /// 1:初始化命令:Scaffold-DbContext       "Data Source=192.168.1.110,1478;Initial Catalog=WxUtilDB;User Id=sa;Password=231oxd6RFeOLSFvjAkNb;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    /// tips:-Tables 可以导出制定表例如:-Tables ConfigDic,Lconfig
    /// EFCore.BulkExtensions如果是sqlserver可以加载这个拓展
    /// </summary>
    public partial class WxUtilDBContext : DbContext
    {
        public virtual DbSet<ConfigDic> ConfigDic { get; set; }
        public virtual DbSet<Holidays> Holidays { get; set; }
        public virtual DbSet<Lconfig> Lconfig { get; set; }
        public virtual DbSet<LotteryDraw> LotteryDraw { get; set; }
        public virtual DbSet<WxUserMessageSendLogTb> WxUserMessageSendLogTb { get; set; }
        public virtual DbSet<WxUserMessageTb> WxUserMessageTb { get; set; }

        // Unable to generate entity type for table 'dbo.UserTB'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=192.168.1.110,1478;Initial Catalog=WxUtilDB;User Id=sa;Password=231oxd6RFeOLSFvjAkNb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigDic>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Type })
                    .HasName("NonClusteredIndex-20181012-103426")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Holidays>(entity =>
            {
                entity.Property(e => e.DayKey)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(125)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lconfig>(entity =>
            {
                entity.ToTable("LConfig");
            });

            modelBuilder.Entity<LotteryDraw>(entity =>
            {
                entity.HasIndex(e => new { e.Token, e.Status })
                    .HasName("NonClusteredIndex-20181019-171754")
                    .IsUnique();

                entity.Property(e => e.ContactInfo)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WxUserMessageSendLogTb>(entity =>
            {
                entity.ToTable("Wx_UserMessageSendLogTB");

                entity.HasIndex(e => new { e.WxOpenId, e.Status, e.SendDate })
                    .HasName("NonClusteredIndex-20180921-163645");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.WxOpenId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WxUserMessageTb>(entity =>
            {
                entity.ToTable("Wx_UserMessageTB");

                entity.HasIndex(e => new { e.Status, e.WxOpenId, e.WxToken, e.ExpireTime })
                    .HasName("NonClusteredIndex-20180906-181230");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.ExpireTime).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.Property(e => e.WxOpenId)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.WxToken)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);
            });
        }
    }
}
