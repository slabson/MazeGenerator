using System;
using UnityEngine;
using UnityEngine.Events;

namespace MazeGenerator
{
    public class MazeGenerator : MonoBehaviour
    {
        public GameObject floorPrefab;
        public GameObject wallPrefab;

        public Vector2Int size = new Vector2Int(5, 5);
        public bool doubleWall = false;
        public MazeAlgorithms algorithmType;

        [Header("Events")]
        public UnityEvent onGenerate = new UnityEvent();

        private Transform trans;

        private void Awake()
        {
            trans = transform;
        }

        public void Start()
        {
            MazeAlgorithm algorithm = GetAlgorithm(algorithmType);
            Generate(algorithm);
        }

        public MazeAlgorithm GetAlgorithm(MazeAlgorithms algorithmType)
        {
            Type type = MazeAlgorithm.GetMazeAlgorithmType(algorithmType);
            return Activator.CreateInstance(type, size.x, size.y) as MazeAlgorithm;
        }

        public void Generate(MazeAlgorithm algorithm)
        {
            MazeCellInfo[,] gridInfo = algorithm.GetGridInfo();
            CreateMaze(gridInfo);

            onGenerate?.Invoke();
        }

        public void CreateMaze(MazeCellInfo[,] gridInfo)
        {
            float scale = wallPrefab.transform.localScale.x;
            float wallOffset = floorPrefab.transform.localScale.x / 2f;
            
            if(doubleWall)
            {
                wallOffset -= wallPrefab.transform.localScale.z / 2f;
            }

            int width = gridInfo.GetLength(0);
            int height = gridInfo.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    MazeCellInfo cellInfo = gridInfo[x, y];
                    BuildCell(cellInfo, x, y, scale, wallOffset);
                }
            }
        }

        public void BuildCell(MazeCellInfo cellInfo, int x, int y, float scale, float wallOffset)
        {
            float yPos = y * scale;
            float xPos = x * scale;

            GameObject floor = Instantiate(floorPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, trans);
            floor.name = string.Format("Floor ({0}, {1})", x, y);

            if (cellInfo.wallN)
            {
                GameObject wallN = Instantiate(wallPrefab, new Vector3(xPos, wallOffset, yPos + wallOffset), Quaternion.identity, trans);
                wallN.name = string.Format("Wall North ({0}, {1})", x, y);
            }

            if (cellInfo.wallE)
            {
                GameObject wallE = Instantiate(wallPrefab, new Vector3(xPos + wallOffset, wallOffset, yPos), Quaternion.Euler(0, 90, 0), trans);
                wallE.name = string.Format("Wall East ({0}, {1})", x, y);
            }

            //Below, if doubleWall is false, create the wall only for maze boundaries
            if (cellInfo.wallS && (doubleWall || y == 0))
            {
                GameObject wallS = Instantiate(wallPrefab, new Vector3(xPos, wallOffset, yPos - wallOffset), Quaternion.identity, trans);
                wallS.name = string.Format("Wall South ({0}, {1})", x, y);
            }

            if (cellInfo.wallW && (doubleWall || x == 0))
            {
                GameObject wallW = Instantiate(wallPrefab, new Vector3(xPos - wallOffset, wallOffset, yPos), Quaternion.Euler(0, 90, 0), trans);
                wallW.name = string.Format("Wall West ({0}, {1})", x, y);
            }
        }

        public void Clear()
        {
            int childCount = trans.childCount;

            for (int i = childCount - 1; i >= 0; i--)
            {
                if (Application.isPlaying)
                {
                    Destroy(trans.GetChild(i).gameObject);
                }
                else
                {
                    DestroyImmediate(trans.GetChild(i).gameObject);
                }
            }
        }
    }
}