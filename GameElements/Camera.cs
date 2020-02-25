using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceFlex.GameElements
{
    public class Camera : IGameComponent
    {
        Game game;
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up)
        {          
            view = Matrix.CreateLookAt(pos, target, up);
            this.game = game;
        }

        public void Initialize()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                        (float)game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height, 1, 100);
        }
    }
}
