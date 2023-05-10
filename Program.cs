using ConsoleApp1.Entities;
using Raylib_cs;

namespace ConsoleApp1
{
    static class Program
    {
        public static void Main()
        {
            Initialize();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                // EntityManager
                EntityManager.Update();
                EntityManager.Draw();
                Spawner.Update();

                Raylib.EndDrawing();
            }

            Dispose();
        }

        public static void Initialize()
        {
            Raylib.InitWindow(800, 600, "Ant Simulator");
            Raylib.SetTargetFPS(60);
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);

            Art.Load();
        }

        public static void Dispose()
        {
            Art.Unload();
            Raylib.CloseWindow();
        }
    }
}