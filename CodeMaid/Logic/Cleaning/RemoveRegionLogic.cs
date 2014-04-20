﻿#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using EnvDTE;
using SteveCadwallader.CodeMaid.Helpers;
using SteveCadwallader.CodeMaid.Model;
using SteveCadwallader.CodeMaid.Model.CodeItems;
using SteveCadwallader.CodeMaid.Properties;
using SteveCadwallader.CodeMaid.UI.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SteveCadwallader.CodeMaid.Logic.Cleaning
{
    /// <summary>
    /// A class for encapsulating the logic of removing regions.
    /// </summary>
    internal class RemoveRegionLogic
    {
        #region Fields

        private readonly CodeMaidPackage _package;
        private readonly CodeModelHelper _codeModelHelper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// The singleton instance of the <see cref="RemoveRegionLogic" /> class.
        /// </summary>
        private static RemoveRegionLogic _instance;

        /// <summary>
        /// Gets an instance of the <see cref="RemoveRegionLogic" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>An instance of the <see cref="RemoveRegionLogic" /> class.</returns>
        internal static RemoveRegionLogic GetInstance(CodeMaidPackage package)
        {
            return _instance ?? (_instance = new RemoveRegionLogic(package));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveRegionLogic" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        private RemoveRegionLogic(CodeMaidPackage package)
        {
            _package = package;
            _codeModelHelper = CodeModelHelper.GetInstance(_package);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Removes all region tags from the specified text document.
        /// </summary>
        /// <param name="textDocument">The text document to update.</param>
        internal void RemoveRegions(TextDocument textDocument)
        {
            // Retrieve the regions and put them in reverse order (reduces line number updates during removal).
            var regions = _codeModelHelper.RetrieveCodeRegions(textDocument).OrderByDescending(x => x.StartLine);

            new UndoTransactionHelper(_package, "CodeMaid Remove All Regions").Run(() =>
            {
                foreach (var region in regions)
                {
                    RemoveRegion(region);
                }
            });
        }

        /// <summary>
        /// Removes all region tags from the specified text selection.
        /// </summary>
        /// <param name="textSelection">The text selection to update.</param>
        internal void RemoveRegions(TextSelection textSelection)
        {
            // Retrieve the regions and put them in reverse order (reduces line number updates during removal).
            var regions = _codeModelHelper.RetrieveCodeRegions(textSelection).OrderByDescending(x => x.StartLine);

            new UndoTransactionHelper(_package, "CodeMaid Remove Selected Regions").Run(() =>
            {
                foreach (var region in regions)
                {
                    RemoveRegion(region);
                }
            });
        }

        /// <summary>
        /// Removes the region tags from the specified regions based on user settings.
        /// </summary>
        /// <param name="regions">The regions to update.</param>
        internal void RemoveRegionsPerSettings(IEnumerable<CodeItemRegion> regions)
        {
            IEnumerable<CodeItemRegion> regionsToRemove;

            switch ((NoneEmptyAll)Settings.Default.Cleaning_RemoveRegions)
            {
                case NoneEmptyAll.All:
                    regionsToRemove = regions;
                    break;

                case NoneEmptyAll.Empty:
                    regionsToRemove = regions.Where(x => x.IsEmpty);
                    break;

                default:
                    regionsToRemove = Enumerable.Empty<CodeItemRegion>();
                    break;
            }

            // Iterate through regions in reverse order (reduces line number updates during removal).
            foreach (var region in regionsToRemove.OrderByDescending(x => x.StartLine))
            {
                RemoveRegion(region);
            }
        }

        /// <summary>
        /// Removes the region tags from the specified region.
        /// </summary>
        /// <param name="region">The region to update.</param>
        internal void RemoveRegion(CodeItemRegion region)
        {
            if (region == null || region.IsInvalidated || region.IsPseudoGroup || region.StartLine <= 0 || region.EndLine <= 0)
            {
                return;
            }

            new UndoTransactionHelper(_package, "CodeMaid Remove Region " + region.Name).Run(() =>
            {
                var end = region.EndPoint.CreateEditPoint();
                end.StartOfLine();
                end.Delete(end.LineLength);
                end.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsVertical);
                end.Insert(Environment.NewLine);

                var start = region.StartPoint.CreateEditPoint();
                start.StartOfLine();
                start.Delete(start.LineLength);
                start.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsVertical);
                start.Insert(Environment.NewLine);

                region.IsInvalidated = true;
            });
        }

        #endregion Methods
    }
}