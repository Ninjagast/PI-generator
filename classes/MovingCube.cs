using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pi_generator.classes
{
    public class MovingCube
    {
        private static MovingCube _instance;
        private static readonly object Padlock = new object();
        private MovingCube() {}
        public static MovingCube Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new MovingCube();
                    }
                    return _instance;
                }
            }
        }
        
        private int _mass; //kg
        private float _velocity;
        private Vector2 _position;
        private SpriteFont _font;
        private Texture2D _texture;

        public void Init(int mass, Vector2 position, float velocity, SpriteFont font, Color[] color, GraphicsDevice graphicsDevice)
        {
            _mass = mass;
            _position = position;
            _velocity = velocity;
            _font = font;
            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(color);
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            //ToDo I'll lose a lot of precision here because the world is mapped in pixels
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, 100, 100), Color.White);
            spriteBatch.DrawString(_font, $"Mass: {_mass} kg", new Vector2((int)_position.X - 50, (int)_position.Y - 50), Color.Black);
            spriteBatch.DrawString(_font, $"Velocity of the red cube: {_velocity} m/s", new Vector2(0, -350), Color.Black);
        }
        
        public void Update()
        {
            //The moving cube moves to the left at first
            if (_position.X < StillCube.Instance.GetPosition().X + 100)
            {
                Counter.Instance.Hit();
                float u1 = _velocity;
                float m1 = _mass;
                float u2 = StillCube.Instance.GetVelocity();
                float m2 = StillCube.Instance.GetMass();

                _velocity = ((m1 - m2) / (m1 + m2) * u1) + ((m2 * 2) / (m1 + m2) * u2);
                StillCube.Instance.SetVelocity(((m1 * 2) / (m1 + m2) * u1) + ((m2 - m1) / (m1 + m2) * u2));

                if (_velocity < 0)
                {
                    _position.X -= _velocity;
                }
                
            }
            else
            {
                if (_position.X > 100)
                {
                    _position.X -= _velocity;
                }
            }
            
        }

        public float GetMomentum()
        {
            return _mass * _velocity;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public void SetPosition(Vector2 vector2)
        {
            _position = vector2;
        }
    }
}