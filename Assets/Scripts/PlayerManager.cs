using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    public bool doublePlayer;
    public bool player2;
    public int HP = 3;
    public int score = 0;
    public bool isDead;
    public bool isFailed;
    public GameObject born;
    public GameObject born2;
    public Text playerScoreText;
    public Text playerHPText;
    public GameObject GameOverUI;
    private static PlayerManager instance;

    public static PlayerManager Instance {
        get {
            return instance;
        }
        set {
            instance = value;
        }
    }

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update() {
        if (isFailed) {
            GameOverUI.SetActive(true);
            Invoke("ReturnToMainMenu", 3);
            return;
        }

        if (isDead) Recover();
        playerScoreText.text = score.ToString();
        playerHPText.text = HP.ToString();
    }

    private void Awake() {
        instance = this;
    }

    private void Recover() {
        if (HP <= 0) {
            isFailed = true;
            Invoke("ReturnToMainMenu", 3);
        } else {
            HP--;
            if (!player2) {
                GameObject player = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
                player.GetComponent<Born>().createPlayer = true;
            } else {
                GameObject player = Instantiate(born2, new Vector3(2, -8, 0), Quaternion.identity);
                player.GetComponent<Born>().createPlayer = true;
            }
            isDead = false;
        }
    }

    private void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }
}
