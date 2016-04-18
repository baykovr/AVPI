using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

using InputManager;

/* Input Engine, handles speech/sysnthesis req
 * can be though of as the engine
 */

namespace GAVPI
{
    public class InputEngine
    {
        SpeechSynthesizer synth;
        SpeechRecognitionEngine speech_re;
        
        private bool pushtotalk_active;
        private bool pushtotalk_keyIsDown;
        private KeyboardHook.KeyDownEventHandler pushtotalk_keyDownHook;
        private KeyboardHook.KeyUpEventHandler pushtotalk_keyUpHook;

        public bool IsListening = false;

        public InputEngine()
        {
            pushtotalk_active      = false;
            pushtotalk_keyIsDown   = false;
            pushtotalk_keyDownHook = new KeyboardHook.KeyDownEventHandler(KeyboardHook_KeyDown);
            pushtotalk_keyUpHook   = new KeyboardHook.KeyUpEventHandler(KeyboardHook_KeyUp);
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
		// Optimizations : 04.28.15
        // 
        public bool load_listen()
        {
            // Don't allocate anything if we have no phrases to hook.
            if ( GAVPI.Profile.Profile_Triggers!= null && 
                 GAVPI.Profile.Profile_Triggers.Count == 0)
            {
                MessageBox.Show("You need to add at least one Trigger");
                return false;
            }

            synth = GAVPI.Profile.synth;
            synth.SelectVoice( GAVPI.Settings.voice_info );
            speech_re = new SpeechRecognitionEngine( GAVPI.Settings.recognizer_info );

            GrammarBuilder phrases_grammar = new GrammarBuilder();
            // Grammer must match speech recognition language localization
            phrases_grammar.Culture = GAVPI.Settings.recognizer_info;

            List<string> glossory = new List<string>();
            
            // Add trigger phrases to glossory of voice recognition engine.
            foreach (Phrase trigger in GAVPI.Profile.Profile_Triggers)
            {
                glossory.Add(trigger.value);
            }


            phrases_grammar.Append(new Choices(glossory.ToArray()));
			speech_re.LoadGrammar(new Grammar(phrases_grammar));

			// event function hook
			speech_re.SpeechRecognized          += phraseRecognized;
			speech_re.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;

			try 
            {
				speech_re.SetInputToDefaultAudioDevice();
			} 
            catch( InvalidOperationException exception ) 
            {
				//  For the time being, we're only catching failures to address an input device (typically a
				//  microphone).
				MessageBox.Show( "Have you connected a microphone to this computer?\n\n" +
                                 "Please ensure that you have successfull connected and configured\n" +
                                 "your microphone before trying again.",
                                 "I cannot hear you! (" + exception.Message+")",
								 MessageBoxButtons.OK,
								 MessageBoxIcon.Exclamation,
		                         MessageBoxDefaultButton.Button1 );
			
				return false;
			}

			speech_re.RecognizeAsync(RecognizeMode.Multiple);

            // Install Push to talk key hooks.
			KeyboardHook.KeyDown += pushtotalk_keyDownHook;
			KeyboardHook.KeyUp += pushtotalk_keyUpHook;
			KeyboardHook.InstallHook();

            if( GAVPI.Settings.pushtotalk_mode != "Hold" && GAVPI.Settings.pushtotalk_mode != "Toggle" && GAVPI.Settings.pushtotalk_mode != "Single")
            {
                pushtotalk_active = true;
            }
			
			//  We have successfully establish an instance of a SAPI engine with a well-formed grammar.

            IsListening = true;

			return true;
			
        }
        public void stop_listen()
        {
            if (speech_re!=null)
            {
                speech_re.RecognizeAsyncCancel();
                speech_re.UnloadAllGrammars();
                speech_re.Dispose();
            }
            KeyboardHook.UninstallHook();
            KeyboardHook.KeyDown -= pushtotalk_keyDownHook;
            KeyboardHook.KeyUp -= pushtotalk_keyUpHook;

            IsListening = false;

        }
        private void phraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (pushtotalk_active)
            {
                string recognized_value = e.Result.Text;

                //predicates are cool
                GAVPI.Profile.Profile_Triggers.Find(trigger => trigger.value == recognized_value).run();
                
                //equivilent code below
                //foreach (VI_Phrase phrase in profile.Profile_Triggers)
                //{
                //    if (phrase.value == recognized_value)
                //    {
                //        phrase.run(); // Fire events
                //    }
                //}

                // Update the log after the action is run, since UpdateStatus is a blocking method.
                UpdateStatusLog(recognized_value.ToString());
                if (GAVPI.Settings.pushtotalk_mode == "Single")
                {
                    pushtotalk_active = false;
                    UpdateStatusLog( "Stop Listening" );
                }
            }

        }
        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e) 
        {
            UpdateStatusLog("?");
        }

        void KeyboardHook_KeyDown(int vkCode)
        {
            if (pushtotalk_keyIsDown == true)
                { return; }

            // Only check if pushtotalk_keyIsDown is false
            // aka : pptKey is not down
            if (((Keys)vkCode).ToString() == GAVPI.Settings.pushtotalk_key)
            {
                //if (pushtotalk_keyIsDown == false)
                    if ( GAVPI.Settings.pushtotalk_mode == "Hold")
                    {
                        pushtotalk_active = true;
                        UpdateStatusLog( "Start Listening" );
                    }
                    else if ( GAVPI.Settings.pushtotalk_mode == "Toggle")
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
                    else if ( GAVPI.Settings.pushtotalk_mode == "Single")
                    {
                        if (pushtotalk_active == false)
                            UpdateStatusLog( "Start Listening" );
                        pushtotalk_active = true;
                    }
                    else
                    {
                        // TODO : Log warning, someone edited settings code 
                        // but did not update the hooks here.
                    }
                    pushtotalk_keyIsDown = true;
            }
        }
        void KeyboardHook_KeyUp(int vkCode)
        {
            if (pushtotalk_keyIsDown == false)
                { return; }
            
            if (((Keys)vkCode).ToString() == GAVPI.Settings.pushtotalk_key)
            {
                //if (pushtotalk_keyIsDown == true)
                    if (GAVPI.Settings.pushtotalk_mode == "Hold")
                    {
                        pushtotalk_keyIsDown = false;
                        pushtotalk_active = false;

                        UpdateStatusLog( "Stop Listening" );
                    }
                    else if (GAVPI.Settings.pushtotalk_mode == "Toggle" || GAVPI.Settings.pushtotalk_mode == "Single")
                    {
                        pushtotalk_keyIsDown = false;
                    }
                    else
                    {
                        // TODO : Log warning, someone edited settings code 
                        // but did not update the hooks here.
                    }
            }
        }



        //
        //  private void UpdateStatusLog( string )
        //
        //  The main Form contains a ListBox showing a running log of all recognised commands and keystrokes,
        //  updated with a log message at each entry.
        //
        private void UpdateStatusLog(string LogMessage)
        {
            GAVPI.Log.Entry(LogMessage);
        }

    }
}
