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
    public class VI_Aurora
    {
        // Current Support for LN ONLY.
        public string model;
        public int pwr_stat;

        public enum variant { MR, LN }

        public VI_Aurora()
        {
            pwr_stat = 0; //Center
            model = "Aurora LN";
        }
        

        #region Weapons
        public void target_nearest_hostile()
        {
            Keyboard.KeyDown(Keys.R);
            Keyboard.KeyUp(Keys.R);
        }
        public void target_next_hostile()
        {
            Keyboard.KeyDown(Keys.T);
            Keyboard.KeyUp(Keys.T);
        }
        public void target_pin()
        {
            Keyboard.KeyDown(Keys.G);
            Keyboard.KeyUp(Keys.G);
        }
        public void missle_lock()
        {
            Mouse.ButtonDown(Mouse.MouseKeys.Middle); 
            Mouse.ButtonUp(Mouse.MouseKeys.Middle);
        }
        public void missle_fire()
        {
            Mouse.ButtonDown(Mouse.MouseKeys.Middle);
            Mouse.ButtonUp(Mouse.MouseKeys.Middle);
        }
        #endregion

        #region Power Distribution
        public void nav_pwr()
        {
            // F1 -> F3
            Keyboard.KeyDown(Keys.F1);
            Keyboard.KeyUp(Keys.F1);
            Thread.Sleep(300);
            Keyboard.KeyDown(Keys.F3);
            Keyboard.KeyUp(Keys.F3);
            Thread.Sleep(300);
        }
        public void select_pwr()
        {
            nav_pwr();
            // Cycle
            for (int i = 0; i < 4; i++)
            {
                Keyboard.KeyDown(Keys.NumPad7);
                Keyboard.KeyUp(Keys.NumPad7);
            }
            // Select
            Keyboard.KeyDown(Keys.NumPad5);
            Keyboard.KeyUp(Keys.NumPad5);
        }
        public void move_distribution(int x, int y)
        {
            // Y Moves First
            Keys x_key, y_key;
            if (x < 0){
                x_key = Keys.NumPad4;
            }
            else {
                x_key = Keys.NumPad6;
            }
            if (y < 0){
                y_key = Keys.NumPad2;
            }
            else{
                y_key = Keys.NumPad8;
            }
            int xax = Math.Abs(x);
            int yax = Math.Abs(y);
            for (int i = 0; i < yax; i++)
            {
                Keyboard.KeyDown(y_key);
                Keyboard.KeyUp(y_key);
            }
            for (int i = 0; i < xax; i++)
            {
                Keyboard.KeyDown(x_key);
                Keyboard.KeyUp(x_key);
            } 
        }
        public void pwr_center()
        {
            select_pwr();
            move_distribution(0, -35);
            move_distribution(0, 16);
            Thread.Sleep(200);
            nav_pwr();
            pwr_stat = 0;
        }

        public void max_pwr_g1()
        {
            select_pwr();
            move_distribution(0,-35);
            move_distribution(-15, 25);
            Thread.Sleep(200);
            nav_pwr();
            pwr_stat = 1;
        }
        public void max_pwr_g2()
        {
            select_pwr();
            move_distribution(0, -35);
            move_distribution(15, 25);
            Thread.Sleep(200);
            nav_pwr();
            pwr_stat = 2;
        }
        public void max_pwr_g3()
        {
            select_pwr();
            move_distribution(0, -35);
            Thread.Sleep(200);
            nav_pwr();
            pwr_stat = 3;
        }
        #endregion
    }
}
