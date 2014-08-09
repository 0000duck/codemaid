﻿#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Prompts
{
    /// <summary>
    /// A view model for providing a yes no prompt.
    /// </summary>
    public class YesNoPromptViewModel : Bindable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        private BitmapSource _iconSource;

        /// <summary>
        /// Gets the icon source.
        /// </summary>
        public BitmapSource IconSource
        {
            get { return _iconSource ?? (_iconSource = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Question.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())); }
        }

        /// <summary>
        /// Gets or sets a flag indicating if the result can be remembered.
        /// </summary>
        public bool CanRemember
        {
            get { return GetPropertyValue<bool>(); }
            set
            {
                if (SetPropertyValue(value) && !CanRemember)
                {
                    Remember = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets a flag indicating if the result should be remembered.
        /// </summary>
        public bool Remember
        {
            get { return GetPropertyValue<bool>(); }
            set
            {
                if (CanRemember || !value)
                {
                    SetPropertyValue(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        public bool? DialogResult
        {
            get { return GetPropertyValue<bool?>(); }
            set { SetPropertyValue(value); }
        }

        #endregion Properties

        #region SetDialogResult Command

        private DelegateCommand _setDialogResultCommand;

        /// <summary>
        /// Gets the set dialog result command.
        /// </summary>
        public DelegateCommand SetDialogResultCommand
        {
            get { return _setDialogResultCommand ?? (_setDialogResultCommand = new DelegateCommand(OnSetDialogResultCommandExecuted)); }
        }

        /// <summary>
        /// Called when the <see cref="SetDialogResultCommand" /> is executed.
        /// </summary>
        /// <param name="parameter">The command parameter.</param>
        private void OnSetDialogResultCommandExecuted(object parameter)
        {
            bool result;
            if (bool.TryParse(parameter as string, out result))
            {
                DialogResult = result;
            }
        }

        #endregion SetDialogResult Command
    }
}