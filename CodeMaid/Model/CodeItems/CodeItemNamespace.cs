#region CodeMaid is Copyright 2007-2013 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2013 Steve Cadwallader.

using System.Threading;
using System.Threading.Tasks;
using EnvDTE;

namespace SteveCadwallader.CodeMaid.Model.CodeItems
{
    /// <summary>
    /// The representation of a code namespace.
    /// </summary>
    public class CodeItemNamespace : BaseCodeItemElementParent
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeItemNamespace"/> class.
        /// </summary>
        public CodeItemNamespace()
        {
            TypeString = "namespace";
        }

        #endregion Constructors

        #region BaseCodeItem Overrides

        /// <summary>
        /// Gets the kind.
        /// </summary>
        public override KindCodeItem Kind
        {
            get { return KindCodeItem.Namespace; }
        }

        /// <summary>
        /// Refreshes the cached fields on this item.
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            Task.Factory.StartNew(() =>
            {
                DocComment = TryDefault(() => CodeNamespace != null ? CodeNamespace.DocComment : null);
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default).Wait();
        }

        #endregion BaseCodeItem Overrides

        #region Properties

        /// <summary>
        /// Gets or sets the underlying VSX CodeNamespace.
        /// </summary>
        public CodeNamespace CodeNamespace { get; set; }

        #endregion Properties
    }
}