namespace SkillProfi.Domain;

public abstract class Entity
{
	public int Id { get; set; }
	public DateTime CreationDate { get; set; }
	public DateTime? UpdatingDate { get; set; }
	public int CreatedById { get; set; }
	public int UpdatedById { get; set; }
}
