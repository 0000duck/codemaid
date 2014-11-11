#region CodeMaid is Copyright 2007-2014 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2014 Steve Cadwallader.

using EnvDTE;
using SteveCadwallader.CodeMaid.Helpers;
using SteveCadwallader.CodeMaid.Properties;
using System;
using System.ComponentModel.Design;

namespace SteveCadwallader.CodeMaid.Integration.Commands
{
    /// <summary>
    /// A command that provides for finding a file in the solution explorer.
    /// </summary>
    internal class FindInSolutionExplorerCommand : BaseCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FindInSolutionExplorerCommand" /> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        internal FindInSolutionExplorerCommand(CodeMaidPackage package)
            : base(package,
                   new CommandID(GuidList.GuidCodeMaidCommandFindInSolutionExplorer, (int)PkgCmdIDList.CmdIDCodeMaidFindInSolutionExplorer))
        {
        }

        #endregion Constructors

        #region BaseCommand Methods

        /// <summary>
        /// Called to update the current status of the command.
        /// </summary>
        protected override void OnBeforeQueryStatus()
        {
            Enabled = Package.ActiveDocument != null;
        }

        /// <summary>
        /// Called to execute the command.
        /// </summary>
        protected override void OnExecute()
        {
            base.OnExecute();

            Document document = Package.ActiveDocument;
            if (document != null)
            {
                if (Settings.Default.Finding_TemporarilyOpenSolutionFolders)
                {
                    ToggleSolutionFoldersOpenTemporarily(UIHierarchyHelper.GetTopUIHierarchyItem(Package));
                }

                if (Package.IDEVersion >= 11)
                {
                    Package.IDE.ExecuteCommand("SolutionExplorer.SyncWithActiveDocument", String.Empty);
                }
                else
                {
                    Package.IDE.ExecuteCommand("View.TrackActivityinSolutionExplorer", String.Empty);
                    Package.IDE.ExecuteCommand("View.TrackActivityinSolutionExplorer", String.Empty);
                    Package.IDE.ExecuteCommand("View.SolutionExplorer", String.Empty);
                }
            }
        }

        #endregion BaseCommand Methods

        #region Methods

        /// <summary>
        /// Toggles all solution folders open temporarily to workaround searches not working inside
        /// solution folders that have never been expanded.
        /// </summary>
        /// <param name="parentItem">The parent item to inspect.</param>
        private void ToggleSolutionFoldersOpenTemporarily(UIHierarchyItem parentItem)
        {
            if (parentItem == null)
            {
                throw new ArgumentNullException("parentItem");
            }

            const string solutionFolderGuid = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

            var project = parentItem.Object as Project;
            bool isCollapsedSolutionFolder = project != null && project.Kind == solutionFolderGuid && !parentItem.UIHierarchyItems.Expanded;

            // Expand the solution folder temporarily.
            if (isCollapsedSolutionFolder)
            {
                OutputWindowHelper.DiagnosticWriteLine(
                    string.Format("FindInSolutionExplorerCommand.ToggleSolutionFoldersOpenTemporarily expanding '{0}'", parentItem.Name));

                parentItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
                Package.IDE.ToolWindows.SolutionExplorer.DoDefaultAction();
            }

            // Run recursively to children as well for nested solution folders.
            foreach (UIHierarchyItem childItem in parentItem.UIHierarchyItems)
            {
                ToggleSolutionFoldersOpenTemporarily(childItem);
            }

            // Collapse the solution folder.
            if (isCollapsedSolutionFolder)
            {
                OutputWindowHelper.DiagnosticWriteLine(
                    string.Format("FindInSolutionExplorerCommand.ToggleSolutionFoldersOpenTemporarily collapsing '{0}'", parentItem.Name));

                parentItem.Select(vsUISelectionType.vsUISelectionTypeSelect);
                Package.IDE.ToolWindows.SolutionExplorer.DoDefaultAction();
            }
        }

        #endregion Methods
    }
}