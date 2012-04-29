#region CodeMaid is Copyright 2007-2012 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2012 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Properties;

namespace SteveCadwallader.CodeMaid.Options.Cleaning
{
    /// <summary>
    /// The view model for cleaning file types options.
    /// </summary>
    public class CleaningFileTypesViewModel : OptionsPageViewModel
    {
        #region Base Members

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header
        {
            get { return "File Types"; }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            ExclusionExpression = Settings.Default.Cleaning_ExclusionExpression;
            IncludeCPlusPlus = Settings.Default.Cleaning_IncludeCPlusPlus;
            IncludeCSharp = Settings.Default.Cleaning_IncludeCSharp;
            IncludeCSS = Settings.Default.Cleaning_IncludeCSS;
            IncludeHTML = Settings.Default.Cleaning_IncludeHTML;
            IncludeJavaScript = Settings.Default.Cleaning_IncludeJavaScript;
            IncludeXAML = Settings.Default.Cleaning_IncludeXAML;
            IncludeXML = Settings.Default.Cleaning_IncludeXML;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public override void SaveSettings()
        {
            Settings.Default.Cleaning_ExclusionExpression = ExclusionExpression;
            Settings.Default.Cleaning_IncludeCPlusPlus = IncludeCPlusPlus;
            Settings.Default.Cleaning_IncludeCSharp = IncludeCSharp;
            Settings.Default.Cleaning_IncludeCSS = IncludeCSS;
            Settings.Default.Cleaning_IncludeHTML = IncludeHTML;
            Settings.Default.Cleaning_IncludeJavaScript = IncludeJavaScript;
            Settings.Default.Cleaning_IncludeXAML = IncludeXAML;
            Settings.Default.Cleaning_IncludeXML = IncludeXML;
        }

        #endregion Base Members

        #region Options

        private string _exclusionExpression;

        /// <summary>
        /// Gets or sets the expression for files to exclude.
        /// </summary>
        public string ExclusionExpression
        {
            get { return _exclusionExpression; }
            set
            {
                if (_exclusionExpression != value)
                {
                    _exclusionExpression = value;
                    NotifyPropertyChanged("ExclusionExpression");
                }
            }
        }

        private bool _includeCPlusPlus;

        /// <summary>
        /// Gets or sets the flag indicating if C++ files should be included.
        /// </summary>
        public bool IncludeCPlusPlus
        {
            get { return _includeCPlusPlus; }
            set
            {
                if (_includeCPlusPlus != value)
                {
                    _includeCPlusPlus = value;
                    NotifyPropertyChanged("IncludeCPlusPlus");
                }
            }
        }

        private bool _includeCSharp;

        /// <summary>
        /// Gets or sets the flag indicating if C# files should be included.
        /// </summary>
        public bool IncludeCSharp
        {
            get { return _includeCSharp; }
            set
            {
                if (_includeCSharp != value)
                {
                    _includeCSharp = value;
                    NotifyPropertyChanged("IncludeCSharp");
                }
            }
        }

        private bool _includeCSS;

        /// <summary>
        /// Gets or sets the flag indicating if CSS files should be included.
        /// </summary>
        public bool IncludeCSS
        {
            get { return _includeCSS; }
            set
            {
                if (_includeCSS != value)
                {
                    _includeCSS = value;
                    NotifyPropertyChanged("IncludeCSS");
                }
            }
        }

        private bool _includeHTML;

        /// <summary>
        /// Gets or sets the flag indicating if HTML files should be included.
        /// </summary>
        public bool IncludeHTML
        {
            get { return _includeHTML; }
            set
            {
                if (_includeHTML != value)
                {
                    _includeHTML = value;
                    NotifyPropertyChanged("IncludeHTML");
                }
            }
        }

        private bool _includeJavaScript;

        /// <summary>
        /// Gets or sets the flag indicating if JavaScript files should be included.
        /// </summary>
        public bool IncludeJavaScript
        {
            get { return _includeJavaScript; }
            set
            {
                if (_includeJavaScript != value)
                {
                    _includeJavaScript = value;
                    NotifyPropertyChanged("IncludeJavaScript");
                }
            }
        }

        private bool _includeXAML;

        /// <summary>
        /// Gets or sets the flag indicating if XAML files should be included.
        /// </summary>
        public bool IncludeXAML
        {
            get { return _includeXAML; }
            set
            {
                if (_includeXAML != value)
                {
                    _includeXAML = value;
                    NotifyPropertyChanged("IncludeXAML");
                }
            }
        }

        private bool _includeXML;

        /// <summary>
        /// Gets or sets the flag indicating if XML files should be included.
        /// </summary>
        public bool IncludeXML
        {
            get { return _includeXML; }
            set
            {
                if (_includeXML != value)
                {
                    _includeXML = value;
                    NotifyPropertyChanged("IncludeXML");
                }
            }
        }

        #endregion Options
    }
}