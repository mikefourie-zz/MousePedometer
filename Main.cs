namespace MousePedometer
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Main form
    /// </summary>
    public partial class Main : Form
    {
        #region private members
        /// <summary>
        /// instance object to hook into the mouse / keyboard events
        /// </summary>
        private UserActivityHook actHook;
        
        /// <summary>
        /// counter to keep track of the left mouse clicks
        /// </summary>
        private int leftCounter;
        
        /// <summary>
        /// counter to keep track of the right mouse clicks
        /// </summary>
        private int rightCounter;
        #endregion
        
        /// <summary>
        /// Initializes a new instance of the Form1 class.
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Mouses the moved.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void MouseMoved(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 0)
            {
                switch (e.Button.ToString())
                { 
                    case "Left":
                        this.leftCounter++;
                        break;
                    case "Right":
                        this.rightCounter++;
                        break;
                }
                
                // now check the counters
                this.CheckCounters();
            }
        }

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.actHook = new UserActivityHook();
            this.actHook.OnMouseActivity += new MouseEventHandler(this.MouseMoved);
            this.actHook.KeyDown += new KeyEventHandler(this.LogKeyPress);

            // this.actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);jjj
            // this.actHook.KeyUp += new KeyEventHandler(MyKeyUp);
            this.actHook.Start();

            // show a baloon tip
            notifyIcon1.BalloonTipTitle = "Mouse Pedometer - " + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
            notifyIcon1.BalloonTipText = "The Mouse Pedometer is active.";
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.ShowBalloonTip(2000);
        }
        
        /// <summary>
        /// Checks the counters
        /// </summary>
        private void CheckCounters()
        {
            // show the tooltip for every 1000 clicks
            if (((this.leftCounter + this.rightCounter) % 1000) == 0)
            {
                notifyIcon1.ShowBalloonTip(5);
            }
        }

        /// <summary>
        /// Handles the Click event of the exitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the MouseClick event of the notifyIcon1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.ToString() == "Left")
            {
                // setup the baloon tip
                notifyIcon1.BalloonTipTitle = "Mouse Pedometer - " + System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion.ToString();
                notifyIcon1.BalloonTipText = "Left: " + this.leftCounter.ToString() + "  Right: " + this.rightCounter.ToString() + "  Total: " + (this.leftCounter + this.rightCounter).ToString();
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.ShowBalloonTip(3000);
            }
        }

        /// <summary>
        /// Logs the key press.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> instance containing the event data.</param>
        private void LogKeyPress(object sender, KeyEventArgs e)
        {
           // odo: e.KeyChar.ToString();
        }

        /// <summary>
        /// Handles the 1 event of the resetToolStripMenuItem_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ResetToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.leftCounter = this.rightCounter = 0;
        }
    }
}