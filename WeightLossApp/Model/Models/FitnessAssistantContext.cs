using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Model.Models
{
    public partial class FitnessAssistantContext : DbContext
    {
        public FitnessAssistantContext()
        {
        }

        public FitnessAssistantContext(DbContextOptions<FitnessAssistantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AchievementAcquirement> AchievementAcquirement { get; set; }
        public virtual DbSet<AchievementData> AchievementData { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<AvailableDesignTheme> AvailableDesignTheme { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DesignThemeData> DesignThemeData { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<Ingridient> Ingridient { get; set; }
        public virtual DbSet<IngridientCategory> IngridientCategory { get; set; }
        public virtual DbSet<IngridientData> IngridientData { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Meal> Meal { get; set; }
        public virtual DbSet<Medicine> Medicine { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PremiumStatus> PremiumStatus { get; set; }
        public virtual DbSet<PremiumSubscription> PremiumSubscription { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileSetting> ProfileSetting { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<SectionTraining> SectionTraining { get; set; }
        public virtual DbSet<SettingData> SettingData { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserMedicine> UserMedicine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=46.39.79.3\\DEV;Initial Catalog=FitnessAssistant;User ID=user5;Password=5555;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AchievementAcquirement>(entity =>
            {
                entity.HasKey(e => new { e.ProfileId, e.AchievementDataId });

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.Property(e => e.AchievementDataId).HasColumnName("AchievementData_ID");

                entity.HasOne(d => d.AchievementData)
                    .WithMany(p => p.AchievementAcquirement)
                    .HasForeignKey(d => d.AchievementDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AchievementAcquirement_AchievementData");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.AchievementAcquirement)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AchievementAcquirement_Profile1");
            });

            modelBuilder.Entity<AchievementData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ImgName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tier)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AvailableDesignTheme>(entity =>
            {
                entity.HasKey(e => new { e.InventoryId, e.DesignThemeId });

                entity.Property(e => e.InventoryId).HasColumnName("Inventory_ID");

                entity.Property(e => e.DesignThemeId).HasColumnName("DesignTheme_ID");

                entity.HasOne(d => d.DesignTheme)
                    .WithMany(p => p.AvailableDesignTheme)
                    .HasForeignKey(d => d.DesignThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvailableDesignTheme_DesignThemeData");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.AvailableDesignTheme)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AvailableDesignTheme_Inventory");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<DesignThemeData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccentColor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BaseColor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IconImage)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.SecondaryColor)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ScheduleId).HasColumnName("Schedule_ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_Schedule");
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ImageName).HasColumnType("image");

                entity.Property(e => e.Instructions).HasMaxLength(50);

                entity.Property(e => e.Length).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Section)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrainingId).HasColumnName("Training_ID");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.Exercise)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Exercise_Training");
            });

            modelBuilder.Entity<Ingridient>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IngridientDataId).HasColumnName("IngridientData_ID");

                entity.Property(e => e.MealId).HasColumnName("Meal_ID");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Ingridient)
                    .HasForeignKey<Ingridient>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingridient_IngridientData");

                entity.HasOne(d => d.Meal)
                    .WithMany(p => p.Ingridient)
                    .HasForeignKey(d => d.MealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingridient_Meal");
            });

            modelBuilder.Entity<IngridientCategory>(entity =>
            {
                entity.HasKey(e => new { e.IngridientId, e.CategoryId });

                entity.Property(e => e.IngridientId).HasColumnName("Ingridient_ID");

                entity.Property(e => e.CategoryId).HasColumnName("Category_ID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.IngridientCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IngridientCategory_Category");

                entity.HasOne(d => d.Ingridient)
                    .WithMany(p => p.IngridientCategory)
                    .HasForeignKey(d => d.IngridientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IngridientCategory_IngridientData");
            });

            modelBuilder.Entity<IngridientData>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CurrentDesignThemeId).HasColumnName("CurrentDesignTheme_ID");

                entity.Property(e => e.PremiumStatusId).HasColumnName("PremiumStatus_ID");

                entity.HasOne(d => d.CurrentDesignTheme)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.CurrentDesignThemeId)
                    .HasConstraintName("FK_Inventory_DesignThemeData");

                entity.HasOne(d => d.PremiumStatus)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.PremiumStatusId)
                    .HasConstraintName("FK_Inventory_PremiumStatus");
            });

            modelBuilder.Entity<Meal>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Dose)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TypeOfMedicine)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Goal)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BoughtItem)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreditCard)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsFixedLength();

                entity.Property(e => e.DateTime).HasColumnType("date");

                entity.Property(e => e.PaymentPurpose)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.Property(e => e.Sum).HasColumnType("money");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Profile");
            });

            modelBuilder.Entity<PremiumStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PremiumSubscriptionId).HasColumnName("PremiumSubscription_id");

                entity.Property(e => e.TimeEnd).HasColumnType("date");

                entity.Property(e => e.TimeStart).HasColumnType("date");

                entity.HasOne(d => d.PremiumSubscription)
                    .WithMany(p => p.PremiumStatus)
                    .HasForeignKey(d => d.PremiumSubscriptionId)
                    .HasConstraintName("FK_PremiumStatus_PremiumSubscription");
            });

            modelBuilder.Entity<PremiumSubscription>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.InventoryId).HasColumnName("Inventory_ID");

                entity.Property(e => e.MemberId).HasColumnName("Member_ID");

                entity.Property(e => e.Settings)
                    .IsRequired()
                    .HasColumnType("xml");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Inventory");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Profile_Member");
            });

            modelBuilder.Entity<ProfileSetting>(entity =>
            {
                entity.HasKey(e => new { e.SettingId, e.ProfileId });

                entity.Property(e => e.SettingId).HasColumnName("Setting_ID");

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.ProfileSetting)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileSetting_Profile");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.ProfileSetting)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProfileSetting_SettingData");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Profile");
            });

            modelBuilder.Entity<SectionTraining>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SettingData>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Complexity)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SectionTrainingId).HasColumnName("SectionTraining_ID");

                entity.HasOne(d => d.SectionTraining)
                    .WithMany(p => p.Training)
                    .HasForeignKey(d => d.SectionTrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Training_SectionTraining");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MemberId).HasColumnName("Member_ID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Member");
            });

            modelBuilder.Entity<UserMedicine>(entity =>
            {
                entity.HasKey(e => new { e.MedicineId, e.ProfileId });

                entity.Property(e => e.MedicineId).HasColumnName("Medicine_ID");

                entity.Property(e => e.ProfileId).HasColumnName("Profile_ID");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.UserMedicine)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMedicine_Medicine");

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.UserMedicine)
                    .HasForeignKey(d => d.ProfileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserMedicine_Profile");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
