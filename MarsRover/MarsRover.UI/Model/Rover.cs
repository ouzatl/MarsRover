using MarsRover.UI.Enums;

namespace MarsRover.UI.Model
{
    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DirectionEnum Direction { get; set; }
        public Mars Mars { get; set; }
        public string RouteMap { get; set; }

        public Rover(int x, int y, DirectionEnum direction, Mars mars, string routeMap)
        {
            this.X = x;
            this.Y = y;
            this.Direction = direction;
            this.Mars = mars;
            this.RouteMap = routeMap;
        }
    }
}
