using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillProfi.Application.Common;
using SkillProfi.Domain;

namespace SkillProfi.Persistence.EntityTypeConfigurations;

public sealed class ClientRequestConfiguration : IEntityTypeConfiguration<ClientRequest>
{
	public void Configure(EntityTypeBuilder<ClientRequest> builder)
	{
		builder.HasKey(clientRequest => clientRequest.Id);
		builder.Property(clientRequest => clientRequest.CreationDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired();
		builder.Property(clientRequest => clientRequest.UpdatingDate).HasColumnType(PostgresSqlTypes.Timestamp).IsRequired(false);
		builder.Property(clientRequest => clientRequest.ClientName).HasMaxLength(FieldLimits.ClientRequestNameMaxLength).IsRequired();
		builder.Property(clientRequest => clientRequest.ClientEmail).HasMaxLength(FieldLimits.ClientRequestEmailMaxLength).IsRequired();
		builder.Property(clientRequest => clientRequest.Message).HasMaxLength(FieldLimits.ClientRequestMessageMaxLength).IsRequired();
		builder.Property(clientRequest => clientRequest.Status);
	}
}
