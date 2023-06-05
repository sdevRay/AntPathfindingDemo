using Raylib_cs;
using System.Numerics;

namespace ConsoleApp1.Entities
{
    internal class Environment
	{
		public class Food : Entity
		{
			public Food(Texture2D texture, Vector2 position) 
			{
				Texture = texture;
				Position = position;
			}

			public static Food CreatePizza(Vector2 position)
			{
				var food = new Food(Art.Pizza, position);
				//insect.SetState(new IdleState());
				return food;
			}

			public override void Update()
			{

			}

			public void Eaten()
			{
				IsExpired = true;
			}
		}
	}
}
