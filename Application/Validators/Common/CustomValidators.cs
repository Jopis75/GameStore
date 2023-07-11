using FluentValidation;

namespace Application.Validators.Common
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string?> PhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder)
        {
            return ruleBuilder.Matches("");
        }
    }
}
