using AntPathfindingDemo.Entities;
using Raylib_cs;
using System.Numerics;

namespace AntPathfindingDemo
{
    public static class WorldCamera
    {
        public static Camera2D GetCamera() 
        { 
            return new Camera2D
            {
                target = AntQueen.Instance.Position,
                offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
                rotation = 0.0f,
                zoom = 1.5f
            };
        }

        public static void Update(ref Camera2D camera)
        {
            camera.zoom += ((float)Raylib.GetMouseWheelMove() * 0.05f);

            if (camera.zoom > 2f)
            {
                camera.zoom = 2f;
            }
            else if (camera.zoom < 0.75f)
            {
                camera.zoom = 0.75f;
            }
        }

        public static void Begin2D(ref Camera2D camera) => Raylib.BeginMode2D(camera);
        public static void End2D() => Raylib.EndMode2D();

        public static void UpdateCameraCenterSmoothFollow(ref Camera2D camera, float delta, int width, int height)
        {
            const float minSpeed = 30;
            const float minEffectLength = 10;
            const float fractionSpeed = 0.8f;

            camera.offset = new Vector2(width / 2, height / 2);
            Vector2 diff = Vector2.Subtract(AntQueen.Instance.Position, camera.target);
            float length = diff.Length();

            if (length > minEffectLength)
            {
                float speed = Math.Max(fractionSpeed * length, minSpeed);
                camera.target = Vector2.Add(camera.target, Vector2.Multiply(diff, speed * delta / length));
            }
        }
    }
}
