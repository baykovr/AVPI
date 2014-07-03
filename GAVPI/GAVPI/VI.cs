using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

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

        public VI(VI_Profile profile,VI_Settings settings)
        {
            this.profile = profile;
            this.settings = settings;

            //get speech rec/syn prefs from settings
            vi_syn = new SpeechSynthesizer();
            vi_sre = new SpeechRecognitionEngine(settings.recognizer_info);

            //--build grammar from vi_phrases
            // more complex grammar later, let the speech engine fight over
            // blurry phrases for now
            GrammarBuilder phrases_grammar = new GrammarBuilder();
            foreach (VI_Phrase trigger in profile.Profile_Triggers)
            {
                phrases_grammar.Append(new Choices(trigger.value));
            }

            phrases_grammar.Append(new Choices("default value"));

            vi_sre.LoadGrammar(new Grammar(phrases_grammar));
            //set event function
            vi_sre.SpeechRecognized += phraseRecognized;

            // TODO : Settings for non default audio
            vi_sre.SetInputToDefaultAudioDevice();
            vi_sre.RecognizeAsync(RecognizeMode.Multiple);
        }
        private void phraseRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string recognized_value = e.Result.Text;

            foreach (VI_Phrase phrase in profile.Profile_Triggers)
            {
                if (phrase.value == recognized_value)
                {
                    phrase.run(); // Fire events
                }
            }

        }

        
    }
}
