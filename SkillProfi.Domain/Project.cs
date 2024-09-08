﻿namespace SkillProfi.Domain;

public sealed class Project : Entity
{
	public string Title { get; set; } = string.Empty;
	public string? ImageUrl { get; set; }
	public string Description { get; set; } = string.Empty;
}
