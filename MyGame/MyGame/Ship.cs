using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Корабль
    /// </summary>
    class Ship: BaseObject
    {
        private static Bitmap ship;
        private static Random rnd;


        static Ship()
        {
            ship = new Bitmap(@"ship.png");
            rnd = new Random();
        }


        public Ship(Point pos, Point dir, Size size) :base(pos,dir,size)
        {

        }
        public override void Draw()
        {
            //размеры делим на 5, так как исходная картинка слишком большая
            Game.Buffer.Graphics.DrawImage(ship, Pos.X, Pos.Y, ship.Width/5, ship.Height/5);
        }
        public override void Update()
        {
            //корабль появляется с некоей периодичностью. для этого увеличил его путь, задав ограничение в 4 ширины экрана
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width * 4)
            {
                Pos.X = 0 - (Game.Width * 4);
                Pos.Y = rnd.Next(ship.Height/5 + 10, Game.Height - ship.Height/5 - 10);
            }
        }
    }
}
