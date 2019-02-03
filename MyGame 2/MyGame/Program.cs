using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    static class Program
    {
        static void Main()
        {
            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width - 200,
                Height = Screen.PrimaryScreen.Bounds.Height - 200
            };
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
