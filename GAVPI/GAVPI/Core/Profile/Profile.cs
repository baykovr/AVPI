using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

/*
 * Anything else file load/unload related.
 */

namespace GAVPI
{
    public class Profile
    {
        public string name;

		//  We retain a copy of the loaded (or saved) Profile's fully-qualified filename...

		private string ProfileFilename;

        //  ...And track whether changes have been made to an unsaved Profile.

        private bool UnsavedProfileChanges = false;

        //  Finally, and optionally, load this Profile's process association.  If this Profile is associated with
        //  a process, this Profile will be automatically loaded whenever the process starts.

        private string AssociatedProcess;
        public SpeechSynthesizer synth;
        public List<Trigger> Profile_Triggers;
        public List<Action_Sequence> Profile_ActionSequences;

        public Database ProfileDB;

        public Profile(string filename)
        {
            try
            {
                Profile_Triggers = new List<Trigger>();
                Profile_ActionSequences = new List<Action_Sequence>();
                ProfileDB = new Database();
                synth = new SpeechSynthesizer();

                if (filename != null) 
                {
                    load_profile( filename );
                }

            }
            catch (Exception profile_err)
            {
                MessageBox.Show("Problem loading profile.\n" + profile_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            finally
            {
               /**/ 
            }
        }
        public void Add_Trigger(Trigger trigger_toAdd)
        {
            Profile_Triggers.Add(trigger_toAdd);
        }
        public void Add_Action_Sequence(Action_Sequence action_sequence_toAdd)
        {
            Profile_ActionSequences.Add(action_sequence_toAdd);
        }

        #region Trigger Validation Function
        public bool isTriggerValueTaken(string value_to_check)
        {
            // Predicate searches for the first match of value to value_to_check
            // it will return the object (trigger), if its not null then it must exist
            // you could also for loop through all triggers here, runtime O(n)
            if (Profile_Triggers.Find(trigger => trigger.value == value_to_check) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool isTriggerNameTaken(string name_to_check)
        {
            // Predicate searches for the first match of name to name_to_check
            // it will return the object (trigger), if its not null then it must exist
            // you could also for loop through all triggers here, runtime O(n)
            if (Profile_Triggers.Find(trigger => trigger.name == name_to_check) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Action Sequence Validation Functions
        public bool isActionSequenceNameTaken(string name_to_check)
        {
            if (Profile_ActionSequences.Find(aseq => aseq.name == name_to_check) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        //
        //  public bool NewProfile()
        //
        //  Requesting a new Profile eliminates any existing Profile, and is therefore destructive.
        //
        //  Returns true if the new Profile was created of false if otherwise.
        //

        public bool NewProfile()
        {
            
            //  Instantiate a fresh Profile...

            Profile_Triggers = new List<Trigger>();
            Profile_ActionSequences = new List<Action_Sequence>();

            //  And reset any states...

            UnsavedProfileChanges = false;
            ProfileFilename = null;
            AssociatedProcess = null;

            return true;

        }  //  public bool NewProfile()

        public bool load_profile( string filename )
        {
            GAVPI.dbg_log.Add("[...] Loading new profile from" + filename);

            if (filename == null) 
                { return false; }

            //  Reset any states...

            NewProfile();

            XmlDocument profile_xml = new XmlDocument();
			
			//  Let's ensure that the user has actually selected a well-formed XML document for us to navigate as
			//  opposed to - oh, I don't know: a picture of their cat?
			
			try 
            {
				profile_xml.Load(filename);	
			} 
            catch( Exception ) 
            {
                GAVPI.dbg_log.Add("[ ! ] Critical error in profile load detected.");
                
				return false;
            }
				
            ////Check first element tag
            // Maybe we should not really care about the root element.
            //if (profile_xml.DocumentElement.Name != "gavpi")
            //{
            //    throw new Exception("Malformed profile expected first tag gavpi got,"
            //        + profile_xml.DocumentElement.Name);
            //}

            XmlNodeList profile_xml_elements = profile_xml.DocumentElement.ChildNodes;
            foreach (XmlNode element in profile_xml_elements)
            {
                if( element.Name == "AssociatedProcess" ) 
                {
                    AssociatedProcess = element.InnerText;
                } 
                // Load Action Sequences and their associated Actions.
                else if (element.Name.Contains("Action_Sequence"))
                {
                    Action_Sequence ack_frm_file = null;
                    try
                    {
                        ack_frm_file = new Action_Sequence(element,ProfileDB,this.synth);
                        // Check if the profile contains any sequences by this name.
                        if (!Profile_ActionSequences.Any(ack => ack.name == ack_frm_file.name))
                        {
                            Profile_ActionSequences.Add(ack_frm_file);
                        }
                        else 
                        {
                            throw new ArgumentException("An action sequence with this name already exists.");
                        }
                    }
                    catch
                    {
                        // Log that we are discarding this malformed action sequence.
                        GAVPI.Log.Entry("[ ! ] Error in profile malformed action sequence.");
                    }
                }
                else if (element.Name.Contains("Trigger"))
                {
                    Trigger trig_frm_file;
                    string trigger_name = element.Attributes.GetNamedItem("name").Value;
                    string trigger_type = element.Attributes.GetNamedItem("type").Value;
                    string trigger_value = element.Attributes.GetNamedItem("value").Value;
                    string trigger_comment= element.Attributes.GetNamedItem("comment").Value;

                    // Add backward compatability with older profiles.
                    if (trigger_type == "VI_Phrase")
                        {trigger_type = "Phrase";}

                    Type new_trigger_type = Type.GetType("GAVPI." + trigger_type);
                    object trigger_isntance = trigger_isntance = Activator.CreateInstance(new_trigger_type, trigger_name, trigger_value);
                    trig_frm_file = (Trigger)trigger_isntance;
                    trig_frm_file.comment = trigger_comment;

                    // Trigger Events
                    foreach (XmlElement trigger_event in element.ChildNodes)
                    {
                        string event_type = trigger_event.Attributes.GetNamedItem("type").Value;
                        string event_name = trigger_event.Attributes.GetNamedItem("name").Value;
                        string event_value = trigger_event.Attributes.GetNamedItem("value").Value;
                        if (event_type.Contains("Action_Sequence"))
                        {
                            trig_frm_file.Add(Profile_ActionSequences.Find( ackseq => ackseq.name == event_name));
                        }
                        else if (event_type.Contains("Phrase"))
                        {
                            Trigger newMetaTrigger;
                            Type meta_trigger_type = Type.GetType("GAVPI." + event_type);
                            object meta_trigger_isntance = Activator.CreateInstance(meta_trigger_type,event_name, event_value);
                            newMetaTrigger = (Trigger)meta_trigger_isntance;

                            trig_frm_file.Add(newMetaTrigger);
                        }
                    }
                    
                    // Malformed xml or double load, need to switch to dictionaries tbh
                    // agreed, but there are enough things to do atm 04.07.15
                    if (!Profile_Triggers.Any(trig=>trig.name == trig_frm_file.name))
                    { 
                        Profile_Triggers.Add(trig_frm_file); 
                    }
                }
                else if (element.Name.Contains("Database") || element.Name.Contains("DB") )
                {
                    // Hand reader to DB
                    if (ProfileDB != null)
                    {
                        ProfileDB.load(element);
                    }
                }
                else
                {
                    GAVPI.dbg_log.Add(String.Format("[ ! ] Additional unrecognized element in profile xml, {0}",element.Name));
                }
            }          

			//
			//  We have successfully loaded the Profile, so retain the Profile's filename for future reference...
			//
			ProfileFilename = filename;

			return true;
			
        }  //  public void load_profile()
		



		//
        //  DEPRECIATED: Call GAVPI.SaveProfile or GAVPI.SaveAsProfile in future.
        //
		//  public bool save_profile()
		//
		//  Save the current Profile to the filename provided by ProfileFilename without explicity requesting a
		//  filename from the user.  save_profile() should only ever be called if a Profile has been previously
		//  loaded, or a profile has been previously saved with a filename, because no sanity checking is
		//  performed.
		//
		//  Return boolean success or failure.
 		//
		
		public bool save_profile() {

            if( IsEmpty() ) return false;

            //  If the Profile either isn't an existing, opened Profile, or it hasn't been previously saved (therefore, in either
            //  case, the Profile doesn't have a file name associated with it) present a File Dialog.

            if( ProfileFilename == null ) {

                using (SaveFileDialog dialog = new SaveFileDialog())
                {

                    //  Give the Dialog a title then establish a default filter to hide anything that isn't an XML file.

                    dialog.Title = "Save your Profile as...";
                    dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";

                    dialog.RestoreDirectory = true;

                    if (dialog.ShowDialog() == DialogResult.Cancel) return false;  //  The user decided not to save after all.

                    ProfileFilename = dialog.FileName;  //  Let's save the Profile's filename.

                }  //  using()

            }  //  if()

            //  Save the profile.

			return save_profile( ProfileFilename );
		
		}  //  public void save_profile()
				
        public bool save_profile(string filename)
        {
            try 
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                XmlWriter writer = XmlWriter.Create(filename, settings);
                writer.WriteStartDocument();
                writer.WriteStartElement("gavpi");

                if( AssociatedProcess != null ) {

                    writer.WriteStartElement( "AssociatedProcess" );
                    writer.WriteString( AssociatedProcess );
                    writer.WriteEndElement();

                }  //  if()

                /* Always write the database elements at the top of the profile (file) 
                 * this is because related action sequences will call database elements by reference (name)
                 * as such the db elements must be read, loaded and ready to query from the profile 
                 * Robert (04.12.15)*/

                if (ProfileDB != null)
                {
                    ProfileDB.save(writer);
                }
                else
                {
                    // No associated database, TODO : log warning
                    //MessageBox.Show("No database is associated in this profile.","Warning");
                }

                foreach (Action_Sequence ack_seq in Profile_ActionSequences)
                {
                    ack_seq.writeXML(writer);
                }
                foreach (Trigger trig in Profile_Triggers)
                {
                    writer.WriteStartElement("Trigger");
                        writer.WriteAttributeString("name", trig.name);
                        writer.WriteAttributeString("value", trig.value);
                        writer.WriteAttributeString("type", trig.type);
                        writer.WriteAttributeString("comment", trig.comment);
                   
                   // TriggerEvents: events which this trigger will raise.
                   // e.g. trigger is activated by phrase, events are several action sequences invoked.
                   foreach(Trigger_Event trigger_event in trig.TriggerEvents)
                   {
                       writer.WriteStartElement("Trigger_Event");
                       writer.WriteAttributeString("name", trigger_event.name);
                       writer.WriteAttributeString("type", trigger_event.type);
                       writer.WriteAttributeString("value", trigger_event.value);
                       writer.WriteAttributeString("comment", trig.comment);
                       writer.WriteEndElement();
                   }
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Flush();
                writer.Close();
            }
            catch (Exception err_saving)
            {
                MessageBox.Show("Error saving to file" + err_saving.Message);
				
				return false;
            }
			
			//
			//  We have successfully saved the Profile, so retain the Profile's filename for future reference and record
            //  that there are no pending changes at the moment.
			//
			
			ProfileFilename = filename;			
            UnsavedProfileChanges = false;			

			return true;
			
        }  //  public void save_profile()



        //
        //  public bool IsEmpty()
        //
        //  Return true if an empty Profile exists (perhaps a Profile that has been edited to oblivion), or false otherwise.
        //

        public bool IsEmpty()
        {

            return ( Profile_Triggers != null && Profile_Triggers.Any() ) ? false : true;

        }  //  public bool IsEmpty()



        //
        //  public void Edited()
        //
        //  Since the Profile maintained within Profile can be edited outside of the class (typically within frmProfile)
        //  then we need a way for external methods to inform Profile that changes are pending.  Ideally all editing
        //  would take place within Profile, but that isn't the case, therefore calling Profile.Edited() allows us
        //  to maintain consistent tracking.
        //

        public void Edited()
        {

           UnsavedProfileChanges = true;

        }  //  public void Edited()



        //
        //  public bool IsEdited()
        //
        //  Return true if the Profile has been modified with changes to save, or false otherwise.
        //

        public bool IsEdited()
        {

            return IsEmpty() ? false : UnsavedProfileChanges;

        }  //  public bool ChangesPending()


        //
        //  public string GetProfileFilename()
        //
        //  Return the filename of the current Profile.  GetProfileFilename() will return null if a Profile has not
        //  yet been established, or if a newly created Profile hasn't previously been saved (and thus named).
        //

        public string GetProfileFilename()
        {

            return ProfileFilename;

        }  //  public string GetProfileFilename()



        //
        //  public string GetAssociatedProcess()
        //
        //  Get the executable that this Profile will be automatically associated with.
        //

        public string GetAssociatedProcess()
        {
        
            return AssociatedProcess;
        
        }  //  public string GetAssociatedProcess()


        //
        // public bool IsAssociatedProcessFocused()
        //
        // Check if the associated process is currently focused.
        //

        public bool IsAssociatedProcessFocused()
        {

            // If we don't have a process associated, skip this check.
            if (AssociatedProcess == null || AssociatedProcess.Length == 0) return true;

            IntPtr handle = Win32_APIs.GetForegroundWindow();
            uint pid = 0;
            Win32_APIs.GetWindowThreadProcessId(handle, out pid);
            Process proc = Process.GetProcessById((int)pid);
            String procPath = proc.MainModule.FileName;

            return String.Compare(Path.GetFullPath(procPath),
                                  Path.GetFullPath(AssociatedProcess),
                                  StringComparison.InvariantCultureIgnoreCase) == 0;

        } // public bool IsAssocaitedProcessFocused()

        //
        //  public void SetAssociatedProcess( string )
        //
        //  Associate the loaded Profile with an executable that the user has chosen.  If enabled within GAVPI,
        //  this Profile will automatically load when the executable loads.
        //

        public void SetAssociatedProcess( string processName )
        {
        
            AssociatedProcess = processName;
        
        }  //  public void SetAssociatedProcess( string )

    }
}
