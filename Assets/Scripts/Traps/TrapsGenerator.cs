using System;
using UnityEngine;

namespace MazeGenerator
{
    [RequireComponent(typeof(MazeGenerator))]
    public class TrapsGenerator : MonoBehaviour
    {
        public TrapsAlgorithms algorithmType;

        [Range(0, 1), Tooltip("0% - 100% of the maze area. 0 = random")]
        public float percentOfTraps;
        public GameObject[] traps;

        private MazeGenerator mazeGenerator;
        private Vector2Int mazeSize;

        private Transform parentOfTraps;

        private void Awake()
        {
            mazeGenerator = GetComponent<MazeGenerator>();
        }

        public void Start()
        {
            mazeSize = mazeGenerator.size;

            TrapsAlgorithm algorithm = GetAlgorithm(algorithmType);
            Generate(traps, algorithm);
        }

        public TrapsAlgorithm GetAlgorithm(TrapsAlgorithms algorithmType)
        {
            Type type = TrapsAlgorithm.GetTrapsAlgorithmType(algorithmType);
            return Activator.CreateInstance(type, mazeSize.x, mazeSize.y, GetNumberOfTraps()) as TrapsAlgorithm;
        }

        public void Generate(GameObject[] traps, TrapsAlgorithm algorithm)
        {
            CreateParentOfTraps();

            Coordinates[] coords = algorithm.GetTrapCoords();

            System.Random rand = new System.Random();
            int coordsLenght = coords.Length;
            int trapsLenght = traps.Length;

            for(int i = 0; i < coordsLenght; i++)
            {
                int trapIndex = rand.Next(0, trapsLenght);

                GameObject trap = Instantiate(traps[trapIndex], new Vector3(coords[i].x, 0, coords[i].y), Quaternion.identity, parentOfTraps);
                trap.name += coords[i].ToString();

                float trapHeight = trap.transform.localScale.y;
                Vector3 trapPos = trap.transform.position;
                trapPos.y += trapHeight / 2f;
                trap.transform.position = trapPos;
            }
        }

        private void CreateParentOfTraps()
        {
            Clear();
            parentOfTraps = new GameObject("Traps").transform;
            parentOfTraps.SetParent(mazeGenerator.transform);
        }

        public int GetNumberOfTraps()
        {
            int cellsNumber = mazeSize.x * mazeSize.y;

            if (percentOfTraps == 0)
            {
                return UnityEngine.Random.Range(1, 101);
            }
            else
            {
                return (int)Mathf.Lerp(1, cellsNumber, percentOfTraps);
            }
        }

        public void Clear()
        {
            if(parentOfTraps != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(parentOfTraps.gameObject);
                }
                else
                {
                    DestroyImmediate(parentOfTraps.gameObject);
                }
            }
        }
    }
}