using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves the obstacletriggers that are parented to this.
public class Obstacle : MonoBehaviour {

    [SerializeField] private Vector2 movement; // assign in editor

    [SerializeField] private float rotation; // assign in editor

    [SerializeField] public Vector2 starOffset; // assign in editor

    private Vector2 spawnPos; // initial spawnPos

    // Start is called before the first frame update
    void Start() {
        spawnPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        // move away from initial spawnPos with an offset
        transform.position = spawnPos + movement * Mathf.Cos(Time.time);

        // rotate
        Vector3 rot = new Vector3(0, 0, rotation);
        transform.Rotate(rot * Time.deltaTime);
    }

    private void FixedUpdate() {
        // despawn if too far from the player (already passed obstacles)
        Ball ball = FindObjectOfType<Ball>();
        if (ball) {
            if (Vector3.Distance(transform.position, ball.transform.position) > 50) {
                Destroy(gameObject);
            }
        }
    }
}
