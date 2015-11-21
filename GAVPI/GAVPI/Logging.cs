using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrivialLogging
{



    //
    //  class Logging< T >
    //
    //  A trivial, thread-safe generic logging class with callback functionality, Logging implements blanket
    //  object locking to ensure only one thread may manipulate or query a simple queue of log entries at a
    //  time.
    //
    //  USAGE:
    //
    //    using TrivialLogging;
    //
    //    ...
    //
    //    try {
    //
    //      Logging< string > log = new Logging< string >( MyCallback );
    //
    //    catch( Exception e ) { ... }
    //
    //    log.Entry( "Hello world!" );
    //
    //    ...
    //
    //    public void MyCallback( string LoggedEntry )
    //    {
    //
    //       Console.Writeln( LoggedEntry );
    //
    //    }
    //
    //  OUTPUT:
    //
    //    Hello World!
    //    

    class Logging< T >
    {

        //  Our log, a FIFO queue:

        private Queue< T > Log;
    
        //  We support callbacks (optionally passed to the constructor), and we'll define and acknowledge it
        //  here.

        public delegate void Callback( T loggedEntry );

        private Callback OnLoggedMessageCallback;



        //
        //  public Logging()
        //
        //  The default constructor will re-throw any exceptions thrown while attempting to instantiate the
        //  queue.
        //

        public Logging()
        { 
        
            try {

                Log = new Queue< T >();

            } catch ( Exception e ) { throw; }
           
        }  //  public Logging()



        //
        //  public Logging( Callback )
        //
        //  A constructor taking a callback function.  The callback function must accept the same type, T,
        //  as the class as its only argument.
        //

        public Logging( Callback callbackFunction ) : this()
        {

            if( callbackFunction != null ) OnLoggedMessageCallback = callbackFunction;

        }  //  Logging( Callback )



        //
        //  public void Entry( T )
        //
        //  Add an entry of expected type T to the log queue, calling an optional callback function upon
        //  completion.
        //

        public void Entry( T logEntry )
        {

            lock( Log ) {

                Log.Enqueue( logEntry );
               
                if( OnLoggedMessageCallback != null ) OnLoggedMessageCallback( logEntry );

            }  //  lock()

        }  //  public void Entry( T )
        
        

        //
        //  public array ToArray()
        //
        //  Return an Array object consisting of the logged Entries.
        //

        public Array ToArray()
        {

            lock( Log ) { 

                return Log.ToArray();

            }
            
        }  //  public Array ToArray()
    


        //
        //  public int Count()
        //
        //  Return the number of entries, zero-based, in the log queue.
        //

        public int Count()
        {

            lock( Log ) { 

                return Log.Count;

            }
        
        }  //  public int Count()



        //
        //  public void Clear()
        //
        //  Clear the log.  This method is destructive, so you may wish to persist the log by way of a call to
        //  ToArray() first.
        //
                
        public void Clear()
        {

            lock( Log ) { 

                Log.Clear();

            }

        }  //  public void Clear()
      
    }  //  class Logging< T >

}  //  namespage TrivialLogging
