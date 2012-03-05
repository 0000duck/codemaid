﻿#region CodeMaid is Copyright 2007-2012 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2012 Steve Cadwallader.

using System;
using System.Linq;
using EnvDTE;
using SteveCadwallader.CodeMaid.CodeItems;
using SteveCadwallader.CodeMaid.CodeTree;

namespace SteveCadwallader.CodeMaid.Helpers
{
    /// <summary>
    /// A helper class for reordering code.
    /// </summary>
    internal class CodeReorderHelper
    {
        #region Fields

        private UndoTransactionHelper _undoTransactionHelper;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// The singleton instance of the <see cref="CodeReorderHelper"/> class.
        /// </summary>
        private static CodeReorderHelper _instance;

        /// <summary>
        /// Gets an instance of the <see cref="CodeReorderHelper"/> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        /// <returns>An instance of the <see cref="CodeReorderHelper"/> class.</returns>
        internal static CodeReorderHelper GetInstance(CodeMaidPackage package)
        {
            return _instance ?? (_instance = new CodeReorderHelper(package));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeReorderHelper"/> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        private CodeReorderHelper(CodeMaidPackage package)
        {
            Package = package;
        }

        #endregion Constructors

        #region Private Properties

        /// <summary>
        /// Gets or sets the hosting package.
        /// </summary>
        private CodeMaidPackage Package { get; set; }

        /// <summary>
        /// Gets the lazy-initialized undo transaction helper.
        /// </summary>
        private UndoTransactionHelper UndoTransactionHelper
        {
            get { return _undoTransactionHelper ?? (_undoTransactionHelper = new UndoTransactionHelper(Package, "CodeMaid Reorganize")); }
        }

        #endregion Private Properties

        #region Internal Methods

        /// <summary>
        /// Moves the specified item above the specified base within an undo transaction.
        /// </summary>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="baseItem">The base item.</param>
        internal void MoveItemAboveBase(BaseCodeItemElement itemToMove, BaseCodeItemElement baseItem)
        {
            UndoTransactionHelper.Run(() => RepositionItemAboveBase(itemToMove, baseItem));
        }

        /// <summary>
        /// Moves the specified item below the specified base.
        /// </summary>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="baseItem">The base item.</param>
        internal void MoveItemBelowBase(BaseCodeItemElement itemToMove, BaseCodeItemElement baseItem)
        {
            UndoTransactionHelper.Run(() => RepositionItemBelowBase(itemToMove, baseItem));
        }

        /// <summary>
        /// Determines whether the specified document can be reorganized.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>True if document can be reorganized, otherwise false.</returns>
        internal bool CanReorganize(Document document)
        {
            return Package.IDE.Debugger.CurrentMode == dbgDebugMode.dbgDesignMode &&
                   document != null &&
                   document.Language == "CSharp";
        }

        /// <summary>
        /// Reorganizes the specified document.
        /// </summary>
        /// <param name="document">The document for reorganizing.</param>
        internal void Reorganize(Document document)
        {
            if (!CanReorganize(document)) return;

            UndoTransactionHelper.Run(
                delegate
                {
                    Package.IDE.StatusBar.Text = String.Format("CodeMaid is reorganizing '{0}'...", document.Name);

                    // Retrieve all relevant code items.
                    var codeItems = CodeModelHelper.RetrieveCodeItemsExcludingRegions(document);
                    codeItems.RemoveAll(x => x is CodeItemUsingStatement || x is CodeItemNamespace);

                    // Build the code tree based on the current file layout.
                    var codeTree =
                        CodeTreeBuilder.RetrieveCodeTree(new CodeTreeRequest(codeItems, TreeLayoutMode.FileLayout));

                    // Recursively reorganize the code tree.
                    RecursivelyReorganize(codeTree);

                    Package.IDE.StatusBar.Text = String.Format("CodeMaid reorganized '{0}'.", document.Name);
                },
                delegate(Exception ex)
                {
                    Package.IDE.StatusBar.Text = String.Format("CodeMaid stopped reorganizing '{0}': {1}", document.Name, ex);
                });
        }

        #endregion Internal Methods

        #region Private Methods

        /// <summary>
        /// Cuts the item to move onto the clipboard.
        /// </summary>
        /// <param name="itemToMove">The item to move.</param>
        private static void CutItemToMoveOntoClipboard(BaseCodeItemElement itemToMove)
        {
            FactoryCodeItems.RefreshCodeItemElement(itemToMove);
            var moveStartPoint = TextDocumentHelper.GetStartPointAdjustedForComments(itemToMove.CodeElement.StartPoint);
            var moveEndPoint = itemToMove.CodeElement.EndPoint;

            moveStartPoint.Cut(moveEndPoint, false);
            moveStartPoint.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsVertical);
        }

