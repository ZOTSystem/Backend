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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-B9LVRBH\\SQLEXPRESS;Database=Db_ZOTSystem;Trusted_Connection=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT__349DA5A66858C329");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.BirthDay).HasColumnType("datetime");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SchoolName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__ACCOUNT__RoleId__45F365D3");
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__ANSWERS__D4825004526787B2");

            entity.ToTable("ANSWERS");

            entity.Property(e => e.AnswerName).HasMaxLength(200);
        });

        modelBuilder.Entity<Combination>(entity =>
        {
            entity.HasKey(e => e.CombinationId).HasName("PK__COMBINAT__D188AC02B6F0F1CE");

            entity.ToTable("COMBINATIONS");

            entity.Property(e => e.CombinationName).HasMaxLength(300);
            entity.Property(e => e.MajorName).HasMaxLength(300);

            entity.HasOne(d => d.School).WithMany(p => p.Combinations)
                .HasForeignKey(d => d.SchoolId)
                .HasConstraintName("FK__COMBINATI__Schoo__46E78A0C");
        });

        modelBuilder.Entity<Groupsubject>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GROUPSUBJECTS");

            entity.HasOne(d => d.Combination).WithMany()
                .HasForeignKey(d => d.CombinationId)
                .HasConstraintName("FK__GROUPSUBJ__Combi__47DBAE45");

            entity.HasOne(d => d.Subject).WithMany()
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__GROUPSUBJ__Subje__48CFD27E");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__LEVELS__09F03C2639E337E2");

            entity.ToTable("LEVELS");

            entity.Property(e => e.LevelName).HasMaxLength(200);
        });

        modelBuilder.Entity<Newcategory>(entity =>
        {
            entity.HasKey(e => e.NewCategoryId).HasName("PK__NEWCATEG__84E7EAB328659B3B");

            entity.ToTable("NEWCATEGORYS");

            entity.Property(e => e.CategoryName).HasMaxLength(200);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewId).HasName("PK__NEWS__7CC3777E9919AD9E");

            entity.ToTable("NEWS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.News)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__NEWS__AccountId__49C3F6B7");

            entity.HasOne(d => d.NewCategory).WithMany(p => p.News)
                .HasForeignKey(d => d.NewCategoryId)
                .HasConstraintName("FK__NEWS__NewCategor__4AB81AF0");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__POSTS__AA126018702B8D88");

            entity.ToTable("POSTS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.PostFile).HasMaxLength(500);
            entity.Property(e => e.PostText).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTS__AccountId__5165187F");

            entity.HasOne(d => d.Subject).WithMany(p => p.Posts)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__POSTS__SubjectId__52593CB8");
        });

        modelBuilder.Entity<Postcomment>(entity =>
        {
            entity.HasKey(e => e.PostCommentId).HasName("PK__POSTCOMM__A955AFED3BF82AD1");

            entity.ToTable("POSTCOMMENTS");

            entity.Property(e => e.CommentDate).HasColumnType("datetime");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.FileComment).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Postcomments)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTCOMME__Accou__4BAC3F29");

            entity.HasOne(d => d.Post).WithMany(p => p.Postcomments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTCOMME__PostI__4CA06362");
        });

        modelBuilder.Entity<Postfavourite>(entity =>
        {
            entity.HasKey(e => e.PostFavouriteId).HasName("PK__POSTFAVO__91F6FB97A2F5D4B4");

            entity.ToTable("POSTFAVOURITES");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Postfavourites)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTFAVOU__Accou__4D94879B");

            entity.HasOne(d => d.Post).WithMany(p => p.Postfavourites)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTFAVOU__PostI__4E88ABD4");
        });

        modelBuilder.Entity<Postlike>(entity =>
        {
            entity.HasKey(e => e.PostLikeId).HasName("PK__POSTLIKE__4CF65C19418CFC9F");

            entity.ToTable("POSTLIKES");

            entity.Property(e => e.LikeDate).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Postlikes)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__POSTLIKES__Accou__4F7CD00D");

            entity.HasOne(d => d.Post).WithMany(p => p.Postlikes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__POSTLIKES__PostI__5070F446");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__QUESTION__0DC06FAC7A283BAE");

            entity.ToTable("QUESTIONS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Questions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__QUESTIONS__Accou__534D60F1");

            entity.HasOne(d => d.Answer).WithMany(p => p.Questions)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK__QUESTIONS__Answe__5441852A");

            entity.HasOne(d => d.Level).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK__QUESTIONS__Level__5535A963");

            entity.HasOne(d => d.Subject).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__QUESTIONS__Subje__5629CD9C");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .HasConstraintName("FK__QUESTIONS__Topic__571DF1D5");
        });

        modelBuilder.Entity<Questiontest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__QUESTION__8CC33160820E5736");

            entity.ToTable("QUESTIONTESTS");

            entity.Property(e => e.TestDetailId).HasColumnName("TestDetailID");

            entity.HasOne(d => d.Answer).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.AnswerId)
                .HasConstraintName("FK__QUESTIONT__Answe__5812160E");

            entity.HasOne(d => d.Question).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__QUESTIONT__Quest__59063A47");

            entity.HasOne(d => d.TestDetail).WithMany(p => p.Questiontests)
                .HasForeignKey(d => d.TestDetailId)
                .HasConstraintName("FK__QUESTIONT__TestD__59FA5E80");
        });

        modelBuilder.Entity<Reportpost>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__REPORTPO__D5BD480506276558");

            entity.ToTable("REPORTPOSTS");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.Reportposts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__REPORTPOS__Accou__5AEE82B9");

            entity.HasOne(d => d.Post).WithMany(p => p.Reportposts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK__REPORTPOS__PostI__5BE2A6F2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__ROLE__8AFACE1A4EEF57F6");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(e => e.SchoolId).HasName("PK__SCHOOLS__3DA4675B041AEBB8");

            entity.ToTable("SCHOOLS");

            entity.Property(e => e.SchoolName).HasMaxLength(200);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__SUBJECTS__AC1BA3A8A7B418C0");

            entity.ToTable("SUBJECTS");

            entity.Property(e => e.ImgLink)
                .IsUnicode(false)
                .HasColumnName("imgLink");
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<Testdetail>(entity =>
        {
            entity.HasKey(e => e.TestDetailId).HasName("PK__TESTDETA__F508594629853DA8");

            entity.ToTable("TESTDETAILS");

            entity.Property(e => e.TestDetailId).HasColumnName("TestDetailID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.TopicId).HasName("PK__TOPICS__022E0F5D1BCFB416");

            entity.ToTable("TOPICS");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Duration).HasMaxLength(200);
            entity.Property(e => e.FinishTestDate).HasColumnType("datetime");
            entity.Property(e => e.StartTestDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TopicName).HasMaxLength(200);

            entity.HasOne(d => d.Subject).WithMany(p => p.Topics)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__TOPICS__SubjectI__6E01572D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
