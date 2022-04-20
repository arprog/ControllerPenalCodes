using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.Maps;
using Microsoft.EntityFrameworkCore;

namespace ControllerPenalCodes
{
	public class DBContext : DbContext
	{
		public DbSet<CriminalCode> CriminalCodes { get; set; }

		public DbSet<Status> Status { get; set; }

		public DbSet<User> Users { get; set; }

		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CriminalCode>().Map();
			modelBuilder.Entity<Status>().Map();
			modelBuilder.Entity<User>().Map();

			base.OnModelCreating(modelBuilder);
		}
	}
}
