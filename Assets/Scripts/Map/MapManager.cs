using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }
        [SerializeField] private MapUIManager mapUIManager;
        private List<MapNode> mapNodes;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            GenerateNewMap();
        }

        public void GenerateNewMap()
        {
            mapNodes = new List<MapNode>();
            MapGenerator generator = new MapGenerator();
            generator.GenerateMap();
            mapNodes = generator.GetMapNodes();
            mapUIManager.GenerateMapUI(mapNodes);
        }

        public void ResetMap()
        {
            mapNodes.Clear();
        }

        public List<MapNode> GetMapNodes()
        {
            return mapNodes;
        }
    }
}