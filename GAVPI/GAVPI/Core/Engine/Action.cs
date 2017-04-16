using InputManager;
using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

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

        public KeyDown(string value) : base(value) { }

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
        public KeyUp(string value) : base(value) { }

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
        public KeyPress(string value) : base(value) { }

        public override string value
        {
            get;
            set;
        }
        public override void run()
        {
            Keyboard.KeyDown((Keys)Enum.Parse(typeof(Keys), value));
            Keyboard.KeyUp((Keys)Enum.Parse(typeof(Keys), value));
        }
    }

    public partial class MouseKeyDown : Action
    {
        public MouseKeyDown(string value) : base(value) { }
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
        public MouseKeyUp(string value) : base(value) { }
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
        public MouseKeyPress(string value) : base(value) { }
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
        public Wait(string value) : base(value) { }
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
        }

        public override string value
        {
            get;
            set;
        }

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

    public partial class Play_Sound : Action
    {
        IWavePlayer wavePlayer;
        WaveOut wavout;
        AudioFileReader audioFileReader;
        int playBackDeviceID;

        public const int defaultDeviceID = -1;

        public Play_Sound(string filename, int deviceID) : base(filename)
        {
            this.playBackDeviceID = deviceID;

            try
            {
                wavout = new WaveOut();
                wavout.DeviceNumber = playBackDeviceID;
                wavePlayer = wavout;
                audioFileReader = new AudioFileReader(this.value);
            }
            catch (Exception e)
            {
                GAVPI.ProfileDebugLog.Entry("[ ! ] Error Details: " + e.Message);
                this.LogError();
            }
        }

        public int getDeviceID()
        {
            return playBackDeviceID;
        }

        public override string value
        {
            get;
            set;
        }

        public void stop()
        {
            if (wavePlayer != null)
            {
                wavePlayer.Stop();
            }
        }

        public override void run()
        {
            try
            {
                // Device ID cannot be changed after init, so we will init here in case there is a live update.
                wavePlayer.Pause();
                audioFileReader.Seek(0, System.IO.SeekOrigin.Begin);
                wavePlayer.Init(audioFileReader);
                wavePlayer.Play();
            }

            catch (NAudio.MmException e)
            {
                GAVPI.ProfileDebugLog.Entry("[ ! ] Error Details: " + e.Message);
                this.LogError();

                // NAudio always throws MmExceptions so we parse from the message for this case.
                if (e.Message.Equals("BadDeviceId calling waveOutOpen"))
                {
                    GAVPI.ProfileDebugLog.Entry("[...] Attempting to playback via default device next time.");

                    // A little self healing, it's likely that device was unplugged or
                    // changed in some way since the profie has been loaded.
                    wavout = new WaveOut();
                    wavout.DeviceNumber = defaultDeviceID;
                    wavePlayer = wavout;
                    audioFileReader = new AudioFileReader(this.value);

                }
            }
            catch (Exception e)
            {
                GAVPI.ProfileDebugLog.Entry("[ ! ] Error Details: " + e.Message);
                this.LogError();
            }
        }

        private void LogError()
        {
            GAVPI.ProfileDebugLog.Entry(
                String.Format("[ ! ] Error in Play_Sound: File {0}, Device {1}",
                this.value, this.playBackDeviceID
                )
            );
        }
    }

    public partial class Stop_Sound : Action
    {
        public Stop_Sound()
        {
            this.value = "Stop All Playback";
        }

        public Stop_Sound(string value) : base(value) { }

        public override string value
        {
            get;
            set;
        }

        public override void run()
        {
            foreach (Action_Sequence action_sequence in GAVPI.Profile.Profile_ActionSequences)
            {
                foreach (Action action in action_sequence.action_sequence)
                {
                    if (action is Play_Sound)
                    {
                        Play_Sound test = (Play_Sound)action;
                        test.stop();
                    }
                }
            }
        }
    }

    #endregion

    #region Data Actions
    public partial class Data_Decrement : Action
    {
        Data data;
        public Data_Decrement(Data data, string value) : base(value) { }
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
        Data data;
        public Data_Increment(Data data, string value) : base(value) { }

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
        Data data;
        public Data_Set(Data data, string value) : base(value) { }
        public override string value
        {
            get { return data.value.ToString(); }
            set { this.value = value; }
        }
        public override void run()
        {


        }
    }

    /* Data Speak's value is the data elements key (it's name)
     * however on invocation it will query data.value for the actual
     * value.
     */
    public partial class Data_Speak : Action
    {
        SpeechSynthesizer Profile_Synthesis;
        Data data;

        public Data_Speak(SpeechSynthesizer Profile_Synthesis, Data data)
        {
            this.Profile_Synthesis = Profile_Synthesis;
            this.data = data;
        }

        public override string value
        {
            get { return data.name; }
            set { this.value = data.name; }
        }

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

    #endregion

    #region System Action
    // System calls and actionss.
    public partial class ProcessExec : Action
    {
        Process proc = new Process();

        public ProcessExec(string proc_name) : base(proc_name) { }

        public override string value
        {
            get; set;
        }

        public override void run()
        {
            Process.Start(this.value);
        }
    }

    public partial class ClipboardCopy : Action
    {
        public ClipboardCopy(string value) : base(value) { }

        public override string value
        {
            get
            {
                throw new NotImplementedException();

            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void run()
        {
            throw new NotImplementedException();
        }
    }

    public partial class ClipboardPaste : Action
    {
        public ClipboardPaste(string value) : base(value) { }

        public override string value
        {
            get; set;
        }

        public override void run()
        {
            Clipboard.SetText(this.value, TextDataFormat.Text);
            SendKeys.Send("^V");
        }
    }

    #endregion

    #region Nest Action
    public partial class Nest : Action
    {

        public Nest(string value) : base(value)
        {
        }
        public override string value
        {
            get;
            set;
        }

        public override void run()
        {
            foreach (Action_Sequence action_sequence in GAVPI.Profile.Profile_ActionSequences)
            {
                if (action_sequence.name.Equals(this.value))
                {
                    action_sequence.run();
                    break;
                }
            }
        }
    }
    #endregion
}
