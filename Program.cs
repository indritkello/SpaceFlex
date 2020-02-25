using System;

namespace SpaceFlex
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameA())
                game.Run();
        }
    }
}
