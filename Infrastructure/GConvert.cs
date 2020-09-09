using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class GConvert
    {
        public static int ToInt32(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            return Convert.ToInt32(value);

        }
        public static int ToInt32(object? value)
        {
            if (value is null)
                return 0;

            return Convert.ToInt32(value);
        }
        public static DateTime ToDateTime(string value)
        {

            if (string.IsNullOrEmpty(value))
                return DateTime.MinValue;

            return Convert.ToDateTime(value);
        }
        public static string ToString(object value)
        {
            if (value is null)
                return string.Empty;

            return Convert.ToString(value);
        }
    }
}
