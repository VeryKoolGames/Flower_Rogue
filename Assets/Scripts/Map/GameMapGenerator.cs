using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapGenerator
    {
        private List<MapNode> mapNodes = new List<MapNode>();
        private int totalNodes = 8;

        public void GenerateMap()
        {
            MapNode startNode = new MapNode(MapNodeType.Fight);
            mapNodes.Add(startNode);
    
            MapNode previousNode = startNode;

            for (int i = 1; i < totalNodes; i++)
            {
                MapNodeType mapNodeType = GetRandomNodeType();
                MapNode newNode = new MapNode(mapNodeType);

                previousNode.Connections.Add(newNode);
                mapNodes.Add(newNode);
                previousNode = newNode;

                if (i > 1 && Random.value > 0.7f)
                {
                    MapNode branchNode = new MapNode(GetRandomNodeType());
                    mapNodes.Add(branchNode);
                    previousNode.Connections.Add(branchNode);
                }
            }
        }

        private MapNodeType GetRandomNodeType()
        {
            float randomValue = Random.value;
            if (randomValue < 0.6f) return MapNodeType.Fight;
            if (randomValue < 0.8f) return MapNodeType.Shop;
            return MapNodeType.Encounter;
        }
    
        public List<MapNode> GetMapNodes()
        {
            return mapNodes;
        }

    }
}