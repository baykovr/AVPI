using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GAVPI
{
    /*
     * This approach is not very scalable, however it sufficies give the
     * time constraint. 
     * Alternative approach is to use a tempalte class, this causes some arithmatic 
     * abstraction problems, however..
     */
    public abstract class Data
    {
        /*
         * Data represents a simple data element which we will allow the user to 
         * manipulate with some primitive functions: set, inc, dec etc.
         * Primary use is for logical triggers which have rely on comparator components.
         * 
         * WARNING : New partical classes MUST implement static functions:
         * failure to do so will result in misbehavior or crash in DB form frm_AddEdit_Data.cs
         *  public static bool validate(string type, string value)
         *      (return indicates true if string value can be converted to inner value ex: int)
         *  public static object ToObject(string value)
         *      (returns converted object to type of inner value ex: int)
         */
        public string name  { get; set; }
        public string type  { get; set; }
        public object value { get; set; }
        public string comment { get; set; }
        
        // Available data types
        public static List<string> Data_Types = new List<string>(
            new string[] { 
                "INT", "FLOAT", "STRING"
            });

        // Operations (all optional)
        // dec by val
        // inc by val
        // set by val

        public Data(string name,string comment)
        {
            this.name = name;
            this.comment = comment;
        }
        public static bool validate(string type, string value)
        {
            if (type != null)
            {
                Type thisType = Type.GetType(type);
                MethodInfo method = thisType.GetMethod("validate");
                bool result = (bool)method.Invoke(null, new string[] { value });
                return result;
            }
            else
            {
                //type is not set
                return false;
            }
    }
    }
    public partial class INT : Data
    {
        //int value;
        public INT(string name, int value, string comment)
            : base(name,comment)
        {
            this.value = value;
            this.type = "INT";
        }
        public void decrement(int val)
        {
            // sigh ...
            int temp_cast = (int)this.value;
            temp_cast -= val;
            this.value = temp_cast;
        }
        public void increment(int val)
        {
            // sigh ...
            int temp_cast = (int)this.value;
            temp_cast += val;
            this.value = temp_cast;
        }
        public void set(int val)
        {
            this.value = val;
        }
        public static bool validate(string value)
        {
            int test;
            return int.TryParse(value, out test);
        }
        public static object ToObject(string value)
        {
            // Incase someone makes a ToObject call without validation
            // at all cost don't crash.
            if (validate(value))
            {
                return int.Parse(value);
            }
            else
            {
                return null;
            }
        }

    }
    public partial class FLOAT : Data
    {
        public FLOAT(string name, float value, string comment)
            : base(name, comment)
        {
            this.value = value;
            this.type = "FLOAT";
        }
        public void decrement(float val)
        {
            // sigh ...
            float temp_cast = (float)this.value;
            temp_cast -= val;
            this.value = temp_cast;
        }
        public void increment(float val)
        {
            // sigh ...
            float temp_cast = (float)this.value;
            temp_cast += val;
            this.value = temp_cast;
        }
        public void set(float val)
        {
            this.value = val;
        }
        public static bool validate(string value)
        {
            float test;
            return float.TryParse(value, out test);
        }
        public static object ToObject(string value)
        {
            // Incase someone makes a ToObject call without validation
            // at all cost don't crash.
            if (validate(value))
            {
                return float.Parse(value);
            }
            else
            {
                return null;
            }
        }
    }
    public partial class STRING : Data
    {
        public STRING(string name, string value, string comment)
            : base(name, comment)
        {
            this.value = value;
            this.type = "STRING";
        }
        public void set(string val)
        {
            this.value = val;
        }
        public static bool validate(string value)
        {
            return !(String.IsNullOrEmpty(value));
        }
        public static object ToObject(string value)
        {
            return value;
        }
    }
}
