using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Content
{
    internal class Bullet
    {
        private float timer;


        private Texture2D _texture;
        private Rectangle _rect;
        private Vector2 _location;
        private Vector2 _direction;
        private float _angle;
        private float _speed;
        private int _size;
        private Random random;

        public Bullet(Texture2D texture, Vector2 location, Vector2 target, int size)
        {
            _size = size;
            _texture = texture;
            _location = location;
            _rect = new Rectangle(location.ToPoint(), new Point(_size, _size));
            _direction = target - location;
            _direction.Normalize();
            _speed = 3;
        }
        public Rectangle Rect
        {
            get { return _rect; }
        }

        public void Update()
        {
            _location += _direction * _speed;
            _rect.Location = _location.ToPoint();
            _angle = (float)Math.Atan2(_direction.Y, _direction.X) + 1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(_rect.Center, _rect.Size), null, Color.White, (float)Math.Atan2(_direction.Y, _direction.X), new Vector2(_texture.Width / 2, _texture.Height / 2), SpriteEffects.None, 1f);

        }
        public bool Collide(Rectangle item)
        {
            return _rect.Intersects(item);
        }

        public Boolean Contains(Rectangle item)
        {
            return _rect.Contains(item);
        }

    }
}
