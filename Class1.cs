using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    public class Button
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Color _color;
        private bool _isClicked;

        public Button(Texture2D texture, Rectangle rectangle)
        {
            _texture = texture;
            _rectangle = rectangle;
            _color = Color.White;
            _isClicked = false;
        }

        public Rectangle Bounds { get { return _rectangle; } }

        public bool IsClicked(MouseState mousestate, MouseState prevmousestate)
        {
            return _rectangle.Contains(mousestate.Position) && mousestate.LeftButton == ButtonState.Pressed && prevmousestate.LeftButton == ButtonState.Released;
        }
        public void Update(MouseState mousestate, MouseState prevMouseState)
        {
            if (_rectangle.Contains(mousestate.Position))
            {
                _color = Color.Gray;
            }
            else
                _color = Color.White;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _rectangle, _color);
        }
    }

    
}
