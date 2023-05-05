using Raylib_cs;

namespace ConsoleApp1
{
	static class Art
	{
		public static Texture2D Insect { get; private set; }

		public static void Load()
		{
			Insect = Raylib.LoadTexture("resources/raylib_logo.png");
		}

		public static void Unload()
		{
			Raylib.UnloadTexture(Insect);
		}
	}
}
