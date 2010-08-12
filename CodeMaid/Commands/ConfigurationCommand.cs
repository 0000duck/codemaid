﻿#region CodeMaid is Copyright 2007-2010 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2010 Steve Cadwallader.

using System.ComponentModel.Design;

namespace SteveCadwallader.CodeMaid.Commands
{
    /// <summary>
    /// A command that provides for launching the CodeMaid configuration.
    /// </summary>
    internal class ConfigurationCommand : BaseCommand
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationCommand"/> class.
        /// </summary>
        /// <param name="package">The hosting package.</param>
        internal ConfigurationCommand(CodeMaidPackage package)
            : base(package,
                   new CommandID(GuidList.GuidCodeMaidCommandConfiguration, (int)PkgCmdIDList.CmdIDCodeMaidConfiguration))
        {
        }

        #endregion Constructors

        #region BaseCommand Methods

        /// <summary>
        /// Called to execute the command.
        /// </summary>
        protected override void OnExecute()
        {
            // Open options to the package options page.
            Package.ShowOptionPage(Package.Options.CleanupGeneral.GetType());
        }

        #endregion BaseCommand Methods
    }
}