using dropshit.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace dropshit
{
    public partial class frmMain : Form
    {
        private FileSorter fSorter = new FileSorter();

        public frmMain()
        {
            AllowDrop = true;
            InitializeComponent();

           
        }

        private void frmMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        
        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            lBox.Items.AddRange(files);

            int notCounted = fSorter.Sort(files);

            if (notCounted > 0 || notCounted >= files.Length)
            {
                MessageBox.Show(notCounted.ToString() + " Files were not sorted due to the fact of them having the same name as some of the ones that has already been sorted.");
                lBox.Items.Clear();
                return;
            }

            MessageBox.Show("Sorted (" + files.Length.ToString() + ") files");

            lBox.Items.Clear();
        }

        private void frmMain_Move(object sender, EventArgs e)
        {
            //When minimized we create a BallonTip at the bottom right of our screen
            //with a title and a message
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(5000, "Title", "Your Message", ToolTipIcon.Info);
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            //When the icon is double clicked we bring the form back up.
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            //When the balloonTip is clicked then we bring the form back up.
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
    }
}


