using Arcadia.GameObjects;
using Arcadia.GameObjects.Characters;
using Arcadia.GameObjects.Tiles;
using Arcadia.GameWorld;
using Arcadia.Graphics;
using Arcadia.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcadia
{
    public class Arcadia : Game
    {
        public static readonly int targetFPS = 240;

        /// <summary>
        /// Constructs the Arcadia game. Initializes the starting variables.
        /// </summary>
        public Arcadia()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(1000f / targetFPS);
        }

        /// <summary>
        /// The <c>Initialize</c> method is called after the constructor but before the main game loop.
        /// This is where you can query any required services and load any non-graphic related content.
        /// </summary>
        protected override void Initialize()
        {
            _screen = new Screen(_graphics, Window);
            _scene = new Scene(this, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            _camera = new Camera(_scene);

            base.Initialize();
        }

        /// <summary>
        /// The <c>LoadContent</c> method is used to load your game content. It is called only
        /// once per game, within the <c>Initialize</c> method, before the man game loop starts.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteManager.CreateInstance(this);

            _world = new World(69, 1000, 500);
            _world.AddCamera(_camera);

            // TODO: use this.Content to load your game content here
            _playerTexture = Content.Load<Texture2D>("test/playertest");
            _tileTexture = Content.Load<Texture2D>("test/tiletest");

            _player = new Player(_playerTexture, new Rectangle(0, 0, Grid.Size * 2, Grid.Size * 3), _world);
            _tile = new Dirt(_tileTexture, 0, 0);
            _world.Generate();

            _camera.Follow(_player);
        }

        /// <summary>
        /// The <c>Update</c> method is called multiple times per second, and it is used to update
        /// your game state (e.g. checking for collisions, gathering input, playing audio).
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyListener.Update();
            MouseListener.Update();
            _screen.Update();
            _player.Update(gameTime);
            _camera.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Similar to the <c>Update</c> method, the <c>Draw</c> method is also called multiple times
        /// per second. This, as the name suggests, is responsible for drawing content to the screen.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        protected override void Draw(GameTime gameTime)
        {
            _scene.EnableRenderTargeting();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteManager.Begin(_camera, false);
            _world.Draw(gameTime);
            _player.Draw(gameTime);
            SpriteManager.End();

            _scene.DisableRenderTargeting();
            _scene.Display(true);
            base.Draw(gameTime);
        }

        public static void Main()
        {
            using var game = new Arcadia();
            game.Run();
        }

        private GraphicsDeviceManager _graphics;

        private Screen _screen;

        private Scene _scene;
        private Camera _camera;

        private Player _player;
        private Tile _tile;

        private World _world;

        private Texture2D _playerTexture;
        private Texture2D _tileTexture;
    }
}