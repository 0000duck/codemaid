﻿#region CodeMaid is Copyright 2007-2015 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License version 3 as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2015 Steve Cadwallader.

using SteveCadwallader.CodeMaid.Helpers;
using SteveCadwallader.CodeMaid.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace SteveCadwallader.CodeMaid.Model.Comments
{
    internal class CommentLineXml : CommentLine
    {
        #region Fields

        private static string[] NewLineElementNames = { "p", "para", "code" };
        private static Regex InterpunctionRegex = new Regex(@"^[^\w]", RegexOptions.Compiled);
        private StringBuilder _innerText;

        #endregion Fields

        #region Constructors

        public CommentLineXml(XElement xml)
            : base(null)
        {
            TagName = xml.Name.LocalName;

            // Tags that are forced to be their own line should never be self closing. This prevents
            // empty tags from getting collapsed.
            OpenTag = CodeCommentHelper.CreateXmlOpenTag(xml);
            Closetag = CodeCommentHelper.CreateXmlCloseTag(xml);

            Lines = new List<ICommentLine>();

            _innerText = new StringBuilder();
            ParseChildNodes(xml);
            CloseInnerText();
        }

        #endregion Constructors

        #region Properties

        public string Closetag { get; set; }

        public ICollection<ICommentLine> Lines { get; private set; }

        public string OpenTag { get; set; }

        public string TagName { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// If there is text left in the buffer, parse and append it as a comment line.
        /// </summary>
        private void CloseInnerText()
        {
            if (_innerText.Length > 0)
            {
                Lines.Add(new CommentLine(_innerText.ToString()));
                _innerText.Clear();
            }
        }

        private void ParseChildNodes(XElement xml)
        {
            if (string.Equals(TagName, "code", StringComparison.OrdinalIgnoreCase))
            {
                // Content of code element should be read literally and preserve whitespace.
                using (var reader = xml.CreateReader())
                {
                    reader.MoveToContent();
                    Content = reader.ReadInnerXml();
                }
            }
            else
            {
                // Loop and parse all child nodes.
                var node = xml.FirstNode;
                while (node != null)
                {
                    // If the node is a sub-element, it needs to be handled seperately.
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        var e = (XElement)node;

                        // All root level elements and certain special sub elements always need to
                        // be on their own line.
                        if (e.Parent == null || e.Parent.Parent == null || NewLineElementNames.Contains(e.Name.LocalName, StringComparer.OrdinalIgnoreCase))
                        {
                            CloseInnerText();
                            Lines.Add(new CommentLineXml(e));
                        }
                        else
                        {
                            // If the tag is not forced to be on it's own line, append it to the
                            // current content as string.
                            _innerText.Append(CodeCommentHelper.CreateXmlOpenTag(e));

                            if (!e.IsEmpty)
                            {
                                if (Settings.Default.Formatting_CommentXmlSpaceTags)
                                {
                                    _innerText.Append(CodeCommentHelper.Spacer);
                                }

                                ParseChildNodes(e);

                                _innerText.Append(CodeCommentHelper.CreateXmlCloseTag(e));
                            }
                        }
                    }
                    else
                    {
                        // Always trim trailing
                        var value = node.ToString().TrimEnd(CodeCommentHelper.Spacer);

                        // If the parent is an element, trim the starting spaces.
                        if (node.PreviousNode == null && node.Parent.NodeType == XmlNodeType.Element && !Settings.Default.Formatting_CommentXmlSpaceTags)
                        {
                            value = value.TrimStart(CodeCommentHelper.Spacer);
                        }

                        // If the previous node was an XML element, put a space before the text
                        // unless the first character is interpunction.
                        if (node.PreviousNode != null && node.PreviousNode.NodeType == XmlNodeType.Element)
                        {
                            if (!StartsWithInterpunction(value))
                            {
                                _innerText.Append(CodeCommentHelper.Spacer);
                            }
                        }

                        _innerText.Append(value);

                        // Add spacing after (almost) each word.
                        if (node.NextNode != null || node.Parent.NodeType != XmlNodeType.Element || Settings.Default.Formatting_CommentXmlSpaceTags)
                        {
                            _innerText.Append(CodeCommentHelper.Spacer);
                        }
                    }

                    node = node.NextNode;
                }
            }
        }

        private static bool StartsWithInterpunction(string value)
        {
            return InterpunctionRegex.IsMatch(value);
        }

        #endregion Methods
    }
}