﻿#region CodeMaid is Copyright 2007-2011 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2011 Steve Cadwallader.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using SteveCadwallader.CodeMaid.CodeItems;

namespace SteveCadwallader.CodeMaid.Quidnunc
{
    /// <summary>
    /// Converts a code item into an attribute string.
    /// </summary>
    public class CodeItemToAttributeStringConverter : IValueConverter
    {
        /// <summary>
        /// A default instance of the <see cref="CodeItemToAttributeStringConverter"/>.
        /// </summary>
        public static CodeItemToAttributeStringConverter Default = new CodeItemToAttributeStringConverter();

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CodeItemMethod)
            {
                return GenerateAttributeStringForMethod((CodeItemMethod)value);
            }

            if (value is CodeItemProperty)
            {
                return GenerateAttributeStringForProperty((CodeItemProperty)value);
            }

            return null;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates an attribute string for the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The generated string.</returns>
        private static string GenerateAttributeStringForMethod(CodeItemMethod method)
        {
            var strings = new List<string>();

            if (method.IsStatic)
            {
                strings.Add("s");
            }

            return string.Join(", ", strings.ToArray());
        }

        /// <summary>
        /// Generates an attribute string for the specified property.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>The generated string.</returns>
        private static object GenerateAttributeStringForProperty(CodeItemProperty property)
        {
            var strings = new List<string>();

            if (property.CodeProperty.Getter != null) // Readable
            {
                strings.Add("r");
            }

            if (property.CodeProperty.Setter != null) // Writeable
            {
                strings.Add("w");
            }

            return string.Join(", ", strings.ToArray());
        }
    }
}