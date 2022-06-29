﻿using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;
using System.Reflection;
using System.Xml;

using TrivialLogging;
using TrivialMRUMenu;
using TrivialProcessMonitor;

namespace GAVPI
{

    static class GAVPI
    {

        //  The application's title, a system-wide unique ID to facilitate single-instancing (see Mutex,
        //  later), and an XML Path to easily extract specific information from GAVPI Profile XML documents.

        public const string BUILD_VERSION = "22.06.28";

        const string APPLICATION_TITLE = "GAVPI";

        const string APPLICATION_ID = "Global\\" + "{c3ab185c-d7f7-4bf9-bc81-0f0e93d52ac3}";       

        const string ASSOCIATED_PROCESS_XML_PATH = "/gavpi/AssociatedProcess";

        //  A message sent via IPC to an existing application instance.

        public static int WM_OPEN_EXISTING_INSTANCE;
        
        //  The application global configuration settings, voice recognition grammar and voice recognition
        //  engine.

        public static Settings Settings;
        public static Profile Profile;

        public static InputEngine vi;

        private static frmGAVPI MainForm;
        private static frmProfile ProfileEditor;
        private static frmSettings SettingsForm;

        //  Our running Log and the Most Recently Used Profile list.
        public static Logging< string > Log;

        //  Profile loading/unload debug log.
        public static Logging< string > ProfileDebugLog;

        private static MRU ProfileMRU;
        
        //
        //  GAVPI may, optionally, automatically load a Profile when a specified process has started, and
        //  commence Listening (stopping Listening automatically if the process is stopped).  We maintain
        //  a dictionary consisting of the fully qualified filename of the executable whose startup we may
        //  detect as the key, and the Profile's filename we may associate with the executable as a value.
        //
        //  The AssociatedProcessMonitor object, when started, will issue callbacks when executables run
        //  or terminate, providing relevant information.
        //

        private static ProcessMonitor AssociatedProcessMonitor;
        private static Dictionary< string, string > AssociatedProfiles = new Dictionary< string, string >();

        //  We maintain a system tray icon and context menu...
        
        public static NotifyIcon sysTrayIcon;
        private static ContextMenu sysTrayMenu;
        public static bool IsFirstClose = true; //is this the first time the main window has been closed

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

            // Additionally we maintain of a log of profile loading/saving information.
            try { 

                Log = new Logging< string >( GAVPI.OnLogMessage );

                ProfileDebugLog = new Logging<string>();

            } catch( Exception ) { throw; }


            Settings = new Settings();
            Profile = new Profile( null );

            vi = new InputEngine();
            
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
            //  associated with an executable and the option to do so has been set.

            if( Properties.Settings.Default.EnableAutoOpenProfile ) EnableAutoOpenProfile( null, null );

            //  And display the main window upon start if specified in the configuration.

            if( Properties.Settings.Default.ShowGAVPI ) OpenMainWindow( null, null );

            Application.Run();          

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

            if( Profile.IsEdited() ) NotifyUnsavedChanges();

            //  Let's serialise the MRU before oblivion.

            ProfileMRU.Serialize();

            //  If we're monitoring process startup, let's stop doing so.

            if( Properties.Settings.Default.EnableAutoOpenProfile ) DisableAutoOpenProfile( null, null );

            //  And persist any of the settings.

            Properties.Settings.Default.Save();

            // Stop listening.
            StopListening(null, null);

            //  And get rid of the system tray icon.
            if (sysTrayIcon != null)
            {
                // Tray icon must be set to null in order to keep it from lingering in the tray.
                sysTrayIcon.Visible = false;
                sysTrayIcon.Dispose();
                sysTrayIcon = null;
            }

            Application.Exit();

        }  //  static private void Exit( object, EventArgs )



        //
        //  static private void OnDoubleClickIcon( object, EventArgs )
        //
        //  When the system tray icon is double-click by the user, let's display the main window.
        //

