using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Autofac;
using Leon.Core.Infrastructure;

namespace Leon.Core.Validation
{
    public class AutofacValidatorFactory : ValidatorFactoryBase
    {
        //private readonly IContainer _container;

        //public AutofacValidatorFactory(IContainer container)
        //{
        //    _container = container;
        //}

        /// <summary>
        /// 尝试创建实例，返回值为 NULL 表示不应用 FluentValidation 来做 MVC 的模型验证
        /// </summary>
        /// <param name="validatorType"></param>
        /// <returns></returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            object instance;
            if (EngineContext.Current.ContainerManager.TryResolve(validatorType, null, out instance))
            {
                return instance as IValidator;
            }
            return null;
        }
    }
}
