using System.Data.SqlTypes;
using System.Drawing;

namespace ConsoleApp1.Pathfinding
{
    // https://www.redblobgames.com/pathfinding/a-star/introduction.html
    public class AStarSearch
    {
        private enum Directions
        {
            TopLeft,
            TopMiddle,
            TopRight,
            Right,
            BottomRight,
            BottomMiddle,
            BottomLeft,
            Left,
        }

        private struct NeighborCoordinate : INullable
        {
            public Directions Direction { get; }
            public Point Point { get; }

            public bool IsNull { get { return !_initialized; } }

            private bool _initialized { get; set; } = false;

            public NeighborCoordinate(Directions direction, Point point)
            {
                _initialized = !_initialized;
                Direction = direction;
                Point = point;
            }
        }

        static IEnumerable<NeighborCoordinate> GetNeighborsCoordinates(Node current)
        {
            return new List<NeighborCoordinate>()
            {
                new NeighborCoordinate(Directions.TopLeft, new Point(current.Point.X + 1, current.Point.Y - 1)),
                new NeighborCoordinate(Directions.TopMiddle, new Point(current.Point.X + 1, current.Point.Y)),
                new NeighborCoordinate(Directions.TopRight, new Point(current.Point.X + 1, current.Point.Y + 1)),
                new NeighborCoordinate(Directions.Right, new Point(current.Point.X, current.Point.Y + 1)),
                new NeighborCoordinate(Directions.BottomRight, new Point(current.Point.X - 1, current.Point.Y + 1)),
                new NeighborCoordinate(Directions.BottomMiddle, new Point(current.Point.X - 1, current.Point.Y)),
                new NeighborCoordinate(Directions.BottomLeft, new Point(current.Point.X - 1, current.Point.Y - 1)),
                new NeighborCoordinate(Directions.Left, new Point(current.Point.X, current.Point.Y - 1))
            };
        }

        public static Stack<Node?> GetPath(Node start, Node goal)
        {
            var cameFrom = GetCameFromPath(start, goal);
            var path = ReconstructPath(start, goal, cameFrom);
            return path;
        }

        private static IDictionary<Node, Node?> GetCameFromPath(Node start, Node goal)
        {
            var frontier = new PriorityQueue<Node, float>();
            frontier.Enqueue(start, 0);
            // path A->B is stored as came_from[B] == A
            var cameFrom = new Dictionary<Node, Node?> { { start, null } };
            var cost_so_far = new Dictionary<Node, float>() { { start, 0 } };

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current == goal)
                    break;

                foreach (var (next, corner) in GetNeighbors(current))
                {
                    // Plus two movement cost for a diagonal move
                    // This makes it cost more like mud to prevent entity from only choosing angle movements
                    var new_cost = cost_so_far[current] + GetMovementCost(current, next) + (corner ? 2 : 0);
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

        private static IEnumerable<(Node neighbor, bool isCorner)> GetNeighbors(Node current)
        {
            var neighborsCoordinates = GetNeighborsCoordinates(current);
            var graph = WorldMap.GetPassableNodes();

            // Find points and multiply new movement cost
            foreach (var node in graph)
            {
                var nodeCoordinate = neighborsCoordinates.Where(n => n.Point.Equals(node.Point)).FirstOrDefault();
                if (!nodeCoordinate.IsNull) // This node is one of the neighbors
                {
                    var corner = false;
                    // block any corner node that is surrounded by impassable nodes to stop angular movement
                    switch (nodeCoordinate.Direction)
                    {
                        case Directions.TopLeft:
                            {
                                var points = neighborsCoordinates
                                    .Where(c => c.Direction == Directions.TopMiddle || c.Direction == Directions.Left)
                                    .Select(c => c.Point);
                                if (graph.Where(n => points.Contains(n.Point)).All(n => n.Terrain.Impassable))
                                {
                                    continue;
                                }

                                corner = !corner;
                            }
                            break;
                        case Directions.TopRight:
                            {
                                var points = neighborsCoordinates
                                    .Where(c => c.Direction == Directions.TopMiddle || c.Direction == Directions.Right)
                                    .Select(c => c.Point);
                                if (graph.Where(n => points.Contains(n.Point)).All(n => n.Terrain.Impassable))
                                {
                                    continue;
                                }

                                corner = !corner;
                            }
                            break;
                        case Directions.BottomLeft:
                            {
                                var points = neighborsCoordinates
                                    .Where(c => c.Direction == Directions.Left || c.Direction == Directions.BottomMiddle)
                                    .Select(c => c.Point);
                                if (graph.Where(n => points.Contains(n.Point)).All(n => n.Terrain.Impassable))
                                {
                                    continue;
                                }

                                corner = !corner;
                            }
                            break;
                        case Directions.BottomRight:
                            {
                                var points = neighborsCoordinates
                                    .Where(c => c.Direction == Directions.Right || c.Direction == Directions.BottomMiddle)
                                    .Select(c => c.Point);
                                if (graph.Where(n => points.Contains(n.Point)).All(n => n.Terrain.Impassable))
                                {
                                    continue;
                                }

                                corner = !corner;
                            }
                            break;
                    }

                    yield return (node, corner);
                }
            }
        }

        private static float GetMovementCost(Node current, Node next)
        {
            return current.Terrain.MovementCost + next.Terrain.MovementCost;
        }

        private static int GetHeuristic(Node goal, Node next)
        {
            // Manhattan distance on a square grid
            return Math.Abs(goal.Point.X - next.Point.X) + Math.Abs(goal.Point.Y - next.Point.Y);
        }

        private static Stack<Node?> ReconstructPath(Node start, Node goal, IDictionary<Node, Node?> cameFrom)
        {
            // The code to reconstruct paths
            var current = goal;
            var path = new HashSet<Node?>();
            while (current != null && current != start)
            {
                _ = path.Add(current);
                current = cameFrom.TryGetValue(current, out Node? value) ? value : null;
            }

            //path.Add(start);
            //path.Reverse();

            // The path.Count will be 1 if it's an unreachable node that isn't a neighbor of the start node
            if (path.Count == 1)
            {
                var (neighbor, _) = GetNeighbors(start).FirstOrDefault(n => n.neighbor.Point.Equals(path.First().Point));
                if(neighbor == null)
                    return new Stack<Node?>();
            }

            var stack = new Stack<Node?>();
            foreach (var item in path)
            {
                if (item is not null)
                {
                    stack.Push(item);

                }
            }

            return stack;
        }
    }
}