        static private void OnDoubleClickIcon( object sender, EventArgs e )
        {
            // Don't spawn multiple main windows on double click.
            if (sysTrayMenu.MenuItems[OPEN_LOG_MENU_ITEM].Enabled)
            {
                OpenMainWindow(sysTrayMenu.MenuItems[OPEN_LOG_MENU_ITEM], null);
            }
            
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

            if( !LoadProfile( ( string ) item.ItemData ) ) ProfileMRU.Remove( item.ItemText );

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
        //  a trivial process monitor with callbacks.
        //

        static public void EnableAutoOpenProfile( object sender, EventArgs e )
        {
                   
            //  Get the path to the running instance of GAVPI, and from there expand it with the Profiles
            //  sub-folder.

            string ProfilePath = new Uri( System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ) ).LocalPath + "\\Profiles";
            
            //  If there isn't a Profiles directory within the directory GAVPI was launched from, go no further.

            if( !Directory.Exists( ProfilePath ) ) return;

            XmlDocument Profile = new XmlDocument();
           
            //  Enumerate the XML Profiles within the sub-folder, extracting the filename of a user-chosen
            //  executable whose process startup we may monitor.  Exceptions are likely limited to badly formed
            //  XML - or some other kind of data but with an .XML extension.

            foreach( string ProfileFilename in Directory.EnumerateFiles( ProfilePath, "*.xml" ) ) {

                try {
                
                    Profile.Load( ProfileFilename );
                    
                    XmlNode AssociatedProcess = Profile.SelectSingleNode( ASSOCIATED_PROCESS_XML_PATH );
                
                    //  We'll store the executable's filename within a key/value Dictionary for later
                    //  scruitiny (see OnProcessStarted(), later).

                    if( AssociatedProcess != null && AssociatedProcess.InnerText != null ) 
                        AddAutoOpenProfile( AssociatedProcess.InnerText, ProfileFilename );

                } catch( Exception ) { break; }               

            }  //  foreach()

            //  We'll start the process monitor whether we have a associations to monitor since associations may
            //  be added at runtime via frmProfile.

            AssociatedProcessMonitor = new ProcessMonitor( OnProcessStarted, OnProcessStopped );

            AssociatedProcessMonitor.Start();

        }  //  static private void EnableAutoOpenProfile( object, EventArgs )



        //
        //  static private void DisableAutoOpenProfile( object, EventArgs )
        //
        //  Stop watching for the startup of processes.
        //

        static public void DisableAutoOpenProfile( object sender, EventArgs e )
        {
            // A quick fix. Found that sometimes the process monitor is null in testings
            // Robert (04.23.15)
            if (AssociatedProcessMonitor == null)
            {
                AssociatedProcessMonitor = new ProcessMonitor(OnProcessStarted, OnProcessStopped); 
            }
       
            AssociatedProcessMonitor.Stop();    
        
        }  //  static private void DisableAutoOpenProfile( object, EventArgs )



        //
        //  static public void AddAutoOpenProfile( string, string )
        //
        //  Associated an executable with a given Profile.
        //

        static public void AddAutoOpenProfile( string executableFilename, string profileFilename )
        {
        
            //  If profile already exits in list, remove it (we'll replace it).  Then add the association.

            RemoveAutoOpenProfile( profileFilename );

            AssociatedProfiles.Add( executableFilename, profileFilename );
        
        }  //  static public void AddAutoOpenProfile( string executableFilename, string profileFilename )



        //
        //  static public void RemoveAutoOpenProfile( string )
        //
        //  Remove an executable associated with the given profile Filename.
        //

        static public void RemoveAutoOpenProfile( string profileFilename )
        {
        
            if( AssociatedProfiles.ContainsValue( profileFilename ) )
                AssociatedProfiles.Remove( AssociatedProfiles.First( x => x.Value == profileFilename ).Key );
        
        }  //  static public void RemoveAutoOpenProfile( string profileFilename )



