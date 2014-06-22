using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;

namespace AVPI
{

    public class VI
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        
        VI_Aurora ship;
        SpeechSynthesizer vpi;
        SpeechRecognitionEngine main_sre = null;
        ManualResetEvent manualResetEvent = null;
        bool additional_sounds;
        bool override_pwr_status;
        bool debug_mode;

        string[] glossary = { "G0", "G1", "G2", "G3", "status", "scan", "target nearest", "next target", "pin target", "missile fire", "missile lock" };

        public VI(VI_Aurora ship)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            this.ship = ship;
            manualResetEvent = new ManualResetEvent(false);
            vpi = new SpeechSynthesizer();
            debug_mode = true;
            override_pwr_status = false;
            additional_sounds = false;


            welcome();
            listen();
        }
        public void listen()
        {
            main_sre = new SpeechRecognitionEngine();
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(new Choices(glossary));
            main_sre.LoadGrammar(new Grammar(grammarBuilder));
            //main_sre.LoadGrammar(new DictationGrammar()); can be used to detect abritrary words (accuracy is so so)
            main_sre.SpeechRecognized += speechRecognitionWithDictationGrammar_SpeechRecognized;
            main_sre.SetInputToDefaultAudioDevice();
            main_sre.RecognizeAsync(RecognizeMode.Multiple);
        }
        #region Dictation
        
        void speechRecognitionWithDictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string voice = e.Result.Text;

            #region Override Power Status
            if (override_pwr_status)
            {

            }
            #endregion

            if (voice.Contains("reset"))
            {
                print_say("goodbye");
            }

            else if (voice.Contains("G0"))
            {
                //Remember async will NOT print text to console only say it.
                _play_sound_async("vaglad/power balance.wav","channeling power to G0");
                ship.pwr_center();

                println(10, '>', "channeling power to G0");
                print_say("completed");
            }
            else if(voice.Contains("G1"))
            {
                _play_sound_async("vaglad/power weapons.wav", "channeling power to G1");
                ship.max_pwr_g1();
                println(10, '>', "channeling power to G1");
                print_say("completed");
            }
            else if(voice.Contains("G2"))
            {
                _play_sound_async("vaglad/power engines.wav", "channeling power to G2");
                ship.max_pwr_g2();

                println(10, '>', "channeling power to G2");
                print_say("completed");
            }
            else if(voice.Contains("G3"))
            {
                _play_sound_async("vaglad/power shields.wav", "channeling power to G3");
                ship.max_pwr_g3();

                println(10, '>', "channeling power to G3");
                _play_sound("vaglad/complete.wav","complete");
            }
            #region Targeting
            else if (voice.Contains("target nearest") || voice.Contains("scan"))
            {
                _play_sound_async("vaglad/target nearest.wav","targeting nearest hostile");
                ship.target_nearest_hostile();
                println(10,'O', "targeting nearest hostile");
            }
            else if (voice.Contains("next target"))
            {
                _play_sound_async("vaglad/target next.wav","targeting next hostile");
                ship.target_next_hostile();
                println(10,'O',"targeting next hostile");
                
            }
            else if (voice.Contains("pin target"))
            {
                print_say("acknowleged pin");
                ship.target_pin();

            }
            else if (voice.Contains("missile lock"))
            {
                _play_sound_async("vaglad/locking missle.wav", "locking missile on target");
                ship.missle_lock();
                println(10, '*', "locking missile on target");
            }
            else if (voice.Contains("missile fire"))
            {
                _play_sound_async("vaglad/firing missile.wav", "firing missile");
                ship.missle_fire();
                println(10, '*', "firing missile");
            }
            #endregion
            else if (voice.Contains("status"))
            {
                print_say("The current configuration is G" + ship.pwr_stat.ToString());
            }
            else
            {
                if (debug_mode)
                {
                    println(5, e.Result.Text);
                }
                if (override_pwr_status)
                {
                    print_say("overriding failed, status G" + ship.pwr_stat.ToString());
                    override_pwr_status = false;
                }
            }
        }
        #endregion

        public void welcome()
        {
            println(5, "-----------------------------------------------");
            println(5, "Aritificial Virtual Pilot Interface v0.02      ");
            println(5, "-----------------------------------------------");
            println(5, "booting......................................OK");
            
            check_starcitizen_running();

            println(5, "loading power management computer............OK");
            //extra logic load here
            check_additional_voices();
            print_say_sync("All systems loaded switching to combat mode");

            println();
            println(5, "AVPI CMBI--------------------------------ONLINE");
            _play_sound("vaglad/combat mode.wav", "combat mode activated, safeties disabled");
            print_say_sync("All systems awaiting commands");
        }
        #region Checks
        public void check_starcitizen_running()
        {
            IntPtr windowHandle = FindWindow(null, "Star Citizen");
            if (windowHandle.ToInt32() > 0)
            {
                println(5, "initial check................................OK");
            }
            else
            {
                println(5,'!',"initial check...........................WARNING");
                print_say('!',"warning, star citizen inactive");
            }
        }
        public void check_additional_voices()
        {
            if (Directory.Exists("vaglad"))
            {
                println(5, "checking for additional voice components.....OK");
                additional_sounds = true;
            }
            else
            {
                println(5, '!', "checking for additional voice components...FAIL");
                print_say('!', "Additional voices not found");
                additional_sounds = false;
            }
        }
        #endregion

        #region helpers
        public void println()
        {
            Console.WriteLine();
        }
        public void println(int speed, string message)
        {
            println(speed, ' ', message);
        }
        public void println(int speed,char symbl,string message)
        {
            if (symbl == '!')
                { Console.ForegroundColor = ConsoleColor.DarkYellow; }
            else if (symbl == '>')
                { Console.ForegroundColor = ConsoleColor.DarkGreen; }
            else if (symbl == '*')
                { Console.ForegroundColor = ConsoleColor.DarkRed; }
            else if (symbl == 'O')
                { Console.ForegroundColor = ConsoleColor.DarkCyan; }

            Console.Write("["+symbl+"] ");
            foreach (char m in message)
            {
                Console.Write(m);
                Thread.Sleep(speed);
            }
            Console.Write('\n');
            if (symbl != ' ')
                { Console.ForegroundColor = ConsoleColor.DarkCyan; }
            
        }
        void print_say(char symbl, string msg)
        {
            println(5,symbl, msg);
            say(msg);
        }
        void print_say(string msg)
        {
            println(5,'>', msg);
            say(msg);
        }
        void print_say_sync(char symbl,string msg)
        {
            println(5,symbl,msg);
            say_sync(msg);
        }
        void print_say_sync(string msg)
        {
            println(5,'>',msg);
            say_sync(msg);
        }
        void _play_sound(string filename, string msg)
        {
            if (additional_sounds)
            {
                try
                {
                    SoundPlayer wav = new SoundPlayer(filename);
                    wav.PlaySync();
                    wav.Dispose();
                    println(1, '>', msg);
                }
                catch (Exception sound_err)
                {
                    //Probably Missing wav file, thats ok.
                    println(1, '*', "File not found " + filename + ", delete folder vaglad to disable additional sounds");
                    print_say(msg);
                }
            }
            else{
                print_say(msg);
            }
        }
        void _play_sound_async(string filename,string voice_msg)
        {
            if (additional_sounds){
                try
                {
                    SoundPlayer wav = new SoundPlayer(filename);
                    wav.Play();
                    wav.Dispose();
                }
                catch (Exception sound_err)
                {
                    //Probably Missing wav file, thats ok.
                    println(1, '*', "File not found " + filename +", delete folder vaglad to disable additional sounds");
                    say(voice_msg);
                }
            }
            else{
                say(voice_msg);
            }
        }
        void say_sync(string msg)
            { this.vpi.Speak(msg); }
        void say(string msg)
            { this.vpi.SpeakAsync(msg); }
        #endregion
    }
}
