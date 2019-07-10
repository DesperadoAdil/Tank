using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public bool player2;
    public float moveSpeed = 3.0f;
    private Vector3 bulletEularAngles;
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bulletPrefab;
    private float timeVal;
    public GameObject explosionPrefab;
    private bool isShielded = true;
    private float shieldTimeVal = 3.0f;
    public GameObject shieldEffectPrefab;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    // Start is called before the first frame update
    void Start() {}

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

        if (isShielded) {
            shieldEffectPrefab.SetActive(true);
            shieldTimeVal -= Time.deltaTime;
            if (shieldTimeVal <= 0) {
                isShielded = false;
                shieldEffectPrefab.SetActive(false);
            }
        }

        if (PlayerManager.Instance.isFailed) return;
        if (timeVal >= 0.4f) {
            Attack();
        } else timeVal += Time.deltaTime;
    }

    private void FixedUpdate() {
        if (PlayerManager.Instance.isFailed) return;
        Move();
    }

    private void Move() {
        float v = 0;
        if (PlayerManager.Instance.doublePlayer) {
            if (!player2) v = Input.GetAxisRaw("VerticalPlayer1");
            else v = Input.GetAxisRaw("VerticalPlayer2");
        } else v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3(0, 0, -180);
        } else if (v > 0) {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3(0, 0, 0);
        }
        if (Mathf.Abs(v) > 0) {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying) moveAudio.Play();
        }
        if (v != 0) {
            return;
        }

        float h = 0;
        if (PlayerManager.Instance.doublePlayer) {
            if (!player2) h = Input.GetAxisRaw("HorizontalPlayer1");
            else h = Input.GetAxisRaw("HorizontalPlayer2");
        } else h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3(0, 0, 90);
        } else if (h > 0) {
            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3(0, 0, -90);
        }

        if (Mathf.Abs(h) > 0) {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying) moveAudio.Play();
        } else {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying) moveAudio.Play();
        }
    }

    private void Attack() {
        if (!player2) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
                timeVal = 0;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
                timeVal = 0;
            }
        }
    }

    private void Die() {
        if (isShielded) return;
        PlayerManager.Instance.player2 = player2;
        PlayerManager.Instance.isDead = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
