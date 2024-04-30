using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace AspNetCoreUtilities.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)// framework si go palni, kogato dojde rewuest
        {
            // home/getprice?price=3,14
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            // praznata stojnost, prazen string, ne e zadalz proverka, v http prot vsi4ko e string
            if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
            {
                decimal result = 0M; // defalt value
                bool success = false;

                try
                {
                    string strValue = valueResult.FirstValue.Trim();
                    strValue = strValue.Replace(".", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
                    strValue = strValue.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);

                    result = Convert.ToDecimal(strValue, CultureInfo.CurrentCulture);
                    success = true;
                }
                // samo fe, vseki drugi e ne iskam da obrabotvam
                catch (FormatException fe)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                }

                if (success) 
                { 
                    bindingContext.Result = ModelBindingResult.Success(result);
                }
            }

            return Task.CompletedTask;
        }
    }
}
