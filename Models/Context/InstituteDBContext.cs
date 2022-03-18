using System;
using Microsoft.EntityFrameworkCore;

using Models.InstituteModel;

#nullable disable

namespace FromDbModels.Context
{
    public partial class InstituteDBContext : DbContext
    {
        public InstituteDBContext()
        {
        }

        public InstituteDBContext(DbContextOptions<InstituteDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<EducationGrade> EducationGrades { get; set; }
        public virtual DbSet<Expenditure> Expenditures { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Password> Passwords { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SalaryPayment> SalaryPayments { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<StudentPayment> StudentPayments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-U3JLLJF\\MSQ2019;Initial Catalog=InstituteDB ; integrated security = true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Persian_100_CI_AI");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.HasIndex(e => e.PersonId, "C_Unique_Account")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.HasOne(d => d.Person)
                    .WithOne(p => p.Account)
                    .HasForeignKey<Account>(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Person");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Class__A25C5AA61F3E253F");

                entity.ToTable("Class", "Education");

                entity.Property(e => e.Describtion).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course", "Education");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fee).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.StartingDay).HasColumnType("date");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.TimeInWeek)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.ClassCodeNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ClassCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__ClassCod__4D94879B");

                entity.HasOne(d => d.LessonCodeNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.LessonCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__LessonCo__4BAC3F29");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Course__TeacherI__4CA06362");
            });

            modelBuilder.Entity<EducationGrade>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Educatio__A25C5AA6EAF2A8CA");

                entity.ToTable("EducationGrade", "Education");

                entity.Property(e => e.Code).ValueGeneratedOnAdd();

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Expenditure>(entity =>
            {
                entity.ToTable("Expenditure", "Payment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Lesson__A25C5AA6A5C6DF3C");

                entity.ToTable("Lesson", "Education");

                entity.Property(e => e.Code).ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.EducationGradeCodeNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.EducationGradeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Lesson__Educatio");
            });

            modelBuilder.Entity<Password>(entity =>
            {
                entity.ToTable("Password");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.HashPassword)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Passwords)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Password_Account");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Person");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Age).HasComputedColumnSql("(datediff(year,[Birthdate],getdate()))", false);

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(101)
                    .HasComputedColumnSql("(([FirstName]+'')+[Lastname])", false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.NationalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.EducationGradecodeNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.EducationGradecode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Person_EducationGrade");

                entity.HasOne(d => d.RoleCodeNavigation)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RoleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Person__RoleCode__5CD6CB2B");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Role__A25C5AA6577DBDEF");

                entity.ToTable("Role", "Person");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(70);
            });

            modelBuilder.Entity<SalaryPayment>(entity =>
            {
                entity.ToTable("SalaryPayment", "Payment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.RemainAmount).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.SalaryPayments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalaryPay__Emplo__5812160E");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("Student_course", "Education");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_course_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Student_c__Stude__5070F446");
            });

            modelBuilder.Entity<StudentPayment>(entity =>
            {
                entity.ToTable("StudentPayment", "Payment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CourseId).HasColumnName("CourseID");

                entity.Property(e => e.PaidAmount).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.RemainAmount).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentPayments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentPa__Cours__5535A963");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentPayments)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StudentPa__Stude__5441852A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
