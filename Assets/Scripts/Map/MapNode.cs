using System.Collections.Generic;

namespace Map
{
    public class MapNode
    {
        public MapNodeType Type { get; private set; }
        public List<MapNode> Connections { get; private set; }

        public MapNode(MapNodeType type)
        {
            Type = type;
            Connections = new List<MapNode>();
        }
    }
}