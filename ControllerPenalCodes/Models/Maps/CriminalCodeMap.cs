using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControllerPenalCodes.Models.Maps
{
	public static class CriminalCodeMap
	{
		public static void Map(this EntityTypeBuilder<CriminalCode> entity)
		{
			entity.ToTable("CriminalCode");

			entity.HasKey(criminalCode => criminalCode.Id);

			entity.HasOne(criminalCode => criminalCode.Status)
				.WithMany()
				.HasForeignKey(criminalCode => criminalCode.StatusId);

			entity.HasOne(criminalCode => criminalCode.CreateUser)
				.WithMany()
				.HasForeignKey(criminalCode => criminalCode.CreateUserId);

			entity.HasOne(criminalCode => criminalCode.UpdateUser)
				.WithMany()
				.HasForeignKey(criminalCode => criminalCode.UpdateUserId);

			entity.Property(criminalCode => criminalCode.Id)
				.HasColumnName("Id")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.Name)
				.HasColumnName("Name")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.Description)
				.HasColumnName("Description")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.Penalty)
				.HasColumnName("Penalty")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.StatusId)
				.HasColumnName("StatusId")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.CreateDate)
				.HasColumnName("CreateDate")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.UpdateDate)
				.HasColumnName("UpdateDate")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.CreateUserId)
				.HasColumnName("CreateUserId")
				.IsRequired();

			entity.Property(criminalCode => criminalCode.UpdateUserId)
				.HasColumnName("UpdateUserId")
				.IsRequired();
		}
	}
}
