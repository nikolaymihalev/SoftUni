using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace AspNetCoreAdvancedDemo.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            if(valueResult!= ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue)) 
            {
                decimal result = 0m;
                bool success = false;

                try
                {
                    string strValue = valueResult.FirstValue.Trim();
                    strValue = strValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    strValue = strValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    result = Convert.ToDecimal(strValue);
                    success = true;
                }
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }
            }

            return Task.CompletedTask;
        }
    }
}
