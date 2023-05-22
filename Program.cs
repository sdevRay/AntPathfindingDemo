using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

namespace ConsoleApp1
{
    static class Program
    {
        public static bool Debug = true;
        public static void Main()
        {
            Initialize();

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                // Update
                Input.Update();
                Spawner.Update();
                EntityManager.Update();

                // Draw
                WorldMap.DrawGraph();
                EntityManager.Draw();

                Raylib.EndDrawing();
            }

            Dispose();
        }

        public static void Initialize()
        {
            Raylib.InitWindow(1280, 720, "Ant Simulator");
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);

            WorldMap.CreateGraph();
            Art.Load();
        }

        public static void Dispose()
        {
            Art.Unload();
            Raylib.CloseWindow();
        }
    }
}