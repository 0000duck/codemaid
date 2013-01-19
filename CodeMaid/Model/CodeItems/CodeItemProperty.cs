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

using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using SteveCadwallader.CodeMaid.Helpers;

namespace SteveCadwallader.CodeMaid.Model.CodeItems
{
    /// <summary>
    /// The representation of a code property.
    /// </summary>
    public class CodeItemProperty : BaseCodeItemElement, ICodeItemComplexity, ICodeItemParameters
    {
        #region Fields

        private int? _complexity;

        #endregion Fields

        #region BaseCodeItem Overrides

        /// <summary>
        /// Gets the kind.
        /// </summary>
        public override KindCodeItem Kind
        {
            get { return IsIndexer ? KindCodeItem.Indexer : KindCodeItem.Property; }
        }

        #endregion BaseCodeItem Overrides

        #region BaseCodeItemElement Overrides

        /// <summary>
        /// Gets the access level.
        /// </summary>
        public override vsCMAccess Access
        {
            // Make exceptions for explicit interface implementations - which report private access but really do not have a meaningful access level.
            get { return TryDefault(() => CodeProperty != null && !IsExplicitInterfaceImplementation ? CodeProperty.Access : vsCMAccess.vsCMAccessPublic); }
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        public override CodeElements Attributes
        {
            get { return TryDefault(() => CodeProperty != null ? CodeProperty.Attributes : null); }
        }

        /// <summary>
        /// Gets the doc comment.
        /// </summary>
        public override string DocComment
        {
            get { return TryDefault(() => CodeProperty != null ? CodeProperty.DocComment : null); }
        }

        /// <summary>
        /// Gets a flag indicating if this property is static.
        /// </summary>
        public override bool IsStatic
        {
            get
            {
                return TryDefault(() => CodeProperty != null &&
                                        ((CodeProperty.Getter != null && CodeProperty.Getter.IsShared) ||
                                         (CodeProperty.Setter != null && CodeProperty.Setter.IsShared)));
            }
        }

        /// <summary>
        /// Gets the type string.
        /// </summary>
        public override string TypeString
        {
            get { return TryDefault(() => CodeProperty != null && CodeProperty.Type != null ? CodeProperty.Type.AsString : null); }
        }

        #endregion BaseCodeItemElement Overrides

        #region Properties

        /// <summary>
        /// Gets or sets the underlying VSX CodeProperty.
        /// </summary>
        public CodeProperty2 CodeProperty { get; set; }

        /// <summary>
        /// Gets the complexity.
        /// </summary>
        public int Complexity
        {
            get
            {
                if (_complexity == null)
                {
                    _complexity = CodeModelHelper.CalculateComplexity(CodeElement);
                }

                return _complexity.Value;
            }
        }

        /// <summary>
        /// Gets a flag indicating if this property is an explicit interface implementation.
        /// </summary>
        public bool IsExplicitInterfaceImplementation
        {
            get { return TryDefault(() => CodeProperty != null && ExplicitInterfaceImplementationHelper.IsExplicitInterfaceImplementation(CodeProperty)); }
        }

        /// <summary>
        /// Gets a flag indicating if this property is an indexer.
        /// </summary>
        public bool IsIndexer
        {
            get { return TryDefault(() => CodeProperty != null && CodeProperty.Parameters != null && CodeProperty.Parameters.Count > 0); }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        public IEnumerable<CodeParameter> Parameters
        {
            get { return TryDefault(() => CodeProperty != null && CodeProperty.Parameters != null ? CodeProperty.Parameters.Cast<CodeParameter>().ToList() : Enumerable.Empty<CodeParameter>()); }
        }

        #endregion Properties
    }
}