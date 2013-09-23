using System;

namespace Tera.Extensions
{
    internal static class ClassExtensions
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult Get<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult defaultValue)
            where TInput : class
        {
            return o == null ? defaultValue : evaluator(o);
        }
    }
}
