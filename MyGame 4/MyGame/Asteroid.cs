using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Объект - астероид
    /// </summary>
    class Asteroid: BaseObject
    {
        public int Power { get; set; }
        private static Bitmap asteroid;
        private static Random rnd;

        static Asteroid()
        {
            asteroid = new Bitmap(@"asteroid.png");
            rnd = new Random();
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Power = 1;
        }

        /// <summary>
        /// Отрисовка астероида
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Asteroid.asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление позиции астероида
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width + Size.Width;
            if (Pos.X > Game.Width + Size.Width) Pos.X = Size.Width;
            if (Pos.Y < 0 - Size.Height) Pos.Y = Game.Height + Size.Height;
            if (Pos.Y > Game.Height + Size.Height) Pos.Y = Size.Height;
        }

        /// <summary>
        /// Сброс позиции астероида в случае столкновения
        /// </summary>
        public override void Respawn()
        {
            Pos.X = Game.Width + Size.Width / 2;
            Pos.Y = rnd.Next(2 * Size.Height, Game.Height - 2 * Size.Height);
        }
    }
}
