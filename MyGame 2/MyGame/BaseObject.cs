using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyGame
{
    /// <summary>
    /// Базовый объект
    /// </summary>
    abstract class BaseObject : ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        static BaseObject()
        {

        }

        protected BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        /// <summary>
        /// Отрисовка объекта
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Обновление позиции объекта
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// Сброс позиции в случае столкновения
        /// </summary>
        public abstract void Respawn();

        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);
        public Rectangle Rect => new Rectangle(Pos, Size);
    }
}
