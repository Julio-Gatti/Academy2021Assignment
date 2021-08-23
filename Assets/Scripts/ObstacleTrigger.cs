using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
// to be parented to an obstacle
public class ObstacleTrigger : MonoBehaviour {

    [SerializeField] private Kolor kolor;

    // Start is called before the first frame update
    void Start() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Game.kolorsToColors.TryGetValue(kolor, out Color color);
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Ball ball = collision.GetComponent<Ball>();
        if (ball && ball.GetKolor() != kolor) {
            ball.Die();
        }
    }
}
