using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Topic_3___Lawnmower //Maxym F.
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        KeyboardState keyboardState;
        Rectangle window;
        Texture2D grassTexture;
        Texture2D mowerTexture;

        Rectangle mowerRect;
        List<Rectangle> grassTiles;


        SoundEffect mowerSound;
        SoundEffectInstance mowerSoundInstance;

        Vector2 mowerSpeed;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 600, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            mowerRect = new Rectangle(100, 100, 30, 30);

            mowerSpeed = Vector2.Zero;

            grassTiles = new List<Rectangle>();
            for (int x = 0; x < window.Width; x += 5)
                for (int y = 0; y < window.Height; y += 5)
                    grassTiles.Add(new Rectangle(x, y, 5, 5));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            mowerTexture = Content.Load<Texture2D>("Images/mower");
            grassTexture = Content.Load<Texture2D>("Images/long_grass");
            mowerSound = Content.Load<SoundEffect>("Sounds/mower_sound");
            mowerSoundInstance = mowerSound.CreateInstance();
            mowerSoundInstance.IsLooped = true;
        }

        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            mowerSpeed = new Vector2(0, 0);

            if (keyboardState.IsKeyDown(Keys.W) && mowerRect.Y != 0)
                mowerSpeed.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S) && mowerRect.Y != window.Height)
                mowerSpeed.Y += 1;
            if (keyboardState.IsKeyDown(Keys.A) && mowerRect.X != 0)
                mowerSpeed.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D) && mowerRect.Right <= window.Width)
                mowerSpeed.X += 1;
                
            mowerRect.Offset(mowerSpeed);

            for (int i = 0; i < grassTiles.Count; i++)
                if (mowerRect.Contains(grassTiles[i]))
                {
                    grassTiles.Remove(grassTiles[i]);

                    i--;
                }

            if (mowerSpeed == Vector2.Zero)
                mowerSoundInstance.Stop();
            else
                mowerSoundInstance.Play();

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGreen);

            // TODO: Add your drawing code here

          
            _spriteBatch.Begin();
            
            _spriteBatch.Draw(mowerTexture, mowerRect, Color.White);

            foreach (Rectangle grass in grassTiles)
                _spriteBatch.Draw(grassTexture, grass, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
