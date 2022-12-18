using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    //genellikle bu tarz toollar static olarak yapılır
    //c#'da static bir sınıfın metotlarıda statik olamalı java da bu durum gerekli değildir
    public static class ValidationTool
    {
        public static void Validate(IValidator validator , object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
