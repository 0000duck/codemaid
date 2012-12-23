﻿using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SteveCadwallader.CodeMaid.Helpers
{
    /// <summary>
    /// A <c>CodeComment</c> contains one or more <see cref="CodeCommentPhrase">Phrases</see>
    /// which represent all the content of a comment.
    /// </summary>
    internal class CodeComment
    {
        private static string[] MajorXmlTags = { "summary", "remarks", "example" };
        private static string[] MinorXmlTags = { "param", "exception", "returns", "value" };
        private static Regex XmlTagRegex = new Regex(@"^<(?<closetag>\/)?(?<tag>(" + String.Join("|", MajorXmlTags.Union(MinorXmlTags)) + ")).*>$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public CodeComment(EditPoint startpoint, EditPoint endpoint, string commentPrefix)
        {
            Phrases = new LinkedList<CodeCommentPhrase>();

            this.StartPoint = startpoint.CreateEditPoint();
            this.EndPoint = endpoint.CreateEditPoint();
            this.CommentPrefix = commentPrefix;

            this.LineCharOffset = StartPoint.LineCharOffset - 1;
            this.IsXmlComment = CommentPrefix.Length == 3;
        }

        public string CommentPrefix { get; private set; }

        public EditPoint EndPoint { get; private set; }

        public bool IsXmlComment { get; private set; }

        public int LineCharOffset { get; private set; }

        public EditPoint StartPoint { get; private set; }

        protected LinkedList<CodeCommentPhrase> Phrases { get; private set; }

        internal virtual void Add(string value)
        {
            var current = new CodeCommentPhrase(value);
            if (Phrases.Last == null || current.IsList || (Phrases.Last.Value.IsList && Phrases.Last.Value.Indent != current.Indent))
                Phrases.AddLast(current);
            else
                Phrases.Last.Value.Append(current);
        }

        internal virtual void Output(int maxWidth)
        {
            if (maxWidth < LineCharOffset + 20)
                maxWidth = LineCharOffset + 20;

            StartPoint.Delete(EndPoint);

            if (IsXmlComment)
                ReformatXmlPhrases();

            // Loop through each phrase.
            var phrase = Phrases.First;
            //bool isNewLine = true;
            while (phrase != null)
            {
                StartPoint.Insert(CommentPrefix);

                // Phrase is a list, so output the list prefix before the first word.
                if (phrase.Value.IsList)
                {
                    StartPoint.Insert(" ");
                    StartPoint.Insert(phrase.Value.ListPrefix);
                }

                // Loop through each word.
                var word = phrase.Value.Words.First;
                while (word != null)
                {
                    // Special handling of important tags in XML comments
                    //var match = XmlTagRegex.Match(word.Value);

                    // If this is not the first word of the phrase (and thus a newline), be aware
                    // of XML tags.
                    //if (!isNewLine && match.Success)
                    //{
                    //    // Add a newline for major tags and minor opening tags.
                    //    if (MajorXmlTags.Contains(match.Groups["tag"].Value) || (MinorXmlTags.Contains(match.Groups["tag"].Value) && !match.Groups["closetag"].Success))
                    //    {
                    //        StartPoint.Insert(GetNewCommentLine(true));
                    //        isNewLine = true;
                    //    }
                    //}

                    // Create newline if next word no longer fits on this line, but keep in
                    // mind some words can by themself already be too long to fit on a line.
                    if (StartPoint.LineCharOffset + word.Value.Length > maxWidth && word.Value.Length < maxWidth)
                    {
                        StartPoint.Insert(GetNewCommentLine(true));
                        //isNewLine = true;

                        // If the current phrase is a list, add extra spacing to create pretty
                        // alignment of list items.
                        if (phrase.Value.IsList)
                            StartPoint.Insert("".PadLeft(phrase.Value.ListPrefix.Length + 1, ' '));
                    }

                    // This is were we write the actual word.
                    StartPoint.Insert(" ");
                    StartPoint.Insert(word.Value);
                    //isNewLine = false;

                    word = word.Next;

                    // Major XML tags are followed by newlines, so put a newline unless this is
                    // the last word (which will be followed by a linebreak anyway).
                    //if (word != null && match.Success && MajorXmlTags.Contains(match.Groups["tag"].Value))
                    //{
                    //    StartPoint.Insert(GetNewCommentLine(true));
                    //    isNewLine = true;
                    //}
                }

                phrase = phrase.Next;

                // If on a comment phrase, and there will be another phrase, add a newline.
                if (phrase != null)
                    StartPoint.Insert(GetNewCommentLine(false));
            }
        }

        internal void SetEndPoint(EditPoint end)
        {
            EndPoint = end.CreateEditPoint();
        }

        /// <summary>
        /// Write a newline at the <c>StartPoint</c> and add indenting.
        /// </summary>
        /// <param name="resumeComment"> If set to <c>true</c> it will also write <c>CommentPrefix</c>. </param>
        protected string GetNewCommentLine(bool resumeComment = false)
        {
            return String.Format("{0}{1}{2}",
                Environment.NewLine,
                "".PadLeft(LineCharOffset, ' '),
                resumeComment ? CommentPrefix : "");
        }

        private void ReformatXmlPhrases()
        {
            var phrase = Phrases.First;
            while (phrase != null)
            {
                if (!phrase.Value.IsList)
                {
                    var word = phrase.Value.Words.First;
                    while (word != null)
                    {
                        var match = XmlTagRegex.Match(word.Value);
                        if (match.Success)
                        {
                            var tagName = match.Groups["tag"].Value;
                            bool isCloseTag = match.Groups["closetag"].Success;
                            bool isMajorTag = MajorXmlTags.Contains(tagName);

                            // Major tags and minor opening tags should be the first word.
                            if (word.Previous != null && (isMajorTag || (!isCloseTag && MinorXmlTags.Contains(tagName))))
                            {
                                // Previous word will be the last word of this phrase.
                                word = word.Previous;

                                // Create a new phrase with the rest of the words.
                                Phrases.AddAfter(phrase, new CodeCommentPhrase(
                                    phrase.Value.Indent,
                                    phrase.Value.IsList,
                                    phrase.Value.ListPrefix,
                                    GetWordsAfter(word)));

                                // Remove the rest.
                                while (word.Next != null)
                                    word.List.Remove(word.Next);
                            }

                            // Major tags should be the last word.
                            if (word.Next != null && isMajorTag)
                            {
                                Phrases.AddAfter(phrase, new CodeCommentPhrase(
                                    phrase.Value.Indent,
                                    phrase.Value.IsList,
                                    phrase.Value.ListPrefix,
                                    GetWordsAfter(word)));

                                while (word.Next != null)
                                    word.List.Remove(word.Next);
                            }
                        }

                        word = word.Next;
                    }
                }

                phrase = phrase.Next;
            }
        }

        private IEnumerable<string> GetWordsAfter(LinkedListNode<string> word)
        {
            while ((word = word.Next) != null)
                yield return word.Value;

            //while (word != null)
            //{
            //    yield return word.Value;
            //    word = word.Next;
            //}
        }
    }
}