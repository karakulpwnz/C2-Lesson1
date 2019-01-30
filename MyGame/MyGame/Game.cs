using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyGame
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        //properties
        //game field's width and height
        public static int Width { get; set; }
        public static int Height { get; set; }
        private static Random rnd;

        static Game()
        {
            rnd = new Random();
        }

        public static void Init(Form form)
        {
            //graphic device for graphic output
            Graphics g;
            //give access to main buffer of graphic context for current application
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            //create object (drawing surface) and connect it with form
            //remember form sizes
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            //connect buffer in memory with graphic object for drawing in buffer
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Game.Load();
            Timer timer = new Timer { Interval = 32 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

        public static BaseObject[] _objs;

        public static void Load()
        {
            _objs = new BaseObject[30];
            int sz;
            int drx;
            int dry;
            int psw;
            int psh;
            //рисуем звезды
            for (int i = 0; i < _objs.Length / 2; i++)
            {
                sz = rnd.Next(1, 3);
                psw = rnd.Next(1, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                drx = sz;
                _objs[i] = new Star(new Point(psw, psh), new Point(drx, 0), new Size(sz, sz));
            }
            //рисуем планеты
            for (int i = (_objs.Length / 2) - 4; i < (_objs.Length / 2) - 1; i++)
            {
                sz = rnd.Next(10, 50);
                psw = rnd.Next(1, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                drx = rnd.Next(sz / 10, 5);
                _objs[i] = new Planet(new Point(psw, psh), new Point(drx, 0), new Size(sz, sz));
            }
            //рисуем астероиды
            for (int i = (_objs.Length / 2) - 1; i < _objs.Length - 1; i++)
            {
                sz = rnd.Next(6, 20);
                drx = rnd.Next(-10, -5);
                dry = rnd.Next(-10, 10);
                while(dry == 0) dry = rnd.Next(-10, 10);
                psw = rnd.Next(1, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                _objs[i] = new BaseObject(new Point(psw, psh), new Point(drx, dry), new Size(sz, sz));
            }
            //рисуем корабль
            for (int i = _objs.Length - 1; i < _objs.Length; i++)
            {
                sz = rnd.Next(10, 50);
                psh = rnd.Next(1, Game.Height - sz);
                drx = rnd.Next(20, 40);
                _objs[i] = new Ship(new Point(0 - 4 * Game.Width, psh), new Point(drx, 0), new Size(sz, sz));
            }
        }
    }
}
