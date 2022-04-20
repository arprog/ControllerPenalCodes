using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControllerPenalCodes.Models.Maps
{
	public static class UserMap
	{
		public static void Map(this EntityTypeBuilder<User> entity)
		{
			entity.ToTable("User");

			entity.HasKey(user => user.Id);

			entity.Property(user => user.Id)
				.HasColumnName("Id")
				.IsRequired();

			entity.Property(user => user.UserName)
				.HasColumnName("UserName")
				.IsRequired();

			entity.Property(user => user.Password)
				.HasColumnName("Password")
				.IsRequired();
		}
	}
}
