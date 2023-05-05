using Raylib_cs;

namespace ConsoleApp1
{
	static class Art
	{
		public static Texture2D Ant { get; private set; }

        public static void Load()
		{
			Ant = Raylib.LoadTexture(@"resources\ant.png");
		}

		// Will more then one instance be able to use this texture?
		// Should the entity itself unload the texture?
		public static void Unload()
		{
			Raylib.UnloadTexture(Ant);
		}
	}
}
