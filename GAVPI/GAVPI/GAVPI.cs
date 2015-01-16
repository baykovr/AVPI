using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using TrivialLogging;
using TrivialMRUMenu;
using System.Text;
using System.Reflection;
using System.Management;
using System.Diagnostics;
using System.Xml;

namespace GAVPI
{
    static class GAVPI
    {

        //  The application's title, a system-wide unique ID to facilitate single-instancing (see Mutex,
        //  later), and an XML Path to easily extract specific information from GAVPI Profile XML documents.
       
        const string APPLICATION_TITLE = "GAVPI";

        const string APPLICATION_ID = "Global\\" + "{c3ab185c-d7f7-4bf9-bc81-0f0e93d52ac3}";       

        const string ASSOCIATED_PROCESS_XML_PATH = "/gavpi/AssociatedProcess";

        //  A message sent via IPC to an existing application instance.

        public static int WM_OPEN_EXISTING_INSTANCE;
        
        //  The application global configuration settings, voice recognition grammar and voice recognition
        //  engine.

        public static VI_Settings vi_settings;
        public static VI_Profile vi_profile;

        public static VI vi;

        private static frmGAVPI MainForm;
        private static frmProfile ProfileEditor;
        private static frmSettings SettingsForm;

        //  Our running Log and the Most Recently Used Profile list.

        public static Logging< string > Log;

        private static MRU ProfileMRU;
        
        private static ManagementEventWatcher EventWatcher;
        private static WqlEventQuery EventQuery;

        private static Dictionary< string, string > AssociatedProcesses = new 
            Dictionary< string, string >();

        //  We maintain a system tray icon and context menu...
        
        private static NotifyIcon sysTrayIcon;
        private static ContextMenu sysTrayMenu;

        //  Our system tray context menu items are declared for convenience.  If we add or remove items at some
        //  point, let's reflect that here.

        private static int OPEN_PROFILE_MENU_ITEM   = 1;
        private static int MODIFY_PROFILE_MENU_ITEM = 2;
        private static int OPEN_LOG_MENU_ITEM       = 4;
        private static int LISTEN_MENU_ITEM         = 6;
        private static int STOP_LISTENING_MENU_ITEM = 7;
        private static int OPEN_SETTINGS_MENU_ITEM  = 8;
        


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

            } catch( Exception ) { throw; }

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

            //
            //  A system tray icon and associated menu offers an alternative UI, providing the key functionality
            //  of the application in a convenient menu.
            //
            //  So, let's set the Tray Icon's tooltip name, use an instance of the application's icon (from the
            //  project's resources, and provide a handler for when a user double-clicks the system tray icon.

            sysTrayIcon = new NotifyIcon();

            sysTrayIcon.Icon  = Properties.Resources.gavpi;
            sysTrayIcon.Visible = true;
            sysTrayIcon.DoubleClick += new System.EventHandler( OnDoubleClickIcon );
            sysTrayIcon.Text = APPLICATION_TITLE;

            //  Our system tray icon's context menu consists of items that may be enabled or disabled depending
            //  on the available workflow.  By default, however, their initial states should be as declared.

            sysTrayMenu = new ContextMenu();

            //  Our MRU menu.  We'll add it to the top of the system tray icon's context menu.

            ProfileMRU = new MRU( "Recent", OnMRUListItem );
            
            sysTrayMenu.MenuItems.Add( ProfileMRU.GetMenu() );
            sysTrayMenu.MenuItems.Add( "Open Profile", LoadProfile );
            sysTrayMenu.MenuItems.Add( "Modify", OpenProfileEditor ).Enabled = false;
            sysTrayMenu.MenuItems.Add( "-" );
            sysTrayMenu.MenuItems.Add( "Show Log", OpenMainWindow );
            sysTrayMenu.MenuItems.Add( "-" );
            sysTrayMenu.MenuItems.Add( "Listen", StartListening ).Enabled = false;
            sysTrayMenu.MenuItems.Add( "Stop", StopListening ).Enabled = false;
            sysTrayMenu.MenuItems.Add( "-" );
            sysTrayMenu.MenuItems.Add( "Settings", OpenSettings);
            sysTrayMenu.MenuItems.Add( "-" );
            sysTrayMenu.MenuItems.Add( "Exit", Exit );

            //  And now we can attach the menu to the system tray icon.

            sysTrayIcon.ContextMenu = sysTrayMenu;

            //  Now let's load the MRU items before running the application propper.

            ProfileMRU.Deserialize();

            //  Let's commence monitoring process startup and automatically open Profiles if any have been
            //  associated with an executable.

