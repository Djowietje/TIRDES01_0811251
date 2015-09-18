using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

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
        Texture2D[] spaceship;

        Vector2 charPosition;
        Vector2 charDirection;
        float charLookAngle;
        float lastCharLookAngle;
        float deltaCharLookAngle;
        float charScale;
        float charCurrentMovSpeed;
        float maxMovementSpeed;
        float accelaration;
        float charRotationSpeed;
        float throttle;
        float friction;
        SpriteFont font;
        string fpsText;
        Vector2 fpsPos;
        Color fpsColor;
        int flames;


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
            spaceship = new Texture2D[3];
            spaceship[0] = Content.Load<Texture2D>("spaceship.png");
            spaceship[1] = Content.Load<Texture2D>("spaceship2.png");
            spaceship[2] = Content.Load<Texture2D>("spaceship3.png");
            charPosition = new Vector2(200.0f, 300.0f);
            charScale = 0.05f;
            accelaration = 2f;
            maxMovementSpeed = 200f;
            charLookAngle = 0f;
            charRotationSpeed = 5f;
            friction = 0.1f;
            font = Content.Load<SpriteFont>("MyFont");
            fpsText = "";
            fpsColor = Color.DarkRed;
            flames = 0;
            charCurrentMovSpeed = 0;
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
            deltaCharLookAngle = 0f;
            CheckForKeyPresses(Keyboard.GetState());
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fpsPos= new Vector2(graphics.GraphicsDevice.Viewport.Width-100, graphics.GraphicsDevice.Viewport.Height-50);
            fpsText = "FPS: " + Math.Round( 1 / deltaTime) +" \nThrottle:"+throttle+"\nSpeed:"+Math.Round(charCurrentMovSpeed);

            if(charCurrentMovSpeed < maxMovementSpeed) charCurrentMovSpeed += throttle / 100f * accelaration;
            if (throttle == 0 && charCurrentMovSpeed > 0) charCurrentMovSpeed -= 1f;
            if (throttle == 0 && charCurrentMovSpeed < 0) charCurrentMovSpeed += 1f;

            charPosition += (charDirection * deltaTime * charCurrentMovSpeed);
            charLookAngle += (deltaCharLookAngle * deltaTime * charRotationSpeed);



         
            base.Update(gameTime);
        }

        protected void CheckForKeyPresses(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (ks.IsKeyDown(Keys.A))
            {
                deltaCharLookAngle = -1f;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                deltaCharLookAngle = 1f;
            }
            if (ks.IsKeyDown(Keys.W))
            {
                flames = 1;
                if (throttle < 100 ) { throttle += 1; }
                charDirection = new Vector2((float)Math.Cos(-0.5*Math.PI-charLookAngle)*-1 , (float)Math.Sin(-0.5 * Math.PI - charLookAngle));
    
            }
           
            else if (ks.IsKeyDown(Keys.S))
            {
                flames = 2;
                if (throttle > -100 ) { throttle -= 1; }
                charDirection = new Vector2((float)Math.Cos(-0.5 * Math.PI - charLookAngle)*-1, (float)Math.Sin(-0.5 * Math.PI - charLookAngle));
             
                lastCharLookAngle = charLookAngle;
            }
            else { throttle = 0; flames = 0; }
            // else { throttle = 0; }



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

            spriteBatch.Draw(spaceship[flames], charPosition, null, Color.White, charLookAngle, new Vector2(350f,350f), charScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, fpsText, fpsPos, fpsColor, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}