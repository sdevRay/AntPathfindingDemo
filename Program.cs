using ConsoleApp1.Entities;
using ConsoleApp1.Pathfinding;
using Raylib_cs;

namespace ConsoleApp1
{
    static class Program
    {
        public static void Main()
        {
            // Initialize
            Raylib.InitWindow(1280, 720, "Ant Simulator Demo");
            Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);

            Art.Load();
            WorldMap.CreateGraph();
            EntityManager.Add(PlayerInsect.Instance);
            EntityManager.Add(Food.CreatePizza(WorldMap.GetRandomNode().PixelOrigion));

            var camera = WorldCamera.GetCamera();

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