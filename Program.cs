using ConsoleApp1.Entities;
using Raylib_cs;
using System.Numerics;

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
			Raylib.InitWindow(800, 480, "Hello World");
			Art.Load();

			
		}

		public static void Dispose()
		{
			Art.Unload();
			Raylib.CloseWindow();
		}
	}
}