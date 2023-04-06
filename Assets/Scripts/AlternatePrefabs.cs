using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternatePrefabs : MonoBehaviour
{
    [SerializeField] List<GameObject> prefabs;
    GameObject a;

    private void Start()
    {
        a = Instantiate(prefabs[Random.Range((int)0, (int)prefabs.Count)]);
        a.transform.parent = transform;
        a.transform.localPosition= Vector3.zero;
        a.transform.forward = transform.forward;
    }
}
