using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace vetappback.Utilities
{
    public class TypeBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var probpertyName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(probpertyName);
            if (value == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(value.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch (System.Exception)
            {

                bindingContext.ModelState.TryAddModelError(probpertyName, "Wrong value");
            }

            return Task.CompletedTask;

        }
    }
}