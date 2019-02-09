using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Объект - аптечка
    /// </summary>
    class Heal : BaseObject
    {
        public int Power { get; set; }
        private static Bitmap heal;
        private static Random rnd;

        static Heal()
        {
            heal = new Bitmap(@"aidkit.png");
            rnd = new Random();
        }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="pos">Позиция</param>
        /// <param name="dir">Направление</param>
        /// <param name="size">Размер</param>
        public Heal(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Отрисовка аптечка
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Heal.heal, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Обновление позиции аптечки
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0 - Size.Width) Pos.X = Game.Width;
        }

        /// <summary>
        /// Возрождение аптечки
        /// </summary>
        public override void Respawn()
        {
            Pos.X = Game.Width;
            Pos.Y = rnd.Next(1, Game.Height - 30);
        }
    }
}
