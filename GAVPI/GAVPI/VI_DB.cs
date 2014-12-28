using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, VI_Data> DB;

        public VI_DB()
        { 
            // null filename db? (perhaps you want a tempdb)
        }
        public VI_DB(string filename)
        {
            DBFilename = filename;
            DB = new Dictionary<string, VI_Data>();
        }
        #region FileIO
        public void load()
        {

 
        }
        public void save()
        {
 
        }
        public void save(string filename)
        {
 
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
    }
}
