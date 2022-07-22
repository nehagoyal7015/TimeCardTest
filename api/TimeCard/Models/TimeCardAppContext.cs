using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TimeCard.Models​
{
    public partial class TimeCardAppContext : DbContext
    {
        public TimeCardAppContext()
        {
        }

        public TimeCardAppContext(DbContextOptions<TimeCardAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessControl> AccessControls { get; set; }
        public virtual DbSet<AppDomain> AppDomains { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientContact> ClientContacts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategories { get; set; }
        public virtual DbSet<DomainSetting> DomainSettings { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupAccessControl> GroupAccessControls { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }
        public virtual DbSet<ProjectInvoice> ProjectInvoices { get; set; }
        public virtual DbSet<ProjectNote> ProjectNotes { get; set; }
        public virtual DbSet<ProjectTask> ProjectTasks { get; set; }
        public virtual DbSet<ProjectUser> ProjectUsers { get; set; }
        public virtual DbSet<TimeSheet> TimeSheets { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserLeave> UserLeaves { get; set; }
        public virtual DbSet<UserOptHoliday> UserOptHolidays { get; set; }
        public object TimeSheet { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(" Data Source =.;Initial Catalog=TimeCardApp;MultipleActiveResultSets=true;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccessControl>(entity =>
            {
                entity.ToTable("AccessControl");

                entity.Property(e => e.AccessName)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PossibleActions)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.AccessControls)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccessCon__Domai__690797E6");
            });

            modelBuilder.Entity<AppDomain>(entity =>
            {
                entity.HasKey(e => e.DomainId)
                    .HasName("PK__AppDomai__2498D75081112782");

                entity.ToTable("AppDomain");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DomainName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RegistrationNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Client__DomainId__0F624AF8");
            });

            modelBuilder.Entity<ClientContact>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ClientContact");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Other)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClientCon__Clien__10566F31");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Details)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Documents__Categ__47A6A41B");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK__Documents__Clien__46B27FE2");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK__Documents__Domai__45BE5BA9");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__Documents__Proje__489AC854");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Documents__UserI__498EEC8D");
            });

            modelBuilder.Entity<DocumentCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__Document__19093A0BE62FCAC5");

                entity.ToTable("DocumentCategory");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.CategoryParent)
                    .WithMany(p => p.InverseCategoryParent)
                    .HasForeignKey(d => d.CategoryParentId)
                    .HasConstraintName("FK__DocumentC__Categ__42E1EEFE");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.DocumentCategories)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK__DocumentC__Domai__41EDCAC5");
            });

            modelBuilder.Entity<DomainSetting>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK__DomainSe__A7C03FF85A83F610");

                entity.ToTable("DomainSetting");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.LogoUrl)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WorkingDay)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.DomainSettings)
                    .HasForeignKey(d => d.DomainId)
                    .HasConstraintName("FK__DomainSet__Domai__114A936A");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.GroupName)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ShortDescription)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Groups__DomainId__625A9A57");
            });

            modelBuilder.Entity<GroupAccessControl>(entity =>
            {
                entity.HasKey(e => new { e.GroupId, e.AccessControlId })
                    .HasName("PK__GroupAcc__2A4126E3CD6926C0");

                entity.ToTable("GroupAccessControl");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.AccessControl)
                    .WithMany(p => p.GroupAccessControls)
                    .HasForeignKey(d => d.AccessControlId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupAcce__Acces__6CD828CA");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupAccessControls)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__GroupAcce__Group__6BE40491");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("Holiday");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.HolidayDate).HasColumnType("date");

                entity.Property(e => e.HolidayDay)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.HolidayReason)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HolidayType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.BudgetCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CloseDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.EstimateCompletion).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerShips)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Task)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientProject");

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Project__DomainI__123EB7A3");
            });

            modelBuilder.Entity<ProjectDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.PkId })
                    .HasName("PK_UserDetail_Project");

                entity.ToTable("ProjectDetail");

                entity.Property(e => e.PkId).HasColumnName("PK_Id");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Pk)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.PkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectDe__PK_Id__3F115E1A");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectDe__Proje__3E1D39E1");
            });

            modelBuilder.Entity<ProjectInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__ProjectI__D796AAB5651FB148");

                entity.ToTable("ProjectInvoice");

                entity.Property(e => e.Comment)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceMonth).HasColumnType("date");

                entity.Property(e => e.InvoiceYear).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectInvoices)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ProjectIn__Proje__1332DBDC");
            });

            modelBuilder.Entity<ProjectNote>(entity =>
            {
                entity.HasKey(e => e.NoteId)
                    .HasName("PK__ProjectN__EACE355FCA3365C3");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectNotes)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectNo__Proje__14270015");
            });

            modelBuilder.Entity<ProjectTask>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__ProjectT__7C6949B1726D57E4");

                entity.ToTable("ProjectTask");

                entity.Property(e => e.BudgetCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectTasks)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ProjectTa__Proje__151B244E");
            });

            modelBuilder.Entity<ProjectUser>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UserId });

                entity.ToTable("ProjectUser");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectUs__Proje__160F4887");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProjectUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectUs__UserI__17036CC0");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UserId, e.TaskId, e.TimeSheetId });

                entity.ToTable("TimeSheet");

                entity.Property(e => e.TimeSheetId).ValueGeneratedOnAdd();

                entity.Property(e => e.BillableHoursNote).HasColumnType("text");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NonBillableHoursNote).HasColumnType("text");

                entity.Property(e => e.RequestDate).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TimeSheets)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__Proje__17F790F9");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TimeSheets)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__TaskI__18EBB532");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TimeSheets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TimeSheet__UserI__19DFD96B");
            });

            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserAcco__1788CC4C7EBFAE28");

                entity.ToTable("UserAccount");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Ssoid).HasColumnName("SSOId");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.UserAccounts)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAccou__Domai__1AD3FDA4");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.PkId)
                    .HasName("PK__UserDeta__8BEF1566369AD19E");

                entity.ToTable("UserDetail");

                entity.Property(e => e.PkId).HasColumnName("Pk_Id");

                entity.Property(e => e.Address1).IsUnicode(false);

                entity.Property(e => e.Address2).IsUnicode(false);

                entity.Property(e => e.AlternateNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Desgination)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmpId)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.JoiningDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.LeavingDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonalEmail)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Profile).IsUnicode(false);

                entity.Property(e => e.Skills).IsUnicode(false);

                entity.Property(e => e.SkypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseEmail)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SpouseName)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.SpousePhone)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDetai__UserI__1BC821DD");
            });

            modelBuilder.Entity<UserGroup>(entity =>
            {
                entity.ToTable("UserGroup");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserGroup__Group__662B2B3B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGroups)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserGroup__UserI__65370702");
            });

            modelBuilder.Entity<UserLeave>(entity =>
            {
                entity.HasKey(e => e.LeaveId)
                    .HasName("PK__UserLeav__796DB959F464B146");

                entity.ToTable("UserLeave");

                entity.Property(e => e.CancelledDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LeaveType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");
            });

            modelBuilder.Entity<UserOptHoliday>(entity =>
            {
                entity.ToTable("UserOptHoliday");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Holiday)
                    .WithMany(p => p.UserOptHolidays)
                    .HasForeignKey(d => d.HolidayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserOptHo__Holid__0880433F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
