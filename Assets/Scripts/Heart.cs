using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    private SpriteRenderer sr;
    public Sprite brokenSprite;
    public GameObject explosionPrefab;
    public AudioClip dieAudio;

    // Start is called before the first frame update
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {}

    private void Die() {
        PlayerManager.Instance.isFailed = true;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        sr.sprite = brokenSprite;
        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
