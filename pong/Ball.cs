using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace pong
{
    public class Ball
    {
        private Texture2D bal;
        private Rectangle ballRect;
        private Viewport view;

        public Rectangle BallRect
        {
            get { return ballRect; }
        }

        private Vector2 pos;

        public Vector2 Pos
        {
            get { return pos; }
        }

        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        
        public Ball(Viewport view)
        {
            this.view = view;
            pos = new Vector2(view.Width / 2, view.Height / 2);
        }

        public void loadContent(Texture2D tex)
        {
            bal = tex;
            ballRect = new Rectangle(view.Width / 2, view.Height / 2, tex.Width, tex.Height);

            velocity = new Vector2(4.0f, 4.0f);
        }

        public void update(Paddle player, Paddle cpu)
        {
            if(pos.X < 0 || pos.X + bal.Width > view.Width)
            {
                reset();
            }

            if(pos.Y < 0 || pos.Y + bal.Height > view.Height)
            {
                velocity.Y *= -1;
            }

            checkCollision(player);
            checkCollision(cpu);

            pos += velocity;
            
            ballRect.X += (int)velocity.X;
            ballRect.Y += (int)velocity.Y;
        }

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(bal, pos, Color.White);
        }

        private void reset()
        {
            pos = new Vector2(view.Width / 2, view.Height / 2);
            ballRect.X = view.Width / 2;
            ballRect.Y = view.Height / 2;

            Random rndX = new Random(DateTime.Now.Millisecond);
            Random rndY = new Random(DateTime.Now.Millisecond);

            if (rndX.Next(0, 2) == 0)
                velocity.X = -4;
            else
                velocity.X = 4;

            if (rndY.Next(0, 2) == 0)
                velocity.Y = -4;
            else
                velocity.Y = 4;
        }

        private void checkCollision(Paddle paddle)
        {
            if (ballRect.Intersects(paddle.PaddleRect))
            {
                if (pos.Y < paddle.PaddleRect.Top && pos.Y + ballRect.Height > paddle.PaddleRect.Top)
                {
                    velocity *= -1;
                }
                
                if(pos.Y < paddle.PaddleRect.Bottom && pos.Y + ballRect.Height > paddle.PaddleRect.Bottom)
                {
                    velocity.Y *= -1;
                }
                
                if(pos.X + ballRect.Width > paddle.PaddleRect.Left && pos.X < paddle.PaddleRect.Left)
                {

                    velocity.X *= -1;
                }
                
                if(pos.X < paddle.PaddleRect.Right && pos.X + ballRect.Width > paddle.PaddleRect.Right)
                {
                    velocity.X *= -1;
                }
            }
        }
    }
}
