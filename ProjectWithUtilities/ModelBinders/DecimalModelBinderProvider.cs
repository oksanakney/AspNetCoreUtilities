using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspNetCoreUtilities.ModelBinders
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        // Providera predi niamawe, sega ima, zawtoto kazva kakyv tip propertita triabva da vikneme
        // Tova go lipsvawe v prediwni versii
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context);
            }

            if (context.Metadata.ModelType == typeof(decimal) 
                || context.Metadata.ModelType == typeof(decimal?))
            {
                return new DecimalModelBinder();
            }

            return null; // ako varne null we byde ignoriran i we se varne kam sledvawia
        }
    }
}
