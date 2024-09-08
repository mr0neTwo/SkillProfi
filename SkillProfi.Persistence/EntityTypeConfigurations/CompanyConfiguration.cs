using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
	public void Configure(EntityTypeBuilder<Company> builder)
	{
		builder.HasKey(company => company.Id);
		builder.Property(company => company.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(company => company.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(company => company.Name).HasMaxLength(50).IsRequired();
		builder.Property(company => company.Email).HasMaxLength(30).IsRequired();
		builder.Property(company => company.PhoneNumber).HasMaxLength(30).IsRequired();
		builder.Property(company => company.Address).HasMaxLength(100).IsRequired();
		builder.Property(company => company.DirectorName).HasMaxLength(50).IsRequired();
	}
}
