using System;

namespace Common
{
    public static class ExtensionsMethods
    {
        public static void TryUpdate<T>(this T toUpdate, T newValue) where T : class
        {
            toUpdate = newValue ?? toUpdate;
        }

        public static string InnerExceptionMessageExtractor(this Exception ex)
        {
            return ex?.InnerException?.Message ?? string.Empty;
        }
    }
}
