using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    /// <summary>
    /// Объект - звезда
    /// </summary>
    class Star: BaseObject
    {
        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Star(Point pos, Point dir, Size size) :base(pos,dir,size)
        {

        }

        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Обновление позиции объекта
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width; 
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
