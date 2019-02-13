using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tivBudget.Dal.Models
{
    public partial class freebyTrackContext : DbContext
    {
        public virtual DbSet<AccountActualRecurrence> AccountActualRecurrences { get; set; }
        public virtual DbSet<AccountActualTemplate> AccountActualTemplates { get; set; }
        public virtual DbSet<AccountActual> AccountActuals { get; set; }
        public virtual DbSet<AccountCategory> AccountCategories { get; set; }
        public virtual DbSet<AccountCategoryTemplate> AccountCategoryTemplates { get; set; }
        public virtual DbSet<AccountTemplate> AccountTemplates { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<BudgetActual> BudgetActuals { get; set; }
        public virtual DbSet<BudgetCategory> BudgetCategories { get; set; }
        public virtual DbSet<BudgetCategoryTemplate> BudgetCategoryTemplates { get; set; }
        public virtual DbSet<BudgetItemAlert> BudgetItemAlerts { get; set; }
        public virtual DbSet<BudgetItemRecurringSetting> BudgetItemRecurringSettings { get; set; }
        public virtual DbSet<BudgetItemTemplate> BudgetItemTemplates { get; set; }
        public virtual DbSet<BudgetItem> BudgetItems { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<ReportCategory> ReportCategories { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("someconnectionstring);
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<AccountActualRecurrence>(entity =>
            {
                entity.ToTable("AccountActualRecurrences", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.ActualTemplateId).HasColumnName("ActualTemplateID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Percent).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountActualRecurrences)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_AccountActualRecurrences_Accounts");

                entity.HasOne(d => d.ActualTemplate)
                    .WithMany(p => p.AccountActualRecurrences)
                    .HasForeignKey(d => d.ActualTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountActualRecurrences_AccountActualTemplates");
            });

            modelBuilder.Entity<AccountActualTemplate>(entity =>
            {
                entity.ToTable("AccountActualTemplates", "freebyTrack");

                entity.HasIndex(e => new { e.AccountTemplateId, e.OwnerId, e.Description })
                    .HasName("UC_AccountActualTemplates")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountTemplateId).HasColumnName("AccountTemplateID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.AccountTemplate)
                    .WithMany(p => p.AccountActualTemplates)
                    .HasForeignKey(d => d.AccountTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountActualTemplates_AccountTemplates");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.AccountActualTemplates)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_AccountActualTemplates_Users");
            });

            modelBuilder.Entity<AccountActual>(entity =>
            {
                entity.ToTable("AccountActuals", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ActualTemplateId).HasColumnName("ActualTemplateID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BudgetActualLinkId).HasColumnName("BudgetActualLinkID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RelevantOn).HasColumnType("date");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.ActualTemplate)
                    .WithMany(p => p.AccountActuals)
                    .HasForeignKey(d => d.ActualTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountActuals_AccountActualTemplates");

                entity.HasOne(d => d.BudgetActualLink)
                    .WithMany(p => p.AccountActuals)
                    .HasForeignKey(d => d.BudgetActualLinkId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AccountActuals_BudgetActuals");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AccountActuals)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_AccountActuals_AccountCategories");
            });

            modelBuilder.Entity<AccountCategory>(entity =>
            {
                entity.ToTable("AccountCategories", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CategoryTemplateId).HasColumnName("CategoryTemplateID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountCategories)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_AccountCategories_Accounts");

                entity.HasOne(d => d.CategoryTemplate)
                    .WithMany(p => p.AccountCategories)
                    .HasForeignKey(d => d.CategoryTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountCategories_AccountCategoryTemplates");
            });

            modelBuilder.Entity<AccountCategoryTemplate>(entity =>
            {
                entity.ToTable("AccountCategoryTemplates", "freebyTrack");

                entity.HasIndex(e => new { e.AccountTemplateId, e.OwnerId, e.Description })
                    .HasName("UC_AccountCategoryTemplates")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountTemplateId).HasColumnName("AccountTemplateID");

                entity.Property(e => e.BackgroundColor)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.AccountTemplate)
                    .WithMany(p => p.AccountCategoryTemplates)
                    .HasForeignKey(d => d.AccountTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountCategoryTemplates_AccountTemplates");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.AccountCategoryTemplates)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_AccountCategoryTemplates_Users");
            });

            modelBuilder.Entity<AccountTemplate>(entity =>
            {
                entity.ToTable("AccountTemplates", "freebyTrack");

                entity.HasIndex(e => new { e.OwnerId, e.Description })
                    .HasName("UC_AccountTemplates")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.AccountTemplates)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_AccountTemplates_Users");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AccountTemplates)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountTemplates_AccountTypes");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountTypes", "freebyTrack");

                entity.HasIndex(e => e.DescriptionPlural)
                    .HasName("UC_AccountTypes_DescriptionPlural")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DescriptionPlural)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.DescriptionSingular)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ModifiedBy).HasMaxLength(128);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NegLineItemDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NegLineItemShortDescription)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.PosLineItemDescription)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PosLineItemShortDescription)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountTemplateId).HasColumnName("AccountTemplateID");

                entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.AccountTemplate)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_AccountTemplates");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_AccountTypes");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.OwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Accounts_Users");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.ToTable("Applications", "Lookups");

                entity.HasIndex(e => e.Name)
                    .HasName("UC_Application_Name")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<BudgetActual>(entity =>
            {
                entity.ToTable("BudgetActuals", "freebyTrack");

                entity.HasIndex(e => e.ItemId)
                    .HasName("CI_BudgetActualsItem")
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.HasIndex(e => e.RelevantOn);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RelevantOn).HasColumnType("date");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.BudgetActuals)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_BudgetActuals_BudgetItems");
            });

            modelBuilder.Entity<BudgetCategory>(entity =>
            {
                entity.ToTable("BudgetCategories", "freebyTrack");

                entity.HasIndex(e => e.BudgetId)
                    .HasName("CI_BudgetCategoriesBudget")
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BudgetId).HasColumnName("BudgetID");

                entity.Property(e => e.CategoryBudgeted).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CategoryRemaining).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CategorySpent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CategoryTemplateId).HasColumnName("CategoryTemplateID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.BudgetCategories)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("FK_BudgetCategories_Budget");

                entity.HasOne(d => d.CategoryTemplate)
                    .WithMany(p => p.BudgetCategories)
                    .HasForeignKey(d => d.CategoryTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BudgetCategories_BudgetCategoryTemplates");
            });

            modelBuilder.Entity<BudgetCategoryTemplate>(entity =>
            {
                entity.ToTable("BudgetCategoryTemplates", "freebyTrack");

                entity.HasIndex(e => new { e.OwnerId, e.Description })
                    .HasName("UC_BudgetCategoryTemplates")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BackgroundColor)
                    .IsRequired()
                    .HasMaxLength(9);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Icon)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.BudgetCategoryTemplates)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_BudgetCategoryTemplates_Users");
            });

            modelBuilder.Entity<BudgetItemAlert>(entity =>
            {
                entity.ToTable("BudgetItemAlerts", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<BudgetItemRecurringSetting>(entity =>
            {
                entity.ToTable("BudgetItemRecurringSettings", "freebyTrack");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<BudgetItemTemplate>(entity =>
            {
                entity.ToTable("BudgetItemTemplates", "freebyTrack");

                entity.HasIndex(e => new { e.CategoryTemplateId, e.OwnerId, e.Description })
                    .HasName("UC_BudgetItemTemplates")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CategoryTemplateId).HasColumnName("CategoryTemplateID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LinkableAccountTemplateId).HasColumnName("LinkableAccountTemplateID");

                entity.Property(e => e.LinkableAccountTypeId).HasColumnName("LinkableAccountTypeID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.TransferableAccountTemplateId).HasColumnName("TransferableAccountTemplateID");

                entity.Property(e => e.TransferableAccountTypeId).HasColumnName("TransferableAccountTypeID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.CategoryTemplate)
                    .WithMany(p => p.BudgetItemTemplates)
                    .HasForeignKey(d => d.CategoryTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BudgetItemTemplates_BudgetCategoryTemplates");

                entity.HasOne(d => d.LinkableAccountTemplate)
                    .WithMany(p => p.BudgetItemTemplatesLinkableAccountTemplate)
                    .HasForeignKey(d => d.LinkableAccountTemplateId)
                    .HasConstraintName("FK_BudgetItemTemplates_AccountTemplates");

                entity.HasOne(d => d.LinkableAccountType)
                    .WithMany(p => p.BudgetItemTemplatesLinkableAccountType)
                    .HasForeignKey(d => d.LinkableAccountTypeId)
                    .HasConstraintName("FK_BudgetItemTemplates_AccountTypes");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.BudgetItemTemplates)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK_BudgetItemTemplates_Users");

                entity.HasOne(d => d.TransferableAccountTemplate)
                    .WithMany(p => p.BudgetItemTemplatesTransferableAccountTemplate)
                    .HasForeignKey(d => d.TransferableAccountTemplateId)
                    .HasConstraintName("FK_BudgetItemTemplates_AccountTemplates1");

                entity.HasOne(d => d.TransferableAccountType)
                    .WithMany(p => p.BudgetItemTemplatesTransferableAccountType)
                    .HasForeignKey(d => d.TransferableAccountTypeId)
                    .HasConstraintName("FK_BudgetItemTemplates_AccountTypes1");
            });

            modelBuilder.Entity<BudgetItem>(entity =>
            {
                entity.ToTable("BudgetItems", "freebyTrack");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("CI_BudgetItemsCategory")
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AccountCategoryLinkId).HasColumnName("AccountCategoryLinkID");

                entity.Property(e => e.AccountLinkId).HasColumnName("AccountLinkID");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.AmountBudgeted).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ItemRemaining).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItemSpent).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ItemTemplateId).HasColumnName("ItemTemplateID");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RecurringSettingsId).HasColumnName("RecurringSettingsID");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.HasOne(d => d.AccountCategoryLink)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.AccountCategoryLinkId)
                    .HasConstraintName("FK_BudgetItems_AccountCategories");

                entity.HasOne(d => d.AccountLink)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.AccountLinkId)
                    .HasConstraintName("FK_BudgetItems_Accounts");

                entity.HasOne(d => d.Alert)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.AlertId)
                    .HasConstraintName("FK_BudgetItems_BudgetItemAlerts");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_BudgetItems_BudgetCategories");

                entity.HasOne(d => d.ItemTemplate)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.ItemTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BudgetItems_BudgetItemTemplates");

                entity.HasOne(d => d.RecurringSettings)
                    .WithMany(p => p.BudgetItems)
                    .HasForeignKey(d => d.RecurringSettingsId)
                    .HasConstraintName("FK_BudgetItems_BudgetItemRecurringSettings");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("Budgets", "freebyTrack");

                entity.HasIndex(e => new { e.OwnerId, e.Month, e.Year, e.Description })
                    .HasName("UC_BudgetsOwnerIDMonthYearDescription")
                    .IsUnique()
                    .HasAnnotation("SqlServer:Clustered", true);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ActualIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActualMinusEstimatedIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActualRemaining).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActualSpending).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.EstimatedIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EstimatedRemaining).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EstimatedSpending).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Groups", "Security");

                entity.HasIndex(e => e.Description)
                    .HasName("UC_GroupsDescription")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<ReportCategory>(entity =>
            {
                entity.ToTable("ReportCategories", "Lookups");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.HasKey(e => new { e.Name, e.UserId, e.ApplicationId });

                entity.ToTable("UserSettings", "Security");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.IsCacheable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsWritable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_Applications");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSettings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSettings_Users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "Security");

                entity.HasIndex(e => e.Email)
                    .HasName("UC_UsersEmail")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("UC_UsersUserName")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.GroupAssociation).HasDefaultValueSql("((1))");

                entity.Property(e => e.LastLockoutDate).HasColumnType("datetime");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.LastPasswordChangeDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ResetPasswordTokenCreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Ts)
                    .IsRequired()
                    .HasColumnName("ts")
                    .IsRowVersion();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingExt(modelBuilder);
        }

        partial void OnModelCreatingExt(ModelBuilder modelBuilder);
    }
}
