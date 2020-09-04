using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LLWP_Core.Models
{
    public partial class dbLLWPContext : DbContext
    {
        public dbLLWPContext()
        {
        }

        public dbLLWPContext(DbContextOptions<dbLLWPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TActivityJoindata> TActivityJoindata { get; set; }
        public virtual DbSet<TActivitydata> TActivitydata { get; set; }
        public virtual DbSet<TMemActivity> TMemActivity { get; set; }
        public virtual DbSet<TMemHealthdata> TMemHealthdata { get; set; }
        public virtual DbSet<TMemberdata> TMemberdata { get; set; }
        public virtual DbSet<TMemlivedata> TMemlivedata { get; set; }
        public virtual DbSet<TMempetdata> TMempetdata { get; set; }
        public virtual DbSet<TOrTable> TOrTable { get; set; }
        public virtual DbSet<TRmTable> TRmTable { get; set; }
        public virtual DbSet<TTryPetTable> TTryPetTable { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=dbLLWP;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TActivityJoindata>(entity =>
            {
                entity.HasKey(e => e.JoinId)
                    .HasName("PK_aJoindata");

                entity.ToTable("tActivityJoindata");

                entity.Property(e => e.FJoinAcPeopleid).HasColumnName("fJoinAcPeopleid");

                entity.Property(e => e.JoinAcCode)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TActivitydata>(entity =>
            {
                entity.HasKey(e => e.FActivityId)
                    .HasName("PK_aActivitydata");

                entity.ToTable("tActivitydata");

                entity.Property(e => e.FActivityId).HasColumnName("fActivityId");

                entity.Property(e => e.FActivityCheck)
                    .IsRequired()
                    .HasColumnName("fActivityCheck")
                    .HasMaxLength(2);

                entity.Property(e => e.FActivityCode)
                    .IsRequired()
                    .HasColumnName("fActivityCode")
                    .HasMaxLength(50);

                entity.Property(e => e.FActivityImages)
                    .IsRequired()
                    .HasColumnName("fActivityImages");

                entity.Property(e => e.FActivityJoinpeople).HasColumnName("fActivityJoinpeople");

                entity.Property(e => e.FActivityLocation)
                    .IsRequired()
                    .HasColumnName("fActivityLocation");

                entity.Property(e => e.FActivityName)
                    .IsRequired()
                    .HasColumnName("fActivityName");

                entity.Property(e => e.FActivityPrice)
                    .HasColumnName("fActivityPrice")
                    .HasColumnType("money");

                entity.Property(e => e.FActivityTime)
                    .IsRequired()
                    .HasColumnName("fActivityTime")
                    .HasMaxLength(50);

                entity.Property(e => e.FActivitypeopleLimit).HasColumnName("fActivitypeopleLimit");
            });

            modelBuilder.Entity<TMemActivity>(entity =>
            {
                entity.HasKey(e => e.FAid)
                    .HasName("PK_mMemActivity_1");

                entity.ToTable("tMemActivity");

                entity.Property(e => e.FAid).HasColumnName("fAId");

                entity.Property(e => e.FAcId).HasColumnName("fAcId");

                entity.Property(e => e.FAcMeId).HasColumnName("fAcMeId");
            });

            modelBuilder.Entity<TMemHealthdata>(entity =>
            {
                entity.HasKey(e => e.FHeId)
                    .HasName("PK_mMemHealthdata_1");

                entity.ToTable("tMemHealthdata");

                entity.Property(e => e.FHeId).HasColumnName("fHeId");

                entity.Property(e => e.FHeBloodOxygen).HasColumnName("fHeBloodOxygen");

                entity.Property(e => e.FHeBloodPresureH)
                    .IsRequired()
                    .HasColumnName("fHeBloodPresureH")
                    .HasMaxLength(50);

                entity.Property(e => e.FHeBloodPresureL)
                    .IsRequired()
                    .HasColumnName("fHeBloodPresureL")
                    .HasMaxLength(50);

                entity.Property(e => e.FHeHeight)
                    .IsRequired()
                    .HasColumnName("fHeHeight")
                    .HasMaxLength(50);

                entity.Property(e => e.FHeMemNumber)
                    .IsRequired()
                    .HasColumnName("fHeMemNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.FHeTemperature).HasColumnName("fHeTemperature");

                entity.Property(e => e.FHeWeight)
                    .IsRequired()
                    .HasColumnName("fHeWeight")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TMemberdata>(entity =>
            {
                entity.HasKey(e => e.FMeId)
                    .HasName("PK_mMemberdata");

                entity.ToTable("tMemberdata");

                entity.Property(e => e.FMeId).HasColumnName("fMeId");

                entity.Property(e => e.FMeAge)
                    .HasColumnName("fMeAge")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FMeBirth)
                    .HasColumnName("fMeBirth")
                    .HasMaxLength(50);

                entity.Property(e => e.FMeEmerName)
                    .HasColumnName("fMeEmerName")
                    .HasMaxLength(50);

                entity.Property(e => e.FMeEmerPhone)
                    .HasColumnName("fMeEmerPhone")
                    .HasMaxLength(50);

                entity.Property(e => e.FMeGender)
                    .HasColumnName("fMeGender")
                    .HasMaxLength(10);

                entity.Property(e => e.FMeMail)
                    .HasColumnName("fMeMail")
                    .HasMaxLength(50);

                entity.Property(e => e.FMeName)
                    .HasColumnName("fMeName")
                    .HasMaxLength(50);

                entity.Property(e => e.FMeNumber)
                    .HasColumnName("fMeNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.FMePass)
                    .HasColumnName("fMePass")
                    .HasMaxLength(50);

                entity.Property(e => e.FMePersonId)
                    .HasColumnName("fMePersonId")
                    .HasMaxLength(50);

                entity.Property(e => e.FMePhone)
                    .HasColumnName("fMePhone")
                    .HasMaxLength(50);

                entity.Property(e => e.FMePhoto).HasColumnName("fMePhoto");
            });

            modelBuilder.Entity<TMemlivedata>(entity =>
            {
                entity.HasKey(e => e.FLoMeId)
                    .HasName("PK_mMemlongdata");

                entity.ToTable("tMemlivedata");

                entity.Property(e => e.FLoMeId).HasColumnName("fLoMeId");

                entity.Property(e => e.FLivedate)
                    .IsRequired()
                    .HasColumnName("fLivedate")
                    .HasMaxLength(50);

                entity.Property(e => e.FLoMeNumber)
                    .IsRequired()
                    .HasColumnName("fLoMeNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.FLoOrderNum)
                    .IsRequired()
                    .HasColumnName("fLoOrderNum")
                    .HasMaxLength(20);

                entity.Property(e => e.FRoomNumber)
                    .IsRequired()
                    .HasColumnName("fRoomNumber")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TMempetdata>(entity =>
            {
                entity.HasKey(e => e.FPeId)
                    .HasName("PK_mMempetdata");

                entity.ToTable("tMempetdata");

                entity.Property(e => e.FPeId).HasColumnName("fPeId");

                entity.Property(e => e.FPeAge)
                    .HasColumnName("fPeAge")
                    .HasMaxLength(5);

                entity.Property(e => e.FPeBirth)
                    .HasColumnName("fPeBirth")
                    .HasMaxLength(20);

                entity.Property(e => e.FPeFix)
                    .HasColumnName("fPeFix")
                    .HasMaxLength(2);

                entity.Property(e => e.FPeMem)
                    .IsRequired()
                    .HasColumnName("fPeMem")
                    .HasMaxLength(50);

                entity.Property(e => e.FPeMemNumber)
                    .HasColumnName("fPeMemNumber")
                    .HasMaxLength(20);

                entity.Property(e => e.FPeNumber)
                    .HasColumnName("fPeNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.FPePhoto)
                    .HasColumnName("fPePhoto")
                    .HasMaxLength(100);

                entity.Property(e => e.FPeSex)
                    .HasColumnName("fPeSex")
                    .HasMaxLength(10);

                entity.Property(e => e.FPeVac)
                    .HasColumnName("fPeVac")
                    .HasMaxLength(2);

                entity.Property(e => e.FPeVarity)
                    .HasColumnName("fPeVarity")
                    .HasMaxLength(10);

                entity.Property(e => e.FPeWeight).HasColumnName("fPeWeight");
            });

            modelBuilder.Entity<TOrTable>(entity =>
            {
                entity.HasKey(e => e.FOrId);

                entity.ToTable("tOrTable");

                entity.Property(e => e.FOrId).HasColumnName("fOrId");

                entity.Property(e => e.FOrCheckIn)
                    .IsRequired()
                    .HasColumnName("fOrCheckIn")
                    .HasMaxLength(20);

                entity.Property(e => e.FOrCheckOut)
                    .IsRequired()
                    .HasColumnName("fOrCheckOut")
                    .HasMaxLength(20);

                entity.Property(e => e.FOrDate)
                    .IsRequired()
                    .HasColumnName("fOrDate")
                    .HasMaxLength(20);

                entity.Property(e => e.FOrGuestOneActivityA).HasColumnName("fOrGuestOneActivityA");

                entity.Property(e => e.FOrGuestOneActivityB).HasColumnName("fOrGuestOneActivityB");

                entity.Property(e => e.FOrGuestOneActivityC).HasColumnName("fOrGuestOneActivityC");

                entity.Property(e => e.FOrGuestOneId).HasColumnName("fOrGuestOneId");

                entity.Property(e => e.FOrGuestTwoActivityA).HasColumnName("fOrGuestTwoActivityA");

                entity.Property(e => e.FOrGuestTwoActivityB).HasColumnName("fOrGuestTwoActivityB");

                entity.Property(e => e.FOrGuestTwoActivityC).HasColumnName("fOrGuestTwoActivityC");

                entity.Property(e => e.FOrGuestTwoId).HasColumnName("fOrGuestTwoId");

                entity.Property(e => e.FOrNum)
                    .IsRequired()
                    .HasColumnName("fOrNum")
                    .HasMaxLength(20);

                entity.Property(e => e.FOrPeople).HasColumnName("fOrPeople");

                entity.Property(e => e.FOrPetId)
                    .HasColumnName("fOrPetId")
                    .HasMaxLength(3);

                entity.Property(e => e.FOrRoomId).HasColumnName("fOrRoomId");

                entity.Property(e => e.FOrTotalPrice)
                    .HasColumnName("fOrTotalPrice")
                    .HasColumnType("money");

                entity.Property(e => e.FOrTryPet)
                    .IsRequired()
                    .HasColumnName("fOrTryPet")
                    .HasMaxLength(4);

                entity.Property(e => e.FOrTryPetId).HasColumnName("fOrTryPetId");

                entity.Property(e => e.FOrday).HasColumnName("fOrday");
            });

            modelBuilder.Entity<TRmTable>(entity =>
            {
                entity.HasKey(e => e.FRmId);

                entity.ToTable("tRmTable");

                entity.Property(e => e.FRmId).HasColumnName("fRmID");

                entity.Property(e => e.FRmBlock)
                    .IsRequired()
                    .HasColumnName("fRmBlock")
                    .HasMaxLength(2);

                entity.Property(e => e.FRmFloor).HasColumnName("fRmFloor");

                entity.Property(e => e.FRmHall).HasColumnName("fRmHall");

                entity.Property(e => e.FRmNum)
                    .IsRequired()
                    .HasColumnName("fRmNum")
                    .HasMaxLength(4);

                entity.Property(e => e.FRmPet)
                    .IsRequired()
                    .HasColumnName("fRmPet")
                    .HasMaxLength(2);

                entity.Property(e => e.FRmPriceDay)
                    .HasColumnName("fRmPriceDay")
                    .HasColumnType("money");

                entity.Property(e => e.FRmPriceDayOld)
                    .HasColumnName("fRmPriceDayOld")
                    .HasColumnType("money");

                entity.Property(e => e.FRmPriceMonthDoublePerson)
                    .HasColumnName("fRmPriceMonthDoublePerson")
                    .HasColumnType("money");

                entity.Property(e => e.FRmPriceMonthSinglePerson)
                    .HasColumnName("fRmPriceMonthSinglePerson")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<TTryPetTable>(entity =>
            {
                entity.HasKey(e => e.FTryPetId);

                entity.ToTable("tTryPetTable");

                entity.Property(e => e.FTryPetId).HasColumnName("fTryPetId");

                entity.Property(e => e.FTryPetAge).HasColumnName("fTryPetAge");

                entity.Property(e => e.FTryPetFix)
                    .IsRequired()
                    .HasColumnName("fTryPetFix")
                    .HasMaxLength(2);

                entity.Property(e => e.FTryPetName)
                    .IsRequired()
                    .HasColumnName("fTryPetName")
                    .HasMaxLength(8);

                entity.Property(e => e.FTryPetNature)
                    .IsRequired()
                    .HasColumnName("fTryPetNature");

                entity.Property(e => e.FTryPetNum)
                    .IsRequired()
                    .HasColumnName("fTryPetNum")
                    .HasMaxLength(4);

                entity.Property(e => e.FTryPetPhoto)
                    .IsRequired()
                    .HasColumnName("fTryPetPhoto");

                entity.Property(e => e.FTryPetSex)
                    .IsRequired()
                    .HasColumnName("fTryPetSex")
                    .HasMaxLength(2);

                entity.Property(e => e.FTryPetVac)
                    .IsRequired()
                    .HasColumnName("fTryPetVac")
                    .HasMaxLength(2);

                entity.Property(e => e.FTryPetVar)
                    .IsRequired()
                    .HasColumnName("fTryPetVar")
                    .HasMaxLength(3);

                entity.Property(e => e.FTryPetWei).HasColumnName("fTryPetWei");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
