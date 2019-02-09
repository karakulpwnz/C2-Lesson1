using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Объект - корабль
    /// </summary>
    class Ship : BaseObject
    {
        public static Bitmap ship;
        private static Random rnd;
        private int _energy = 100;
        public int Energy => _energy;
        public static event Message MessageDie;

        static Ship()
        {
            ship = new Bitmap(@"ship.png");
            rnd = new Random();
        }

        /// <summary>
        /// Урон кораблю
        /// </summary>
        /// <param name="n">Величина урона</param>
        public void EnergyLow(int n)
        {
            _energy -= n;
        }

        /// <summary>
        /// Восстановление здоровья
        /// </summary>
        /// <param name="n">Величина восстанавливаемого здоровья</param>
        public void EnergyUp(int n)
        {
            _energy += n;
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

        public override void Update()
        {

        }


        public override void Respawn()
        {
          
        }

        /// <summary>
        /// Движение вверх
        /// </summary>
        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        /// <summary>
        /// Движение вверх
        /// </summary>
        public void Down()
        {
            if (Pos.Y < Game.Height - ship.Height / 5 - Dir.Y) Pos.Y = Pos.Y + Dir.Y;
        }

        /// <summary>
        /// Смерть
        /// </summary>
        public void Die()
        {
            MessageDie?.Invoke();
        }
    }
}
