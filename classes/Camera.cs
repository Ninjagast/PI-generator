using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pi_generator.classes
{
    public class Camera
    {
        private Rectangle _bounds;
        private Vector2 _pos;
        public Matrix Transform;

        private int MovementSpeed { get; set; } = 20;
        private float Zoom { get; set; } = 0.6f;
        
        public Camera(Viewport viewport)
        {
            _bounds = viewport.Bounds;
            _pos = new Vector2(665,0);
        }

        private void _updateMatrix()
        {
            Transform = Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                        Matrix.CreateScale(Zoom) *
                        Matrix.CreateTranslation(new Vector3(_bounds.Width * 0.5f, _bounds.Height * 0.5f, 0));
        }

        private void _moveCamera(Vector2 movePosition)
        {
            Vector2 newPosition = _pos + movePosition;

            _pos.X = newPosition.X;
            _pos.Y = newPosition.Y;
        }

        public void UpdateCamera(Viewport bounds)
        {
            _bounds = bounds.Bounds;
            _updateMatrix();
            Vector2 cameraMovement = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraMovement.Y = -MovementSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraMovement.Y = MovementSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraMovement.X = -MovementSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraMovement.X = MovementSpeed;
            }
            _moveCamera(cameraMovement);
        }
    }
}