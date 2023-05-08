using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1
{
    static class Program
	{
		public static Vector2 ScreenSize = new(800, 600);
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
				InsectSpawner.Update();

				Raylib.EndDrawing();
			}

			Dispose();
		}

        public static void Initialize()
		{
			Raylib.InitWindow((int)ScreenSize.X, (int)ScreenSize.Y, "Ant Simulator");
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