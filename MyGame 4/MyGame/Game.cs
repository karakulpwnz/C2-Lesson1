using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace MyGame
{
    public delegate string Log(int n);
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
        private static Timer timer;
        private static Ship _ship = new Ship(new Point(10, 800), new Point(20, 20), new Size(Ship.ship.Width / 5, Ship.ship.Height / 5));
        private static int score;
        private static int damage;
        private static int heal;
        public static BaseObject[] _objs;
        private static Heal[] _heals;
        private static List<string> logs;
        private static List<Bullet> _bullets;
        private static int AsteroidsCount;

        static Game()
        {
            rnd = new Random();
            timer = new Timer { Interval = 25 };
            score = 0;
            logs = new List<string>();
            AsteroidsCount = 30;
            _asteroids = new List<Asteroid>();
            _bullets = new List<Bullet>();
        }

        /// <summary>
        /// Запись лога урона
        /// </summary>
        /// <param name="n">Величина урона</param>
        /// <returns></returns>
        private static string LogGetDamage(int n)
        {
            String log = "Получен урон " + n;
            return log;
        }

        /// <summary>
        /// Запись лога лечения
        /// </summary>
        /// <param name="n">Величина лечения</param>
        /// <returns></returns>
        private static string LogHeal(int n)
        {
            String log = "Восстновлено " + n + " здоровья";
            return log;
        }

        /// <summary>
        /// Запись лога взрыва астероида
        /// </summary>
        /// <param name="n">Количество очков</param>
        /// <returns></returns>
        private static string LogKill(int n)
        {
            String log = "Взорван астероид. Получено " + n + " очко";
            return log;
        }

        /// <summary>
        /// Метод вывода лога через делегат
        /// </summary>
        /// <param name="log">Делегат - строка</param>
        /// <param name="n">Числовой параметр строки</param>
        public static void PrintLog(Log log, int n)
        {
            Console.WriteLine(log(n));
            logs.Add(log(n));
        }

        /// <summary>
        /// Считывание нажатия клавиши
        /// </summary>
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) _bullets.Add(new Bullet(new Point(_ship.Rect.X + Ship.ship.Width / 5, _ship.Rect.Y + Ship.ship.Height / 10), new Point(400, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) _ship.Up();
            if (e.KeyCode == Keys.Down) _ship.Down();
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
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
            timer.Start();
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Таймер
        /// </summary>
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
            foreach (Asteroid a in _asteroids)
                a?.Draw();
            foreach (Heal heal in _heals)
                heal.Draw();
            for (var i = 0; i < _bullets.Count; i++)
            {
                if(_bullets[i] != null) _bullets[i].Draw();
            }
            _ship?.Draw();
            if (_ship != null) Buffer.Graphics.DrawString("Energy:" + _ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            Buffer.Graphics.DrawString("Score: " + score, SystemFonts.DefaultFont, Brushes.White, 0, 30);
            Buffer.Render();
        }
        /// <summary>
        /// Обновление позиции объектов
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();

            foreach (Heal heal in _heals)
                heal.Update();
            for (var i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i] != null)
                {
                    _bullets[i].Update();
                    if (_bullets[i].Position() == false)
                    {
                        _bullets.RemoveRange(i, 1);
                        i--;
                    }
                }
            }
                

            for (var i = 0; i < _asteroids.Count; i++)
            {
                if (_asteroids[i] == null) continue;
                _asteroids[i].Update();
                for (var j = 0; j < _bullets.Count; j++)
                {
                    if (_asteroids[i] != null)
                    {
                        if (_bullets[j] != null && _bullets[j].Collision(_asteroids[i]))
                        {
                            System.Media.SystemSounds.Hand.Play();
                            PrintLog(LogKill, _asteroids[i].Power);
                            score = score + _asteroids[i].Power;
                            if (_asteroids.Count == 0)
                            {
                                AsteroidsCount++;
                                AsteroidsCreate(AsteroidsCount);
                            }
                            _bullets[j] = null;
                            _asteroids[i] = null;
                            continue;
                        }
                    }
                }
                _bullets.RemoveAll(Bullet => Bullet == null);
                if (_asteroids[i] != null)
                {
                    if (!_ship.Collision(_asteroids[i])) continue;
                    damage = rnd.Next(1, 10);
                    _ship?.EnergyLow(damage);
                    System.Media.SystemSounds.Asterisk.Play();
                    PrintLog(LogGetDamage, damage);
                    _asteroids[i] = null;
                }
                if (_asteroids.Count == 0)
                {
                    AsteroidsCount += 10;
                    AsteroidsCreate(AsteroidsCount);
                }
                if (_ship.Energy <= 0) _ship.Die();
            }
            _asteroids.RemoveAll(Asteroid => Asteroid == null);

            if (_asteroids.Count == 0)
            {
                AsteroidsCount++;
                AsteroidsCreate(AsteroidsCount);
            }

            for (var i = 0; i < _heals.Length; i++)
            {
                if (_heals[i] == null) continue;
                _heals[i].Update();
                if (!_ship.Collision(_heals[i])) continue;
                heal = rnd.Next(10, 20);
                _ship?.EnergyUp(heal);
                System.Media.SystemSounds.Exclamation.Play();
                PrintLog(LogHeal, heal);
                _heals[i].Respawn();
                if (_ship.Energy <= 0) _ship.Die();
            }
        }

        private static List<Asteroid> _asteroids;

        /// <summary>
        /// Создание астероидов
        /// </summary>
        /// <param name="n">Кол-во астероидов</param>
        private static void AsteroidsCreate(int n)
        {
            for (int i = 0; i < n; i++)
            {
                int sz;
                int dr;
                int psw;
                int psh;
                sz = rnd.Next(20, 40);
                dr = rnd.Next(-10, -5);
                while (dr == 0) dr = rnd.Next(-10, 10);
                psw = rnd.Next(800, Game.Width - sz);
                psh = rnd.Next(1, Game.Height - sz);
                _asteroids.Add(new Asteroid(new Point(psw, psh), new Point(dr, dr), new Size(sz, sz)));
            }
        }



        /// <summary>
        /// Загрузка объектов
        /// </summary>
        public static void Load()
        {
            _objs = new BaseObject[14];
            _heals = new Heal[3];
            int sz;
            int drx;
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

            AsteroidsCreate(AsteroidsCount);

            for (int i = 0; i < _heals.Length; i++)
            {
                psh = rnd.Next(1, Game.Height - 30);
                psw = rnd.Next(Game.Width, Game.Width * 2);
                _heals[i] = new Heal(new Point(psw, psh), new Point(-5, 0), new Size(30, 30));
            }
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The end\n Your score " + score, new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
            StreamWriter SW = new StreamWriter(new FileStream(@"logs.txt", FileMode.Create, FileAccess.Write));
            foreach (string a in logs)
            {
                SW.Write(a + "\n");
            }
            SW.Write("-----------------------------\nНАБРАНО ОЧКОВ " + score);
            SW.Close();
        }
    }
}
