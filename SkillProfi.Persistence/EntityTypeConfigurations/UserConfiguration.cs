using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(user => user.Id);
		builder.Property(user => user.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(user => user.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(user => user.Name).HasMaxLength(FieldLimits.UserNameMaxLength).IsRequired();
		builder.Property(user => user.Email).HasMaxLength(FieldLimits.UserEmailMaxLength).IsRequired();
		builder.HasIndex(user => user.Email).IsUnique();
		builder.Property(user => user.PasswordHash).HasMaxLength(100).IsRequired();
	}
}