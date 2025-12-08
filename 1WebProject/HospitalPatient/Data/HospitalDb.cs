
using Microsoft.EntityFrameworkCore;
using HospitalPatient.Models;

namespace HospitalPatient.Data;

public class HospitalDb : DbContext
{
    public HospitalDb(DbContextOptions<HospitalDb> options) : base(options) { }

    public DbSet<IPatient> Patients => Set<IPatient>();
    public DbSet<IDoctor> Doctors => Set<IDoctor>();
    public DbSet<IDepartment> Departments => Set<IDepartment>();
    public DbSet<IAppointment> Appointments => Set<IAppointment>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Patient
        b.Entity<IPatient>().ToTable("Patients");
        b.Entity<IPatient>().HasIndex(p => p.MedicalRecordNumber).IsUnique();
        b.Entity<IPatient>().Property(p => p.FullName).IsRequired().HasMaxLength(200);

        // Doctor
        b.Entity<IDoctor>().ToTable("Doctors");
        b.Entity<IDoctor>().HasIndex(d => d.LicenseNumber).IsUnique();
        b.Entity<IDoctor>().Property(d => d.FullName).IsRequired().HasMaxLength(200);
        b.Entity<IDoctor>()
            .HasOne(d => d.Department)
            .WithMany(dep => dep.Doctors)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Department
        b.Entity<IDepartment>().ToTable("Departments");
        b.Entity<IDepartment>().HasIndex(dep => dep.Name).IsUnique();
        b.Entity<IDepartment>().Property(dep => dep.Name).IsRequired().HasMaxLength(100);

        // Appointment
        b.Entity<IAppointment>().ToTable("Appointments");
        b.Entity<IAppointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
        b.Entity<IAppointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        b.Entity<IAppointment>().HasIndex(a => new { a.DoctorId, a.ScheduledAt });
        b.Entity<IAppointment>().Property(a => a.DurationMinutes).HasDefaultValue(30);
        b.Entity<IAppointment>().Property(a => a.CreatedAt).IsRequired();
    }
}
