using System;
using System.Collections.Generic;
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
    /*Stage 1: One profile at a time (for now).*/
    public class VI_Profile
    {
        public string name;

		//  We retain a copy of the loaded (or saved) Profile's fully-qualified filename...

		private string ProfileFilename;

        //  ...And track whether changes have been made to an unsaved Profile.

        private bool UnsavedProfileChanges = false;

        public SpeechSynthesizer synth;

        public List<VI_Trigger> Profile_Triggers;

        public List<VI_Action_Sequence> Profile_ActionSequences;

        public VI_DB ProfileDB;

        public VI_Profile(string filename)
        {
            try
            {
                Profile_Triggers = new List<VI_Trigger>();
                Profile_ActionSequences = new List<VI_Action_Sequence>();
                

                synth = new SpeechSynthesizer(); //used by action Speak

                if (filename != null) 
                { 
                    load_profile();
                    
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
        public void Add_Trigger(VI_Trigger trigger_toAdd)
        {
            Profile_Triggers.Add(trigger_toAdd);
        }
        public void Add_Action_Sequence(VI_Action_Sequence action_sequence_toAdd)
        {
            Profile_ActionSequences.Add(action_sequence_toAdd);
        }

        #region Trigger Validation Function
        public bool isTriggerValueTaken(string value_to_check)
        {
            // Predicate searches for the first match of value to vvalue_to_check
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

            Profile_Triggers = new List<VI_Trigger>();
            Profile_ActionSequences = new List<VI_Action_Sequence>();

            //  And reset any states...

            UnsavedProfileChanges = false;
            ProfileFilename = null;

            return true;

        }  //  public bool NewProfile()

        public bool load_profile()
        {

            string filename;

            //
            //  Present the user with a File Open Dialog through which they may choose a Profile to load.
            //

            using( OpenFileDialog profile_dialog = new OpenFileDialog() ) {

                //  Give the Dialog a title and then establish a filter to hide anything that isn't an XML file by default.

                profile_dialog.Title = "Select a Profile to open";
                profile_dialog.Filter = "Profiles (*.XML)|*.XML|All Files (*.*)|*.*";
                profile_dialog.RestoreDirectory = true;

                if ( profile_dialog.ShowDialog() == DialogResult.Cancel ) return false;

                //  Save the loaded Profile's filename for convenience sake.

                filename = profile_dialog.FileName;

            }

            //  Reset any states...

            UnsavedProfileChanges = false;

            //Clean Current
            Profile_Triggers = new List<VI_Trigger>();
            Profile_ActionSequences = new List<VI_Action_Sequence>();
            //Constructor will catch.
            XmlDocument profile_xml = new XmlDocument();
			
			//  Let's ensure that the user has actually selected a well-formed XML document for us to navigate as
			//  opposed to - oh, I don't know: a picture of their cat?
			
			try {
			
				profile_xml.Load(filename);
				
			} catch( XmlException exception ) {
			
				MessageBox.Show( "There appears to be a problem with the Profile you have chosen.\n\n" +
				                 "It may not been an actual Profile, or it may have become corrupted.",
								 "I cannot load the Profile",
								 MessageBoxButtons.OK,
								 MessageBoxIcon.Exclamation,
		                         MessageBoxDefaultButton.Button1 );
			
				return false;
				
			}
				
            //Check first element tag
            if (profile_xml.DocumentElement.Name != "gavpi") {
                throw new Exception("Malformed profile expected first tag gavpi got,"
                + profile_xml.DocumentElement.Name);
            }
            XmlNodeList profile_xml_elements = profile_xml.DocumentElement.ChildNodes;
            foreach (XmlNode element in profile_xml_elements)
            {
                if (element.Name == "VI_Action_Sequence")
                {
                    VI_Action_Sequence ack_frm_file;
                    ack_frm_file = new VI_Action_Sequence(element.Attributes.GetNamedItem("name").Value);
                    ack_frm_file.type = element.Attributes.GetNamedItem("type").Value;
                    ack_frm_file.comment = element.Attributes.GetNamedItem("comment").Value;
                    
                    foreach (XmlNode action in element.ChildNodes)
                    {
                        string action_type = action.Attributes.GetNamedItem("type").Value;
                        string action_value = action.Attributes.GetNamedItem("value").Value;
                        Type new_action_type = Type.GetType("GAVPI." + action_type );
                        object action_instance;
                        if (action_type == "Speak")
                        {
                            action_instance = Activator.CreateInstance(new_action_type,this.synth, action_value);
                        }
                        else
                        {
                            action_instance = Activator.CreateInstance(new_action_type, action_value);
                        }
                        ack_frm_file.Add((Action)action_instance);
                    }
                    if (!Profile_ActionSequences.Any(ack => ack.name == ack_frm_file.name))
                    {
                        Profile_ActionSequences.Add(ack_frm_file);
                    }
                }
                else if (element.Name == "VI_Trigger")
                {
                    VI_Trigger trig_frm_file;
                    string trigger_name = element.Attributes.GetNamedItem("name").Value;
                    string trigger_type = element.Attributes.GetNamedItem("type").Value;
                    string trigger_value = element.Attributes.GetNamedItem("value").Value;
                    string trigger_comment= element.Attributes.GetNamedItem("comment").Value;

                    Type new_trigger_type = Type.GetType("GAVPI." + trigger_type);
                    object trigger_isntance = trigger_isntance = Activator.CreateInstance(new_trigger_type, trigger_name, trigger_value);
                    trig_frm_file = (VI_Trigger)trigger_isntance;
                    trig_frm_file.comment = trigger_comment;

                    // Trigger Events
                    foreach (XmlElement trigger_event in element.ChildNodes)
                    {
                        string event_type = trigger_event.Attributes.GetNamedItem("type").Value;
                        string event_name = trigger_event.Attributes.GetNamedItem("name").Value;
                        string event_value = trigger_event.Attributes.GetNamedItem("value").Value;
                        if (event_type == "VI_Action_Sequence")
                        {
                            trig_frm_file.Add(Profile_ActionSequences.Find( ackseq => ackseq.name == event_name));
                        }
                        else if (event_type == "VI_Phrase")
                        {
                            VI_Trigger newMetaTrigger;
                            Type meta_trigger_type = Type.GetType("GAVPI." + event_type);
                            object meta_trigger_isntance = Activator.CreateInstance(meta_trigger_type,event_name, event_value);
                            newMetaTrigger = (VI_Trigger)meta_trigger_isntance;

                            trig_frm_file.Add(newMetaTrigger);
                        }
                    }
                    // Malformed xml or double load, need to switch to dictionaries tbh
                    if (!Profile_Triggers.Any(trig=>trig.name == trig_frm_file.name))
                    { 
                        Profile_Triggers.Add(trig_frm_file); 
                    }
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

                foreach (VI_Action_Sequence ack_seq in Profile_ActionSequences)
                {
                    writer.WriteStartElement("VI_Action_Sequence");
                    writer.WriteAttributeString("name", ack_seq.name);
                    writer.WriteAttributeString("type", ack_seq.type);
                    writer.WriteAttributeString("comment", ack_seq.comment);

                    foreach (Action act in ack_seq.action_sequence)
                    {
                        writer.WriteStartElement("Action");
                        writer.WriteAttributeString("type", act.type);
                        writer.WriteAttributeString("value", act.value);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
                foreach (VI_Trigger trig in Profile_Triggers){
                    writer.WriteStartElement("VI_Trigger");
                        writer.WriteAttributeString("name", trig.name);
                        writer.WriteAttributeString("value", trig.value);
                        writer.WriteAttributeString("type", trig.type);
                        writer.WriteAttributeString("comment", trig.comment);
                        //TriggerEvents (Events which this trigger will cause to happen)
                   foreach(VI_TriggerEvent trigger_event in trig.TriggerEvents)
                   {
                       writer.WriteStartElement("VI_TriggerEvent");
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
        //  Since the Profile maintained within VI_Profile can be edited outside of the class (typically within frmProfile)
        //  then we need a way for external methods to inform VI_Profile that changes are pending.  Ideally all editing
        //  would take place within VI_Profile, but that isn't the case, therefore calling VI_Profile.Edited() allows us
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

    }
}
