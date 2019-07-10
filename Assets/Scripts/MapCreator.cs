using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour {
    public bool doublePlayer;
    public GameObject[] item;
    private List<Vector3> visit = new List<Vector3>();
    private int[] itemNumber = {0, 60, 20, 20, 20};

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {}

    private void createItem(GameObject GO, Vector3 position, Quaternion rotation) {
        GameObject itemGO = Instantiate(GO, position, rotation);
        itemGO.transform.SetParent(gameObject.transform);
        visit.Add(position);
    }

    private void Awake() {
        InvokeRepeating("CreateEnemy", 4, 5);
        InitMap();
    }

    private void InitMap() {
        InitBoundary();
        InitHeart();

        GameObject player = Instantiate(item[5], new Vector3(-2, -8, 0), Quaternion.identity);
        player.GetComponent<Born>().createPlayer = true;

        if (doublePlayer) {
            GameObject player2 = Instantiate(item[7], new Vector3(2, -8, 0), Quaternion.identity);
            player2.GetComponent<Born>().createPlayer = true;
        }

        createItem(item[5], new Vector3(-10, 8, 0), Quaternion.identity);
        createItem(item[5], new Vector3(0, 8, 0), Quaternion.identity);
        createItem(item[5], new Vector3(10, 8, 0), Quaternion.identity);

        for (int i = 1; i <= 4; i++)
            for (int j = 1; j <= itemNumber[i]; j++)
                createItem(item[i], CreateRandomPosition(), Quaternion.identity);
    }

    private void InitBoundary() {
        for (int i = -11; i <= 11; i++) createItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        for (int i = -11; i <= 11; i++) createItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        for (int i = -8; i <= 8; i++) createItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        for (int i = -8; i <= 8; i++) createItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
    }

    private void InitHeart() {
        createItem(item[1], new Vector3(-1, -7, 0), Quaternion.identity);
        createItem(item[1], new Vector3(0, -7, 0), Quaternion.identity);
        createItem(item[1], new Vector3(1, -7, 0), Quaternion.identity);
        createItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        createItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        createItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
    }

    private Vector3 CreateRandomPosition() {
        while (true) {
            Vector3 position = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!visit.Contains(position)) return position;
        }
    }

    private void CreateEnemy() {
        int num = Random.Range(0, 3);
        Vector3 position = new Vector3();
        switch(num) {
            case 0:
                position = new Vector3(-10, 8, 0);
                break;
            case 1:
                position = new Vector3(0, 8, 0);
                break;
            case 2:
                position = new Vector3(10, 8, 0);
                break;
        }
        createItem(item[5], position, Quaternion.identity);
    }
}
