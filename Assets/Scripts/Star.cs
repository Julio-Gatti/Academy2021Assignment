using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Star : MonoBehaviour {

    [SerializeField] private ParticleSystem dieParticle;
    [SerializeField] private AudioClip dieSound;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // if the other had the ball script spawn the particles and then destroy this
        Ball ball = collision.GetComponent<Ball>();

        if (ball) {
            ball.GetStar();

            if (dieParticle) {
                Instantiate(dieParticle, transform.position, transform.rotation);
            }

            if (dieSound) {
                ball.GetComponent<AudioSource>().PlayOneShot(dieSound);
            }

            Destroy(gameObject);
        }
    }
}
