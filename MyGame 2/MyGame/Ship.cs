using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Объект - корабль
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

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Ship(Point pos, Point dir, Size size) :base(pos,dir,size)
        {

        }
        /// <summary>
        /// Отрисовка корабля
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(ship, Pos.X, Pos.Y, ship.Width/5, ship.Height/5);
        }
        /// <summary>
        /// Обновление позиции корабля
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X > Game.Width * 4)
            {
                Pos.X = 0 - (Game.Width * 4);
                Pos.Y = rnd.Next(ship.Height/5 + 10, Game.Height - ship.Height/5 - 10);
            }
        }
        /// <summary>
        /// Сброс позиции в случае столкновения
        /// </summary>
        public override void Respawn()
        {
          
        }
    }
}
