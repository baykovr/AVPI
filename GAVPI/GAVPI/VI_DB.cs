using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Forms;
using System.Reflection;

namespace GAVPI
{
    public class VI_DB
    {
        /*
         * Since the profile is going to be depended on 
         * certain database elements existings, tie it to the profile.
         * Each profile will contain one db. 
         * (However we keep the db in a seperate file)
         */
        public string DBFilename;
        public Dictionary<string, VI_Data> DB;
        public VI_DB()
        {
            DB = new Dictionary<string, VI_Data>();
        }
        public VI_DB(string filename)
        {
            DBFilename = filename;
            DB = new Dictionary<string, VI_Data>();
        }
        #region FileIO
        //public void load()
        //{
        //    if (string.isnullorempty(dbfilename))
        //    {
        //        throw new exception("no database filename specified");
        //    }
        //    else
        //    {
        //        this.load(dbfilename);
        //    }
        //}
        public void load(XmlNode db_elements)
        {
            try
            {
                foreach (XmlNode data in db_elements.ChildNodes)
                {
                    string data_name = data.Attributes.GetNamedItem("name").Value;
                    string data_type = data.Attributes.GetNamedItem("type").Value;
                    string data_comment = data.Attributes.GetNamedItem("comment").Value;
                    string data_value = data.Attributes.GetNamedItem("value").Value;

                    Type new_data_type = Type.GetType("GAVPI." + data_type);

                    object data_type_instance  = null;
                    object data_entry_instance = null;

                    MethodInfo info = new_data_type.GetMethod("ToObject");

                    data_type_instance = info.Invoke(null, new object[] { data_value });

                    data_entry_instance = Activator.CreateInstance(new_data_type, data_name, data_type_instance, data_comment);

                    if (data_entry_instance != null)
                    {
                        if (!Insert((VI_Data)data_entry_instance))
                        {
                            // TODO: Log a warning, variable failed to load.
                        }
                        else
                        {
                            // success, log it?
                        }
                    }
                }
            }
            catch(Exception erro)
            {
                MessageBox.Show("Warning: Failed to load database from xml, error details: " + erro.Message);
            }
        }

        public void save(XmlWriter writer)
        {
            // VI_Profile will call save with writer
            // after writing the rest of the profile data to writer.

            writer.WriteStartElement("VI_DB");

            foreach (KeyValuePair<string, VI_Data> data in DB)
            {
                writer.WriteStartElement("VI_Data");
                    writer.WriteAttributeString("name",   data.Key);
                    writer.WriteAttributeString("type",   data.Value.type);
                    writer.WriteAttributeString("value",  data.Value.value.ToString());
                    writer.WriteAttributeString("comment",data.Value.comment);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.Flush();
        }
        #endregion
        #region Insert/Remove
        public bool Insert(VI_Data data)
        {
            if (!DB.ContainsKey(data.name)){
                DB.Add(data.name,data);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(VI_Data data)
        {
            if (DB.ContainsKey(data.name))
            {
                DB.Remove(data.name);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(VI_Data data)
        {
            if (DB.ContainsKey(data.name)) 
            {
                DB[data.name] = data;
                return true;
            }
            else
            { 
                return false; 
            }

        }
        #endregion
        #region Validation
        public bool isDataNameTaken(string name_to_check)
        {
            return DB.ContainsKey(name_to_check);
        }
        #endregion
    }
}