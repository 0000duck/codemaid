﻿#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Properties;
using SteveCadwallader.CodeMaid.UI.Enumerations;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Options.General
{
    /// <summary>
    /// The view model for general options.
    /// </summary>
    public class GeneralViewModel : OptionsPageViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralViewModel" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        public GeneralViewModel(CodeMaidPackage package)
            : base(package)
        {
            Mappings = new SettingsToOptionsList(this)
            {
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_CacheFiles, x => CacheFiles),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_DiagnosticsMode, x => DiagnosticsMode),
                new SettingToOptionMapping<string, string>(x => Settings.Default.General_Font, x => Font),
                new SettingToOptionMapping<int, IconSetMode>(x => Settings.Default.General_IconSet, x => IconSetMode),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_LoadModelsAsynchronously, x => LoadModelsAsynchronously),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_Multithread, x => Multithread),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_ShowStartPageOnSolutionClose, x => ShowStartPageOnSolutionClose),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_SkipUndoTransactionsDuringAutoCleanupOnSave, x => SkipUndoTransactionsDuringAutoCleanupOnSave),
                new SettingToOptionMapping<bool, bool>(x => Settings.Default.General_UseUndoTransactions, x => UseUndoTransactions)
            };
        }

        #endregion Constructors

        #region Overrides of OptionsPageViewModel

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header
        {
            get { return "General"; }
        }

        #endregion Overrides of OptionsPageViewModel

        #region Options

        /// <summary>
        /// Gets or sets the flag indicating if files should be cached.
        /// </summary>
        public bool CacheFiles
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if diagnostics mode should be enabled.
        /// </summary>
        public bool DiagnosticsMode
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the string representing the font.
        /// </summary>
        public string Font
        {
            get { return GetPropertyValue<string>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets which icon set should be utilized.
        /// </summary>
        public IconSetMode IconSetMode
        {
            get { return GetPropertyValue<IconSetMode>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if models can be loaded asynchronously.
        /// </summary>
        public bool LoadModelsAsynchronously
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if multithreading should be utilized.
        /// </summary>
        public bool Multithread
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if the start page should be shown when the solution is closed.
        /// </summary>
        public bool ShowStartPageOnSolutionClose
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if undo transactions should not be used during auto
        /// cleanup on save.
        /// </summary>
        public bool SkipUndoTransactionsDuringAutoCleanupOnSave
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        public ThemeMode ThemeMode
        {
            get { return GetPropertyValue<ThemeMode>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets a flag indicating if undo transactions should be utilized.
        /// </summary>
        public bool UseUndoTransactions
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        #endregion Options
    }
}