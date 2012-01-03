using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projector.Site.Helpers.ModelBinders
{
    public class GuidModelBinder : IModelBinder 
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var parameter = bindingContext
                .ValueProvider
                .GetValue(bindingContext.ModelName);

            return Guid.Parse(parameter.AttemptedValue);
        }
    }
}