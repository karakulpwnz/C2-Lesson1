using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace MyGame
{
    /// <summary>
    /// Объект - пуля
    /// </summary>
    class Bullet : BaseObject
    {
        private static Random rnd;
        private static bool res;

        static Bullet()
        {
            rnd = new Random();
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawRectangle(Pens.OrangeRed, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление позиции пули
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + 20;
        }

        public bool Position()
        {
            res = true;
            if (Pos.X > Game.Width + Size.Width)
            {
                Pos.X = Size.Width;
                res = false;
            }
            return res;

        }

        /// <summary>
        /// Сброс позиции пули в случае столкновения
        /// </summary>
        public override void Respawn()
        {
            Pos.X = 0;
            Pos.Y = -20;
        }
    }
}
