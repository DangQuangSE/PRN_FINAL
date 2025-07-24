using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities;

public partial class GenderHealthCareSystemContext : DbContext
{
    public GenderHealthCareSystemContext()
    {
    }

    public GenderHealthCareSystemContext(DbContextOptions<GenderHealthCareSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<ConsultantFeedback> ConsultantFeedbacks { get; set; }

    public virtual DbSet<ConsultantProfile> ConsultantProfiles { get; set; }

    public virtual DbSet<ConsultationBooking> ConsultationBookings { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<MenstrualCycle> MenstrualCycles { get; set; }

    public virtual DbSet<MenstrualCycleHistory> MenstrualCycleHistories { get; set; }

    public virtual DbSet<Pill> Pills { get; set; }

    public virtual DbSet<PillSchedule> PillSchedules { get; set; }

    public virtual DbSet<ProfileDetail> ProfileDetails { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionComment> QuestionComments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StisBooking> StisBookings { get; set; }

    public virtual DbSet<StisFeedback> StisFeedbacks { get; set; }

    public virtual DbSet<StisInvoice> StisInvoices { get; set; }

    public virtual DbSet<StisResult> StisResults { get; set; }

    public virtual DbSet<StisService> StisServices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=GenderHealthCareSystem;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586D7BB92C3");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ResetOtpExpiry).HasPrecision(6);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK6v79e3k6wk63b4clv2pba57qj");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__BlogPost__AA126038E7FFD0BF");

            entity.ToTable("BlogPost");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ConsultantId).HasColumnName("ConsultantID");
            entity.Property(e => e.PublishedAt).HasPrecision(6);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Tags).HasMaxLength(255);
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("ThumbnailURL");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Consultant).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FKchsvc4wn4j1h1chbsxwn1msuk");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA5167FFC7");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasPrecision(6);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK8oct4pus8j9twryxks9qork19");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKl9tmq4i356gryve2ajvxr3i0l");
        });

        modelBuilder.Entity<ConsultantFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Consulta__6A4BEDF6058EE6A9");

            entity.ToTable("ConsultantFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ConsultantProfileId).HasColumnName("ConsultantProfileID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Booking).WithMany(p => p.ConsultantFeedbacks)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FKo6wy0ywulrkub2us4yy03nif6");

            entity.HasOne(d => d.ConsultantProfile).WithMany(p => p.ConsultantFeedbacks)
                .HasForeignKey(d => d.ConsultantProfileId)
                .HasConstraintName("FK1mem66ghugrvsq1qaat4fu92e");

            entity.HasOne(d => d.Customer).WithMany(p => p.ConsultantFeedbacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK8cb3n7o423p4vmv7q7pts2q28");
        });

        modelBuilder.Entity<ConsultantProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Consulta__290C88844E7BC97C");

            entity.ToTable("ConsultantProfile");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.ConsultantId).HasColumnName("ConsultantID");

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultantProfiles)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FKd6cxdr6gf2f7h2d5qgvv5bkik");
        });

        modelBuilder.Entity<ConsultationBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Consulta__73951ACDDEBA0FE7");

            entity.ToTable("ConsultationBooking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasPrecision(6);
            entity.Property(e => e.ConsultantId).HasColumnName("ConsultantID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.MeetLink)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultationBookingConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FKlmsv2rwp66x62yj3msr4l9uss");

            entity.HasOne(d => d.Customer).WithMany(p => p.ConsultationBookingCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FKi2u7k5sxlvvihpk0oyblsguo5");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__F58DFD49F1684159");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaidAt).HasPrecision(6);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RefundStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK83jctipjvnno7ir2739acgqlx");
        });

        modelBuilder.Entity<MenstrualCycle>(entity =>
        {
            entity.HasKey(e => e.CycleId).HasName("PK__Menstrua__077B24D94D4393C6");

            entity.ToTable("MenstrualCycle");

            entity.Property(e => e.CycleId).HasColumnName("CycleID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LastNotificationType)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.MenstrualCycles)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FKiqkdaseiamhm0f05w35vtpmjj");
        });

        modelBuilder.Entity<MenstrualCycleHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__Menstrua__4D7B4ADD2952B91C");

            entity.ToTable("MenstrualCycleHistory");

            entity.Property(e => e.HistoryId).HasColumnName("HistoryID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CycleId).HasColumnName("CycleID");

            entity.HasOne(d => d.Cycle).WithMany(p => p.MenstrualCycleHistories)
                .HasForeignKey(d => d.CycleId)
                .HasConstraintName("FKp4kg8paew8g5wqeyu7li4ybti");
        });

        modelBuilder.Entity<Pill>(entity =>
        {
            entity.HasKey(e => e.PillId).HasName("PK__Pills__6A5F7D782CDA596F");

            entity.Property(e => e.PillId).HasColumnName("PillID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.NotificationFrequency)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PillType)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Customer).WithMany(p => p.Pills)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK269x646p3ebet5rog0l7jlesx");
        });

        modelBuilder.Entity<PillSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Pill_Sch__9C8A5B69AB19A16C");

            entity.ToTable("Pill_Schedules");

            entity.HasIndex(e => e.ConfirmToken, "UKbg944r1i44mebqp2etub0b4wk").IsUnique();

            entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
            entity.Property(e => e.ConfirmToken)
                .HasMaxLength(36)
                .IsUnicode(false);
            entity.Property(e => e.PillId).HasColumnName("PillID");

            entity.HasOne(d => d.Pill).WithMany(p => p.PillSchedules)
                .HasForeignKey(d => d.PillId)
                .HasConstraintName("FKbuiob9bvdue1qsxd5ik59lnaf");
        });

        modelBuilder.Entity<ProfileDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("PK__ProfileD__135C314D6CAA5B5A");

            entity.ToTable("ProfileDetail");

            entity.Property(e => e.DetailId).HasColumnName("DetailID");
            entity.Property(e => e.DetailType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileDetails)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKde345dpf5io5a3cwa0nbuajmb");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06F8C638278DC");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.AnsweredAt).HasPrecision(6);
            entity.Property(e => e.ConsultantId).HasColumnName("ConsultantID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.AnswerByNavigation).WithMany(p => p.QuestionAnswerByNavigations)
                .HasForeignKey(d => d.AnswerBy)
                .HasConstraintName("FK5l1ejop2453lxm99d4vvtnqsp");

            entity.HasOne(d => d.Consultant).WithMany(p => p.QuestionConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .HasConstraintName("FKb0vg7bxbmuy5c1or3ie83ib90");

            entity.HasOne(d => d.Customer).WithMany(p => p.QuestionCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK1y808rxvm82vo0shmj8tfn7k8");
        });

        modelBuilder.Entity<QuestionComment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Question__C3B4DFAAB205E679");

            entity.ToTable("QuestionComment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.QuestionId).HasColumnName("QuestionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionComments)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK126lmsn088vmew04w3dml2rka");

            entity.HasOne(d => d.User).WithMany(p => p.QuestionComments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKeo1x9mln5i22gg7d68oqo4jf3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3AC4233561");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StisBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__STIsBook__73951ACD2D90CC5A");

            entity.ToTable("STIsBooking");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingDate).HasPrecision(6);
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ResultedAt).HasPrecision(6);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasPrecision(6);

            entity.HasOne(d => d.Customer).WithMany(p => p.StisBookings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK32xwaojejfofgs40xo3dk5vcf");

            entity.HasOne(d => d.Service).WithMany(p => p.StisBookings)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK8dg1in8l5p5iifqhherau3ikp");
        });

        modelBuilder.Entity<StisFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__STIsFeed__6A4BEDF630BD1235");

            entity.ToTable("STIsFeedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasPrecision(6);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Booking).WithMany(p => p.StisFeedbacks)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FKncy3o126gkd1bdu18e2wbgt40");

            entity.HasOne(d => d.Service).WithMany(p => p.StisFeedbacks)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FKb718t0iog3us4meimwj6aenww");
        });

        modelBuilder.Entity<StisInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__STIsInvo__D796AAD51E2FA18B");

            entity.ToTable("STIsInvoice");

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Currency)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PaidAt).HasPrecision(6);
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.StisInvoices)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FKiyb3lftqgslm5vqjhny7o0v5m");
        });

        modelBuilder.Entity<StisResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__STIsResu__9769022892530687");

            entity.ToTable("STIsResult");

            entity.Property(e => e.ResultId).HasColumnName("ResultID");
            entity.Property(e => e.BookingId).HasColumnName("booking_id");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(6)
                .HasColumnName("created_at");
            entity.Property(e => e.Note).HasColumnName("note");
            entity.Property(e => e.PdfResultUrl)
                .HasMaxLength(255)
                .HasColumnName("pdf_result_url");
            entity.Property(e => e.ReferenceRange)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reference_range");
            entity.Property(e => e.ResultDate)
                .HasPrecision(6)
                .HasColumnName("result_date");
            entity.Property(e => e.ResultText).HasColumnName("result_text");
            entity.Property(e => e.ResultValue)
                .HasMaxLength(255)
                .HasColumnName("result_value");
            entity.Property(e => e.TestCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("test_code");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(6)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Booking).WithMany(p => p.StisResults)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FKfb0ylll0lxjgg21j50gk5mgo3");
        });

        modelBuilder.Entity<StisService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__STIsServ__C51BB0EAC189096C");

            entity.ToTable("STIsService");

            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.Duration)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("numeric(10, 2)");
            entity.Property(e => e.ServiceName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasPrecision(6);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACE941A9B6");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt).HasPrecision(6);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Provider)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdatedAt).HasPrecision(6);
            entity.Property(e => e.UserImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK2314qccimck91eprl8706rjm2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