        //
        //  static private void OnProcessStarted( Int32, string )
        //
        //  Receive an event containing process-relevant information whenever a process is started.  If the process
        //  fully qualified filename has been associated with a Profile, we load the Profile and optionally commence
        //  Listening.
        //

        static private void OnProcessStarted( Int32 processID, string processFilename )
        {

            //  If the process isn't one we have previously recognised as being associated with a Profile then go no
            //  further, otherwise load the associated Profile if it isn't already loaded and, if the user opted to
            //  automatically Listen on the Profile, begin listening...

            if( !AssociatedProfiles.ContainsKey( processFilename ) ) return;

            if( vi.IsListening ) StopListening( null, null );

            if( Profile.GetProfileFilename() != AssociatedProfiles[ processFilename ] )
                LoadProfile( AssociatedProfiles[ processFilename ] );
                
            if( Properties.Settings.Default.EnableAutoListen ) StartListening( null, null );

        }  //  static private void OnProcessStarted( Int32, string )



        //
        //  static private void OnProcessStopped( Int32, string )
        //
        //  Receive an event containing process-relevant information whenever a process is stopped.  If that process
        //  caused GAVPI to start Listening on voice commands via OnProcessStarted() then stop Listening.
        //

        static private void OnProcessStopped( Int32 processID, string processFilename )
        {

            //  If the process is one whose running and terminated states we are tracking and auto-listen is enabled,
            //  stop Listening.

            if( vi.IsListening &&
                processFilename != null &&
                AssociatedProfiles.ContainsKey( processFilename ) && 
                Properties.Settings.Default.EnableAutoListen &&
                Profile.GetProfileFilename() == AssociatedProfiles[ processFilename ] ) StopListening( null, null );

        }  //  static private void OnProcessStopped( Int32, string )



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
                   ( Profile.IsEdited() ? " [UNSAVED] " : " " ) +
                   Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() );

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
            vi = new InputEngine();

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
                        
            SettingsForm = new frmSettings( Settings );

            SettingsForm.ShowDialog();            
        
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

                if( Profile.GetProfileFilename() == null && !SaveAsProfile() ) return false;
                else if( !SaveProfile() ) return false;

            }  //  if()

            return true;

        }  //  static private bool NotifyUnsavedChanges()



        //
        //  static public bool LoadProfile()
        //
        //  A convenient centralised method returning boolean success or failure, LoadProfile directs Profile
        //  to read a Profile from disk while ensuring that any currently open form reflects the opened Profile
        //  status in its UI.  This method considers whether a currently open Profile has unsaved edits, offering
        //  the opportunity to persist the Profile.
        //
        //  LoadProfile returns boolean success or failure.
        //

        static public bool LoadProfile( string filename )
        {
          
            //  Offer to persist any unsaved Profile edits...

            if( Profile.IsEdited() && NotifyUnsavedChanges() == false ) return false;

            //  Present the user with a File Open Dialog through which they may choose a Profile to load.
            
            if( filename == null ) using( OpenFileDialog profile_dialog = new OpenFileDialog() ) {

                //  Give the Dialog a title and then establish a filter to hide anything that isn't an XML
                //  file by default.

                profile_dialog.Title = "Select a Profile to open";
                profile_dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";

                //  Try get the path to a directory within GAVPI's own directory called "Profiles", and make that
                //  the default directory in the OpenFileDialog.

                string GAVPIPath = new Uri( System.IO.Path.GetDirectoryName( 
                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase ) ).LocalPath;

                if( Directory.Exists( GAVPIPath + "\\Profiles" ) ) GAVPIPath += "\\Profiles";

                profile_dialog.InitialDirectory = GAVPIPath;

                if ( profile_dialog.ShowDialog() == DialogResult.Cancel ) return false;

                //  Save the loaded Profile's filename for convenience sake.

                filename = profile_dialog.FileName;

            }  //  if()... using()...
            
            //  Attempt to load the given Profile...

            if( !Profile.load_profile( filename ) ) {
             
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
                Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() );

            //  And now we can add the loaded Profile to the MRU list.

            ProfileMRU.Add( Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() ),
                Profile.GetProfileFilename() );

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
        //  This method should be used instead of Profile.save_profile which will become DEPRECIATED.
        //

        static public bool SaveAsProfile()
        {

            if( Profile.IsEmpty() ) return false;

            using( SaveFileDialog dialog = new SaveFileDialog() ) {

                //  Give the Dialog a title then establish a default filter to hide anything that isn't an XML
                //  file.

                dialog.Title = "Save your Profile as...";
                dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";

                dialog.RestoreDirectory = true;

                if( dialog.ShowDialog() == DialogResult.Cancel ) return false;
                
                //  Let's update the system tray icon's tooltip text to reflect the name of the Profile.

                sysTrayIcon.Text = APPLICATION_TITLE + ": " +
                    Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() );
               
                //  And then add the saved Profile to the MRU list.

                ProfileMRU.Add( Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() ),
                    Profile.GetProfileFilename() );

                return Profile.save_profile( dialog.FileName ) ? true : false;

            }  //  using()           

        }  //  static public bool SaveAsProfile()



        //
        //  static public bool SaveProfile()
        //
        //  Save the Profile as maintained by Profile to the existing filename maintained within Profile.
        //  Returns a boolean value denoting success or failure.
        //

        static public bool SaveProfile()
        {

            if( Profile.IsEmpty() ) return false;

            //  Let's add the saved Profile to the MRU list.

            ProfileMRU.Add( Path.GetFileNameWithoutExtension( Profile.GetProfileFilename() ),
                Profile.GetProfileFilename() );

            return Profile.save_profile( Profile.GetProfileFilename() ) ? true : false;

        }  //  static public bool SaveProfile()
    
    

        //
        //  static public bool NewProfile()
        //
        //  A convenient stub to Profile.NewProfile, returning a boolean value for success or failure.
        //  This method offers the user an opportunity to persist an existing Profile if there are unsaved
        //  edits.
        //

        static public bool NewProfile()
        {

            if( Profile.IsEdited() && !NotifyUnsavedChanges() ) return false;

            //  Clear the log before requesting frmGAVPI refresh its UI otherwise the ListBox may remain
            //  unpopulated.

            Log.Clear();

            if( Application.OpenForms.OfType<frmGAVPI>().Count() > 0 )
                MainForm.RefreshUI( GetStatusString() );

            sysTrayMenu.MenuItems[ MODIFY_PROFILE_MENU_ITEM ].Enabled = true;

            return Profile.NewProfile() ? true : false;
            
        }  //  static public bool NewProfile()

        //
        // static public void ShowTrayMessage(string msg)
        //
        // Display a message pop up bubble over the system tray icon
        // (if it is availalble)
        //
        static public void ShowTrayMessage(string tittle, string msg)
        {
            if (sysTrayIcon != null)
            {
                sysTrayIcon.BalloonTipTitle = tittle;
                sysTrayIcon.BalloonTipText = msg;
                sysTrayIcon.ShowBalloonTip(2);
            }
        }

    }  //  static class GAVPI



    //
    //  class ProcessItem
    //
    //  We maintain a list of processes that have GAVPI Profiles associated with their filenames, which may cause
    //  the Profile to automatically load and GAVPI to begin listening.  The ProcessItem class notes associated
    //  Profile to facilitate auto-loading and auto-listening, and the process's process ID to compare against
    //  stopped processes (allowing us to stop listening when a process we were listening on terminates).
    //

    class ProcessItem
    {

        public Int32 ProcessID;
        public string AssociatedProfile;

        public ProcessItem( Int32 processID, string associatedProfile )
        {
    
            ProcessID = processID;
            AssociatedProfile = associatedProfile;
    
        }  //  public ProcessItem( Int32, string )

    }  //  class ProcessItem

}
