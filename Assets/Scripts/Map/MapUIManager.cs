using System;
using System.Collections.Generic;
using Map;
using UnityEngine;

[Serializable]
public class MapNodePrefabs
{
    public MapNodeType type;
    public GameObject prefab;
}

namespace Map
{
    public class MapUIManager : MonoBehaviour
    {
        [SerializeField] private List<MapNodePrefabs> mapNodePrefabs = new List<MapNodePrefabs>();
        [SerializeField] private Transform mapCanvas;
        [SerializeField] private GameObject connectionPrefab;
        public void GenerateMapUI(List<MapNode> mapNodes)
        {
            int nodeCount = mapNodes.Count;

            float canvasHeight = mapCanvas.GetComponent<RectTransform>().rect.height;
            float spacing = canvasHeight / (nodeCount + 1);

            for (int i = 0; i < nodeCount; i++)
            {
                MapNode mapNode = mapNodes[i];
                float yPosition = mapCanvas.position.y - (canvasHeight / 2) + (i + 1) * spacing;
                int connectionCount = mapNode.Connections.Count;
                float horizontalOffset = (connectionCount - 1) * 0.5f * spacing;
                for (int j = 0; j < connectionCount; j++)
                {
                    GameObject mapNodePrefab = mapNodePrefabs.Find(x => x.type == mapNode.Connections[j].Type).prefab;
                    float xPosition = mapCanvas.position.x + (j * spacing - horizontalOffset);
                    Vector3 position = new Vector3(xPosition, yPosition, 0);
                    Instantiate(mapNodePrefab, position, Quaternion.identity, mapCanvas);
                }
            }
        }
        
        private void DrawConnection(MapNode startNode, MapNode endNode, List<MapNode> mapNodes)
        {
            Vector3 startPosition = mapCanvas.position + new Vector3(0, mapNodes.IndexOf(startNode) * 150, 0);
            Vector3 endPosition = mapCanvas.position + new Vector3(0, mapNodes.IndexOf(endNode) * 150, 0);
            Vector3 direction = (endPosition - startPosition).normalized;
            Vector3 connectionPosition = startPosition + direction * 75;
            Instantiate(connectionPrefab, connectionPosition, Quaternion.identity, mapCanvas);
        }
        
    }
}