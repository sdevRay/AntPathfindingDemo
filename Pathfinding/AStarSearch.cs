using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Pathfinding
{
    public class AStarSearch
    {
        public static Stack<Node?> GetPath(Node start, Node goal)
        {
            var cameFrom = GetCameFromPath(start, goal);
            var path = ReconstructPath(start, goal, cameFrom);
            return path;
        }

        private static IDictionary<Node, Node?> GetCameFromPath(Node start, Node goal)
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
                    if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
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

        private static IEnumerable<Node> GetNeighbors(Node current)
        {
            var p1 = new Point(current.Point.X + 1, current.Point.Y);
            var p2 = new Point(current.Point.X - 1, current.Point.Y);
            var p3 = new Point(current.Point.X, current.Point.Y + 1);
            var p4 = new Point(current.Point.X, current.Point.Y - 1);


            var neighbors = WorldMap.Graph.Where(n => n.Point.Equals(p1) || n.Point.Equals(p2) || n.Point.Equals(p3) || n.Point.Equals(p4));
            return neighbors.Where(n => !n.Impassable);
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

            // The path.Count will be 1 if it's an unreachable node that isn't a neighbor of the start node
            if(path.Count == 1)
            {
                var isNeighbor = GetNeighbors(start).Any(n => n.Equals(path.First()));
                if (!isNeighbor)
                    return new Stack<Node?>();
            }

            var stack = new Stack<Node?>();
            foreach (var item in path)
            {
                if (item is not null)
                {
                    stack.Push(item);

                    // For visual reference can remmove later
                    item.Color = Raylib_cs.Color.RED;
                }
            }



            return stack;
        }
    }
}
