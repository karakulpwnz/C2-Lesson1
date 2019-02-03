using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    /// <summary>
    /// Класс с механикой игры
    /// </summary>
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        private static Random rnd;

        static Game()
        {
            rnd = new Random();
        }
        /// <summary>
        /// Инициация игры
        /// </summary>
        /// <param name="form">Форма</param>
        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Game.Load();
            Timer timer = new Timer { Interval = 25 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Отрисовка объектов
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            foreach (Asteroid obj in _asteroids)
                obj.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// Обновление позиции объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
            foreach (Asteroid a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    System.Media.SystemSounds.Hand.Play();
                    _bullet.Respawn();
                    a.Respawn();
                }
            }
            _bullet.Update();
        }

        public static BaseObject[] _objs;
        private static Bullet _bullet;
        private static Asteroid[] _asteroids;
        /// <summary>
        /// Загрузка объектов
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[14];
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            _asteroids = new Asteroid[14];
            int sz;
            int drx;
            int dry;
            int psw;
            int psh;
            for (int i = 0; i < _objs.Length; i++)
            {
                sz = rnd.Next(1, 3);
                psw = rnd.Next(1, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                drx = sz;
                _objs[i] = new Star(new Point(psw, psh), new Point(drx, 0), new Size(sz, sz));
            }
            //for (int i = (_objs.Length / 2) - 4; i < (_objs.Length / 2) - 1; i++)
            //{
            //    sz = rnd.Next(10, 50);
            //    psw = rnd.Next(1, Game.Width - sz);
            //    psh = rnd.Next(1, Game.Height - sz);
            //    drx = rnd.Next(sz / 10, 5);
            //    _objs[i] = new Planet(new Point(psw, psh), new Point(drx, 0), new Size(sz, sz));
            //}

            for (int i = 0; i < _asteroids.Length; i++)
            {
                sz = rnd.Next(20, 30);
                drx = rnd.Next(-10, -5);
                dry = rnd.Next(-10, 10);
                while(dry == 0) dry = rnd.Next(-10, 10);
                psw = rnd.Next(1, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                _asteroids[i] = new Asteroid(new Point(psw, psh), new Point(drx, dry), new Size(sz, sz));
            }

            //for (int i = _objs.Length - 1; i < _objs.Length; i++)
            //{
            //    sz = rnd.Next(10, 50);
            //    psh = rnd.Next(1, Game.Height - sz);
            //    drx = rnd.Next(20, 40);
            //    _objs[i] = new Ship(new Point(0 - 4 * Game.Width, psh), new Point(drx, 0), new Size(sz, sz));
            //}
        }
    }
}
