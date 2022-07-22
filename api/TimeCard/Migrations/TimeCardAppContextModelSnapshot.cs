﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeCard.Models;

namespace TimeCard.Migrations
{
    [DbContext(typeof(TimeCardAppContext))]
    partial class TimeCardAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TimeCard.Models.AppDomain", b =>
                {
                    b.Property<int>("DomainId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("ClientCode")
                        .HasColumnType("int");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("DomainName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("DomainId")
                        .HasName("PK__AppDomai__2498D75081112782");

                    b.ToTable("AppDomain");
                });

            modelBuilder.Entity("TimeCard.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("ContactNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("RegistrationNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.HasKey("ClientId");

                    b.HasIndex("DomainId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("TimeCard.Models.ClientContact", b =>
                {
                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Other")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientContact");
                });

            modelBuilder.Entity("TimeCard.Models.DomainSetting", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("DomainId")
                        .HasColumnType("int");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("OptingHolidayCount")
                        .HasColumnType("int");

                    b.Property<int>("SickLeaveCount")
                        .HasColumnType("int");

                    b.Property<string>("TimeZone")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("TotalLeave")
                        .HasColumnType("int");

                    b.Property<int>("UnPlannedLeaveCount")
                        .HasColumnType("int");

                    b.Property<string>("WorkingDay")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<float>("WorkingHours")
                        .HasColumnType("real");

                    b.HasKey("PkId")
                        .HasName("PK__DomainSe__A7C03FF85A83F610");

                    b.HasIndex("DomainId");

                    b.ToTable("DomainSetting");
                });

            modelBuilder.Entity("TimeCard.Models.Holiday", b =>
                {
                    b.Property<int>("HolidayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HolidayDate")
                        .HasColumnType("date");

                    b.Property<string>("HolidayDay")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("HolidayReason")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("HolidayType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsFloating")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpting")
                        .HasColumnType("bit");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.HasKey("HolidayId");

                    b.ToTable("Holiday");
                });

