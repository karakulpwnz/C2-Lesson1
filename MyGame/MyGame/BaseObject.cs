using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;
        private static Bitmap asteroid;

        static BaseObject()
        {
            asteroid = new Bitmap(@"asteroid.png");
        }

        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawImage(BaseObject.asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width + Size.Width) Pos.X = Size.Width;
            if (Pos.Y < 0) Pos.Y = Game.Height + Size.Height;
            if (Pos.Y > Game.Height + Size.Height) Pos.Y = Size.Height;
        }
    }
}
