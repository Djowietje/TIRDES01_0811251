using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    public class LaserHandler : Game1
    {
        SpriteBatch spriteBatch;
        int index;
        float laserSpeed;
        public Laser[] laserArray = new Laser[20];
        float screenWidth;
        float screenHeight;
        Texture2D laserTex;
        bool spotFound;
        public LaserHandler()
        {
            index = 0;
            laserSpeed = 5f;
            spotFound = false;
        }
        
        

        public void AddLaser(SpriteBatch spriteBatch, Texture2D laserTex, Vector2 startingPos, float direction, float screenWidth, float screenHeight)
        {
            this.spriteBatch = spriteBatch;
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            this.laserTex = laserTex;
            

            for (int i = 0; i < laserArray.Length; i++)
            {
                if (!spotFound)
                {
                    if(laserArray[i]==null || laserArray[i].active == false)
                    {
                        spotFound = true;
                        index = i;
                    }
                }
            }
            laserArray[index] = new Laser(startingPos, direction, laserSpeed);
            spotFound = false;
        }
        public bool DrawTheLaser()
        {
            
            
           
            foreach (Laser x in laserArray)
            {
                if (x != null && x.active)
                {
                    x.charPosition += x.directionVector * x.laserSpeed;
                    spriteBatch.Draw(laserTex, x.charPosition, null, Color.White, x.direction, new Vector2(350f, 350f), 0.1f, SpriteEffects.None, 0f);
                    if (x.charPosition.X > screenWidth | x.charPosition.X < 0 | x.charPosition.Y > screenHeight | x.charPosition.Y < 0)
                    {
                        x.active = false;
                    }
                }
            }
           

            return false;
        }

    }

}