            modelBuilder.Entity("TimeCard.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float?>("ActualBudget")
                        .HasColumnType("real");

                    b.Property<decimal?>("BudgetCost")
                        .HasColumnType("decimal(18,0)");

                    b.Property<float?>("BudgetHours")
                        .HasColumnType("real");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CloseDate")
                        .HasColumnType("datetime");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<int?>("DomainId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EstimateCompletion")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("OwnerShips")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Task")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("ClientId");

                    b.HasIndex("DomainId");

                    b.HasIndex("PK_Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectInvoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .IsUnicode(false)
                        .HasColumnType("varchar(500)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("InvoiceDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("InvoiceMonth")
                        .HasColumnType("date");

                    b.Property<DateTime?>("InvoiceYear")
                        .HasColumnType("datetime");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<float?>("TotalBillableHours")
                        .HasColumnType("real");

                    b.Property<float?>("TotalNonBillableHours")
                        .HasColumnType("real");

                    b.HasKey("InvoiceId")
                        .HasName("PK__ProjectI__D796AAB5651FB148");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectInvoice");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectNote", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Note")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("NoteId")
                        .HasName("PK__ProjectN__EACE355FCA3365C3");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectNotes");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectTask", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.Property<bool>("IsBillable")
                        .HasColumnType("bit");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("TaskId")
                        .HasName("PK__ProjectT__7C6949B1726D57E4");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectTask");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectUser", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("TimeCard.Models.TimeSheet", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BillableHours")
                        .HasColumnType("real");

                    b.Property<string>("BillableHoursNote")
                        .HasColumnType("text");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("date");

                    b.Property<bool>("IsBilled")
                        .HasColumnType("bit");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<float?>("NonBillableHours")
                        .HasColumnType("real");

                    b.Property<string>("NonBillableHoursNote")
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("date");

                    b.HasKey("ProjectId", "UserId", "TaskId", "TimeSheetId");

                    b.HasIndex("TaskId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeSheet");
                });

            modelBuilder.Entity("TimeCard.Models.UserAccount", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("Ssoid")
                        .HasColumnType("int")
                        .HasColumnName("SSOId");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.HasKey("UserId")
                        .HasName("PK__UserAcco__1788CC4C7EBFAE28");

                    b.HasIndex("DomainId");

                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity("TimeCard.Models.UserDetail", b =>
                {
                    b.Property<int>("PkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Pk_Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Address2")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("AlternateNumber")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime");

                    b.Property<string>("Desgination")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMaritalStatus")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("JoiningDate")
                        .HasColumnType("datetime");

                    b.Property<string>("LastName")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("datetime");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("MotherName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("PersonalEmail")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Profile")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Skills")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SkypeId")
                        .IsRequired()
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("SpouseEmail")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("SpouseName")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("SpousePhone")
                        .HasMaxLength(40)
                        .IsUnicode(false)
                        .HasColumnType("varchar(40)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PkId")
                        .HasName("PK__UserDeta__8BEF1566369AD19E");

                    b.HasIndex("UserId");

                    b.ToTable("UserDetail");
                });

            modelBuilder.Entity("TimeCard.Models.UserLeave", b =>
                {
                    b.Property<int>("LeaveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CancelledBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CancelledDate")
                        .HasColumnType("date");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsCancelled")
                        .HasColumnType("bit");

                    b.Property<string>("LeaveType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LeaveId")
                        .HasName("PK__UserLeav__796DB959F464B146");

                    b.ToTable("UserLeave");
                });

            modelBuilder.Entity("TimeCard.Models.Client", b =>
                {
                    b.HasOne("TimeCard.Models.AppDomain", "Domain")
                        .WithMany("Clients")
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK__Client__DomainId__5CD6CB2B")
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("TimeCard.Models.ClientContact", b =>
                {
                    b.HasOne("TimeCard.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK__ClientCon__Clien__60A75C0F")
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("TimeCard.Models.DomainSetting", b =>
                {
                    b.HasOne("TimeCard.Models.AppDomain", "Domain")
                        .WithMany("DomainSettings")
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK__DomainSet__Domai__267ABA7A");

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("TimeCard.Models.Project", b =>
                {
                    b.HasOne("TimeCard.Models.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_ClientProject")
                        .IsRequired();

                    b.HasOne("TimeCard.Models.AppDomain", "Domain")
                        .WithMany("Projects")
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK__Project__DomainI__6E01572D");

                    b.Navigation("Client");

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectInvoice", b =>
                {
                    b.HasOne("TimeCard.Models.Project", "Project")
                        .WithMany("ProjectInvoices")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__ProjectIn__Proje__38996AB5");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectNote", b =>
                {
                    b.HasOne("TimeCard.Models.Project", "Project")
                        .WithMany("ProjectNotes")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__ProjectNo__Proje__3B75D760")
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectTask", b =>
                {
                    b.HasOne("TimeCard.Models.Project", "Project")
                        .WithMany("ProjectTasks")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__ProjectTa__Proje__31EC6D26");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectUser", b =>
                {
                    b.HasOne("TimeCard.Models.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__ProjectUs__Proje__34C8D9D1")
                        .IsRequired();

                    b.HasOne("TimeCard.Models.UserAccount", "User")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__ProjectUs__UserI__35BCFE0A")
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeCard.Models.TimeSheet", b =>
                {
                    b.HasOne("TimeCard.Models.Project", "Project")
                        .WithMany("TimeSheets")
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("FK__TimeSheet__Proje__3F466844")
                        .IsRequired();

                    b.HasOne("TimeCard.Models.ProjectTask", "Task")
                        .WithMany("TimeSheets")
                        .HasForeignKey("TaskId")
                        .HasConstraintName("FK__TimeSheet__TaskI__403A8C7D")
                        .IsRequired();

                    b.HasOne("TimeCard.Models.UserAccount", "User")
                        .WithMany("TimeSheets")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__TimeSheet__UserI__3E52440B")
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeCard.Models.UserAccount", b =>
                {
                    b.HasOne("TimeCard.Models.AppDomain", "Domain")
                        .WithMany("UserAccounts")
                        .HasForeignKey("DomainId")
                        .HasConstraintName("FK__UserAccou__Domai__29572725")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("TimeCard.Models.UserDetail", b =>
                {
                    b.HasOne("TimeCard.Models.UserAccount", "User")
                        .WithMany("UserDetails")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__UserDetai__UserI__2C3393D0")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeCard.Models.AppDomain", b =>
                {
                    b.Navigation("Clients");

                    b.Navigation("DomainSettings");

                    b.Navigation("Projects");

                    b.Navigation("UserAccounts");
                });

            modelBuilder.Entity("TimeCard.Models.Client", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("TimeCard.Models.Project", b =>
                {
                    b.Navigation("ProjectInvoices");

                    b.Navigation("ProjectNotes");

                    b.Navigation("ProjectTasks");

                    b.Navigation("ProjectUsers");

                    b.Navigation("TimeSheets");
                });

            modelBuilder.Entity("TimeCard.Models.ProjectTask", b =>
                {
                    b.Navigation("TimeSheets");
                });

            modelBuilder.Entity("TimeCard.Models.UserAccount", b =>
                {
                    b.Navigation("ProjectUsers");

                    b.Navigation("TimeSheets");

                    b.Navigation("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}