namespace ConsoleApp1.Entities
{
    static class EntityManager
	{
		static List<Entity> _entities = new();
		static List<Insect> _insects = new();
		public static List<Environment.Food> Foods = new();

		public static void Add(Entity entity)
		{
			_entities.Add(entity);

			if (entity is Insect insect)
			{
				_insects.Add(insect);
			}

			if (entity is Environment.Food food)
			{
				Foods.Add(food);
			}
		}

		public static void Update()
		{
			HandleCollisions();

			foreach (var entity in _entities) 
			{ 
				entity.Update();
			}

			_entities = _entities.Where(entity => !entity.IsExpired).ToList();
			Foods = Foods.Where(entity => !entity.IsExpired).ToList();
			_insects = _insects.Where(entity => !entity.IsExpired).ToList();
		}

		public static void HandleCollisions() 
		{ 
			// TODO
		}

		public static void Draw()
		{
			foreach(var entity in _entities) 
			{ 
				entity.Draw();
			}
		}
	}
}
