using System;
using System.Reflection;
using System.Threading.Tasks;

namespace TestAndBenchmark
{
    /// <summary>
    /// Invokes a discovered converter method via reflection, regardless of whether
    /// it is generic, takes extra optional parameters, or returns a Task.
    /// </summary>
    public static class ConverterInvoker
    {
        public static async Task<object?> InvokeAsync(ConverterDescriptor descriptor, object sourceInstance)
        {
            var method = descriptor.Method;

            if (method.IsGenericMethodDefinition)
            {
                var genericArguments = descriptor.Attribute.GenericArguments
                    ?? throw new InvalidOperationException(
                        $"{descriptor.DisplayName}: generic converter requires GenericArguments on [ImageConverter].");
                method = method.MakeGenericMethod(genericArguments);
            }

            var parameters = method.GetParameters();
            var arguments = new object?[parameters.Length];
            arguments[0] = sourceInstance;
            for (int i = 1; i < parameters.Length; i++)
                arguments[i] = Type.Missing;

            var raw = method.Invoke(null, arguments);

            if (raw is Task task)
            {
                await task.ConfigureAwait(true);
                var resultProperty = task.GetType().GetProperty("Result");
                return resultProperty?.GetValue(task);
            }

            return raw;
        }
    }
}
