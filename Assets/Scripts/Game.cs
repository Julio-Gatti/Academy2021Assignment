using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// identifier used by the ball and the obstacles
// spelled with a k just to not confuse the unity color struct
public enum Kolor {
    Purple,
    Blue,
    Green,
    Red
}

[RequireComponent(typeof(AudioSource))]
// Manages the game by handling object spawning.
public class Game : MonoBehaviour {

    public static Dictionary<Kolor, Color> kolorsToColors = new Dictionary<Kolor, Color>();

    [SerializeField] private List<Obstacle> obstaclePrefabs; // assign in editor

    [SerializeField] private Star starPrefab; // assign in editor

    [SerializeField] private ColorSwitcher colorSwitcherPrefab; // assign in editor

    [SerializeField] private float distanceBetweenSpawns = 10; // assign in editor

    private Ball ball; // the players ball in the scene

    private int stars; // the players collected stars

    private float spawnPos = -15; // the height the player needs to reach for to spawn something

    private bool lastSpawnedColorSwitcher;

    private void Awake() {
        kolorsToColors.Clear();
        kolorsToColors.Add(Kolor.Purple, new Color(0.9f, 0, 0.9f));
        kolorsToColors.Add(Kolor.Blue, new Color(0, 0.1f, 1));
        kolorsToColors.Add(Kolor.Green, new Color(0, 1, 0.1f));
        kolorsToColors.Add(Kolor.Red, new Color(1, 0.1f, 0));
    }

    // Start is called before the first frame update
    void Start() {
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update() {
        HandleCamera();
    }

    // called in update since needs to be smooth
    // relies that there is a main camera in the scene
    private void HandleCamera() {
        if (ball) {
            if (ball.transform.position.y > Camera.main.transform.position.y) {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, ball.transform.position - Vector3.forward * 10, Time.deltaTime * 5);
            }
        }
    }

    private void FixedUpdate() {
        HandleSpawning();
    }

    // called in fixedupdate since doesn't really need to be done every frame, just fast enough
    private void HandleSpawning() {
        if (ball) {
            // when the ball reaches the spawnPos
            if (ball.transform.position.y >= spawnPos) {
                // spawn an obstacle with a star or a colorSwitcher
                Vector2 offScreen = new Vector2(0, spawnPos + 25);

                int r = Random.Range(0, 2);
                if (r == 0 && !lastSpawnedColorSwitcher) {
                    Instantiate(colorSwitcherPrefab, offScreen, Quaternion.Euler(Vector3.zero));
                    lastSpawnedColorSwitcher = true;
                } else {
                    // spawn an obstacle offscreen
                    int r2 = Random.Range(0, obstaclePrefabs.Count);
                    
                    Obstacle spawnedObstacle = Instantiate(obstaclePrefabs[r2], offScreen, Quaternion.Euler(Vector3.zero));

                    // also spawn a star, which will be offset from the obstacle (circle center vs line offset)
                    Instantiate(starPrefab, offScreen + spawnedObstacle.starOffset, Quaternion.Euler(Vector3.zero));
                    lastSpawnedColorSwitcher = false;
                }

                // increase the distance for the next one
                if (lastSpawnedColorSwitcher) {
                    spawnPos += distanceBetweenSpawns / 2;
                } else {
                    spawnPos += distanceBetweenSpawns;
                }
            }
        }
    }

    public void AddStar() {
        stars++;
    }

    public int GetStars() {
        return stars;
    }

    public void Reset() {
        StartCoroutine(OnReset());
    }

    public IEnumerator OnReset() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
