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

using System.Windows;
using System.Windows.Controls;
using SteveCadwallader.CodeMaid.CodeItems;

namespace SteveCadwallader.CodeMaid.Spade
{
    /// <summary>
    /// A template selector for code item tool tips.
    /// </summary>
    public class CodeItemToolTipTemplateSelector : DataTemplateSelector
    {
        #region Properties

        /// <summary>
        /// Gets or sets the constructor data template.
        /// </summary>
        public DataTemplate ConstructorDataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the method data template.
        /// </summary>
        public DataTemplate MethodDataTemplate { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate"/> based on custom logic.
        /// </summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate"/> or null. The default value is null.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var codeItem = item as BaseCodeItem;
            if (codeItem != null)
            {
                switch (codeItem.Kind)
                {
                    case KindCodeItem.Constructor:
                        return ConstructorDataTemplate;

                    case KindCodeItem.Method:
                        return MethodDataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }

        #endregion Methods
    }
}