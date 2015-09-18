using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GamePadState gp1;

        Texture2D charTexture;
        Vector2 charPosition;
        Vector2 charDirection;
        float charScale;
        float charSpeed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            charTexture = Content.Load<Texture2D>("spaceship.png");
            charPosition = new Vector2(200.0f, 300.0f);
            charScale = 0.05f;
            charSpeed = 100f;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            charDirection = Vector2.Zero;
            CheckForKeyPresses(Keyboard.GetState());
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            charDirection *= charSpeed;
            charPosition += (charDirection * deltaTime);

            base.Update(gameTime);
        }

        public void CheckForKeyPresses(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (ks.IsKeyDown(Keys.A))
            {
                charDirection += new Vector2(-1.0f, 0.0f);
            }
            if (ks.IsKeyDown(Keys.D))
            {
                charDirection += new Vector2(1.0f, 0.0f);
            }
            if (ks.IsKeyDown(Keys.W))
            {
                charDirection += new Vector2(0.0f, -1.0f);
            }
            if (ks.IsKeyDown(Keys.S))
            {
                charDirection += new Vector2(0.0f, 1.0f);
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            spriteBatch.Draw(charTexture, charPosition, null, Color.White, 0f, Vector2.Zero, charScale, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}