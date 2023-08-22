using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pi_generator.classes
{
    public class Counter
    {
        private static Counter _instance;
        private static readonly object Padlock = new object();
        private Counter() {}
        public static Counter Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Counter();
                    }
                    return _instance;
                }
            }
        }

        private int _collisionCount = 0;

        public void Hit()
        {
            _collisionCount += 1;
        }
        
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Collisions: " + _collisionCount.ToString(), new Vector2(1000, -350), Color.Black);
        }

        public int GetCount()
        {
            return _collisionCount;
        }
    }
}