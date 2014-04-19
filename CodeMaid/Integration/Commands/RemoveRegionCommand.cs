﻿#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using EnvDTE;
using SteveCadwallader.CodeMaid.Logic.Reorganizing;
using System.ComponentModel.Design;

namespace SteveCadwallader.CodeMaid.Integration.Commands
{
    /// <summary>
    /// A command that provides for removing region(s).
    /// </summary>
    internal class RemoveRegionCommand : BaseCommand
    {
        #region Fields

        private readonly RemoveRegionLogic _removeRegionLogic;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRegionCommand" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        internal RemoveRegionCommand(CodeMaidPackage package)
            : base(package,
                   new CommandID(GuidList.GuidCodeMaidCommandRemoveRegion, (int)PkgCmdIDList.CmdIDCodeMaidRemoveRegion))
        {
            _removeRegionLogic = RemoveRegionLogic.GetInstance(package);
        }

        #endregion Constructors

        #region Enumerations

        /// <summary>
        /// An enumeration of region command scopes.
        /// </summary>
        private enum RegionCommandScope
        {
            None,
            Document,
            CurrentLine,
            Selection
        }

        #endregion Enumerations

        #region BaseCommand Methods

        /// <summary>
        /// Called to update the current status of the command.
        /// </summary>
        protected override void OnBeforeQueryStatus()
        {
            var regionCommandScope = GetRegionCommandScope();

            Enabled = regionCommandScope != RegionCommandScope.None;

            switch (regionCommandScope)
            {
                case RegionCommandScope.CurrentLine:
                    Text = "&Remove Current Region"; //TODO: Add name of region? e.g. "&Remove Region Constructors"
                    break;

                case RegionCommandScope.Selection:
                    Text = "&Remove Selected Regions";
                    break;

                default:
                    Text = "&Remove All Regions";
                    break;
            }
        }

        /// <summary>
        /// Called to execute the command.
        /// </summary>
        protected override void OnExecute()
        {
            base.OnExecute();

            var regionCommandScope = GetRegionCommandScope();
            switch (regionCommandScope)
            {
                case RegionCommandScope.CurrentLine:
                    //TODO: Implement.
                    break;

                case RegionCommandScope.Selection:
                    _removeRegionLogic.RemoveRegions(ActiveTextDocument.Selection);
                    break;

                case RegionCommandScope.Document:
                    _removeRegionLogic.RemoveRegions(ActiveTextDocument);
                    break;
            }
        }

        #endregion BaseCommand Methods

        #region Private Properties

        /// <summary>
        /// Gets the active text document, otherwise null.
        /// </summary>
        private TextDocument ActiveTextDocument
        {
            get
            {
                var document = Package.IDE.ActiveDocument;

                return document != null ? document.Object("TextDocument") as TextDocument : null;
            }
        }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        /// Gets the region command scope based on the current document and selection conditions.
        /// </summary>
        /// <returns>The scope that should be used for the region command.</returns>
        private RegionCommandScope GetRegionCommandScope()
        {
            var activeTextDocument = ActiveTextDocument;
            if (activeTextDocument != null)
            {
                var textSelection = activeTextDocument.Selection;
                if (textSelection != null)
                {
                    if (!textSelection.IsEmpty)
                    {
                        return RegionCommandScope.Selection;
                    }

                    //TODO: Extend conditional to determine if the current line contains a region, if not we'll fall back to the document.
                    return RegionCommandScope.CurrentLine;
                }

                return RegionCommandScope.Document;
            }

            return RegionCommandScope.None;
        }

        #endregion Private Methods
    }
}