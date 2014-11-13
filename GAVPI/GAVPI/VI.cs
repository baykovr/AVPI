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
		
		//
		//  public bool load_listen()
		//
		//  load_listen() establishes the speech recognition engine based on the command glossary stored within the
		//  currently loaded Profile.  load_listen() may fail, returning Boolean FALSE, if a Profile's glossary does
		//  not meet the engine's grammar requirements; load_listen() will also fail, returning Boolean FALSE, should
		//  an exception occur that cannot be resolved within the method.  load_listen() will return Boolean TRUE upon
		//  success.
		//
		
        public bool load_listen(VI_Profile profile, VI_Settings settings, ListView statusContainer)
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
                return false;
            }
            phrases_grammar.Append(new Choices(glossory.ToArray()));

			vi_sre.LoadGrammar(new Grammar(phrases_grammar));
			//set event function
			vi_sre.SpeechRecognized += phraseRecognized;
			vi_sre.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;

			try {

				vi_sre.SetInputToDefaultAudioDevice();

			} catch( InvalidOperationException exception ) {
			
				//  For the time being, we're only catching failures to address an input device (typically a
				//  microphone).
			
				MessageBox.Show( "Have you connected a microphone to this computer?\n\n" +
                                 "Please ensure that you have successfull connected and configured\n" +
                                 "your microphone before trying again.",
								 "I cannot hear you!",
								 MessageBoxButtons.OK,
								 MessageBoxIcon.Exclamation,
		                         MessageBoxDefaultButton.Button1 );
			
				return false;
			
			}

			vi_sre.RecognizeAsync(RecognizeMode.Multiple);

			//  TODO:
			//  Push-to-Talk keyboard hook.  Unimplemented.

			try {

				KeyboardHook.KeyDown += pushtotalk_keyDownHook;
				KeyboardHook.KeyUp += pushtotalk_keyUpHook;
				KeyboardHook.InstallHook();
				
			} catch( OverflowException exception ) {
			
				//  TODO:
				//  InputManager library, which we rely upon, has issues with .Net 4.5 and throws an Overflow exception.
				//  We'll catch it here and pretty much let it go for now (since Push-to-Talk isn't implemented yet)
				//  with the intent of resolving it later.
			
			}

            if (settings.pushtotalk_mode != "Hold" && settings.pushtotalk_mode != "PressOnce")
            {
                pushtotalk_active = true;
            }
			
			//  We have successfully establish an instance of a SAPI engine with a well-formed grammar.
			
			return true;
			
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

                UpdateStatusLog( recognized_value.ToString() );

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

            UpdateStatusLog( "?" );

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
                        UpdateStatusLog( "Start Listening" );
                    }
                    else if (settings.pushtotalk_mode == "PressOnce")
                    {
                        if (pushtotalk_active == false)
                        {
                            pushtotalk_active = true;
                            UpdateStatusLog( "Start Listening" );
                        }
                        else
                        {
                            pushtotalk_active = false;
                            UpdateStatusLog( "Stop Listening" );
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

                        UpdateStatusLog( "Stop Listening" );
                    }
                    else if (settings.pushtotalk_mode == "PressOnce")
                    {
                        pushtotalk_keyIsDown = false;
                    }
                }
            }
        }

        //
        //  private void UpdateStatusLog( string )
        //
        //  The main Form contains a ListView control maintains a running log of all recognised commands and keystrokes,
        //  updated with a log message at each entry.
        //

        private void UpdateStatusLog( string LogMessage )
        {

            //  Add the passed message to the running log...

            statusContainer.Items.Add( LogMessage );

            //  ... And always ensure that the last entry added to the statusContainer list is visible...

            statusContainer.EnsureVisible( statusContainer.Items.Count - 1 );

            statusContainer.Refresh();

        }  //  private void UpdateStatusLog( string )

    }
}
