using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect
    {
        //Aspect : Metotun başında sonunda hata verdiğinde çalışacak yapı
        //Interception : Araya Girmek

        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //defensive coding : Savunma odaklı kodlama 
            //asagidaki if yazmasakta çalışır 
            //ama type'in IValidator türünden olmasını istiyoruz

            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu Bir Doğrulama Sınıfı Değildir");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
