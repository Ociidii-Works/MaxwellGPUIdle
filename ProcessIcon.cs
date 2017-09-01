﻿using System;
using System.Reflection;
using System.Windows.Forms;
using MaxwellGPUIdle.Properties;

namespace MaxwellGPUIdle
{
    /// <summary>
    /// </summary>
    internal class ProcessIcon : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        public static NotifyIcon ni;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessIcon" /> class.
        /// </summary>
        public ProcessIcon()
        {
            // Instantiate the NotifyIcon object.
            ni = new NotifyIcon();
        }

        /// <summary>
        /// Displays the icon in the system tray.
        /// </summary>
        public void Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);
            ni.Icon = Resources.MaxwellGPUIdle;
            ni.Text = "MaxwellGPUIdle";
            ni.Visible = true;

            // Attach a context menu.
            //ni.ContextMenuStrip = new ContextMenus().Create();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            ni.Dispose();
        }

        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">
        /// The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.
        /// </param>
        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            // Handle mouse button clicks.
            if (e.Button == MouseButtons.Left)
            {
                ni.ContextMenuStrip = new ContextMenus().CreateFeedsMenu();
                mi.Invoke(ni, null);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ni.ContextMenuStrip = new ContextMenus().CreateOptionsMenu();
                mi.Invoke(ni, null);
            }
        }
    }
}
