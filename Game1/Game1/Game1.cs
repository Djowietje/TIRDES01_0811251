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
        Texture2D laserTex;
        float charSpeed;
        Vector2 charPosition;
        Vector2 charDirection;
        float charLookAngle;
        float deltaCharLookAngle;
        float charScale;
        float charCurrentMovSpeed;
        float charRotationSpeed;
        float deltaTime;
        Vector2 momentumForce;
        Vector2 spaceshipForce;
        float throttle;
        SpriteFont font;
        string fpsText;
        Vector2 fpsPos;
        Color fpsColor;
        int flames;
        Laser lasers;


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
            laserTex = Content.Load<Texture2D>("blueLaserAlpha.png");
            charPosition = new Vector2(200.0f, 300.0f);
            momentumForce = new Vector2(0.0f, 0.0f);
            spaceshipForce = new Vector2(0.0f, 0.0f);
            charSpeed = 100f;
            charScale = 0.1f;
            charLookAngle = 0f;
            charRotationSpeed = 5f;
            font = Content.Load<SpriteFont>("MyFont");
            fpsText = "";
            fpsColor = Color.DarkRed;
            flames = 0;
            charCurrentMovSpeed = 0;
            lasers = new Laser();
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
            spaceshipForce = Vector2.Zero;
            deltaCharLookAngle = 0f;
            CheckForKeyPresses(Keyboard.GetState());
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fpsPos= new Vector2(graphics.GraphicsDevice.Viewport.Width-100, graphics.GraphicsDevice.Viewport.Height-50);

            fpsText = "FPS: " + Math.Round( 1 / deltaTime) +" \nThrottle:"+throttle+"\nSpeed:"+Math.Round(charCurrentMovSpeed);

            if (spaceshipForce.X < momentumForce.X) momentumForce.X-=throttle/10000;
            else if (spaceshipForce.X > momentumForce.X) momentumForce.X += throttle / 10000;
            if (spaceshipForce.Y < momentumForce.Y) momentumForce.Y -= throttle / 10000;
            else if (spaceshipForce.Y > momentumForce.Y) momentumForce.Y += throttle / 10000;


            charLookAngle += (deltaCharLookAngle * deltaTime * charRotationSpeed );
            charPosition += (momentumForce * deltaTime * charSpeed);
            //Console.WriteLine("SpaceshipForce ="+spaceshipForce + "  \n MomentumForce ="+momentumForce);
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
                if(throttle<100) throttle += 1;
                spaceshipForce = new Vector2((float)Math.Cos(-0.5*Math.PI-charLookAngle)*-1 , (float)Math.Sin(-0.5 * Math.PI - charLookAngle));
    
            }
            else { if (throttle > 0) throttle -= 1; flames = 0; }
           
            if (ks.IsKeyDown(Keys.S))
            {
                flames = 2;
                if (throttle > -100) throttle -= 1;
                spaceshipForce = new Vector2((float)Math.Cos(-0.5 * Math.PI - charLookAngle)*-1, (float)Math.Sin(-0.5 * Math.PI - charLookAngle));
            }

            if (ks.IsKeyDown(Keys.Space))
            {
                //Fire Laser at current look angle
                lasers.AddLaser(spriteBatch, laserTex, charPosition, charLookAngle, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            }

        }

        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            if (lasers != null)
            {
                lasers.DrawTheLaser();
            }
            spriteBatch.Draw(spaceship[flames], charPosition, null, Color.White, charLookAngle, new Vector2(350f,350f), charScale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, fpsText, fpsPos, fpsColor, 0f, Vector2.Zero, 0.7f, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}