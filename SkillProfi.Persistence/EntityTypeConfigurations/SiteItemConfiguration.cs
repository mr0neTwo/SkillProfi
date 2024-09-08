using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class SiteItemConfiguration : IEntityTypeConfiguration<SiteItem>
{
	public void Configure(EntityTypeBuilder<SiteItem> builder)
	{
		builder.HasKey(siteItem => siteItem.Id);
		builder.Property(siteItem => siteItem.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(siteItem => siteItem.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(siteItem => siteItem.Key).HasMaxLength(FieldLimits.SiteItemKexMaxLength).IsRequired();
		builder.Property(siteItem => siteItem.Title).HasMaxLength(FieldLimits.SiteItemTitleMaxLength).IsRequired();
	}
}
