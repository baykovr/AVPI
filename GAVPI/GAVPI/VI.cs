using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

/*Virtual Interface, handles speech/sysnthesis req*/

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

            vi_syn = new SpeechSynthesizer();
            vi_sre = new SpeechRecognitionEngine(settings.recognizer_info);

        }

    }
}
