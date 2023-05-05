namespace ConsoleApp1.Entities
{
	static class EntityManager
	{
		static List<Entity> _entities = new List<Entity>();

		public static void Add(Entity entity)
		{
			_entities.Add(entity);
		}

		public static void Update()
		{
			foreach(var entity in _entities) 
			{ 
				entity.Update();
			}

			_entities = _entities.Where(entity => !entity.IsExpired).ToList();
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
