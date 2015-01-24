using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Diagnostics;

namespace TrivialProcessMonitor
{
    


    //
    //  class ProcessMonitor
    //
    //  A trivial class providing callbacks whenever a process starts and stops.  Callbacks are established at
    //  object instantiation, where a null callback disables either process start monitoring or process
    //  termination monitoring.  Callbacks are provided with an integer process ID and, if process startup is
    //  monitored, a fully qualified filename for the process.
    //
    //  USAGE:
    //
    //    using ProcessMonitor;
    //
    //    ...
    //
    //    ProcessMonitor Monitor = new ProcessMonitor( MyStartCallback, MyStopCallback );
    //
    //    Monitor.Start();
    //
    //    ...
    //
    //    Monitor.Stop();
    //
    //    ...
    //
    //    public void MyStartCallback( int processID, string processFilename )
    //    {
    //
    //        Console.WriteLine( "Started: [" + processID + "] " + processFilename );
    //
    //    }
    //
    //    public void MyStopCallback( int processID, string processFilename )
    //    {
    //
    //        Console.WriteLine( "Stopped: [" + processID + "] " + processFilename );
    //
    //    }
    //
    //  OUTPUT (EXAMPLE):
    //
    //  Started: [3298] C:\Program Files\Android-Studio\bin\studio64.exe
    //  Started: [6201] C:\Program Files\Origin\Battlefield5\Battlefield5.exe
    //  Stopped: [6201] C:\Program Files\Origin\Battlefield5\Battlefield5.exe
    //  Started: [2743] C:\Program Files\Origin\Plants vs Zombies Garden Warfare 2\PvZGW2.exe
    //

    class ProcessMonitor 
    {

        //  ManagementEventWatcher accepts a query (in our case queries regarding process startup and
        //  termination) and fires an event to an event handler method.

        private ManagementEventWatcher ProcessStartMonitor;
        private ManagementEventWatcher ProcessStopMonitor;
        
        //  Our process tracking dictionary associates process IDs with process filenames.

        private Dictionary< Int32, string > TrackedProcesses;

        //  Let's define our callback function.

        public delegate void Callback( Int32 processID, string processFilename );

        //  We issue callbacks whenever a process starts or stops.

        private Callback OnProcessStartedCallback = null;
        private Callback OnProcessStoppedCallback = null;



        //
        //  public ProcessMonitor( Callback, Callback )
        //
        //  When provided with a non-null onProcessStartedCallback, ProcessMonitor will track the startup
        //  of Windows processes, recording the started process ID and its fully qualified filename, and
        //  present that information via the callback function.  Providing an onProcessStoppedCallback will
        //  present the same information when a tracked process stops (however, if the startup wasn't
        //  tracked, the filename will be null).
        //

        public ProcessMonitor( Callback onProcessStartedCallback, Callback onProcessStoppedCallback )
        {

            TrackedProcesses = new Dictionary< Int32, string >();

            if( onProcessStartedCallback != null ) { 

                ProcessStartMonitor = new ManagementEventWatcher( "SELECT * FROM Win32_ProcessStartTrace" );
    		    ProcessStartMonitor.EventArrived += new EventArrivedEventHandler( OnProcessStarted );

                OnProcessStartedCallback = onProcessStartedCallback;           

            }  //  if()

            if( onProcessStoppedCallback != null ) { 

                ProcessStopMonitor = new ManagementEventWatcher( "SELECT * FROM Win32_ProcessStopTrace" );
                ProcessStopMonitor.EventArrived += new EventArrivedEventHandler( OnProcessStopped );

                OnProcessStoppedCallback = onProcessStoppedCallback;

            }  //  if()

        }  //  public ProcessMonitor( Callback, Callback )
    


        //
        //  public void Start()
        //
        //  Begin monitoring process start and termination.  Process start monitoring will only occur if an
        //  onProcessStartedCallback callback was provided to the constructor at object instantiation, and
        //  process termination monitoring will only occur if an onProcessStoppedCallback callback was provided
        //  to the constructor at object instantiation.
        //

        public void Start()
        {

            if( OnProcessStartedCallback != null ) ProcessStartMonitor.Start();
            if( OnProcessStoppedCallback != null ) ProcessStopMonitor.Start();  
         
        }  //  public void Start()
    


        //
        //  public void Stop()
        //
        //  Stop monitoring processes.
        //

        public void Stop()
        {
            
            if( OnProcessStartedCallback != null ) ProcessStartMonitor.Stop();
            if( OnProcessStoppedCallback != null ) ProcessStopMonitor.Stop();

        }  //  public void Stop()



        //
        //  private void OnProcessStarted( object, EventArrivedEventArgs )
        //
        //  Receive an event containing process-relevant information whenever a process is started and forward the
        //  relevant information (process ID and fully qualified filename) to a provided callback function.  (This
        //  method will never be called if an onProcessStartedCallback function wasn't provided in the constructor.)
        //

        private void OnProcessStarted( object sender, EventArrivedEventArgs managementEvent )
        {
            
            string ProcessFilename = null;

            Int32 ProcessID;

            try { 


                //  Let's attempt to get both the process ID and the fully qualified filename of the new
                //  process. If the extracted process ID exists within the list of running processes, get its
                //  filename. (This could fail with an exception if the process closes in the time it takes
                //  to get the filename, or access is denied because the process was started by the system.)

                ProcessID = Convert.ToInt32( managementEvent.NewEvent.Properties[ "ProcessID" ].Value );

                if( Process.GetProcesses().Any( x => x.Id == ProcessID ) )
                    ProcessFilename = Process.GetProcessById( ProcessID ).MainModule.FileName;

            } catch( Exception ) { return; }

            if( ProcessFilename != null ) {

                //  Start tracking the process by adding it to the list, then pass the process ID and filename
                //  to relevant callback.
               
                if( !TrackedProcesses.ContainsKey( ProcessID ) ) TrackedProcesses.Add( ProcessID, ProcessFilename );

                OnProcessStartedCallback( ProcessID, ProcessFilename );

            }  //  if()

        }  //  private void ProcessStarted( object, EventArrivedEventArgs )



        //
        //  private void OnProcessStopped( object, EventArrivedEventArgs )
        //
        //  Receive an event containing process-relevant information whenever a process is stopped and.
        //  The process ID and, if the process start was tracked, its fully qualified filename will be
        //  passed as arguments to the associated callback, otherwise null will be passed instead of
        //  the filename.  If the callback function is null, this method will never be called.
        //

        private void OnProcessStopped( object sender, EventArrivedEventArgs managementEvent )
        {
            
            try { 

                //  Let's attempt to get the process ID of the process that we've been informed has stopped
                //  executing and pass the relevant information to the callback function.  Finally, remove
                //  the process from the process list if it exists.               

                Int32 ProcessID = Convert.ToInt32( managementEvent.NewEvent.Properties[ "ProcessID" ].Value );

                OnProcessStoppedCallback( ProcessID,
                    TrackedProcesses.ContainsKey( ProcessID ) ? TrackedProcesses[ ProcessID ] : null );

                if( TrackedProcesses.ContainsKey( ProcessID ) ) TrackedProcesses.Remove( ProcessID );

            } catch( Exception ) { return; }

        }  //  private void ProcessStopped( object, EventArrivedEventArgs )

    }  //  class ProcessMonitor

}  //  namespace TrivialProcessMonitor
