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
using System.Windows.Forms;

namespace SteveCadwallader.CodeMaid.Options
{
    /// <summary>
    /// The options control hosting cleanup insert options.
    /// </summary>
    public partial class CleanupInsertOptionsControl : UserControl
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanupInsertOptionsControl"/> class.
        /// </summary>
        public CleanupInsertOptionsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CleanupInsertOptionsControl"/> class.
        /// </summary>
        /// <param name="optionsPage">The options page.</param>
        public CleanupInsertOptionsControl(CleanupInsertOptionsPage optionsPage)
            : this()
        {
            OptionsPage = optionsPage;

            insertBlankLinePaddingBeforeUsingStatementBlocksCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeUsingStatementBlocks;
            insertBlankLinePaddingAfterUsingStatementBlocksCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterUsingStatementBlocks;
            insertBlankLinePaddingBeforeNamespacesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeNamespaces;
            insertBlankLinePaddingAfterNamespacesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterNamespaces;
            insertBlankLinePaddingBeforeRegionTagsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeRegionTags;
            insertBlankLinePaddingAfterRegionTagsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterRegionTags;
            insertBlankLinePaddingBeforeEndRegionTagsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeEndRegionTags;
            insertBlankLinePaddingAfterEndRegionTagsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterEndRegionTags;
            insertBlankLinePaddingBeforeClassesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeClasses;
            insertBlankLinePaddingAfterClassesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterClasses;
            insertBlankLinePaddingBeforeEnumerationsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeEnumerations;
            insertBlankLinePaddingAfterEnumerationsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterEnumerations;
            insertBlankLinePaddingBeforeEventsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeEvents;
            insertBlankLinePaddingAfterEventsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterEvents;
            insertBlankLinePaddingBeforeFieldsWithCommentsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeFieldsWithComments;
            insertBlankLinePaddingAfterFieldsWithCommentsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterFieldsWithComments;
            insertBlankLinePaddingBeforeInterfacesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeInterfaces;
            insertBlankLinePaddingAfterInterfacesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterInterfaces;
            insertBlankLinePaddingBeforeMethodsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeMethods;
            insertBlankLinePaddingAfterMethodsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterMethods;
            insertBlankLinePaddingBeforePropertiesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeProperties;
            insertBlankLinePaddingAfterPropertiesCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterProperties;
            insertBlankLinePaddingBeforeStructsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingBeforeStructs;
            insertBlankLinePaddingAfterStructsCheckBox.Checked = OptionsPage.InsertBlankLinePaddingAfterStructs;

            insertExplicitAccessModifiersOnClassesCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnClasses;
            insertExplicitAccessModifiersOnEnumerationsCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnEnumerations;
            insertExplicitAccessModifiersOnEventsCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnEvents;
            insertExplicitAccessModifiersOnInterfacesCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnInterfaces;
            insertExplicitAccessModifiersOnMethodsCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnMethods;
            insertExplicitAccessModifiersOnPropertiesCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnProperties;
            insertExplicitAccessModifiersOnStructsCheckBox.Checked = OptionsPage.InsertExplicitAccessModifiersOnStructs;
        }

        #endregion Constructors

        #region Private Properties

        /// <summary>
        /// Gets or sets the options page behind this control.
        /// </summary>
        private CleanupInsertOptionsPage OptionsPage { get; set; }

        #endregion Private Properties

        #region Private Event Handlers

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeUsingStatementBlocksCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeUsingStatementBlocksCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeUsingStatementBlocks = insertBlankLinePaddingBeforeUsingStatementBlocksCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterUsingStatementBlocksCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterUsingStatementBlocksCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterUsingStatementBlocks = insertBlankLinePaddingAfterUsingStatementBlocksCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeNamespacesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeNamespacesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeNamespaces = insertBlankLinePaddingBeforeNamespacesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterNamespacesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterNamespacesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterNamespaces = insertBlankLinePaddingAfterNamespacesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeRegionTagsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeRegionTagsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeRegionTags = insertBlankLinePaddingBeforeRegionTagsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterRegionTagsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterRegionTagsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterRegionTags = insertBlankLinePaddingAfterRegionTagsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeEndRegionTagsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeEndRegionTagsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeEndRegionTags = insertBlankLinePaddingBeforeEndRegionTagsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterEndRegionTagsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterEndRegionTagsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterEndRegionTags = insertBlankLinePaddingAfterEndRegionTagsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeClassesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeClassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeClasses = insertBlankLinePaddingBeforeClassesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterClassesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterClassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterClasses = insertBlankLinePaddingAfterClassesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeEnumerationsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeEnumerationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeEnumerations = insertBlankLinePaddingBeforeEnumerationsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterEnumerationsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterEnumerationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterEnumerations = insertBlankLinePaddingAfterEnumerationsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeEventsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeEvents = insertBlankLinePaddingBeforeEventsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterEventsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterEvents = insertBlankLinePaddingAfterEventsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeFieldsWithCommentsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeFieldsWithCommentsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeFieldsWithComments = insertBlankLinePaddingBeforeFieldsWithCommentsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterFieldsWithCommentsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterFieldsWithCommentsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterFieldsWithComments = insertBlankLinePaddingAfterFieldsWithCommentsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeInterfacesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeInterfacesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeInterfaces = insertBlankLinePaddingBeforeInterfacesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterInterfacesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterInterfacesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterInterfaces = insertBlankLinePaddingAfterInterfacesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeMethodsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeMethodsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeMethods = insertBlankLinePaddingBeforeMethodsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterMethodsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterMethodsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterMethods = insertBlankLinePaddingAfterMethodsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforePropertiesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforePropertiesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeProperties = insertBlankLinePaddingBeforePropertiesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterPropertiesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterPropertiesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterProperties = insertBlankLinePaddingAfterPropertiesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingBeforeStructsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingBeforeStructsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingBeforeStructs = insertBlankLinePaddingBeforeStructsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertBlankLinePaddingAfterStructsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertBlankLinePaddingAfterStructsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertBlankLinePaddingAfterStructs = insertBlankLinePaddingAfterStructsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnClassesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnClassesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnClasses = insertExplicitAccessModifiersOnClassesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnEnumerationsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnEnumerationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnEnumerations = insertExplicitAccessModifiersOnEnumerationsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnEventsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnEventsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnEvents = insertExplicitAccessModifiersOnEventsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnInterfacesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnInterfacesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnInterfaces = insertExplicitAccessModifiersOnInterfacesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnMethodsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnMethodsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnMethods = insertExplicitAccessModifiersOnMethodsCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnPropertiesCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnPropertiesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnProperties = insertExplicitAccessModifiersOnPropertiesCheckBox.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the insertExplicitAccessModifiersOnStructsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void insertExplicitAccessModifiersOnStructsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OptionsPage.InsertExplicitAccessModifiersOnStructs = insertExplicitAccessModifiersOnStructsCheckBox.Checked;
        }

        #endregion Private Event Handlers
    }
}