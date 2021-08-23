using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class ColorSwitcher : MonoBehaviour {

    [SerializeField] private AudioClip dieSound;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.GetComponent<Ball>();

        if (ball) {
            ball.ChangeColor();

            if (dieSound) {
                ball.GetComponent<AudioSource>().PlayOneShot(dieSound);
            }

            Destroy(gameObject);
        }
    }
}
