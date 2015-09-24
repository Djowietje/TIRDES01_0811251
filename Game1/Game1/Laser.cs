using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    public class Laser : Game1
    {
        SpriteBatch spriteBatch;
        int laserNumber;
        Vector2 startingPos;
        float direction;
        Vector2 directionVector;
        Texture2D laserTex;
        Vector2 charPosition;
        float laserSpeed;
        Laser[] lasers = new Laser[20];
        bool active = true;
        float screenWidth;
        float screenHeight;

        public Laser()
        {
            laserNumber = 0;
          
        }

        public void AddLaser(SpriteBatch spriteBatch, Texture2D laserTex, Vector2 startingPos, float direction, float screenWidth, float screenHeight)
        {
            this.spriteBatch = spriteBatch;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;

            lasers[laserNumber] = new Laser();

            lasers[laserNumber].startingPos = startingPos;

            lasers[laserNumber].directionVector = new Vector2((float)Math.Cos(-0.5 * Math.PI - direction) * -1, (float)Math.Sin(-0.5 * Math.PI - direction));
            lasers[laserNumber].direction = direction + (float)(0.5 * Math.PI);
            lasers[laserNumber].charPosition = startingPos;
            this.laserTex = laserTex;
            lasers[laserNumber].laserSpeed = 5f;
            laserNumber++;
        }
        public bool DrawTheLaser()
        {
            
            
           
            foreach (Laser x in lasers)
            {
                if (x != null && x.active)
                {
                    x.charPosition += x.directionVector * x.laserSpeed;
                    spriteBatch.Draw(laserTex, x.charPosition, null, Color.White, x.direction, new Vector2(350f, 350f), 0.1f, SpriteEffects.None, 0f);
                    if (x.charPosition.X > screenWidth | x.charPosition.X < 0 | x.charPosition.Y > screenHeight | x.charPosition.Y < 0)
                    {
                        x.active = false;
                        laserNumber--;
                    }
                }
            }
           

            return false;
        }

    }

}
