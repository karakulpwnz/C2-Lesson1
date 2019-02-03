using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Объект - планета
    /// </summary>
    class Planet: BaseObject
    {
        private static Bitmap planet;

        static Planet()
        {
            planet = new Bitmap(@"planet.png");
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Planet(Point pos, Point dir, Size size) :base(pos,dir,size)
        {

        }
        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(planet, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        /// <summary>
        /// Обновление позиции объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width + Size.Width; 
            if (Pos.X > Game.Width + Size.Width) Pos.X = Size.Width;
        }
        /// <summary>
        /// Сброс позиции в случае столкновения
        /// </summary>
        public override void Respawn()
        {

        }
    }
}
