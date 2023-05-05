using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

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

				Raylib.EndDrawing();
			}

			Dispose();
		}

        public static void Initialize()
		{
			Raylib.InitWindow(800, 480, "Ant Simulator");
			Raylib.SetTraceLogLevel(TraceLogLevel.LOG_DEBUG);

            Art.Load();

			EntityManager.Add(new Ant(Art.Ant, new Vector2(25, 25)));
		}

		public static void Dispose()
		{
			Art.Unload();
			Raylib.CloseWindow();
		}
	}
}