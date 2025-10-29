using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemyColisionBlue : MonoBehaviour
{
    [Header("Player References")]
    public Rigidbody2D body;

    [Header("Movement Settings")]
    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public float angle;

    [Header("Weight Settings")]
    [Range(0.1f, 5f)] public float player1Weight = 1f;
    [Range(0.1f, 5f)] public float player2Weight = 1f;


    static System.Random rand = new System.Random(System.DateTime.Now.Millisecond);
    bool rotated = false;
    AudioSource audioPlayer;
    private EnemyMovement gameFuncs;
    private bool gaveScoreOut = false;

    private void Start()
    {
        body = this.gameObject.GetComponent<Rigidbody2D>();
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.clip = (AudioClip)Resources.Load("Damage Sounds/" + rand.Next(1, 23) + ". Damage Grunt (Male)");
        StartCoroutine(ChangeAngle());
        gameFuncs = GameObject.Find("EnemyMovement").GetComponent<EnemyMovement>();
        StartCoroutine(GarbageCollect());
    }

    void FixedUpdate()
    {
        ApplyMovement(body, player1Weight);
    }

    IEnumerator GarbageCollect()
    {
        yield return new WaitForSeconds(50);
        Object.Destroy(this.gameObject);
    }


    IEnumerator ChangeAngle()
    {
        yield return new WaitForSeconds(2);
        if (rotated)
        {
            rotated = false;
            angle -= 50;
        }
        else
        {
            rotated = true;
            angle += 50;
        }
        StartCoroutine(ChangeAngle());
    }

    void ApplyMovement(Rigidbody2D rb, float weight)
    {

        Vector2 input = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        if (input.sqrMagnitude > 0.01f)
        {
            Vector2 desiredForce = input.normalized * moveForce / weight;
            rb.AddForce(desiredForce);
        }

        // Clamp speed for control
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.ToString().Substring(0, 4) == "Rope")
        {
            Camera camera = Camera.main;
            if (camera != null)
            {
                AudioSource sfxplayer = camera.GetComponent<AudioSource>();
                sfxplayer.clip = audioPlayer.clip;
                sfxplayer.Play();
            }

            //increment the score here
            if (!gaveScoreOut)
            {
                gaveScoreOut = true;
                gameFuncs.IncrementScore();
            }

            Object.Destroy(this.gameObject);
        }
        else if (collision.rigidbody.ToString().Substring(0, 4) == "Play")
        {
            Camera camera = Camera.main;
            if (camera != null)
            {
                AudioSource sfxplayer = camera.GetComponent<AudioSource>();
                sfxplayer.clip = (AudioClip)Resources.Load("Damage");
                sfxplayer.Play();
            }

            //damage the player.
            HealthManager.Instance.TakeDamage(10);
            Object.Destroy(this.gameObject);

            //this line of code currently ends the game when a player gets hit by an enemy
            //gameFuncs.EndGame();

        }
    }
}
