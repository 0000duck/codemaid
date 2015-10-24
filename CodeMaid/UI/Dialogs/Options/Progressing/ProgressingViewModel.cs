#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Options.Progressing
{
    /// <summary>
    /// The view model for progressing options.
    /// </summary>
    public class ProgressingViewModel : OptionsPageViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressingViewModel" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <param name="activeSettings">The active settings.</param>
        public ProgressingViewModel(CodeMaidPackage package, Settings activeSettings)
            : base(package, activeSettings)
        {
            Mappings = new SettingsToOptionsList(ActiveSettings, this)
            {
                new SettingToOptionMapping<bool, bool>(x => ActiveSettings.Progressing_HideBuildProgressOnBuildStop, x => HideBuildProgressOnBuildStop),
                new SettingToOptionMapping<bool, bool>(x => ActiveSettings.Progressing_ShowBuildProgressOnBuildStart, x => ShowBuildProgressOnBuildStart),
                new SettingToOptionMapping<bool, bool>(x => ActiveSettings.Progressing_ShowProgressOnWindowsTaskbar, x => ShowProgressOnWindowsTaskbar)
            };
        }

        #endregion Constructors

        #region Overrides of OptionsPageViewModel

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header
        {
            get { return "Progressing"; }
        }

        #endregion Overrides of OptionsPageViewModel

        #region Options

        /// <summary>
        /// Gets or sets a flag indicating if build progress should be hidden when a build stops.
        /// </summary>
        public bool HideBuildProgressOnBuildStop
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets a flag indicating if build progress should be shown when a build starts.
        /// </summary>
        public bool ShowBuildProgressOnBuildStart
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets a flag indicating if build progress should be shown on the windows taskbar.
        /// </summary>
        public bool ShowProgressOnWindowsTaskbar
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        #endregion Options
    }
}