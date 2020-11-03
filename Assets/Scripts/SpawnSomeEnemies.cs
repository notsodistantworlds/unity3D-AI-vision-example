using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSomeEnemies : MonoBehaviour {
    [SerializeField]
    int count;
    [SerializeField]
    GameObject prefab;

    void Start() {
        for(int i = 0; i<count; i++) {
            var position = Random.insideUnitSphere * 100f;
            position.y = 0f;
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
