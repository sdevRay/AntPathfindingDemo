using ConsoleApp1.Entities;
using Raylib_cs;
using System.Drawing;
using System.Numerics;

namespace ConsoleApp1.Pathfinding
{

    // https://www.redblobgames.com/pathfinding/a-star/introduction.html
    internal static class WorldMap
    {
        public static List<Node> Graph = new List<Node>();

        private static void AddNode(int x, int y, int pixelBoundWidth, int pixelBoundHeight, int movementCost) 
            => Graph.Add(new Node(x, y, pixelBoundWidth, pixelBoundHeight, movementCost));

        public static void CreateGraph()
        {
            // Waypoints are the key decision points where you might have to change direction.
            // Consider designing this around waypoints, and setting the insects seek state to each waypoint.

            int x = 0;
            int y = 0;

            int width = 14;
            int height = 14;

            var pixelBoundWidth = Raylib.GetScreenWidth() / width;
            var pixelBoundHeight = Raylib.GetScreenHeight() / height;

            for (; x < width; x++)
            {
                for (; y < height; y++) 
                {
                    // Testing Astar with random heavy movement cost nodes
                    var movementCost = Raylib.GetRandomValue(1, 14);

                    AddNode(x, y, pixelBoundWidth, pixelBoundHeight, movementCost);
                }

                y = 0;
            }   
        }

        public static IDictionary<Node, Node?> AStarSearch(Node start, Node goal)
        {
            //frontier = PriorityQueue()
            //frontier.put(start, 0)
            //came_from = dict()
            //cost_so_far = dict()
            //came_from[start] = None
            //cost_so_far[start] = 0

            //while not frontier.empty():
            //   current = frontier.get()

            //   if current == goal:
            //      break

            //   for next in graph.neighbors(current):
            //      new_cost = cost_so_far[current] + graph.cost(current, next)
            //      if next not in cost_so_far or new_cost < cost_so_far[next]:
            //         cost_so_far[next] = new_cost
            //         priority = new_cost
            //         frontier.put(next, priority)
            //         came_from[next] = current

            var frontier = new PriorityQueue<Node, int>();
            frontier.Enqueue(start, 0);
            // path A->B is stored as came_from[B] == A
            var cameFrom = new Dictionary<Node, Node?> { { start, null } };
            var cost_so_far = new Dictionary<Node, int>() { { start, 0 } };

            while (frontier.Count > 0) 
            { 
                var current = frontier.Dequeue();

                if (current == goal)
                    break;

                foreach (var next in GetNeighbors(current))
                {
                    var new_cost = cost_so_far[current] + GetMovementCost(current, next);
                    if(!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                    {
                        cost_so_far[next] = new_cost;
                        var priority = new_cost + GetHeuristic(goal, next);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            return cameFrom;
            //ReconstructPath(start, goal, cameFrom);
        }

        public static Stack<Node?> GetPath(Node start, Node goal)
        {
            var cameFrom = AStarSearch(start, goal);
            var path = ReconstructPath(start, goal, cameFrom);
            return path;
        }

        public static Node GetStartingNode(Entity entity)
        {
            return GetNode(entity.Position);
        }

        public static Node GetNode(Vector2 position)
        {
            return Graph.First(n => Raylib.CheckCollisionPointRec(position, n.DestinationRectangle));
        }

        private static Stack<Node?> ReconstructPath(Node start, Node goal, IDictionary<Node, Node?> cameFrom)
        {
            // The code to reconstruct paths

            //current = goal
            //path = []
            //while current != start: 
            //   path.append(current)
            //   current = came_from[current]
            //path.append(start) # optional
            //path.reverse() # optional

            var current = goal;
            var path = new HashSet<Node?>();
            while (current != null && current != start)
            {
                _ = path.Add(current);
                current = cameFrom.TryGetValue(current, out Node? value) ? value : null;
            }
            //path.Add(start);
            path.Reverse();

            var stack = new Stack<Node?>();
            foreach (var item in path)
            {
                if (item is not null)
                {
                    stack.Push(item);
                    // For visual reference
                    item.Color = Raylib_cs.Color.RED;
                }
            }

            return stack;
        }

        private static IEnumerable<Node> GetNeighbors(Node current)
        {
            var p1 = new Point(current.Point.X + 1, current.Point.Y);
            var p2 = new Point(current.Point.X - 1, current.Point.Y);
            var p3 = new Point(current.Point.X, current.Point.Y + 1);
            var p4 = new Point(current.Point.X, current.Point.Y - 1);


            var neighbors = Graph.Where(n => n.Point.Equals(p1) || n.Point.Equals(p2) || n.Point.Equals(p3) || n.Point.Equals(p4));            
            return neighbors;
        }

        private static int GetMovementCost(Node current, Node next)
        {
            return current.MovementCost + next.MovementCost;
        }

        private static int GetHeuristic(Node goal, Node next)
        {
            // Manhattan distance on a square grid
            return Math.Abs(goal.Point.X - next.Point.X) + Math.Abs(goal.Point.Y - next.Point.Y);
        }

        public static void DrawGraph()
        {
            foreach (var node in Graph)
            {
                Raylib.DrawTexture(Art.Grass, (int)node.DestinationRectangle.x, (int)node.DestinationRectangle.y, Raylib_cs.Color.WHITE);
                Raylib.DrawRectangleLines((int)node.DestinationRectangle.x, (int)node.DestinationRectangle.y, (int)node.DestinationRectangle.x + (int)node.DestinationRectangle.width, (int)node.DestinationRectangle.y + (int)node.DestinationRectangle.height, Raylib_cs.Color.BLACK);
                Raylib.DrawText(node.Point.ToString(), (int)node.DestinationRectangle.x + 5, (int)node.DestinationRectangle.y + 5, 15, node.Color);
                Raylib.DrawText(node.MovementCost.ToString(), (int)node.DestinationRectangle.x + 10, (int)node.DestinationRectangle.y + 20, 20, node.Color);
            }
        }
    } 
}
