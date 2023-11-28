using Raylib_cs;

namespace AntPathfindingDemo
{
    static class Art
    {
        public static Texture2D AntQueen { get; private set; }
        public static Texture2D Ant { get; private set; }
        public static Texture2D Pizza { get; private set; }
        public static Texture2D Grass { get; private set; }
        public static Texture2D Rocky { get; private set; }
        public static Texture2D Impassable { get; private set; }

        public static void Load()
        {
            AntQueen = Raylib.LoadTexture("Assets/antqueen-sheet.png");
            Ant = Raylib.LoadTexture("Assets/ant-sheet.png");
            Pizza = Raylib.LoadTexture("Assets/pizza.png");
            Grass = Raylib.LoadTexture("Assets/grass.png");
            Rocky = Raylib.LoadTexture("Assets/rocky.png");
            Impassable = Raylib.LoadTexture("Assets/impassable.png");
        }

        public static void Unload()
        {
            Raylib.UnloadTexture(AntQueen);
            Raylib.UnloadTexture(Ant);
            Raylib.UnloadTexture(Pizza);
            Raylib.UnloadTexture(Grass);
            Raylib.UnloadTexture(Rocky);
            Raylib.UnloadTexture(Impassable);
        }
    }
}
