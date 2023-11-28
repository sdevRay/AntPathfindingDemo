using AntPathfindingDemo.Entities;
using AntPathfindingDemo.Pathfinding;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo
{
    static class Program
    {
        public static void Main()
        {
            var screenSize = new Vector2(1280, 720);

            // Initialize
            Raylib.InitWindow((int)screenSize.X, (int)screenSize.Y, "Ant Pathfinding Demo");
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);
            //Raylib.TraceLog(TraceLogLevel.LOG_INFO, $"{DateTime.Now.TimeOfDay} Initialized");

            UI.ScreenSize = screenSize;
            Art.Load();
            WorldMap.CreateGraph();
            EntityManager.Add(AntQueen.Instance);

            var camera = WorldCamera.GetCamera();

            Raylib.SetTargetFPS(144);
            while (!Raylib.WindowShouldClose())
            {
                // Update
                WorldCamera.Update(ref camera);
                WorldCamera.UpdateCameraCenterSmoothFollow(ref camera, Raylib.GetFrameTime(), Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                Input.Update(ref camera);
                EntityManager.Update();

                // Draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DARKBROWN);
                WorldCamera.Begin2D(ref camera);
                WorldMap.Draw();
                EntityManager.Draw();
                WorldCamera.End2D();
                UI.Draw();
                Raylib.EndDrawing();
            }

            // Dispose
            Art.Unload();
            Raylib.CloseWindow();
        }
    }
}