using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using TrivialLogging;

namespace GAVPI
{
    static class GAVPI
    {

        //  The application's system-wide unique ID to facilitate single-instancing (see Mutex, later).

        const string APPLICATION_ID = "Global\\" + "{c3ab185c-d7f7-4bf9-bc81-0f0e93d52ac3}";
        
        //  A message sent via IPC to an existing application instance.

        public static int WM_OPEN_EXISTING_INSTANCE;
        
        //
        //  The application global configuration settings, voice recognition grammar and voice recognition engine.
        //

        public static VI_Settings vi_settings;
        public static VI_Profile vi_profile;

        public static VI vi;

        private static frmGAVPI MainForm;
        private static frmProfile ProfileEditor;

        //  Our running Log...

        public static Logging< string > Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            //  Instantiate a log that maintains a running record of the recognised voice commands.  This log
            //  will be displayed within a ListBox in the main form, frmGAVPI.  We specify a callback method so
            //  that we may inform an already open frmGAVPI to update the ListBox with the log content.

            try { 

                Log = new Logging< string >( GAVPI.OnLogMessage );

            } catch(Exception e) { throw; }

            vi_settings = new VI_Settings();
            vi_profile = new VI_Profile( null );

            vi = new VI();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //
            //  To ensure that only a single instance of the application can be executed at a time, we'll use
            //  Mutex ownership to determine if we're already running.  I used http://www.guidgenerator.com/
            //  to create a Globally Unique ID (GUID) as the name of a Mutex, which the application will attempt
            //  to secure before running proper.  If the Mutex can't be secured it means our application is
            //  already running.
            //

            Mutex LockApplicationInstance;
            bool OnlyApplicationInstance = false;
            
            //
            //  We'll do something useful if the application is already running by sending it a message asking
            //  it to show itself.  This may be achieved through the native Win32 API PostMessage (an asynchronous
            //  call).  But regardless of whether another instance of the application is running or now, we should
            //  request a message that is unique to our application.
            //

            if( ( WM_OPEN_EXISTING_INSTANCE = Win32_APIs.RegisterWindowMessage( "WM_OPEN_EXISTING_INSTANCE" ) ) == 0 ) {

                throw new Win32Exception();

            }
     
            //  Now we can check to see if our application already running...

            if( ( ( LockApplicationInstance = new Mutex( true, APPLICATION_ID, out OnlyApplicationInstance ) ) != null ) &&
                !OnlyApplicationInstance ) {

                //
                //  It is already running.  Now assuming the user wants to do something productive, let's bring the
                //  existing instance to the front.  PostMessage is non-blocking native Win32 call, so will return
                //  immediately, whose intent is picked up in the frmGAVPI.WndProc() method of the existing instance.
                //

                Win32_APIs.PostMessage( (IntPtr)Win32_APIs.HWND_BROADCAST,
                                        WM_OPEN_EXISTING_INSTANCE,
                                        IntPtr.Zero,
                                        IntPtr.Zero );

                //  We can happily quit now.

                return;

            }  //  if()

            MainForm = new frmGAVPI();

            Application.Run( MainForm );

            //  We don't want the garbage collector to think our Mutex is up for grabs before we close the program,
            //  so let's protect it.

            GC.KeepAlive( LockApplicationInstance );

            LockApplicationInstance.ReleaseMutex();

        }  //  static void Main()



        //
        //  static public void OnLogMessage( string )
        //
        //  Our logging class accepts OnLogMessage as a callback function, where we request that frmGAVPI update
        //  the ListBox displaying the log content - via a call to frmGAVPI.RefreshUI().
        //

        static public void OnLogMessage( string loggedMessage )
        {

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( null );              
            
        }  //  static public void OnLogMessage( string )


        
        //
        //  static public bool StartListening()
        //
        //  Returning boolean success or failure, StartListening() attempts to establish command recognition.
        //

        static public bool StartListening()
        {

            //  If we're not already listening on voice commands, try to start listening (this is a sanity check
            //  that we shouldn't need, but to hell with it)...

            if( vi.IsListening ) return true;

            if( !vi.load_listen() ) return false;
            
            //  Update the main form's UI to reflect the listening state.

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( "LISTENING:" + ( vi_profile.IsEdited() ? " [UNSAVED] " : " " ) +
                    Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ) );