            if( Properties.Settings.Default.EnableAutoOpenProfile ) EnableAutoOpenProfile( null, null );

            //  And display the main window upon start if specified in the configuration.

            if( Properties.Settings.Default.ShowGAVPI ) OpenMainWindow( null, null );

            Application.Run();          

            //  If we're monitoring process startup, let's stop doing so.

            if( Properties.Settings.Default.EnableAutoOpenProfile ) DisableAutoOpenProfile( null, null );

            //  We don't want the garbage collector to think our Mutex is up for grabs before we close the program,
            //  so let's protect it.

            GC.KeepAlive( LockApplicationInstance );

            LockApplicationInstance.ReleaseMutex();

        }  //  static void Main()



        //
        //  static private void Exit( object, EventArgs )
        //
        //  Our application exit routine, conveniently wrapped up in an event handler object (making it idea
        //  for calling from a UI element).
        //

        static private void Exit( object SelectedMenuItem, EventArgs e )
        {

            if( vi_profile.IsEdited() ) NotifyUnsavedChanges();

            //  Let's serialise the MRU before oblivion.

            ProfileMRU.Serialize();

            //  And persist any of the settings.

            Properties.Settings.Default.Save();

            //  And get rid of the system tray icon.

            sysTrayIcon.Dispose();

            Application.Exit();

        }  //  static private void Exit( object, EventArgs )



        //
        //  static private void OnDoubleClickIcon( object, EventArgs )
        //
        //  When the system tray icon is double-click by the user, let's display the main window.
        //

        static private void OnDoubleClickIcon( object sender, EventArgs e )
        {

            OpenMainWindow( sysTrayMenu.MenuItems[ OPEN_LOG_MENU_ITEM ], null );
            
        }  //   static private void OnDoubleClickIcon( object, EventArgs )



        //
        //  static public void OnLogMessage( string )
        //
        //  Our logging class accepts OnLogMessage as a callback function, where we request that frmGAVPI update
        //  the ListBox displaying the log content - via a call to frmGAVPI.RefreshUI().
        //

        static public void OnLogMessage( string loggedMessage )
        {

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 ) MainForm.RefreshUI( null );              
            
        }  //  static public void OnLogMessage( string )



        //
        //  static public void OnMRUListItem( MRU.MRUItem )
        //
        //  Our MRU management class accepts OnMRUListItem as a callback function, informing the user of the
        //  MRU item that was selected.  An object of type MRU.MRUItem is passed as the only argument.  If the
        //  associated Profile cannot be opened it will be removed from the MRU list.
        //

        static public void OnMRUListItem( MRU.MRUItem item )
        {

            if( !LoadProfile( ( string ) item.ItemData ) ) {

                ProfileMRU.Remove( item.ItemText );

            }  //  if()

        }  //  static public void OnMRUListItem( MRU.MRUItem )



        //
        //  static private void EnableAutoOpenProfile( object, EventArgs )
        //
        //  One of the features of GAVPI is that of associating a Profile with an executable file (whether
        //  an application, game, etc) and having that Profile automatically loaded whenever that executable
        //  is ran.  To facilitate this, a node is allocated within a Profile that holds the fully qualified
        //  filename of an associated executable; these associations are collated at initial GAVPI startup,
        //  and the assumption is made that all Profiles are stored within the Profiles sub-folder in which
        //  GAVPI resides.  We then request the Operating System inform GAVPI when a process starts, and via
        //  an event handler - OnProcessStarted() - we determine if the process is an associated process.
        //  If so, we automatically load the relevant Profile.  So...
        //

        static public void EnableAutoOpenProfile( object sender, EventArgs e )
        {
                   
            //  If we're already watching out for processes opening then let's not do so again...

            if( EventWatcher != null ) return;

            //  Get the path to the running instance of GAVPI, and from there expand it with the Profiles
            //  sub-folder.

            string ProfilePath = new Uri( System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ) ).LocalPath + "\\Profiles";
            
            XmlDocument Profile = new XmlDocument();
           
            //  Enumerate the XML Profiles within the sub-folder, extracting the filename of a user-chosen
            //  executable whose process startup we may monitor.  Exceptions are likely limited to badly formed
            //  XML - or some other data with an .XML extension.

            foreach( string ProfileFilename in Directory.EnumerateFiles( ProfilePath, "*.xml" ) ) {

                try {
                
                    Profile.Load( ProfileFilename );
                    
                    XmlNode AssociatedProcess = Profile.SelectSingleNode( ASSOCIATED_PROCESS_XML_PATH );
                
                    //  We'll store the executable's filename within a key/value Dictionary for later
                    //  scruitiny (see OnProcessStarted(), later).

                    if( AssociatedProcess != null && AssociatedProcess.InnerText != null )
                        AssociatedProcesses.Add( AssociatedProcess.InnerText, ProfileFilename );

                } catch( Exception ) { break; }               

            }  //  foreach()

            //  We can now establish and start a Management Event watcher, asking it to inform us when a
            //  process starts (we'll compare the process specifics with a list of processes we should
            //  automatically load a related Profile for).

            EventWatcher = null;
            EventQuery = null;          
        
            try {
    		
                EventQuery = new WqlEventQuery();
    		    EventQuery.EventClassName = "Win32_ProcessStartTrace";
    		    
                EventWatcher = new ManagementEventWatcher( EventQuery );
    		    EventWatcher.EventArrived += new EventArrivedEventHandler( OnProcessStarted );

                //  We've told the Management Event watcher that we're insterestd in processes starting, so
                //  let's set it in motion.

    		    EventWatcher.Start();
    		                
            } catch( Exception ) {}   	        
        
        }  //  static private void EnableAutoOpenProfile( object, EventArgs )



        //
        //  static private void DisableAutoOpenProfile( object, EventArgs )
        //
        //  Stop watching for the startup of processes.
        //

        static public void DisableAutoOpenProfile( object sender, EventArgs e )
        {
       
            //  If we're not monitoring process starts, let's not go any further.

            if( EventWatcher == null ) return;

            try {
                
                //  We no longer need the services of our Management Event watcher...

                EventWatcher.Stop();
        
            } catch( Exception) { }       
        
        }  //  static private void DisableAutoOpenProfile( object, EventArgs )



        //
        //  static private void OnProcessStarted( object, EventArrivedEventArgs )
        //
        //  Receive an event containing process-relevant information whenever a process is started.
        //

        static private void OnProcessStarted( object sender, EventArrivedEventArgs managementEvent )
        {

            string Filename = null;

            //  Let's attempt to get the fully qualified filename of the newly started process.  If the call
            //  to GetProcessById() throws an exception it is likely because we received an event relating to
            //  a process that is has stopped (hence the process ID will be an invalid argument).

            try { 

                Filename = Process.GetProcessById(  Convert.ToInt32( managementEvent
                              .NewEvent.Properties[ "ProcessID" ].Value ) ).MainModule.FileName;

            } catch( Exception ) { return; }

            //  If the filename of the newly started process exists within our dictionary of processes to watch
            //  out for, load the related Profile...

            if( Filename != null && AssociatedProcesses.ContainsKey( Filename ) ) {

                LoadProfile( AssociatedProcesses[ Filename ] );

                //  If the user has selected the option to automatically begin Listening, in Settings, let's
                //  begin Listening...

                if( Properties.Settings.Default.EnableAutoListen ) StartListening( null, null );

            }  //  if()

        }  //  static private void ProcessStarted( object, EventArrivedEventArgs )



        //
        //  static public string GetStatusString()
        //
        //  Build a string based on the state of the voice recognition engine, the loaded profile and its
        //  edited state.  This string is typically displayed within the status bar of prominent forms, like
        //  frmGAVPI and frmProfile.
        //

        static public string GetStatusString()
        {                  
        
            return ( vi.IsListening ? "LISTENING:" : "NOT LISTENING:" ) + " " +
                   ( vi_profile.IsEdited() ? " [UNSAVED] " : " " ) +
                   Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() );
        
        }  //  static public string GetStatusString()

        

        //
        //  static public void StartListening()
        //
        //  StartListening() attempts to establish command recognition.
        //

        static public void StartListening( object SelectedMenuItem, EventArgs e )
        {

            //  If we're not already listening on voice commands, try to start listening (this is a sanity
            //  check that we shouldn't need, but to hell with it)...

            if( vi.IsListening || !vi.load_listen() ) return;
            
            //  Update the main form's UI to reflect the listening state.

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( GetStatusString() );
            
            //  ...And mirror the state in the system tray menu...

            sysTrayMenu.MenuItems[ LISTEN_MENU_ITEM ].Enabled = false;
            sysTrayMenu.MenuItems[ STOP_LISTENING_MENU_ITEM ].Enabled = true;

            //  ...Then disable the option to modify the Profile from the system tray...

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = false;

            //  ...While conveniently changing the icon.

            sysTrayIcon.Icon = Properties.Resources.gavpi_listening;          

        }  //  static public void StartListening()



        //
        //  static public void StopListening()
        //
        //  Typically called via the user interface, StopListening() requests that the voice recognition engine
        //  stops listening for spoken commands.
        //

        static public void StopListening( object SelectedMenuItem, EventArgs e )
        {

            if( !vi.IsListening ) return;

            //  Stop listening on voice commands...

            vi.stop_listen();
            vi = new VI();

            //  Update the main form to reflect the stopped state...

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( GetStatusString() );               

            //  Update the system tray menu to reflect the state...

            sysTrayMenu.MenuItems[ LISTEN_MENU_ITEM ].Enabled = true;
            sysTrayMenu.MenuItems[ STOP_LISTENING_MENU_ITEM ].Enabled = false;

            //  ...And enable to option to edit the Profile from the system tray...

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = true;

            //  ...Then finish up by returning the system tray icon to the application's default.

            sysTrayIcon.Icon = Properties.Resources.gavpi;

        }  //  static public void StopListening()



        //
        //  static public void OpenMainWindow( object, EventArgs )
        //
        //  A convenient sanity-checking method for opening the main window.
        //

        static public void OpenMainWindow( object SelectedMenuItem, EventArgs e )
        {

            MainForm = new frmGAVPI();

            if( MainForm == null ) return;
           
            //  Let's disable the system tray menu item that allows a user to open the main window (since it
            //  will already be open.

            sysTrayMenu.MenuItems[ OPEN_LOG_MENU_ITEM ].Enabled = false;

            MainForm.ShowDialog();

            CloseMainForm();

        }  //  static public void OpenMainWindow( object, EventArgs )



        //
        //  static public void CloseMainForm()
        //
        //  CloseMainForm() is the sanity-checking preferred way of programmatically closing the main form
        //  window when opened by a call to OpenMainWindow().
        //

        static public void CloseMainForm()
        {
        
            if( MainForm == null ) return;
        
            MainForm.Dispose();

            MainForm = null;

            sysTrayMenu.MenuItems[ OPEN_LOG_MENU_ITEM ].Enabled = true;

        }  //  static public void CloseMainForm()



        //
        //  static public void OpenProfileEditor
        //
        //  This method is the prefered way of instantiating the Profile editing form, frmProfile.
        //

        static public void OpenProfileEditor( object SelectedMenuItem, EventArgs e )
        {

            ProfileEditor = new frmProfile();

            if( ProfileEditor == null ) return;
            
            //  For now, at least, stop listening if the Profile is in danger of being edited.

            StopListening( null, null );

            //  We don't want the user to be able to open two instances of the Profile editing form, so let's
            //  disable the system tray menu item.

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = false;

            //  And we don't want the user to be able to listen on commands while we're editing the Profile (at
            //  least yet)...

            sysTrayMenu.MenuItems[ LISTEN_MENU_ITEM ].Enabled = false;

            ProfileEditor.ShowDialog();

            CloseProfileEditor();

            return;

        }  //  static public void OpenProfileEditor



        //
        //  static public void CloseProfileEditor()
        //
        //  CloseProfileEditor is the sanity-checking prefered way of closing the Profile editor form
        //  (frmProfile) when opened via OpenProfileEditor().
        //

        static public void CloseProfileEditor()
        {

            if( ProfileEditor == null ) return;

            ProfileEditor.Dispose();

            ProfileEditor = null;

            //  Let's allow the user to open the Profile editor from within the system tray's menu once more.

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = true;

            //  And let's also allow the user to commence listening on voice recognition commands.

            sysTrayMenu.MenuItems[ LISTEN_MENU_ITEM ].Enabled = true;

        }  //  static public void CloseProfileEditor()



        //
        //  static private void OpenSettings()
        //
        //  Open the Settings window.
        //

        static public void OpenSettings( object SelectedMenuItem, EventArgs e )
        {

            //  If the Settings form is already open, bring it to the front...

            if( Application.OpenForms.OfType< frmSettings >().Count() > 0 ) {

                SettingsForm.TopMost = true;

                return;

            }  //  if()
                        
            SettingsForm = new frmSettings( vi_settings );
            
            SettingsForm.Show();               
        
        }  //  static private void OpenSettings()



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
        //  status in its UI.  This method considers whether a currently open Profile has unsaved edits, offering
        //  the opportunity to persist the Profile.
        //
        //  LoadProfile returns boolean success or failure.
        //

        static public bool LoadProfile( string filename )
        {
          
            //  Offer to persist any unsaved Profile edits...

            if( vi_profile.IsEdited() && NotifyUnsavedChanges() == false ) return false;

            //  Present the user with a File Open Dialog through which they may choose a Profile to load.
            
            if( filename == null ) using( OpenFileDialog profile_dialog = new OpenFileDialog() ) {

                //  Give the Dialog a title and then establish a filter to hide anything that isn't an XML
                //  file by default.

                profile_dialog.Title = "Select a Profile to open";
                profile_dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";
                profile_dialog.RestoreDirectory = true;

                if ( profile_dialog.ShowDialog() == DialogResult.Cancel ) return false;

                //  Save the loaded Profile's filename for convenience sake.

                filename = profile_dialog.FileName;

            }  //  if()... using()...
            
            //  Attempt to load the given Profile...

            if( !vi_profile.load_profile( filename ) ) {
             
				MessageBox.Show( "There appears to be a problem with the Profile you have chosen.\n\n" +
				                 "The Profile may have been moved or deleted, or it may not be an\n" +
                                 "actual Profile. It may even have become corrupted. Please check\n" +
                                 "the Profile by attempting to open it in the Profile Editor.",
								 "I cannot open the Profile",
								 MessageBoxButtons.OK,
								 MessageBoxIcon.Exclamation,
		                         MessageBoxDefaultButton.Button1 );		   

                return false;

            }  //  if()

            //  Clear the log before requesting frmGAVPI refresh its UI otherwise the ListBox may remain
            //  populated.

            Log.Clear();            
            
            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( GetStatusString() );
                
            if( Application.OpenForms.OfType<frmProfile>().Count() > 0 )
                ProfileEditor.RefreshUI( GetStatusString() );

            //  Let's enable or disable specific system tray menu items to reflect the application's state,
            //  and the viable workflow.  Finish by updating the system tray icon's tooltip to reflect the
            //  loaded Profile.

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = true;
            
            sysTrayMenu.MenuItems[ LISTEN_MENU_ITEM ].Enabled = true;
            sysTrayMenu.MenuItems[ STOP_LISTENING_MENU_ITEM ].Enabled = false;

            sysTrayIcon.Text = APPLICATION_TITLE + ": " +
                Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() );

            //  And now we can add the loaded Profile to the MRU list.

            ProfileMRU.Add( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ),
                vi_profile.GetProfileFilename() );

            return true;

        }  //  static public bool LoadProfile()



        //
        //  static public void LoadProfile( object, EventArgs )
        //
        //  An overloaded instance of LoadProfile( string ), suitable as a menu item click handler.
        //

        static public void LoadProfile( object SelectedMenuItem, EventArgs e )
        {

            LoadProfile( null );
        
        }  //  static public void LoadProfile( object, EventArgs )



        //
        //  static public bool LoadProfile()
        //
        //  An overloaded instance of LoadProfile( string ) ideally suited for untargetted Profile loading.
        //

        static public bool LoadProfile()
        {

            return LoadProfile( null );
        
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

                //  Give the Dialog a title then establish a default filter to hide anything that isn't an XML
                //  file.

                dialog.Title = "Save your Profile as...";
                dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";

                dialog.RestoreDirectory = true;

                if( dialog.ShowDialog() == DialogResult.Cancel ) return false;
                
                //  Let's update the system tray icon's tooltip text to reflect the name of the Profile.

                sysTrayIcon.Text = APPLICATION_TITLE + ": " +
                    Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() );
               
                //  And then add the saved Profile to the MRU list.

                ProfileMRU.Add( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ),
                    vi_profile.GetProfileFilename() );

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

            //  Let's add the saved Profile to the MRU list.

            ProfileMRU.Add( Path.GetFileNameWithoutExtension( vi_profile.GetProfileFilename() ),
                vi_profile.GetProfileFilename() );

            return vi_profile.save_profile( vi_profile.GetProfileFilename() ) ? true : false;

        }  //  static public bool SaveProfile()
    
    

        //
        //  static public bool NewProfile()
        //
        //  A convenient stub to vi_profile.NewProfile, returning a boolean value for success or failure.
        //  This method offers the user an opportunity to persist an existing Profile if there are unsaved
        //  edits.
        //

        static public bool NewProfile()
        {

            if( vi_profile.IsEdited() && !NotifyUnsavedChanges() ) return false;

            //  Clear the log before requesting frmGAVPI refresh its UI otherwise the ListBox may remain
            //  unpopulated.

            Log.Clear();

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( GetStatusString() );

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = true;

            return vi_profile.NewProfile() ? true : false;
            
        }  //  static public bool NewProfile()

    }  //  static class GAVPI

}
