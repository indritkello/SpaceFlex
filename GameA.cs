using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceFlex.GameElements;

namespace SpaceFlex
{
    public class GameA : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /*  Game elements   */
        Camera camera;
        VertexPositionColor[] verts; 
        VertexBuffer vertexBuffer;
        BasicEffect effect;
        Matrix world = Matrix.Identity;
        public GameA()
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
            verts = new VertexPositionColor[3]; 
            verts[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Blue); 
            verts[1] = new VertexPositionColor(new Vector3(1, -1, 0), Color.Red);
            verts[2] = new VertexPositionColor(new Vector3(-1, -1, 0), Color.Green);

            // Set vertex data in VertexBuffer 
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor),
                                            verts.Length, BufferUsage.None);
            vertexBuffer.SetData(verts);
            // Initialize the BasicEffect 
            effect = new BasicEffect(GraphicsDevice); 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Translation
            KeyboardState keyboardState = Keyboard.GetState(  ); 
            if (keyboardState.IsKeyDown(Keys.Left))   
                world *= Matrix.CreateTranslation(-.01f, 0, 0);
            if (keyboardState.IsKeyDown(Keys.Right))    
                world *= Matrix.CreateTranslation(.01f, 0, 0);

            // Rotation 
            world *= Matrix.CreateRotationY(MathHelper.PiOver4 / 60); 

            //Note! world *= is done to keep the translation that is done before, if you use just world=, you will lose it
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // Drawing code
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            //Set object and camera info 
            effect.World = world; 
            effect.View = camera.view; 
            effect.Projection = camera.projection;
            effect.VertexColorEnabled = true;
            // Begin effect and draw for each pass 
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, verts, 0, 1);
            }
            base.Draw(gameTime);
        }
    }
}
