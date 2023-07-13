using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace be.Models;

public partial class DbZotsystemContext : DbContext
{
    public DbZotsystemContext()
    {
    }

    public DbZotsystemContext(DbContextOptions<DbZotsystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Combination> Combinations { get; set; }

    public virtual DbSet<Groupsubject> Groupsubjects { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Newcategory> Newcategorys { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Postcomment> Postcomments { get; set; }

    public virtual DbSet<Postfavourite> Postfavourites { get; set; }

    public virtual DbSet<Postlike> Postlikes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Questiontest> Questiontests { get; set; }

    public virtual DbSet<Reportpost> Reportposts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<School> Schools { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Testdetail> Testdetails { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<Typetest> Typetests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0DMD9O9V\\SQLEXPRESS;Database=Db_ZOTSystem;Trusted_Connection=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT__349DA5A67801516F");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SchoolName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__ACCOUNT__RoleId__70DDC3D8");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__ANSWERS__D4825004EED2C438");

            entity.ToTable("ANSWERS");

            entity.Property(e => e.AnswerName).HasMaxLength(200);
        });

        modelBuilder.Entity<Combination>(entity =>
        {
            entity.HasKey(e => e.CombinationId).HasName("PK__COMBINAT__D188AC02C1EA1ACC");

            entity.ToTable("COMBINATIONS");

            entity.Property(e => e.CombinationName).HasMaxLength(300);
            entity.Property(e => e.MajorName).HasMaxLength(300);

            entity.HasOne(d => d.School).WithMany(p => p.Combinations)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK__COMBINATI__Schoo__71D1E811");
        });

        modelBuilder.Entity<Groupsubject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GROUPSUBJECTS");

            entity.HasOne(d => d.Combination).WithMany()
                .HasForeignKey(d => d.CombinationId)
                .HasConstraintName("FK__GROUPSUBJ__Combi__73BA3083");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__GROUPSUBJ__Subje__72C60C4A");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__LEVELS__09F03C2661DAA68D");

            entity.ToTable("LEVELS");

            entity.Property(e => e.LevelName).HasMaxLength(200);
        });

        modelBuilder.Entity<Newcategory>(entity =>
        {
            entity.HasKey(e => e.NewCategoryId).HasName("PK__NEWCATEG__84E7EAB354EEC0C7");

            entity.ToTable("NEWCATEGORYS");

            entity.Property(e => e.CategoryName).HasMaxLength(200);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewId).HasName("PK__NEWS__7CC3777EBD8133EB");

            entity.ToTable("NEWS");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Subtitle).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Account).WithMany(p => p.News)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__NEWS__AccountId__74AE54BC");

            entity.HasOne(d => d.NewCategory).WithMany(p => p.News)
                .HasForeignKey(d => d.NewCategoryId)
                .HasConstraintName("FK__NEWS__NewCategor__75A278F5");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__POSTS__AA1260182D012596");

            entity.ToTable("POSTS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PostFile).HasMaxLength(500);
            entity.Property(e => e.PostText).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTS__AccountId__7D439ABD");

            entity.HasOne(d => d.Subject).WithMany(p => p.Posts)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__POSTS__SubjectId__7C4F7684");
        });

        modelBuilder.Entity<Postcomment>(entity =>
        {
            entity.HasKey(e => e.PostCommentId).HasName("PK__POSTCOMM__A955AFED0F602569");

            entity.ToTable("POSTCOMMENTS");

            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.FileComment).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Postcomments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTCOMME__Accou__76969D2E");

            entity.HasOne(d => d.Post).WithMany(p => p.Postcomments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTCOMME__PostI__778AC167");
        });

        modelBuilder.Entity<Postfavourite>(entity =>
        {
            entity.HasKey(e => e.PostFavouriteId).HasName("PK__POSTFAVO__91F6FB97D38F30E3");

            entity.ToTable("POSTFAVOURITES");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Postfavourites)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTFAVOU__Accou__787EE5A0");

            entity.HasOne(d => d.Post).WithMany(p => p.Postfavourites)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTFAVOU__PostI__797309D9");
        });

        modelBuilder.Entity<Postlike>(entity =>
        {
            entity.HasKey(e => e.PostLikeId).HasName("PK__POSTLIKE__4CF65C1941287A1F");

            entity.ToTable("POSTLIKES");

            entity.Property(e => e.LikeDate).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Postlikes)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTLIKES__Accou__7A672E12");

            entity.HasOne(d => d.Post).WithMany(p => p.Postlikes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTLIKES__PostI__7B5B524B");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__QUESTION__0DC06FACAE1A0F15");

            entity.ToTable("QUESTIONS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.OptionA).HasMaxLength(300);
            entity.Property(e => e.OptionB).HasMaxLength(300);
            entity.Property(e => e.OptionC).HasMaxLength(300);
            entity.Property(e => e.OptionD).HasMaxLength(300);
            entity.Property(e => e.QuestionContext).HasMaxLength(300);
            entity.Property(e => e.Solution).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Questions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__QUESTIONS__Accou__01142BA1");

            entity.HasOne(d => d.Answer).WithMany(p => p.Questions)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK__QUESTIONS__Answe__02084FDA");

            entity.HasOne(d => d.Level).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__QUESTIONS__Level__03F0984C");

            entity.HasOne(d => d.Subject).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__QUESTIONS__Subje__00200768");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__QUESTIONS__Topic__04E4BC85");

            entity.HasOne(d => d.TypeTest).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TypeTestId)
                .HasConstraintName("FK__QUESTIONS__TypeT__02FC7413");
        });

        modelBuilder.Entity<Questiontest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__QUESTION__8CC33160AC60EAD6");

            entity.ToTable("QUESTIONTESTS");

            entity.Property(e => e.TestDetailId).HasColumnName("TestDetailID");

            entity.HasOne(d => d.Answer).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK__QUESTIONT__Answe__05D8E0BE");

            entity.HasOne(d => d.Question).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QUESTIONT__Quest__07C12930");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.TestDetailId)
                .HasConstraintName("FK__QUESTIONT__TestD__06CD04F7");
        });

        modelBuilder.Entity<Reportpost>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__REPORTPO__D5BD480546E53CBE");

            entity.ToTable("REPORTPOSTS");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Reportposts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__REPORTPOS__Accou__08B54D69");

            entity.HasOne(d => d.Post).WithMany(p => p.Reportposts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__REPORTPOS__PostI__09A971A2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLE__8AFACE1AF06AA84E");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PK__SCHOOLS__3DA4675BEB0A73D6");

            entity.ToTable("SCHOOLS");

            entity.Property(e => e.SchoolName).HasMaxLength(200);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__SUBJECTS__AC1BA3A803B0D73F");

            entity.ToTable("SUBJECTS");

            entity.Property(e => e.ImgLink)
                .IsUnicode(false)
                .HasColumnName("imgLink");
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<Testdetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__TESTDETA__F50859467FFB536D");

            entity.ToTable("TESTDETAILS");

            entity.Property(e => e.TestDetailId).HasColumnName("TestDetailID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Testdetails)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__TESTDETAI__Accou__0B91BA14");

            entity.HasOne(d => d.Subject).WithMany(p => p.Testdetails)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__TESTDETAI__Subje__0A9D95DB");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__TOPICS__022E0F5DB2D67082");

            entity.ToTable("TOPICS");

            entity.Property(e => e.Duration).HasMaxLength(200);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TopicName).HasMaxLength(200);
        });

        modelBuilder.Entity<Typetest>(entity =>
        {
            entity.HasKey(e => e.TypeTestId).HasName("PK__TYPETEST__49DEEBA63DE2CB6D");

            entity.ToTable("TYPETESTS");

            entity.Property(e => e.TestName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
