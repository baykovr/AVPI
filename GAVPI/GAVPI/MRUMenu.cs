using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace TrivialMRUMenu
{



    //
    //  class MRU
    //
    //  A trivial Most Recently Used (MRU) submenu management class handling list persistence through
    //  serialization, with callback support.
    //
    //  USAGE:
    //
    //    using TrivialMRUMenu;
    //
    //    ...
    //
    //    MRU mru = new MRU( "Recent Files", MyCallback );
    //
    //    mru.Deserialize();
    //
    //    ...
    //
    //    ContextMenu myMenu = new ContextMenu();
    //  
    //    myMenu.MenuItems.Add( mru.GetMenu() );
    //
    //    mru.Add( "Machine", "Pack-a-Punch" );
    //    mru.Add( "Drink", "Perk-a-Cola" );
    //    mru.Add( "Monkey", "Throw away!" );
    //
    //    ...
    //
    //    mru.Serialize();
    //
    //    ...
    //
    //    public void MyCallback( object ItemData )
    //    {
    //
    //       Console.WriteLine( "You selected: " + ( string ) ItemText );
    //       Console.WriteLine( "Consisting of: " + ( string ) ItemData );
    //
    //    }
    //

    class MRU
    {

        //
        //  These constants declare the maximum number of entries we'll support within the MRU list and the
        //  name of the file in which the MRU list is persisted.  These are the only lines of code you will
        //  likely wish to modify.
        //
        //  MAX_MRU_ITEMS is ZERO-BASED, so therefore it supports 1 additional item to the maximum presented.
        //

        const int MAX_MRU_ITEMS = 2;

        const string MRU_FILENAME = "MRU.lst";

        //  We support a callback function (provided to the constructor) through which we may notify the
        //  instantiator when an MRU item has been selected.

        public delegate void Callback( MRUItem item );

        private Callback OnItemSelectCallback;

        //  Here is our MRU submenu, waiting to be populated with MRU items.  A call to GetMenu() makes it
        //  accessible.

        private MenuItem MRUMenu;



        //
        //  public MRU( string, Callback )
        //
        //  Our constructor expects a string designating the title of the MRU submenu's display name, and a
        //  callback function through which we may present the instantiator with any item data associated
        //  with the selected MRU item.
        //

        public MRU( string menuTitle, Callback callbackFunction )
        {
        
            //  Register the callback function.  Without a callback function we can't inform the instantiator
            //  when an MRU item has been selected.

            if( callbackFunction != null ) OnItemSelectCallback = callbackFunction;
                     
            //  Our MRU menu, which can be accessed by a call to GetMenu().

            MRUMenu = new MenuItem( menuTitle, OnSelectItem );

            MRUMenu.Enabled = false;
     
        }  //  public MRU( string, Callback )



        //
        //  public bool Serialize()
        //
        //  Serialize() takes care of persisting the MRU list.  Returns boolean success or failure as true or
        //  false respectively.
        //

        public bool Serialize()
        {
       
            //  Persistence is futile if our MRU list is empty...

            if( MRUMenu.MenuItems.Count == 0 ) return false;

            try {
			
                //  We'll create the MRU file, copy MRUMenu over to a temporary list of string/object pairs,
                //  then serialize it.  Exceptions will be caught, and should usually always relate to file
                //  I/O (likely an inability to create or write to the MRU file).

                using( Stream stream = File.Open( MRU_FILENAME, FileMode.Create ) )
			    {
			    
                    List< MRUItem > PersistentMRU = new List< MRUItem >();

                    foreach( MenuItem item in MRUMenu.MenuItems )
                        PersistentMRU.Add( new MRUItem( item.Text, item.Tag ) );

                    BinaryFormatter Persist = new BinaryFormatter();
			        Persist.Serialize( stream, PersistentMRU );

			    }  //  using()
                
		    } catch( Exception ) { return false; }        
        
            return true;

        }  //  public bool Serialize()



        //
        //  public bool Deserialize()
        //
        //  Attempting to load the persisted MRU returns boolean success or failure as true or false
        //  respectively.  Failure typically relates to the abssense of the MRU file.
        //

        public bool Deserialize()
        {

            //  Attempt to deserialize the MRU list from persistent storage.

            try  {
			
                //
                //  Open the MRU file, if it exists, and attempt to read the contents into a list of string/
                //  object pairs; load those object pairs into MRUMenu with a call to Add().  We'll catch
                //  and fail on any exception we come across (they'll typically relate to file I/O, which
                //  will likely boil down to the absence of an MRU file).
                //

                using( Stream stream = File.Open( MRU_FILENAME, FileMode.Open ) ) {

			        BinaryFormatter Persisted = new BinaryFormatter();

			        List< MRUItem > PersistedMRU = ( List< MRUItem > ) Persisted.Deserialize( stream );
			    
                    foreach( MRUItem item in PersistedMRU ) {

                        MRUMenu.MenuItems.Add( new MenuItem( item.ItemText, OnSelectItem ) );
                        MRUMenu.MenuItems[ MRUMenu.MenuItems.Count -1 ].Tag = item.ItemData;

                    }  //  foreach()

			    }  //  using()
                
		    } catch( Exception ) {  return false; }   

            if( MRUMenu.MenuItems.Count != 0 ) MRUMenu.Enabled = true;

            return true;

        }  //  public bool Deserialize()



         //
        //  public MenuItem GetMenu()
        //
        //  To facilite incorporating the MRU menu into a user interface, a call to GetMRU() will return the
        //  actual MenuItem.
        //

        public MenuItem GetMenu()
        {

            return MRUMenu;

        }  //  public MenuItem GetMRU()

        

        //
        //  public void Add( string mruItemName, object data )
        //
        //  Adding an item to the MRU list is as simple as calling Add(), providing the item's display name
        //  and any optional data to associate with the item (considering that MRU lists typically maintain
        //  a list of most recently used files, this item could could - for example - be the file's fully
        //  qualified filename).
        //

        public void Add( string mruItemName, object mruItemData )
        {
            
            if( MRUMenu.MenuItems.Count != 0 ) {

                //  If the named item is already in the list we'll remove it.  This may seem redundant, but
                //  it will be added to the top of the list later - exactly where it should be.

                foreach( MenuItem item in MRUMenu.MenuItems ) if( item.Text == mruItemName ) {
                        
                    MRUMenu.MenuItems.Remove( item );

                    break;

                }

                //  If we'll have reached the maximum allowed size of the MRU list by adding this item then
                //  let's make room for it by removing the last item in the list.

                if( ( MRUMenu.MenuItems.Count -1 ) == MAX_MRU_ITEMS )
                    MRUMenu.MenuItems.RemoveAt( MRUMenu.MenuItems.Count -1 );
            
            }  //  if()

            //  Now let's add the item to the top of the list and associate any item data...
            
            MRUMenu.MenuItems.Add( 0, new MenuItem( mruItemName, OnSelectItem ) );
            MRUMenu.MenuItems[ 0 ].Tag = mruItemData;
            
            //  Since the MRU menu is populated, let's ensure it is enabled...

            MRUMenu.Enabled = true;

        }  //  public void Add( string mruItemName, object data )
       


        //
        //  public void Clear()
        //
        //  An option to clear the MRU list may be useful, so here it is...
        //

        public void Clear()
        {

            MRUMenu.MenuItems.Clear();

            MRUMenu.Enabled = false;

        }  //  public void Clear()



        //
        //  private void OnSelectItem( object, EventArgs )
        //
        //  We'll make a callback to the instantiator when an MRU list menu item is selected, providing the
        //  item data of the MRU item that was chosen.
        //

        private void OnSelectItem( object sender, EventArgs e )
        {

            OnItemSelectCallback( new MRUItem( ( ( MenuItem ) sender ).Text, ( ( MenuItem ) sender ).Tag ) );
            
        }  //  private void OnSelectItem( object sender, EventArgs e )



        //
        //  [Serializable()]
        //  class MRUItem
        //
        //  Holder of MRU item name string and item data.  This class can be serialised.
        //

        [Serializable()]
        public class MRUItem
        {

            public object ItemData;
            public string ItemText;

            public MRUItem( string itemText, object itemData )
            {
        
                ItemText = itemText;
                ItemData = itemData;

            }  //  public MRUItem( string, object )

        }  //  class MRUItem

    }  //  class MRU
    
}  //  namespace TrivialMRUMenu
