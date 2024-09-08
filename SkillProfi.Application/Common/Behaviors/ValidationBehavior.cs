using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace SkillProfi.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
	: IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		ValidationContext<TRequest> context = new(request);

		List<ValidationFailure> failures = (await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context, cancellationToken))))
										   .SelectMany(result => result.Errors)
										   .Where(failure => failure != null)
										   .ToList();

		if (failures.Count != 0)
		{
			throw new ValidationException(failures);
		}

		return await next();
	}
}
