using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            vi_settings = new VI_Settings();
            vi_profile = new VI_Profile(null);

            vi = new VI();

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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run( new frmGAVPI() );

            //  We don't want the garbage collector to think our Mutex is up for grabs before we close the program,
            //  so let's protect it.

            GC.KeepAlive( LockApplicationInstance );

            LockApplicationInstance.ReleaseMutex();

        }  //  static void Main()
    }
}
