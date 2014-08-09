#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Properties;
using SteveCadwallader.CodeMaid.UI.Enumerations;

namespace SteveCadwallader.CodeMaid.UI.Dialogs.Options.Cleaning
{
    /// <summary>
    /// The view model for cleaning remove options.
    /// </summary>
    public class CleaningRemoveViewModel : OptionsPageViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CleaningRemoveViewModel" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        public CleaningRemoveViewModel(CodeMaidPackage package)
            : base(package)
        {
        }

        #endregion Constructors

        #region Overrides of OptionsPageViewModel

        /// <summary>
        /// Gets the header.
        /// </summary>
        public override string Header
        {
            get { return "Remove"; }
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            RemoveBlankLinesAfterAttributes = Settings.Default.Cleaning_RemoveBlankLinesAfterAttributes;
            RemoveBlankLinesAfterOpeningBraces = Settings.Default.Cleaning_RemoveBlankLinesAfterOpeningBrace;
            RemoveBlankLinesAtBottom = Settings.Default.Cleaning_RemoveBlankLinesAtBottom;
            RemoveBlankLinesAtTop = Settings.Default.Cleaning_RemoveBlankLinesAtTop;
            RemoveBlankLinesBeforeClosingBraces = Settings.Default.Cleaning_RemoveBlankLinesBeforeClosingBrace;
            RemoveBlankLinesBeforeClosingTags = Settings.Default.Cleaning_RemoveBlankLinesBeforeClosingTags;
            RemoveBlankLinesBetweenChainedStatements = Settings.Default.Cleaning_RemoveBlankLinesBetweenChainedStatements;
            RemoveBlankSpacesBeforeClosingAngleBrackets = Settings.Default.Cleaning_RemoveBlankSpacesBeforeClosingAngleBrackets;
            RemoveEndOfFileTrailingNewLine = Settings.Default.Cleaning_RemoveEndOfFileTrailingNewLine;
            RemoveEndOfLineWhitespace = Settings.Default.Cleaning_RemoveEndOfLineWhitespace;
            RemoveMultipleConsecutiveBlankLines = Settings.Default.Cleaning_RemoveMultipleConsecutiveBlankLines;
            RemoveRegions = (NoneEmptyAll)Settings.Default.Cleaning_RemoveRegions;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public override void SaveSettings()
        {
            Settings.Default.Cleaning_RemoveBlankLinesAfterAttributes = RemoveBlankLinesAfterAttributes;
            Settings.Default.Cleaning_RemoveBlankLinesAfterOpeningBrace = RemoveBlankLinesAfterOpeningBraces;
            Settings.Default.Cleaning_RemoveBlankLinesAtBottom = RemoveBlankLinesAtBottom;
            Settings.Default.Cleaning_RemoveBlankLinesAtTop = RemoveBlankLinesAtTop;
            Settings.Default.Cleaning_RemoveBlankLinesBeforeClosingBrace = RemoveBlankLinesBeforeClosingBraces;
            Settings.Default.Cleaning_RemoveBlankLinesBeforeClosingTags = RemoveBlankLinesBeforeClosingTags;
            Settings.Default.Cleaning_RemoveBlankLinesBetweenChainedStatements = RemoveBlankLinesBetweenChainedStatements;
            Settings.Default.Cleaning_RemoveBlankSpacesBeforeClosingAngleBrackets = RemoveBlankSpacesBeforeClosingAngleBrackets;
            Settings.Default.Cleaning_RemoveEndOfFileTrailingNewLine = RemoveEndOfFileTrailingNewLine;
            Settings.Default.Cleaning_RemoveEndOfLineWhitespace = RemoveEndOfLineWhitespace;
            Settings.Default.Cleaning_RemoveMultipleConsecutiveBlankLines = RemoveMultipleConsecutiveBlankLines;
            Settings.Default.Cleaning_RemoveRegions = (int)RemoveRegions;
        }

        #endregion Overrides of OptionsPageViewModel

        #region Options

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed after attributes.
        /// </summary>
        public bool RemoveBlankLinesAfterAttributes
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed after an opening brace.
        /// </summary>
        public bool RemoveBlankLinesAfterOpeningBraces
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed at the bottom of the file.
        /// </summary>
        public bool RemoveBlankLinesAtBottom
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed at the top of the file.
        /// </summary>
        public bool RemoveBlankLinesAtTop
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed before a closing brace.
        /// </summary>
        public bool RemoveBlankLinesBeforeClosingBraces
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed before a closing tag.
        /// </summary>
        public bool RemoveBlankLinesBeforeClosingTags
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank lines should be removed between chained statements.
        /// </summary>
        public bool RemoveBlankLinesBetweenChainedStatements
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if blank spaces should be removed before a closing
        /// angle bracket.
        /// </summary>
        public bool RemoveBlankSpacesBeforeClosingAngleBrackets
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if the end of file trailing newline should be removed.
        /// </summary>
        public bool RemoveEndOfFileTrailingNewLine
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if end of line whitespace should be removed.
        /// </summary>
        public bool RemoveEndOfLineWhitespace
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the flag indicating if multiple consecutive blank lines should be removed.
        /// </summary>
        public bool RemoveMultipleConsecutiveBlankLines
        {
            get { return GetPropertyValue<bool>(); }
            set { SetPropertyValue(value); }
        }

        /// <summary>
        /// Gets or sets the options for removing regions.
        /// </summary>
        public NoneEmptyAll RemoveRegions
        {
            get { return GetPropertyValue<NoneEmptyAll>(); }
            set { SetPropertyValue(value); }
        }

        #endregion Options
    }
}