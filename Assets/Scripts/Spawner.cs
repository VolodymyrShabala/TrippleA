using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour{

    [SerializeField]
    private GameObject[] groups = new GameObject[7];

    void Start(){
        SpawnNext();
    }

    public void SpawnNext() {
        int i = Random.Range(0, groups.Length);
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}