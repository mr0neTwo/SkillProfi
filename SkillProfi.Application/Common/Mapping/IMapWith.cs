using AutoMapper;

namespace SkillProfi.Application.Common.Mapping;

public interface IMapWith<T>
{
	public void Mapping(Profile profile);
}