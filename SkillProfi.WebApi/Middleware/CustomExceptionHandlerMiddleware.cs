﻿using System.Net;
using System.Text.Json;
using FluentValidation;
using SkillProfi.Application.Common.Exceptions;

namespace SkillProfi.WebApi.Middleware;

public sealed class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception exception)
		{
			await HandleExceptionAsync(context, exception);
		}
	}

	public Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var code = HttpStatusCode.InternalServerError;
		var result = string.Empty;

		switch (exception)
		{
			case ValidationException validationException:
				code = HttpStatusCode.BadRequest;
				result = JsonSerializer.Serialize(validationException.Errors);
				
				break;
			
			case NotFoundException:
				code = HttpStatusCode.NotFound;
				
				break;
		}

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)code;

		if (result == string.Empty)
		{
			result = JsonSerializer.Serialize(new { errpr = exception.Message });
		}
		
		return context.Response.WriteAsync(result);
	}
}