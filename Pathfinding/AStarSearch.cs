using System.Drawing;

namespace AntPathfindingDemo.Pathfinding
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

        private static IDictionary<Point, Directions> NeighborCoordinateMapping(Node node)
        {
            return new Dictionary<Point, Directions>()
            {
                { new Point(node.Point.X + 1, node.Point.Y - 1), Directions.TopLeft },
                { new Point(node.Point.X + 1, node.Point.Y), Directions.TopMiddle },
                { new Point(node.Point.X + 1, node.Point.Y + 1), Directions.TopRight },
                { new Point(node.Point.X, node.Point.Y + 1), Directions.Right },
                { new Point(node.Point.X - 1, node.Point.Y + 1), Directions.BottomRight },
                { new Point(node.Point.X - 1, node.Point.Y), Directions.BottomMiddle },
                { new Point(node.Point.X - 1, node.Point.Y - 1), Directions.BottomLeft },
                { new Point(node.Point.X, node.Point.Y - 1), Directions.Left }
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
                    // This makes it cost more to prevent entity from only choosing angle movements
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
            var neighborsDict = NeighborCoordinateMapping(current);

            IEnumerable<(Directions direction, Node node) > GetNodeNeighbors()
            {
                foreach (var node in WorldMap.GetPassableNodes())
                {
                    if (neighborsDict.TryGetValue(node.Point, out Directions direction))
                    {
                        yield return (direction, node);
                    }
                }
            }

            foreach((Directions direction, Node node) in GetNodeNeighbors())
            {
                var corner = false;
                switch (direction)
                {
                    case Directions.TopRight:
                        {
                            var impassable = GetNodeNeighbors()
                                .Where(n => n.direction == Directions.TopMiddle || n.direction == Directions.Right)
                                .All(n => n.node.Terrain.Impassable);

                            if (impassable)
                                continue;

                            corner = true;
                            break;
                        }
                    case Directions.BottomRight:
                        {
                            var impassable = GetNodeNeighbors()
                                .Where(n => n.direction == Directions.BottomMiddle || n.direction == Directions.Right)
                                .All(n => n.node.Terrain.Impassable);

                            if (impassable)
                                continue;

                            corner = true;
                            break;
                        }
                    case Directions.TopLeft:
                        {
                            var impassable = GetNodeNeighbors()
                                .Where(n => n.direction == Directions.TopMiddle || n.direction == Directions.Left)
                                .All(n => n.node.Terrain.Impassable);

                            if (impassable)
                                continue;

                            corner = true;
                            break;
                        }
                    case Directions.BottomLeft:
                        {
                            var impassable = GetNodeNeighbors()
                                .Where(n => n.direction == Directions.BottomMiddle || n.direction == Directions.Left)
                                .All(n => n.node.Terrain.Impassable);

                            if (impassable)
                                continue;

                            corner = true;
                            break;
                        }
                }

                yield return (node, corner);
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
                if(!GetNeighbors(start).Any(n => n.neighbor.Point == path.FirstOrDefault()?.Point))
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
