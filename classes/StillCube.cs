using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pi_generator.classes
{
    public class StillCube
    {
        private static StillCube _instance;
        private static readonly object Padlock = new object();
        private StillCube() {}
        public static StillCube Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new StillCube();
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
            spriteBatch.DrawString(_font, $"Mass: {_mass} kg", new Vector2((int)_position.X - 30, (int)_position.Y - 50), Color.Black);
            spriteBatch.DrawString(_font, $"Velocity of the white cube: {_velocity} m/s", new Vector2(0, -300), Color.Black);
        }

        public void Update()
        {
            //The moving cube moves to the left at first
            if (_position.X < 1)
            {
                _velocity = _velocity * -1;
                Counter.Instance.Hit();
            }
            if (!(_position.X + 100 > MovingCube.Instance.GetPosition().X && _velocity < 0.001))
            {
                _position.X -= _velocity;
            }
            else if (_position.X > 3000 && _velocity * -1 < 2500)
            {
                MovingCube.Instance.SetPosition(new Vector2(_position.X + _velocity * -3 + 120, _position.Y));
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

        public void SetVelocity(float velocity)
        {
            _velocity = velocity;
        }

        public int GetMass()
        {
            return _mass;
        }

        public float GetVelocity()
        {
            return _velocity;
        }
    }
}