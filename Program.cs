using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

namespace ConsoleApp1
{
    static class Program
    {
        public static bool Debug = false;
        public static void Main()
        {
            // Initialize
            Raylib.InitWindow(1280, 720, "Ant Simulator");
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);

            Art.Load();
            WorldMap.CreateGraph();
            EntityManager.Add(PlayerInsect.Instance);

            var camera = WorldCamera.GetCamera();

            while (!Raylib.WindowShouldClose())
            {

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.GREEN);
                WorldCamera.Begin2D(ref camera);

                //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.BLACK);

                // Update
                WorldCamera.Update(ref camera);
                WorldCamera.UpdateCameraCenterSmoothFollow(ref camera, Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                Input.Update(ref camera);
                Spawner.Update();
                EntityManager.Update();

                // Draw
                WorldMap.DrawGraph();
                EntityManager.Draw();

                WorldCamera.End2D();
                Raylib.EndDrawing();
            }

            // Dispose
            Art.Unload();
            Raylib.CloseWindow();
        }       
    }
}