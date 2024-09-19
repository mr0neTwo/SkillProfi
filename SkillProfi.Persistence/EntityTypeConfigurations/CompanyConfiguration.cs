using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder.HasKey(company => company.Id);
		builder.Property(company => company.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(company => company.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(company => company.Name).HasMaxLength(FieldLimits.CompanyNameMaxLength).IsRequired();
		builder.Property(company => company.Email).HasMaxLength(FieldLimits.CompanyEmailMaxLength).IsRequired();
		builder.Property(company => company.PhoneNumber).HasMaxLength(FieldLimits.CompanyPhoneMaxLength).IsRequired();
		builder.Property(company => company.Address).HasMaxLength(FieldLimits.CompanyAddressMaxLength).IsRequired();
		builder.Property(company => company.DirectorName).HasMaxLength(FieldLimits.CompanyDirectorNameMaxLength).IsRequired();
		builder.Property(company => company.MapLink).HasMaxLength(FieldLimits.CompanyMapLinkMaxLength).IsRequired(false);
	}
}
