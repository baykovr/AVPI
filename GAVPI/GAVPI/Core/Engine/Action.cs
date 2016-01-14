using InputManager;
using NAudio.Wave;
using System;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

/*
*  Arbitrary abstract Action
*  Currently one of 
*  Keyboard up/down/press
*  Mouse up/down/press
*  Wait 
*  ...add your own
*  e.g. public partial class PlaySound() ...
*/

namespace GAVPI
{
    public abstract class Action
    {
        public string type { get; set; }
        public abstract string value { get; set; }

        public abstract void run();

        public Action()
        {
            this.type = this.GetType().Name;
        }
        public Action(string value)
        {
            this.value = value;
            this.type = this.GetType().Name;
        }
    }

    public partial class KeyDown : Action
    {
        public KeyDown(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get; set;
        }
        public override void run()
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), value));
        }
    }
    public partial class KeyUp : Action
    {
        public KeyUp(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Keyboard.KeyUp((Keys)Enum.Parse(typeof(Keys), value));
        }
    }
    public partial class KeyPress : Action
    {
        public KeyPress(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Keyboard.KeyPress((Keys)Enum.Parse(typeof(Keys), value));
        }
    }

    public partial class MouseKeyDown : Action
    {
        public MouseKeyDown(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Mouse.ButtonDown((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class MouseKeyUp : Action
    {
        public MouseKeyUp(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Mouse.ButtonUp((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class MouseKeyPress : Action
    {
        public MouseKeyPress(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Mouse.PressButton((Mouse.MouseKeys)Enum.Parse(typeof(Mouse.MouseKeys), value));
        }
    }
    public partial class Wait : Action
    {
        public Wait(string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            //Thread.Sleep( Int32.Parse(value) ); // doesnt like this, why?
            int sleep = Int32.Parse(value);
            Thread.Sleep(sleep);
        }
    }
    public partial class Speak : Action
    {
        SpeechSynthesizer Profile_Synthesis;
        public Speak(SpeechSynthesizer Profile_Synthesis, string value) : base(value)
        {
            this.Profile_Synthesis = Profile_Synthesis;
            this.value = value;
        }
        public override string value
        {
            get;
            set;
        }

        /*public static Speak readXML(XmlNode element, SpeechSynthesizer synth)
        {
            return (Speak)Activator.CreateInstance(
                Type.GetType("GAVPI." + element.Attributes.GetNamedItem("type").Value),
                element.Attributes.GetNamedItem("value").Value
                );
        }*/

        public override void run()
        {
            try
            {
                Profile_Synthesis.SpeakAsync(value);
            }
            catch (Exception synth_err)
            {
                MessageBox.Show("Speech Profile_Synthesis Err : " + synth_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
        }
    }
    #region PlaySound Actions
    // More formats support
    public partial class Play_Sound : Action
    {
        // Optional : WMP
        // WMPLib.WindowsMediaPlayer player;

        IWavePlayer wavePlayer;
        AudioFileReader audioFileReader;
        int playBackDeviceID;

        public const int defaultDeviceID = -1;
        
        /* Extending actions to multiple values is a job for another day,
         * for now we will simply serialize the deviceid and filename as such:
         * ID|filename
         * Deliminator is | since it is an invalid filename character, if no delim or other error fallback to default device.
         * */
        public Play_Sound(string filename,int deviceID)
        {
            this.value = filename;
            this.playBackDeviceID = deviceID;

            try
            {
                // Optional : WMP
                // player = new WMPLib.WindowsMediaPlayer();
                WaveOut wavout = new WaveOut();
                wavout.DeviceNumber = playBackDeviceID;
                wavePlayer = wavout;
                audioFileReader = new AudioFileReader(this.value);
            }
            catch (Exception e)
            {
                // TODO : Notify error.
            }
        }

        public int getDeviceID()
        {
            return playBackDeviceID;
        }

        /*
        * Instead of using an encoding hack, we modify the action parsing/saving cases in the profile code.
        public static Tuple<int, string> decodeFilename(string encoded_filename)
        {
            const char delim = '|';
            // No delim present, this is probably just the filename from an old profile.
            if (!encoded_filename.Contains(delim))
            {
                return new Tuple<int, string>(defaultDeviceID, encoded_filename);
            }
            else
            {
                int decodedID;
                string[] decoded = encoded_filename.Split(delim);
                if (decoded.Length == 2 && Int32.TryParse(decoded[0], out decodedID))
                {
                    return new Tuple<int, string>(decodedID, decoded[1]);
                }
                else
                {
                    return new Tuple<int, string>(defaultDeviceID, encoded_filename);
                }
            }
        }
        public static string encodeFilename(int deviceID, string filename)
        {
            return deviceID.ToString() + delim + filename;
        }*/

        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            try
            {
                // Optional : WMP
                // player.URL = this.value;
                // player.controls.play();

                // Device ID cannot be changed after init, so we will init here in case there is a live update.
                wavePlayer.Init(audioFileReader);
                wavePlayer.Play();
            }
            catch (Exception e)
            {
                // TODO : Notify error.
            }
        }
    }
    #endregion

    #region Data Actions
    public partial class Data_Decrement : Action
    {
        VI_Data data;
        public Data_Decrement(VI_Data data, string value) : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get { return data.value.ToString(); }
            set { this.value = value; }
        }
        public override void run()
        {

        }
    }
    public partial class Data_Increment : Action
    {
        VI_Data data;
        public Data_Increment(VI_Data data, string value)
            : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get { return data.value.ToString(); }
            set { this.value = value; }
        }
        public override void run()
        {

        }
    }
    public partial class Data_Set : Action
    {
        VI_Data data;
        public Data_Set(VI_Data data, string value)
            : base(value)
        {
            this.value = value;
        }
        public override string value
        {
            get { return data.value.ToString(); }
            set { this.value = value; }
        }
        public override void run()
        {


        }
    }
    public partial class Data_Speak : Action
    {
        /* Data Speak's value is the data elements key (it's name)
         * however on invocation it will query data.value for the actual
         * value. (in other words we access the value by reference)
         * Robert (04.12.15)
         */
        SpeechSynthesizer Profile_Synthesis;
        VI_Data data;

        public Data_Speak(SpeechSynthesizer Profile_Synthesis, VI_Data data)
        {
            this.Profile_Synthesis = Profile_Synthesis;
            this.data = data;
        }

        public override string value
        {
            get { return data.name; }
            set { this.value = data.name; }
        }

        //public ValueType 
        public override void run()
        {
            try
            {
                Profile_Synthesis.SpeakAsync(data.value.ToString());
            }
            catch (Exception synth_err)
            {
                MessageBox.Show("Speech Profile_Synthesis Err : " + synth_err.Message, "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }


        }
    }

    // Simple WAV implementation taken from AVPI
    //public partial class Play_WAV : Action
    //{
    //    System.Media.SoundPlayer wav;

    //    public Play_WAV(string filename)
    //    {
    //        this.value = filename;
    //        try
    //        {
    //            wav = new System.Media.SoundPlayer(this.value);
    //        }
    //        catch (Exception e)
    //        {
    //            // TODO : Notify error.
    //        }
    //    }

    //    public override void run()
    //    {
    //        //Quick and easy.
    //        try
    //        {
    //            wav.Play(); //async
    //            wav.Dispose();
    //        }
    //        catch (Exception e)
    //        {
    //            // TODO : Notify error.
    //        }
    //    }
    //}

    #endregion
}
