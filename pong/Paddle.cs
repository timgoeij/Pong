using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace pong
{
    public class Paddle
    {
        private Texture2D paddle;
        private Vector2 pos;
        private Rectangle paddleRect;
        private Boolean cpu;
        private Viewport view;

        public Rectangle PaddleRect
        {
            get { return paddleRect; }   
        }

        public Vector2 Pos
        {
            get { return pos; }
        }

        public Paddle(Boolean cpu, Viewport view)
        {
            this.cpu = cpu;
            this.view = view;

            if(cpu)
            {
                pos = new Vector2(view.Width - 70, view.Height / 2);
            }
            else
            {
                pos = new Vector2(40, view.Height / 2);
            }
        }
        
        public void loadContent(Texture2D tex)
        {
            paddle = tex;

            if(cpu)
            {
                paddleRect = new Rectangle(view.Width - 70, view.Height / 2, tex.Width, tex.Height);
            }
            else
            {
                paddleRect = new Rectangle(40, view.Height / 2, tex.Width, tex.Height);
            }
        }

        public void cpuUpdate(Ball ball)
        {
            if(ball.Pos.Y + ball.BallRect.Height < pos.Y + (paddleRect.Height / 2))
            {
                if(pos.Y > 50)
                {
                    pos.Y -= 3;
                    paddleRect.Y -= 3;
                }
            }

            if(ball.Pos.Y > pos.Y + paddleRect.Height / 2)
            {
                if(pos.Y + paddleRect.Y < view.Height - 50)
                {
                    pos.Y += 3;
                    paddleRect.Y += 3;
                }
            }
        }

        public void playerUpdate()
        {
            KeyboardState kbs = Keyboard.GetState();

            if(kbs.IsKeyDown(Keys.Up) && pos.Y > 50)
            {
                pos.Y -= 3;
                paddleRect.Y -= 3;
            }

            if(kbs.IsKeyDown(Keys.Down) && pos.Y + paddle.Height < view.Height - 50)
            {
                pos.Y += 3;
                paddleRect.Y += 3;
            }
        }

       

        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(paddle, pos, Color.White);
        }
    }
}
