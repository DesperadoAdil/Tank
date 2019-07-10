using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {
    private int width;
    private int height;
    private float position;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start() {
        width = Screen.width;
        height = Screen.height;
        position = -height/2;
        moveSpeed = height/100;
        transform.position = new Vector3(width/2, position, 0);
    }

    // Update is called once per frame
    void Update() {
        if (position < height/2) {
            position += moveSpeed;
            transform.position = new Vector3(width/2, position, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
