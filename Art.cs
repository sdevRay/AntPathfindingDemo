using Raylib_cs;

namespace ConsoleApp1
{
	static class Art
	{
		public static Texture2D Ant { get; private set; }
		public static Texture2D Pizza { get; private set; }
        public static Texture2D Grass { get; private set; }
        public static Texture2D Dirt { get; private set; }
        public static Texture2D Mud { get; private set; }

        public static void Load()
		{
			Ant = Raylib.LoadTexture("Assets/ant.png");
			Pizza = Raylib.LoadTexture("Assets/pizza.png");
            Grass = Raylib.LoadTexture("Assets/grass.png");
            Dirt = Raylib.LoadTexture("Assets/dirt.png");
            Mud = Raylib.LoadTexture("Assets/mud.png");
        }

        // Will more then one instance be able to use this texture?
        // Should the entity itself unload the texture?
        public static void Unload()
		{
			Raylib.UnloadTexture(Ant);
			Raylib.UnloadTexture(Pizza);
            Raylib.UnloadTexture(Grass);
            Raylib.UnloadTexture(Dirt);
            Raylib.UnloadTexture(Mud);
        }
    }
}
