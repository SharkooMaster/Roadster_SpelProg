using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Generator : MonoBehaviour
{
    private List<GameObject> instantiated = new List<GameObject>();
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> gameObjectsScale = new List<Vector3>();
    [SerializeField] private List<Vector2> randomPosX = new List<Vector2>();
    [SerializeField] private List<Vector3Int> randomInit = new List<Vector3Int>();

    private int nextEvent = 0;

    [Header("Generator Parameters")]
    [SerializeField] private int startCount = 5;
    [SerializeField] private int destroyInterval = 5;
    private int currentOffset = 0;
    private int currentInstantiated = 0;

    private void Start()
    {
        // ###-GenerateInit-### //
        for (int i = 0; i <= startCount; i++)
        {
            for (int j = 0; j < gameObjects.Count; j++)
            {
                if (Random.Range(randomInit[j][0], randomInit[j][1]) == randomInit[j][2] && randomInit[j][1] != 0)
                {
                    Vector3 pos = new Vector3(gameObjectsScale[j].x * (i + 1) + Random.Range(randomPosX[j][0], randomPosX[j][1]), gameObjectsScale[j].y * (i + 1), gameObjectsScale[j].z * (i + 1));
                    GameObject go = Instantiate(gameObjects[j], pos, Quaternion.identity);
                    go.transform.parent = transform;
                    instantiated.Add(go);
                }
                if (randomInit[j][1] == 0)
                {
                    Vector3 pos = new Vector3(gameObjectsScale[j].x * (i + 1) + Random.Range(randomPosX[j][0], randomPosX[j][1]), gameObjectsScale[j].y * (i + 1), gameObjectsScale[j].z * (i + 1));
                    GameObject go = Instantiate(gameObjects[j], pos, Quaternion.identity);
                    go.transform.parent = transform;
                    instantiated.Add(go);
                }
            }
        }
        currentOffset = startCount;
    }

    public void optimSync()
    {
        List<GameObject> temp = instantiated.GetRange(0, currentInstantiated);
        instantiated.RemoveRange(0, currentInstantiated);
        currentInstantiated = 0;
        foreach (var item in temp)
        {
            Destroy(item);
        }
        currentOffset += 1;
        for (int j = 0; j < gameObjects.Count; j++)
        {
            if (Random.Range(randomInit[j][0], randomInit[j][1]) == randomInit[j][2] && randomInit[j][1] != 0)
            {
                Vector3 pos = new Vector3(gameObjectsScale[j].x * (currentOffset + 1) + Random.Range(randomPosX[j][0], randomPosX[j][1]), gameObjectsScale[j].y * (currentOffset + 1), gameObjectsScale[j].z * (currentOffset + 1));
                instantiated.Add(Instantiate(gameObjects[j], pos, Quaternion.identity));
                currentInstantiated++;
            }
            if(randomInit[j][1] == 0)
            {
                Vector3 pos = new Vector3(gameObjectsScale[j].x * (currentOffset + 1) + Random.Range(randomPosX[j][0], randomPosX[j][1]), gameObjectsScale[j].y * (currentOffset + 1), gameObjectsScale[j].z * (currentOffset + 1));
                instantiated.Add(Instantiate(gameObjects[j], pos, Quaternion.identity));
                currentInstantiated++;
            }
        }
        nextEvent += destroyInterval;
    }
}
