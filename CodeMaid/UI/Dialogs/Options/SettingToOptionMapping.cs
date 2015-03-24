﻿#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Properties;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Options
{
    /// <summary>
    /// A model class for mapping a setting and an option together, with optional overrides for how
    /// the cast between the two occurs.
    /// </summary>
    public class SettingToOptionMapping<TS, TO> : ISettingToOptionMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingToOptionMapping{TS,TO}"/> class.
        /// </summary>
        /// <param name="settingExpression">The expression describing the setting property.</param>
        /// <param name="optionExpression">The expression describing the option property.</param>
        public SettingToOptionMapping(Expression<Func<object, TS>> settingExpression, Expression<Func<object, TO>> optionExpression)
        {
            SettingProperty = GetPropertyInfo(settingExpression);
            OptionProperty = GetPropertyInfo(optionExpression);
        }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> defining the setting property.
        /// </summary>
        public PropertyInfo SettingProperty { get; private set; }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> defining the option property.
        /// </summary>
        public PropertyInfo OptionProperty { get; private set; }

        /// <summary>
        /// Copies the value within the setting property onto the option property.
        /// </summary>
        /// <param name="optionClass">The class instance for the option property.</param>
        public void CopySettingToOption(object optionClass)
        {
            var settingValue = (TO)SettingProperty.GetValue(Settings.Default);

            // Note: No need to do an equality comparison before assignment as all options already have that through the Bindable base class.
            OptionProperty.SetValue(optionClass, settingValue);
        }

        /// <summary>
        /// Copies the value within the option property onto the setting property.
        /// </summary>
        /// <param name="optionClass">The class instance for the option property.</param>
        public void CopyOptionToSetting(object optionClass)
        {
            var optionValue = (TS)OptionProperty.GetValue(optionClass);
            var settingValue = (TS)SettingProperty.GetValue(Settings.Default);

            if (!EqualityComparer<TS>.Default.Equals(optionValue, settingValue))
            {
                SettingProperty.SetValue(Settings.Default, optionValue);
            }
        }

        /// <summary>
        /// Retrieves a <see cref="PropertyInfo"/> from the specified expression.
        /// </summary>
        /// <typeparam name="TA">The type of the source object.</typeparam>
        /// <typeparam name="TR">The type of the source property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="PropertyInfo"/> described by the expression.</returns>
        private static PropertyInfo GetPropertyInfo<TA, TR>(Expression<Func<TA, TR>> expression)
        {
            var body = (MemberExpression)expression.Body;
            var prop = (PropertyInfo)body.Member;
            return prop;
        }
    }
}