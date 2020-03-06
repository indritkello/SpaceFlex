using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceFlex.GameElements;
using SpaceFlex.Generators;
using System.Collections.Generic;
using System.Linq;

namespace SpaceFlex
{
    public class PongGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /*  Game elements   */
        Camera camera;
        List<VertexPositionColor> allVertices = new List<VertexPositionColor>();
        VertexPositionColor[] vertsRight;
        VertexPositionColor[] vertsLeft;
        VertexBuffer vertexBuffer;
        BasicEffect rEffect;
        BasicEffect lEffect;
        Matrix rWorld = Matrix.Identity;
        Matrix lWorld = Matrix.Identity;
        public PongGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Initialize camera 
            camera = new Camera(this, new Vector3(0, 0, 10),
                                Vector3.Zero,
                                Vector3.Up);
            Components.Add(camera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialize vertices 
            vertsRight = LineGenerator.CreateRectangleLine(9, 3, 1, 6, Color.DarkBlue);
            vertsLeft = LineGenerator.CreateRectangleLine(-9, 3, 1, 6, Color.DarkRed);
            
            allVertices.AddRange(vertsLeft);
            allVertices.AddRange(vertsRight);
            // Set vertex data in VertexBuffer 
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor),
                                            allVertices.Count, BufferUsage.None);
            vertexBuffer.SetData(allVertices.ToArray());
            // Initialize the BasicEffect 
            rEffect = new BasicEffect(GraphicsDevice);
            lEffect = new BasicEffect(GraphicsDevice);

            // Set cullmode to none (Usually should not be set to none, only for debugging)
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Translation Right
            KeyboardState rightKeyboardState = Keyboard.GetState();
            if (rightKeyboardState.IsKeyDown(Keys.Down)){
                if(rWorld.Translation.Y>(-1))
                    rWorld *= Matrix.CreateTranslation(0, -.01f, 0);
            }
            if (rightKeyboardState.IsKeyDown(Keys.Up))
                rWorld *= Matrix.CreateTranslation(0, .01f, 0);


            // Translation Right
            KeyboardState leftKeyboardState = Keyboard.GetState();
            if (leftKeyboardState.IsKeyDown(Keys.S))
            {
                if (lWorld.Translation.Y > (-1))
                    lWorld *= Matrix.CreateTranslation(0, -.01f, 0);
            }
            if (leftKeyboardState.IsKeyDown(Keys.W))
                lWorld *= Matrix.CreateTranslation(0, .01f, 0);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing code
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info 
            rEffect.World = Matrix.CreateScale(.5f) * rWorld;
            rEffect.View = camera.view;
            rEffect.Projection = camera.projection;
            rEffect.VertexColorEnabled = true;
            // Begin effect and draw for each pass 
            foreach (EffectPass pass in rEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertsRight, 0, 2);
            }

            //Set object and camera info 
            lEffect.World = Matrix.CreateScale(.5f) * lWorld;
            lEffect.View = camera.view;
            lEffect.Projection = camera.projection;
            lEffect.VertexColorEnabled = true;
            // Begin effect and draw for each pass 
            foreach (EffectPass pass in lEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertsLeft, 0, 2);
            }

            base.Draw(gameTime);
        }
    }
}
