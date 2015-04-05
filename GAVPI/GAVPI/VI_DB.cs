using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
        public void load()
        {
            if (String.IsNullOrEmpty(DBFilename))
            {
                throw new Exception("No database filename specified");
            }
            else
            {
                this.load(DBFilename);
            }

           
        }
        public void load(string filename)
        {
            //sample code
            //gavpi_root.Add(new XElement("Student",
            //           new XElement("FirstName", "David"),
            //           new XElement("LastName", "Smith")));

            //XmlDocument dbxml = new XmlDocument();
            //try
            //{
            //    dbxml.Load(DBFilename);

            //    // Attempt to parse and load
            //    if (dbxml.DocumentElement.Name != "gavpidb")
            //    {
            //        throw new Exception("Malformed settings file expected first tag gavpidb got,"
            //        + dbxml.DocumentElement.Name);
            //    }
            //    XmlNodeList dbxml_elements = dbxml.DocumentElement.ChildNodes;
            //    foreach (XmlNode element in dbxml_elements)
            //    {
            //        /*populate dictionary*/
            //    }
            //}
            //catch (Exception loading_err)
            //{
            //    /*Not good.*/
            //}
        }
        //public void save()
        //{
        //    if (String.IsNullOrEmpty(DBFilename))
        //    {
        //        throw new Exception("No database filename specified");
        //    }
        //    else
        //    {
        //        this.save(DBFilename);
        //    }
        //}
        public void save(XmlWriter writer)
        {
            // VI_Profile will call save with writer
            // after writing the rest of the profile data to writer.

            writer.WriteStartElement("VI_DB");

            foreach (KeyValuePair<string, VI_Data> data in DB)
            {
                writer.WriteStartElement("VI_Data");
                    writer.WriteAttributeString("name", data.Key);
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
            else{
                return false;
            }
        }
        public bool Remove(VI_Data data)
        {
            if (DB.ContainsKey(data.name)){
                DB.Remove(data.name);
                return true;
            }
            else{
                return false;
            }
        }
        public bool Update(VI_Data data)
        {
            if (DB.ContainsKey(data.name)) {
                DB[data.name] = data;
                return true;
            }
            else{ 
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