        /// <summary>
        /// Determines if the two specified items should be separated by a newline.
        /// </summary>
        /// <param name="firstItem">The first item.</param>
        /// <param name="secondItem">The second item.</param>
        /// <returns>True if the items should be separated by a newline, otherwise false.</returns>
        private static bool ShouldBeSeparatedByNewLine(BaseCodeItemElement firstItem, BaseCodeItemElement secondItem)
        {
            if (firstItem is CodeItemField && firstItem.StartLine == firstItem.EndLine &&
                secondItem is CodeItemField && secondItem.StartLine == secondItem.EndLine)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Repositions the specified item above the specified base.
        /// </summary>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="baseItem">The base item.</param>
        private static void RepositionItemAboveBase(BaseCodeItemElement itemToMove, BaseCodeItemElement baseItem)
        {
            if (itemToMove == baseItem) return;

            CutItemToMoveOntoClipboard(itemToMove);

            FactoryCodeItems.RefreshCodeItemElement(baseItem);
            var baseStartPoint = TextDocumentHelper.GetStartPointAdjustedForComments(baseItem.CodeElement.StartPoint);
            var pastePoint = baseStartPoint.CreateEditPoint();

            pastePoint.Paste();
            pastePoint.Insert(Environment.NewLine);
            if (ShouldBeSeparatedByNewLine(itemToMove, baseItem))
            {
                pastePoint.Insert(Environment.NewLine);
            }

            pastePoint.EndOfLine();
            baseStartPoint.SmartFormat(pastePoint);
        }

        /// <summary>
        /// Repositions the specified item below the specified base.
        /// </summary>
        /// <param name="itemToMove">The item to move.</param>
        /// <param name="baseItem">The base item.</param>
        private static void RepositionItemBelowBase(BaseCodeItemElement itemToMove, BaseCodeItemElement baseItem)
        {
            if (itemToMove == baseItem) return;

            CutItemToMoveOntoClipboard(itemToMove);

            FactoryCodeItems.RefreshCodeItemElement(baseItem);
            var baseEndPoint = baseItem.CodeElement.EndPoint.CreateEditPoint();
            var pastePoint = baseEndPoint.CreateEditPoint();

            pastePoint.Insert(Environment.NewLine);
            if (ShouldBeSeparatedByNewLine(itemToMove, baseItem))
            {
                pastePoint.Insert(Environment.NewLine);
            }

            var formatPoint = pastePoint.CreateEditPoint();

            pastePoint.Paste();

            formatPoint.EndOfLine();
            baseEndPoint.SmartFormat(formatPoint);
        }

        /// <summary>
        /// Recursively reorganizes the specified code items.
        /// </summary>
        /// <param name="codeItems">The code items.</param>
        private void RecursivelyReorganize(SetCodeItems codeItems)
        {
            var codeItemElements = codeItems.OfType<BaseCodeItemElement>();
            BaseCodeItemElement baseItem = null;

            // Organize the items in the desired order.
            var orderedItems = codeItemElements.OrderBy(x => CodeItemTypeComparer.CalculateNumericRepresentation(x));
            orderedItems = Package.Options.Reorganize.AlphabetizeMembersOfTheSameGroup
                               ? orderedItems.ThenBy(y => y.Name)
                               : orderedItems.ThenBy(y => y.StartOffset);

            // Iterate across the items in the desired order.
            foreach (var itemToMove in orderedItems)
            {
                if (itemToMove.Children.Any() && !(itemToMove is CodeItemEnum))
                {
                    RecursivelyReorganize(itemToMove.Children);
                }

                if (baseItem == null)
                {
                    // The first desired item should be placed above the first actual item.
                    RepositionItemAboveBase(itemToMove, codeItemElements.First());
                    baseItem = itemToMove;
                }
                else
                {
                    // All other items should be placed after the last placed item in the desired order.
                    RepositionItemBelowBase(itemToMove, baseItem);
                    baseItem = itemToMove;
                }
            }
        }

        #endregion Private Methods
    }
}