using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pi_generator.classes;

namespace Pi_generator
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private Camera _camera;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 15d); //30 fps
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d); //60 fps
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //120 fps
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 120d); //240 fps
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 240); //480 fps
            // TargetElapsedTime = TimeSpan.FromSeconds(1d / 480); //960 fps
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 960); //1920 fps

            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _camera = new Camera(_graphics.GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _font = Content.Load<SpriteFont>("font");

            MovingCube.Instance.Init(100000000, new Vector2(500, 300), 1, _font, new[] {Color.Red}, GraphicsDevice);
            StillCube.Instance.Init(1, new Vector2(50,300), 0, _font, new[] {Color.White}, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _camera.UpdateCamera(_graphics.GraphicsDevice.Viewport);
            
            MovingCube.Instance.Update();
            StillCube.Instance.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, transformMatrix: _camera.Transform);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Counter.Instance.Draw(_spriteBatch, _font);
            
            StillCube.Instance.Draw(_spriteBatch);
            MovingCube.Instance.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
