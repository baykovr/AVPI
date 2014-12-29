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
        string BUILD_VERSION = "GAVPI Alpha Build 0.04 12.18.14";

        public frmGAVPI()
        {
            InitializeComponent();
        }
        #region Main form

        //
        //  private void frmGAVPI_FormClosing( object, FormClosingEventArgs )
        //
        //  If the Form is closed, either explicitly (by clicking the close window button) or implicitly (by
        //  calling Form.Close) our frmGAVPI_FormClosing handler will prompt the user to save any unsaved Profile
        //  changes.
        //

        private void frmGAVPI_FormClosing( object sender, FormClosingEventArgs e )
        {

            //
            //  If a Profile has been previously modified via the Profile editor but those changes haven't been
            //  saved, offer the user an opportunity to save those changes before leaving the program.  (If the
            //  user cancels the ensuing Save File Dialog then don't exit the application out of common courtesy.)
            //

            if( GAVPI.vi_profile.IsEdited() ) {

                DialogResult saveChanges = MessageBox.Show( "It appears you have made changes to your Profile.\n\n" +
                                                             "Would you like to save those changes now?",
                                                             "Unsaved Profile",
                                                              MessageBoxButtons.YesNo );

                if( saveChanges == DialogResult.Yes ) {

                    if( GAVPI.vi_profile.ProfileFilename == null && !GAVPI.SaveAsProfile() ) e.Cancel = true;
                    else if (!GAVPI.SaveProfile()) e.Cancel = true;

                }  //  if()

            }  //  if()

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

            //  Attempt to open a Profile and, if successful, enable the Listen button.

            if( GAVPI.LoadProfile() ) btnMainListen.Enabled = true;  //  Enable the "Listen" button.
	     
            //  Maintain a consistent Form status...
       
            btmStripStatus.Text = "NOT LISTENING: " + ( GAVPI.vi_profile.IsEdited() ? "[UNSAVED] " : " " ) +
                Path.GetFileNameWithoutExtension( GAVPI.vi_profile.ProfileFilename );

            return;
        }
		
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        #endregion
        #region Profile

        // New Profile
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Exactly like modify, except we warn the user to save their current profile
            // before creating a new one.

            GAVPI.NewProfile();

            GAVPI.OpenProfileEditor();

        }

        // Modify Existing
        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            try
            {

                GAVPI.OpenProfileEditor();

                //
                //  VI_Profile takes care of tracking changes and the saved/unsaved state of the current Profile.
                //  We can act on this knowledge to update the status in the UI and also inform the user of those
                //  unsaved changes should they choose a potentially destructive act (exiting the program, opening
                //  an existing Profile).
                //
                
                btmStripStatus.Text = "NOT LISTENING: " + (GAVPI.vi_profile.IsEdited() ? "[UNSAVED] " : " ") +
                    Path.GetFileNameWithoutExtension(GAVPI.vi_profile.ProfileFilename);

                //  Allow the user to start issuing voice commands if we have an actual Profile...

                btnMainListen.Enabled = !GAVPI.vi_profile.IsEmpty();
                
            }
            catch (Exception profile_exception)
            {
                MessageBox.Show("Profile Editor Crashed.\n" + profile_exception.Message, "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }
            finally
            {

                // Not much we can do for now, invoke new profile
                // to attempt a clean up.

                GAVPI.NewProfile();

            }

        }  //  private void modifyToolStripMenuItem_Click( object, EventArgs )

        #endregion
        #region Settings
        private void mainStripSettings_Click(object sender, EventArgs e)
        {
            frmSettings modSettingsFrm = new frmSettings( GAVPI.vi_settings );
            modSettingsFrm.ShowDialog();
        }
        #endregion

        private void btnMainListen_Click(object sender, EventArgs e)
        {
		
			if( GAVPI.vi.load_listen( lstMainHearing ) ) {
				
				//
				//  We have successfully instantiated the speech recognition engine and we are ready to accept user
				//  input, so let's update the Status Bar's state to show "LISTENING" (while retaining the filename
				//  of the presently loaded Profile as a reminder for the user).  We also enable the "Stop" button,
				//  disable the "Listen" button and disable the Profile->Modify menu item.
				//
				
				btnMainStop.Enabled = true;
				btnMainListen.Enabled = false;
				editToolStripMenuItem.Enabled = false;

                btmStripStatus.Text = "LISTENING" + ( GAVPI.vi_profile.IsEdited() ? ": [UNSAVED] " : ": ") +
                    Path.GetFileNameWithoutExtension( GAVPI.vi_profile.ProfileFilename);
			
				return;
			
			}  //  if()
				
            btmStripStatus.Text = "NOT LISTENING" + ( GAVPI.vi_profile.IsEdited() ? ": [UNSAVED] " : ": ") +
                Path.GetFileNameWithoutExtension( GAVPI.vi_profile.ProfileFilename);

			GAVPI.vi = new VI();
			
        }

        private void btnMainStop_Click(object sender, EventArgs e)
        {
            // Stop
            GAVPI.vi.stop_listen();
            // Clean
            GAVPI.vi = new VI();

			//
			//  Let's refresh the User Interface by enabling the "Listen" button (so the user may commence speech
			//  recognition) and the Profile->Modify menu item, disable the "Stop" button (since we have already
			//  stopped listening) and then update the Status Bar to reflect our current state.
			//

            btnMainListen.Enabled = true;
			btnMainStop.Enabled = false;
			editToolStripMenuItem.Enabled = true;
			
            btmStripStatus.Text = "NOT LISTENING" + ( GAVPI.vi_profile.IsEdited() ? ": [UNSAVED] " : ": " ) +
                Path.GetFileNameWithoutExtension( GAVPI.vi_profile.ProfileFilename );

        }

        private void mainStripAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(BUILD_VERSION);
        }



        //
        //  public void RefreshUI( string )
        //
        //  Request that the User Interface updates any elements that are dependant on states that may change
        //  beyond the scope of the current thread of execution.  This method is typically called by way of the
        //  GAVPI class.
        //

        public void RefreshUI( string Status )
        {

            //  Refresh the UI...

            btmStripStatus.Text = Status;

        }  //  public void RefreshUI()



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
