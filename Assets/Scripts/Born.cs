using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {
    public GameObject playerPrefab;
    public GameObject[] enemyPrefabList;
    public bool createPlayer;

    // Start is called before the first frame update
    void Start() {
        Invoke("BornTank", 0.8f);
        Destroy(gameObject, 0.8f);
    }

    // Update is called once per frame
    void Update() {}

    private void BornTank() {
        if (createPlayer) {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        } else {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
