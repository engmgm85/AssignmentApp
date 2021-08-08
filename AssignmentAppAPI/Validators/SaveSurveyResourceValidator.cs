
using System;
using FluentValidation;
using AssignmentAppAPI.Resources;



namespace AssignmentAppAPI.Validations
{
    public class SaveSurveyResourceValidator : AbstractValidator<SaveSurveyResource>
    {
        public SaveSurveyResourceValidator()
        {
            RuleFor(m => m.FullName).NotEmpty().WithMessage("Full Name is Required").MinimumLength(10).WithMessage("Full Name must more than 10 Char.").MaximumLength(50).WithMessage("Full Name Maxium Allowed Charcter is 50 Char");
            RuleFor(m => m.Birthdate).NotEmpty().LessThanOrEqualTo(DateTime.Now);
            RuleFor(m => m.Gender).NotEmpty();
            RuleFor(m => m.NumberOfKids).GreaterThanOrEqualTo(0);

        }

    }
}
