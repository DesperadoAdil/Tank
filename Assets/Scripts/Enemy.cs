using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float moveSpeed = 3.0f;
    private Vector3 bulletEularAngles;
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bulletPrefab;
    private float timeVal;
    public GameObject explosionPrefab;
    private float timeValChangeDirection;
    private float v = -1;
    private float h;

    // Start is called before the first frame update
    void Start() {}

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (timeVal >= 3) {
            Attack();
        } else timeVal += Time.deltaTime;
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        if (timeValChangeDirection >= 4) {
            int num = Random.Range(0, 8);
            if (num > 4) {
                v = -1;
                h = 0;
            } else if (num == 0) {
                v = 1;
                h = 0;
            } else if (num > 0 && num < 3) {
                h = -1;
                v = 0;
            } else if (num > 2 && num < 5) {
                h = 1;
                v = 0;
            }
            timeValChangeDirection = 0;
        } else timeValChangeDirection += Time.fixedDeltaTime;

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0) {
            sr.sprite = tankSprite[2];
            bulletEularAngles = new Vector3(0, 0, -180);
        } else if (v > 0) {
            sr.sprite = tankSprite[0];
            bulletEularAngles = new Vector3(0, 0, 0);
        }
        if (v != 0) {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0) {
            sr.sprite = tankSprite[3];
            bulletEularAngles = new Vector3(0, 0, 90);
        } else if (h > 0) {
            sr.sprite = tankSprite[1];
            bulletEularAngles = new Vector3(0, 0, -90);
        }
    }

    private void Attack() {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEularAngles));
        timeVal = 0;
    }

    private void Die() {
        PlayerManager.Instance.score++;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            timeValChangeDirection = 4;
        }
    }
}
