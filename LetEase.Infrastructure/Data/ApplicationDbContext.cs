using LetEase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LetEase.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Property> Properties { get; set; }
		public DbSet<UserProperty> UserProperties { get; set; }
		public DbSet<Room> Rooms { get; set; }
		public DbSet<Amenity> Amenities { get; set; }
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Images> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Contract>(entity =>
			{
				entity.HasOne(c => c.Client)
					.WithMany(cl => cl.Contracts)
					.HasForeignKey(c => c.ClientId)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(c => c.Company)
					.WithMany()
					.HasForeignKey(c => c.companyID)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(c => c.Property)
					.WithMany()
					.HasForeignKey(c => c.PropertyId)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(c => c.Room)
					.WithMany()
					.HasForeignKey(c => c.RoomId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<User>()
				.HasOne(u => u.Company)
				.WithMany(c => c.Users)
				.HasForeignKey(u => u.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<User>()
           .Property(u => u.Id)
           .HasConversion<string>();

			modelBuilder.Entity<Property>()
				.HasOne(p => p.Company)
				.WithMany(c => c.Properties)
				.HasForeignKey(p => p.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserProperty>()
				.HasKey(up => new { up.UserId, up.PropertyId });

			modelBuilder.Entity<UserProperty>()
				.HasOne(up => up.User)
				.WithMany(u => u.ManagedProperties)
				.HasForeignKey(up => up.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<UserProperty>()
				.HasOne(up => up.Property)
				.WithMany(p => p.Managers)
				.HasForeignKey(up => up.PropertyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Room>()
				.HasOne(r => r.Property)
				.WithMany(p => p.Rooms)
				.HasForeignKey(r => r.PropertyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Client>(entity =>
			{
				entity.HasMany(c => c.Contracts)
					.WithOne(co => co.Client)
					.HasForeignKey(co => co.ClientId)
					.OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Company>(entity =>
			{
				entity.HasMany(c => c.Properties)
					.WithOne(p => p.Company)
					.HasForeignKey(p => p.CompanyId)
					.OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(c => c.Users)
					.WithOne(u => u.Company)
					.HasForeignKey(u => u.CompanyId)
					.OnDelete(DeleteBehavior.Restrict);
			});
		}
	}
}

//using LetEase.Domain.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace LetEase.Infrastructure.Data
//{
//	public class ApplicationDbContext : DbContext
//	{
//		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//			: base(options)
//		{
//		}

//		public DbSet<Company> Companies { get; set; }
//		public DbSet<Client> Clients { get; set; }
//		public DbSet<Property> Properties { get; set; }
//		public DbSet<Room> Rooms { get; set; }
//		public DbSet<Contract> Contracts { get; set; }
//		public DbSet<Payment> Payments { get; set; }
//		public DbSet<AIVerification> AIVerifications { get; set; }
//		public DbSet<Document> Documents { get; set; }
//		public DbSet<EmergencyContact> EmergencyContacts { get; set; }
//		public DbSet<Reference> References { get; set; }
//		public DbSet<User> Users { get; set; }
//		public DbSet<Address> Addresses { get; set; }
//		public DbSet<Amenity> Amenities { get; set; }
//		public DbSet<Booking> Bookings { get; set; }
//		public DbSet<ActivityLog> ActivityLogs { get; set; }

//		protected override void OnModelCreating(ModelBuilder modelBuilder)
//		{
//			base.OnModelCreating(modelBuilder);

//			modelBuilder.Entity<Contract>(entity =>
//			{
//				entity.HasOne(c => c.Client)
//					.WithMany(cl => cl.Contracts)
//					.HasForeignKey(c => c.ClientId)
//					.OnDelete(DeleteBehavior.Restrict);

//				entity.HasOne(c => c.Company)
//					.WithMany()
//					.HasForeignKey(c => c.companyID)
//					.OnDelete(DeleteBehavior.Restrict);

//				entity.HasOne(c => c.Property)
//					.WithMany()
//					.HasForeignKey(c => c.PropertyId)
//					.OnDelete(DeleteBehavior.Restrict);

//				entity.HasOne(c => c.Room)
//					.WithMany()
//					.HasForeignKey(c => c.RoomId)
//					.OnDelete(DeleteBehavior.Restrict);
//			});

//			modelBuilder.Entity<Property>(entity =>
//			{
//				entity.HasOne(p => p.Company)
//					.WithMany(c => c.Properties)
//					.HasForeignKey(p => p.CompanyId)
//					.OnDelete(DeleteBehavior.Restrict);
//			});

//			modelBuilder.Entity<Room>(entity =>
//			{
//				entity.HasOne(r => r.Property)
//					.WithMany(p => p.Rooms)
//					.HasForeignKey(r => r.PropertyId)
//					.OnDelete(DeleteBehavior.Restrict);
//			});

//			modelBuilder.Entity<Client>(entity =>
//			{
//				entity.HasMany(c => c.Contracts)
//					.WithOne(co => co.Client)
//					.HasForeignKey(co => co.ClientId)
//					.OnDelete(DeleteBehavior.Restrict);
//			});

//			modelBuilder.Entity<Company>(entity =>
//			{
//				entity.HasMany(c => c.Properties)
//					.WithOne(p => p.Company)
//					.HasForeignKey(p => p.CompanyId)
//					.OnDelete(DeleteBehavior.Restrict);

//				entity.HasMany(c => c.Users)
//					.WithOne(u => u.Company)
//					.HasForeignKey(u => u.CompanyId)
//					.OnDelete(DeleteBehavior.Restrict);
//			});

//			// Add any other entity configurations here
//		}
//	}
//}
