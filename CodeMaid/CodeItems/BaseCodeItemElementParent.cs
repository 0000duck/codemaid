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

namespace SteveCadwallader.CodeMaid.CodeItems
{
    /// <summary>
    /// A base class representation of all code items that have an underlying VSX CodeElement and contain children.
    /// </summary>
    public abstract class BaseCodeItemElementParent : BaseCodeItemElement, ICodeItemParent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCodeItemElementParent"/> class.
        /// </summary>
        protected BaseCodeItemElementParent()
        {
            Children = new SetCodeItems();
        }

        #endregion Constructors

        #region Implementation of ICodeItemParent

        /// <summary>
        /// Gets the children of this code item, may be empty.
        /// </summary>
        public SetCodeItems Children { get; private set; }

        #endregion Implementation of ICodeItemParent

        #region Properties

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        public abstract string Namespace { get; }

        #endregion Properties
    }
}