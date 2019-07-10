using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {
    private bool choice;
    public Transform position1;
    public Transform position2;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        float v = Input.GetAxisRaw("Vertical");
        if (v == 1) {
            choice = false;
            transform.position = position1.position;
        }
        else if (v == -1) {
            choice = true;
            transform.position = position2.position;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (!choice) SceneManager.LoadScene(1);
            else SceneManager.LoadScene(2);
        }
    }
}
