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
                if (i > 1 && Random.value > 0.7f)
                {
                    MapNode branchNode = new MapNode(GetRandomNodeTypeExcept(previousNode.Connections[0].Type));
                    previousNode.Connections.Add(branchNode);
                }
                mapNodes.Add(newNode);
                previousNode = newNode;

            }
        }

        private MapNodeType GetRandomNodeType()
        {
            float randomValue = Random.value;
            if (randomValue < 0.6f) return MapNodeType.Fight;
            if (randomValue < 0.8f) return MapNodeType.Shop;
            return MapNodeType.Encounter;
        }
        
        private MapNodeType GetRandomNodeTypeExcept(MapNodeType exceptType)
        {
            float randomValue = Random.value;
            if (randomValue < 0.6f && exceptType != MapNodeType.Fight) return MapNodeType.Fight;
            if (randomValue < 0.8f && exceptType != MapNodeType.Shop) return MapNodeType.Shop;
            return MapNodeType.Encounter;
        }
    
        public List<MapNode> GetMapNodes()
        {
            return mapNodes;
        }

    }
}