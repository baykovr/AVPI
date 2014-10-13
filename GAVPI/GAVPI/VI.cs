using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

using InputManager;

/* Virtual Interface, handles speech/sysnthesis req
 * can be though of as the engine
 */

namespace GAVPI
{
    public class VI
    {
        VI_Profile profile;
        VI_Settings settings;

        SpeechSynthesizer vi_syn;
        SpeechRecognitionEngine vi_sre;
        ListView statusContainer; // listview in main form
        private bool pushtotalk_active;
        private bool pushtotalk_keyIsDown;
        private KeyboardHook.KeyDownEventHandler pushtotalk_keyDownHook;
        private KeyboardHook.KeyUpEventHandler pushtotalk_keyUpHook;

        public VI()
        {
            pushtotalk_active = false;
            pushtotalk_keyIsDown = false;
            pushtotalk_keyDownHook = new KeyboardHook.KeyDownEventHandler(KeyboardHook_KeyDown);
            pushtotalk_keyUpHook = new KeyboardHook.KeyUpEventHandler(KeyboardHook_KeyUp);
        }
        public void load_listen(VI_Profile profile, VI_Settings settings, ListView statusContainer)
        {
            this.profile = profile;
            this.settings = settings;
            this.statusContainer = statusContainer;

            vi_syn = profile.synth;
            vi_syn.SelectVoice(settings.voice_info);
            vi_sre = new SpeechRecognitionEngine(settings.recognizer_info);

            GrammarBuilder phrases_grammar = new GrammarBuilder();
            List<string> glossory = new List<string>();

            foreach (VI_Phrase trigger in profile.Profile_Triggers)
            {
                glossory.Add(trigger.value);
            }
            if (glossory.Count == 0)
            {
                MessageBox.Show("You need to add at least one Trigger");
                return;
            }
            phrases_grammar.Append(new Choices(glossory.ToArray()));

            vi_sre.LoadGrammar(new Grammar(phrases_grammar));
            //set event function
            vi_sre.SpeechRecognized += phraseRecognized;
            vi_sre.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            vi_sre.SetInputToDefaultAudioDevice();
            vi_sre.RecognizeAsync(RecognizeMode.Multiple);


            KeyboardHook.KeyDown += pushtotalk_keyDownHook;
            KeyboardHook.KeyUp += pushtotalk_keyUpHook;
            KeyboardHook.InstallHook();

            if (settings.pushtotalk_mode != "Hold" && settings.pushtotalk_mode != "PressOnce")
            {
                pushtotalk_active = true;
            }
        }
        public void stop_listen()
        {
            if (vi_sre!=null)
            {
                vi_sre.RecognizeAsyncCancel();
                vi_sre.UnloadAllGrammars();
                vi_sre.Dispose();
            }
            KeyboardHook.UninstallHook();
            KeyboardHook.KeyDown -= pushtotalk_keyDownHook;
            KeyboardHook.KeyUp -= pushtotalk_keyUpHook;
        }
        private void phraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (pushtotalk_active)
            {
                string recognized_value = e.Result.Text;

                statusContainer.Items.Add(recognized_value.ToString());
                statusContainer.Refresh();

                //predicates are cool
                profile.Profile_Triggers.Find(trigger => trigger.value == recognized_value).run();
                //equivilent code below
                //foreach (VI_Phrase phrase in profile.Profile_Triggers)
                //{
                //    if (phrase.value == recognized_value)
                //    {
                //        phrase.run(); // Fire events
                //    }
                //}
            }

        }
        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            statusContainer.Items.Add("?");
            statusContainer.Refresh();
        }

        void KeyboardHook_KeyDown(int vkCode)
        {
            if (((Keys)vkCode).ToString() == settings.pushtotalk_key)
            {
                if (pushtotalk_keyIsDown == false)
                {
                    if (settings.pushtotalk_mode == "Hold")
                    {
                        pushtotalk_active = true;
                        statusContainer.Items.Add("start listening");
                    }
                    else if (settings.pushtotalk_mode == "PressOnce")
                    {
                        if (pushtotalk_active == false)
                        {
                            pushtotalk_active = true;
                            statusContainer.Items.Add("start listening");
                        }
                        else
                        {
                            pushtotalk_active = false;
                            statusContainer.Items.Add("stop listening");
                        }
                    }
                    pushtotalk_keyIsDown = true;
                }
            }
        }
        void KeyboardHook_KeyUp(int vkCode)
        {
            if (((Keys)vkCode).ToString() == settings.pushtotalk_key)
            {
                if (pushtotalk_keyIsDown == true)
                {
                    if (settings.pushtotalk_mode == "Hold")
                    {
                        pushtotalk_keyIsDown = false;
                        pushtotalk_active = false;
                        statusContainer.Items.Add("stop listening");
                    }
                    else if (settings.pushtotalk_mode == "PressOnce")
                    {
                        pushtotalk_keyIsDown = false;
                    }
                }
            }
        }
    }
}
