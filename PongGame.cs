﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceFlex.GameElements;

namespace SpaceFlex
{
    public class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /*  Game elements   */
        Camera camera;
        VertexPositionColor[] verts;
        VertexBuffer vertexBuffer;
        BasicEffect effect;
        Matrix world = Matrix.Identity;
        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize camera 
            camera = new Camera(this, new Vector3(0, 0, 5),
                                Vector3.Zero,
                                Vector3.Up);
            Components.Add(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize vertices 
            verts = new VertexPositionColor[6];
            verts[0] = new VertexPositionColor(new Vector3(3, 3, 0), Color.Green);
            verts[1] = new VertexPositionColor(new Vector3(3, -3, 0), Color.Green);
            verts[2] = new VertexPositionColor(new Vector3(4, -3, 0), Color.Green);
            verts[3] = new VertexPositionColor(new Vector3(4, -3, 0), Color.Green);
            verts[4] = new VertexPositionColor(new Vector3(3, 3, 0), Color.Green);
            verts[5] = new VertexPositionColor(new Vector3(4, 3, 0), Color.Green);

            // Set vertex data in VertexBuffer 
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor),
                                            verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);
            // Initialize the BasicEffect 
            effect = new BasicEffect(GraphicsDevice);

            // Set cullmode to none (Usually should not be set to none, only for debugging)
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Translation
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Down)){
                if(world.Translation.Y>(-1))
                world *= Matrix.CreateTranslation(0, -.01f, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Up))
                world *= Matrix.CreateTranslation(0, .01f, 0);


            //Note! world *= is done to keep the translation that is done before, if you use just world=, you will lose it
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing code
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info 
            effect.World = Matrix.CreateScale(.5f) * world;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.VertexColorEnabled = true;
            // Begin effect and draw for each pass 
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, verts, 0, 2);
            }
            base.Draw(gameTime);
        }
    }
}