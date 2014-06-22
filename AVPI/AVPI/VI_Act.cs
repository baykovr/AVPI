using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InputManager;
using System.Threading;
using System.Windows.Forms;

namespace AVPI
{
    public class VI_Act
    {
        public void max_power_g1()
        {
            Keyboard.KeyDown(Keys.F1);
            Keyboard.KeyUp(Keys.F1);
            Thread.Sleep(1000);

            Keyboard.KeyDown(Keys.F3);
            Keyboard.KeyUp(Keys.F3);
            Thread.Sleep(1000);

            Keyboard.KeyDown(Keys.NumPad7);
            Keyboard.KeyUp(Keys.NumPad7);

            Keyboard.KeyDown(Keys.NumPad7);
            Keyboard.KeyUp(Keys.NumPad7);
            Keyboard.KeyDown(Keys.NumPad7);
            Keyboard.KeyUp(Keys.NumPad7);
            Keyboard.KeyDown(Keys.NumPad7);
            Keyboard.KeyUp(Keys.NumPad7);
            Thread.Sleep(1000);

            Keyboard.KeyDown(Keys.NumPad5);
            Keyboard.KeyUp(Keys.NumPad5);
            Thread.Sleep(1000);

            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);
            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);
            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);
            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);
            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);
            Keyboard.KeyDown(Keys.NumPad8);
            Keyboard.KeyUp(Keys.NumPad8);

            Thread.Sleep(1000);
            vpi.Speak("fifty percent completed");

            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);
            Keyboard.KeyDown(Keys.NumPad4);
            Keyboard.KeyUp(Keys.NumPad4);

            Thread.Sleep(1000);

            Keyboard.KeyDown(Keys.F1);
            Keyboard.KeyUp(Keys.F1);
        }
    }
}
