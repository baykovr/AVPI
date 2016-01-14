using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GAVPI
{
    public partial class frmGAVPI : Form
    {

        public frmGAVPI()
        {

            InitializeComponent();

        }
        #region Main form
        



        //
        //  private void frmGAVPI_Activated( object, System.EventArgs )
        //
        //  Handling Activated events upon a Form offers the opportunty of a callback whenever the Form gains
        //  focus.
        //

        private void frmGAVPI_Activated( object sender, System.EventArgs e )
        {
            
            RefreshUI( null );

        }  //  private void frmGAVPI_Activated( object, System.EventArgs )




        //
        //  private void frmGAVPI_FormClosing( object, FormClosingEventArgs )
        //
        //  If the Form is closed, either explicitly (by clicking the close window button) or implicitly (by
        //  calling Form.Close) our frmGAVPI_FormClosing handler will prompt the user to save any unsaved Profile
        //  changes.
        //

        private void frmGAVPI_FormClosing( object sender, FormClosingEventArgs e )
        {
            if (GAVPI.IsFirstClose)
            {
                GAVPI.sysTrayIcon.BalloonTipTitle = "GAVPI is still running.";
                GAVPI.sysTrayIcon.BalloonTipText =
                    "The GAVPI application is still listening in the background and can be accessed from the system tray.";
                GAVPI.sysTrayIcon.ShowBalloonTip(5000);

<<<<<<< HEAD
                GAVPI.IsFirstClose = false;
            }
=======
<<<<<<< HEAD:GAVPI/GAVPI/GUI/frmGAVPI.cs
=======
                GAVPI.IsFirstClose = false;
            }
>>>>>>> 4ef8287f2083c27d4a673aa934f82904e10d6a28:GAVPI/GAVPI/frmGAVPI.cs
>>>>>>> AdamJamesNaylor-master
        }  //  private void frmGAVPI_FormClosing

        //
        //  loadToolStripMenuItem_Click()
        //
        //  A handler for the Load Profile menu item in the File menu, allowing the user to select an existing
        //  Binding Definition Profile.  The Binding Definition Profile may be edited and saved within frmProfile.
        //

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //  If we're Listening for voice commands, stop Listening since we may be about to open another
            //  Profile...

            if( GAVPI.vi.IsListening ) btnMainStop_Click( sender, e );

            //  Attempt to open a Profile and, if successful, enable the Listen button and the Profile->Modify
            //  menu item (these items should both be disabled in the absense of a loaded Profile during
            //  the frmGAVPI_Activated event handler).

            if( GAVPI.LoadProfile() ) {

                btnMainListen.Enabled = true;           
                editToolStripMenuItem.Enabled = true;   

            }  //  if()

            //  Maintain a consistent Form status...
       
            btmStripStatus.Text = GAVPI.GetStatusString();

            return;
        }
		
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            GAVPI.ShowTrayMessage("GAVPI", "is still running, close it from the tray.");
        }
        
        #endregion
        #region Profile

        // New Profile
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exactly like modify, except we warn the user to save their current profile
            // before creating a new one.

            if( !GAVPI.NewProfile() ) return;

            //  Refer to our general Profile editing handler...

            modifyToolStripMenuItem_Click( sender, e );
                        
        }

        // Modify Existing
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            try
            {

                GAVPI.OpenProfileEditor( sender, e );
                
                //  Enable both the Listen button and the Profile->Modify menu item if the current Profile is
                //  populated.

                if( GAVPI.vi_profile.IsEmpty() ) return;
                
                btnMainListen.Enabled = true;
                editToolStripMenuItem.Enabled = true;

                //
                //  VI_Profile takes care of tracking changes and the saved/unsaved state of the current Profile.
                //  We can act on this knowledge to update the status in the UI and also inform the user of those
                //  unsaved changes should they choose a potentially destructive act (exiting the program, opening
                //  an existing Profile).
                //

                btmStripStatus.Text = GAVPI.GetStatusString();
                
            }
            catch (Exception profile_exception)
            {
                MessageBox.Show("Profile Editor Crashed.\n" + profile_exception.Message, "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);

                GAVPI.vi_profile.NewProfile();

            }

        }  //  private void modifyToolStripMenuItem_Click( object, EventArgs )

        #endregion
        #region Settings
        private void mainStripSettings_Click(object sender, EventArgs e)
        {
           
            GAVPI.OpenSettings( sender, e );

        }
        #endregion

        private void btnMainListen_Click(object sender, EventArgs e)
        {
		
            GAVPI.StartListening( sender, e );
          			
        }

        private void btnMainStop_Click(object sender, EventArgs e)
        {

            GAVPI.StopListening( sender, e );
            
        }

        private void mainStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GAVPI.BUILD_VERSION);
        }



        //
        //  public void RefreshUI( string )
        //
        //  Request that the User Interface updates any elements that are dependant on states that may change
        //  beyond the scope of the current thread of execution.  This method is typically called by way of the
        //  GAVPI class.
        //

        public void RefreshUI( string status )
        {

            //  Ensure the log is updated to reflect any additional entries and move to the last log item...

            lstMainHearing.DataSource = GAVPI.Log.ToArray();
            lstMainHearing.TopIndex = GAVPI.Log.Count() - 1;

            //  The UI reflects a different workflow depending on whether it is currently listening on voice
            //  recognition commands or otherwise.

            if( GAVPI.vi.IsListening ) { 
            
                btnMainStop.Enabled = true;
				btnMainListen.Enabled = false;
				editToolStripMenuItem.Enabled = false;
                
            } else { 
            
                btnMainListen.Enabled = true;
	    		btnMainStop.Enabled = false;
		    	editToolStripMenuItem.Enabled = true;
			
            }  //  if()

            //  If a Profile isn't loaded, disable Profile->Modify and the Listen button.

            if( GAVPI.vi_profile.IsEmpty() ) {

                btnMainListen.Enabled = false;
                editToolStripMenuItem.Enabled = false;

            }

            //  If we don't wish to update the status text, simply pass null as an argument to RefreshUI()

            if( status != null ) btmStripStatus.Text = status;
            else btmStripStatus.Text = GAVPI.GetStatusString();
            
        }  //  public void RefreshUI( string )



        //
        //  protected override void WndProc( ref Message )
        //
        //  (See GAVPI.cs and Win32_APIs.cs)
        //
        //  In Win32 circles - for those who choose to program in C (or C++ but without the convenience of
        //  Microsoft's object orientated APIs - WndProc is a function that receives messages sent to a Window
        //  and lets us act upon them.  Win32 offers a far more powerful API than C# natively allows, which
        //  means if we want fine-grained control over our application we must venture into arcana.  This
        //  implementation of WndProc is entirely for the convenience of supporting IPC between an existing
        //  instance of the application and any future instances.
        //

        protected override void WndProc( ref Message message )
        {

            //  A trivial message handler, we're only interested in the message we have registered in GAVPI.cs
            //  that asks us to open an existing instance of the application: WM_OPEN_EXISTING_INSTANCE.

            if( message.Msg == GAVPI.WM_OPEN_EXISTING_INSTANCE ) {

                //  If we're minimized, let's unminimize ourself...

                if( WindowState == FormWindowState.Minimized ) WindowState = FormWindowState.Normal;            

                //  Pulse ourself as the topmost window...

                TopMost = true;
                TopMost = false;

                //  ...Then active the window for the convenience of the user.

                this.Activate();

            }  //  if()

            //  Be sure to pass the message to the base WndProc for behind-the-scenes post-processing.
        
            base.WndProc( ref message );

        }  //  protected override void WndProc( ref Message )
    }
}
