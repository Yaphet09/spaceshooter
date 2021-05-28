﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace spaceshooter
{
    class Player : PhysicalObject
    {
        int points = 0;
        List<Bullet> bullets;
        Texture2D bulletTexture;
        double timeSinceLastBullet = 0;

        public List<Bullet> Bullets { get { return bullets; } }
        public int Points { get { return points; } set { points = value; } }

        public Player(Texture2D texture, float X, float Y, float speedX, float speedY, Texture2D bulletTexture) : base(texture, X, Y, speedX, speedY)
        {
            bullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardstate = Keyboard.GetState();
            if (keyboardstate.IsKeyDown(Keys.Escape))
                isAlive = false;

            if (vector.X <= window.ClientBounds.Width - texture.Width && vector.X >= 0)
            {
                if (keyboardstate.IsKeyDown(Keys.Right))
                    vector.X += speed.X;
                if (keyboardstate.IsKeyDown(Keys.Left))
                    vector.X -= speed.X;
            }

            if (vector.Y <= window.ClientBounds.Height - texture.Height && vector.Y >= 0)
            {
                if (keyboardstate.IsKeyDown(Keys.Down))
                    vector.Y += speed.Y;
                if (keyboardstate.IsKeyDown(Keys.Up))
                    vector.Y -= speed.Y;
            }

            if (vector.X < 0)
                vector.X = 0;

            if (vector.X > window.ClientBounds.Width - texture.Width)
            {
                vector.X = window.ClientBounds.Width - texture.Width;
            }

            if (vector.Y < 0)
                vector.Y = 0;

            if (vector.Y > window.ClientBounds.Height - texture.Height)
            {
                vector.Y = window.ClientBounds.Height - texture.Height;
            }

            if (keyboardstate.IsKeyDown(Keys.Space))
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 200)
                {
                    Bullet temp = new Bullet(bulletTexture, vector.X + texture.Width / 2, vector.Y);
                    bullets.Add(temp);
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            foreach (Bullet b in bullets.ToList())
            {
                b.Update();
                if (!b.IsAlive)
                    bullets.Remove(b);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
            foreach (Bullet b in bullets)
                b.Draw(spriteBatch);
        }

        public void Reset(float X, float Y, float speedX, float speedY)
        {
            vector.X = X;
            vector.Y = Y;
            speed.X = speedX;
            speed.Y = speedY;
            bullets.Clear();
            timeSinceLastBullet = 0;
            points = 0;
            isAlive = true;
        }
    }

    class Bullet : PhysicalObject
    {
        public Bullet(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 3f)
        {

        }

        public void Update()
        {
            vector.Y -= speed.Y;
            if (vector.Y < 0)
                isAlive = false;
        }
    }
}