using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    static class Program
    {       
        static void Main()
        {
            Console.WriteLine("fsdgjsdfkljgjdfg");
            Console.ReadLine();
            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width - 1400,
                Height = Screen.PrimaryScreen.Bounds.Height - 400
            };
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}
