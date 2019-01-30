using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Планета
    /// </summary>
    class Planet: BaseObject
    {
        private static Bitmap planet;

        static Planet()
        {
            planet = new Bitmap(@"planet.png");
        }


        public Planet(Point pos, Point dir, Size size) :base(pos,dir,size)
        {

        }
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(planet, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width + Size.Width; 
            if (Pos.X > Game.Width + Size.Width) Pos.X = Size.Width;
        }
    }
}
