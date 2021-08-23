using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]

// The script for the ball. It is moved through transform.translate, since there is no need for proper
// rigidbody physics or charactermovement collision. It just needs a rigidbody and a collider to be able to
// hit triggers. The rigidbody should have "is kinematic" set as not to get actual rigidbody movement.
public class Ball : MonoBehaviour {

    [SerializeField] private float gravity = -8; // assign in editor

    [SerializeField] private float jumpSpeed = 6; // assign in editor

    [SerializeField] private ParticleSystem dieParticle; // assign in editor

    [SerializeField] private AudioClip dieSound; // assign in editor

    [SerializeField] private AudioClip jumpSound; // assign in editor

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidBody;

    private Vector3 movement;

    private Kolor kolor;

    private bool moving; // false at start to not apply gravity

    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();

        rigidBody.isKinematic = true;
        rigidBody.bodyType = RigidbodyType2D.Kinematic;

        ChangeColor();
    }

    // Update is called once per frame
    void Update() {
        Move();

        // really dont know if this is the best way to do this, only works with this exact aspect ratio and camera
        if (transform.position.y < Camera.main.transform.position.y - 16) {
            Die();
        }
    }

    // called every frame to move the ball with gravity, also reads for input
    private void Move() {
        if (moving) {
            // Applying gravity. This is multiplied by deltatime, to get accelerating gravity
            movement.y = movement.y + gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // jumping isn't additive like in the original, it resets gravity so just create a new movement vector
            // this also means that the player can't spam the jump to move faster
            movement = new Vector3(0, jumpSpeed, 0);
            moving = true;

            if (jumpSound) {
                AudioSource audioSource = GetComponent<AudioSource>();
                audioSource.PlayOneShot(jumpSound);
            }
        }

        // zeroing these out just to be sure
        movement.x = 0;
        movement.z = 0;

        // at last moving the gameobject with deltatime as not to have the framerate affect the speed
        transform.Translate(movement * Time.deltaTime);
    }

    // called when the color switches
    public void ChangeColor() {
        List<Kolor> possibleKolors = new List<Kolor>();
        foreach (Kolor k in Game.kolorsToColors.Keys) {
            if (k != kolor) {
                possibleKolors.Add(k);
            }
        }
        
        int r = Random.Range(0, possibleKolors.Count);
        kolor = possibleKolors[r];

        Game.kolorsToColors.TryGetValue(kolor, out Color color);
        spriteRenderer.color = color;
    }

    // called when a star is reached
    public void GetStar() {
        Debug.Log("Reached star");
        Game game = FindObjectOfType<Game>();
        game.AddStar();
    }

    // called when the ball is to be destroyed
    public void Die() {
        Debug.Log("Ball died");
        
        if (dieParticle) {
            Instantiate(dieParticle, transform.position, transform.rotation);
        }

        Game game = FindObjectOfType<Game>();
        game.Reset();

        if (dieSound) {
            game.GetComponent<AudioSource>().PlayOneShot(dieSound);
        }

        Destroy(gameObject);
    }

    // getter for the kolor, used by obstacles
    public Kolor GetKolor() {
        return kolor;
    }

    public bool getMoving() {
        return moving;
    }
}
