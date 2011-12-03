#region CodeMaid is Copyright 2007-2011 Steve Cadwallader.

// CodeMaid is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License version 3
// as published by the Free Software Foundation.
//
// CodeMaid is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details <http://www.gnu.org/licenses/>.

#endregion CodeMaid is Copyright 2007-2011 Steve Cadwallader.

using System;
using System.Diagnostics;
using System.Windows;

namespace SteveCadwallader.CodeMaid.Dialogs
{
    /// <summary>
    /// Interaction logic for AboutForm.xaml
    /// </summary>
    public partial class AboutForm
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutForm"/> class.
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Event Handlers

        /// <summary>
        /// Called when the Website link is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnWebsiteLinkClick(object sender, RoutedEventArgs e)
        {
            //TODO: Wire up Website link.
        }

        /// <summary>
        /// Called when the Email link is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnEmailLinkClick(object sender, RoutedEventArgs e)
        {
            LaunchLink(@"mailto:codemaid@gmail.com");
        }

        /// <summary>
        /// Called when the Twitter link is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnTwitterLinkClick(object sender, RoutedEventArgs e)
        {
            LaunchLink(@"http://twitter.com/codemaid/");
        }

        /// <summary>
        /// Called when the RSS link is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnRSSLinkClick(object sender, RoutedEventArgs e)
        {
            //TODO: Wire up RSS link.
        }

        /// <summary>
        /// Called when the Google+ link is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnGooglePlusLinkClick(object sender, RoutedEventArgs e)
        {
            //TODO: Wire up Google+ link.
        }

        #endregion Event Handlers

        #region Methods

        /// <summary>
        /// Attempts to launch the specified link.
        /// </summary>
        /// <param name="link">The link.</param>
        private static void LaunchLink(string link)
        {
            try
            {
                Process.Start(link);
            }
            catch (Exception)
            {
                // Do nothing if default application handler is not associated.
            }
        }

        #endregion Methods
    }
}