using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    [SerializeField] private Text scoreText; // assign in editor

    [SerializeField] private Text clickText; // assign in editor

    private Game game;

    // Start is called before the first frame update
    void Start() {
        game = FindObjectOfType<Game>();
    }

    // Update is called once per frame
    void Update() {
        if (scoreText) {
            scoreText.text = game.GetStars().ToString();
        }

        Ball ball = FindObjectOfType<Ball>();
        if (ball) {
            clickText.enabled = !ball.getMoving();
        }
    }
}