            return true;
            
        }  //  static public bool StartListening()



        //
        //  static public void StopListening()
        //
        //  Typically called via the user interface, StopListening() requests that the voice recognition engine
        //  stops listening for spoken commands.
        //

        static public void StopListening()
        {

            if( !vi.IsListening ) return;

            //  Stop listening on voice commands...

            vi.stop_listen();
            vi = new VI();

            //  Update the main form to reflect the stopped state...

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( "NOT LISTENING:" + ( vi_profile.IsEdited() ? " [UNSAVED] " : " " ) + 
                    Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ) );               

        }  //  static public void StopListening()



        //
        //  static public void OpenProfileEditor
        //
        //  While not absolutely necessary at the moment, this method is the prefered way of instantiating the
        //  Profile Editing form frmProfile.
        //

        static public void OpenProfileEditor()
        {

            ProfileEditor = new frmProfile();

            if( ProfileEditor == null ) return;
            
            ProfileEditor.ShowDialog();

            CloseProfileEditor();

            return;

        }  //  static public void OpenProfileEditor



        //
        //  static public void CloseProfileEditor()
        //
        //  CloseProfileEditor is the sanity-checking prefered way of closing the Profile editor form (frmProfile)
        //  when opened via OpenProfileEditor().
        //

        static public void CloseProfileEditor()
        {

            if( ProfileEditor == null ) return;

            ProfileEditor.Dispose();

            ProfileEditor = null;

        }  //  static public void CloseProfileEditor()



        //
        //  static private bool NotifyUnsavedChanges()
        //
        //  NotifyUnsavedChanges is employed by any method that acts destructively on an existing Profile with
        //  unsaved edits, prompting the user to persist the Profile.
        //

        static private bool NotifyUnsavedChanges()
        {

            DialogResult SaveChanges = MessageBox.Show( "It appears you have made changes to your Profile.\n\n" +
                                                        "Would you like to save those changes now?",
                                                        "Unsaved Profile",
                                                        MessageBoxButtons.YesNo );

            if( SaveChanges == DialogResult.Yes ) {

                if( vi_profile.GetProfileFilename() == null && !SaveAsProfile() ) return false;
                else if( !SaveProfile() ) return false;

            }  //  if()

            return true;

        }  //  static private bool NotifyUnsavedChanges()



        //
        //  static public bool LoadProfile()
        //
        //  A convenient centralised method returning boolean success or failure, LoadProfile directs vi_profile
        //  to read a Profile from disk while ensuring that any currently open form reflects the opened Profile
        //  status in its UI.  This method considers whether a currently open Profile has been edited yet remains
        //  unsaved, offering the opportunity to persist the Profile.
        //
        //  LoadProfile returns boolean success or failure.
        //

        static public bool LoadProfile()
        {

            //  Offer to persist any unsaved Profile edits...

            if (vi_profile.IsEdited() && NotifyUnsavedChanges() == false) return false;

            if( !vi_profile.load_profile() ) return false;

            //  Clear the log before requesting frmGAVPI refresh its UI otherwise the ListBox may remain populated.

            Log.Clear();            
            
            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ) );
                
            if( Application.OpenForms.OfType<frmProfile>().Count() > 0 )
                ProfileEditor.RefreshUI( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ) );

            return true;

        }  //  static public bool LoadProfile()



        //
        //  static public bool SaveAsProfile()
        //
        //  Assuming an existing Profile hasn't been previously saved, query the user for a filename then save
        //  the Profile.  Returns a boolean value denoting success or failure.
        //
        //  This method should be used instead of vi_profile.save_profile which will become DEPRECIATED.
        //

        static public bool SaveAsProfile()
        {

            if( vi_profile.IsEmpty() ) return false;

            using( SaveFileDialog dialog = new SaveFileDialog() ) {

                //  Give the Dialog a title then establish a default filter to hide anything that isn't an XML file.

                dialog.Title = "Save your Profile as...";
                dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";

                dialog.RestoreDirectory = true;

                if( dialog.ShowDialog() == DialogResult.Cancel ) return false;

                return vi_profile.save_profile( dialog.FileName ) ? true : false;

            }  //  using()           

        }  //  static public bool SaveAsProfile()



        //
        //  static public bool SaveProfile()
        //
        //  Save the Profile as maintained by vi_profile to the existing filename maintained within vi_profile.
        //  Returns a boolean value denoting success or failure.
        //

        static public bool SaveProfile()
        {

            if( vi_profile.IsEmpty() ) return false;

            return vi_profile.save_profile( vi_profile.GetProfileFilename() ) ? true : false;

        }  //  static public bool SaveProfile()
    
    

        //
        //  static public bool NewProfile()
        //
        //  A convenient stub to vi_profile.NewProfile, returning a boolean value for success or failure.  This
        //  method offers the user an opportunity to persist an existing Profile if there are unsaved edits.
        //

        static public bool NewProfile()
        {

            if( vi_profile.IsEdited() && !NotifyUnsavedChanges() ) return false;

            //  Clear the log before requesting frmGAVPI refresh its UI otherwise the ListBox may remain populated.

            Log.Clear();

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ) );

            return vi_profile.NewProfile() ? true : false;
            
        }  //  static public bool NewProfile()

    }  //  static class GAVPI

}
