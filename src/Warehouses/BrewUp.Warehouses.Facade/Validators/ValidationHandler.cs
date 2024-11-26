using FluentValidation;

namespace BrewUp.Warehouses.Facade.Validators;

public sealed class ValidationHandler
{
	public bool IsValid { get; private set; } = true;
	public Dictionary<string, string[]> Errors { get; private set; } = new();

	public async Task ValidateAsync<T>(IValidator<T> validator, T validateMe) where T : class
	{
		var validationResult = await validator.ValidateAsync(validateMe);
		if (validationResult.IsValid)
		{
			IsValid = true;
			Errors = new Dictionary<string, string[]>();
			return;
		}

		Errors = validationResult.Errors
			.GroupBy(e => e.PropertyName)
			.ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());

		IsValid = false;
	}
}