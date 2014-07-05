using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;

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

        public VI(VI_Profile profile, VI_Settings settings, ListView statusContainer)
        {
            this.profile = profile;
            this.settings = settings;
            this.statusContainer = statusContainer;
            //get speech rec/syn prefs from settings
            vi_syn = new SpeechSynthesizer();
            vi_sre = new SpeechRecognitionEngine(settings.recognizer_info);

            //--build grammar from vi_phrases
            // more complex grammar later, let the speech engine fight over
            // blurry phrases for now
            GrammarBuilder phrases_grammar = new GrammarBuilder();

            List<string> glossory = new List<string>();

            foreach (VI_Phrase trigger in profile.Profile_Triggers)
            {
                glossory.Add(trigger.value);
            }
            if (glossory.Count == 0)
            {
                MessageBox.Show("You need to add at least on Phrase");
                return;
            }
            phrases_grammar.Append(new Choices(glossory.ToArray()));

            vi_sre.LoadGrammar(new Grammar(phrases_grammar));
            //set event function
            vi_sre.SpeechRecognized += phraseRecognized;
            vi_sre.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            // TODO : Settings for non default audio
            vi_sre.SetInputToDefaultAudioDevice();
            vi_sre.RecognizeAsync(RecognizeMode.Multiple);
        }
        private void phraseRecognized(object sender, SpeechRecognizedEventArgs e)
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
        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            statusContainer.Items.Add("?");
            statusContainer.Refresh();
        }

        
    }
}
