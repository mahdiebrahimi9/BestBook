﻿using Common.Application.Validation.FluentValidations;
using Common.Application.Validation;
using FluentValidation;

namespace Shop.Application.Products.Edit
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        public EditProductCommandValidator()
        {
            RuleFor(r => r.Title)
               .NotEmpty()
               .WithMessage(ValidationMessages.required("عنوان"));

            RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("توضیحات"));

            RuleFor(r => r.Slug)
            .NotEmpty()
            .WithMessage(ValidationMessages.required("slug"));

            RuleFor(r => r.ImageFile)
                .JustImageFile();
        }
    }
}
