using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControllerPenalCodes.Models.Maps
{
	public static class StatusMap
	{
		public static void Map(this EntityTypeBuilder<Status> entity)
		{
			entity.ToTable("Status");

			entity.HasKey(status => status.Id);

			entity.Property(status => status.Id)
				.HasColumnName("Id")
				.IsRequired();

			entity.Property(status => status.Name)
				.HasColumnName("Name")
				.IsRequired();
		}
	}
}
