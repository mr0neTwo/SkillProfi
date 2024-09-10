﻿using MediatR;

namespace SkillProfi.Application.CQRS.Posts.Queries.GetList;

public class GetPostListQuery : IRequest<GetPostListResponse>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}