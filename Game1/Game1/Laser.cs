using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Game1
{
    public class Laser
    {
        public Vector2 startingPos;
        public float direction;
        public Vector2 directionVector;
        public Vector2 charPosition;
        public float laserSpeed;
        public bool active;

       
       public Laser(Vector2 startPos, float direc, float laserSpeed)
        {
            
            this.startingPos = startPos;
            this.directionVector = new Vector2((float)Math.Cos(-0.5 * Math.PI - direc) * -1, (float)Math.Sin(-0.5 * Math.PI - direc));
            this.direction = direc + (float)(0.5 * Math.PI);
            this.charPosition = startPos;
            this.laserSpeed = laserSpeed;
            this.active = true;

        }
    }
}
