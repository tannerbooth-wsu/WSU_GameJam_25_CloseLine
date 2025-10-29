using UnityEngine;
using System;

public class WalkingSpriteController : MonoBehaviour
{
    private SpriteRenderer r;
    private Texture2D t;
    private Sprite[] s = new Sprite[5];
    private Vector2 velocity;
    private float timer = 0f;

    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        t = (Texture2D)Resources.Load("Images/Character/character_still");
        s[0] = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        t = (Texture2D)Resources.Load("Images/Character/character_left_1");
        s[1] = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        t = (Texture2D)Resources.Load("Images/Character/character_left_2");
        s[2] = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        t = (Texture2D)Resources.Load("Images/Character/character_forward");
        s[3] = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
        t = (Texture2D)Resources.Load("Images/Character/character_backward");
        s[4] = Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }

    void Update()
    {
        // use appropriate left/right/up/down/still sprites
        // if walking, animate by alternating between two sprites every 0.3 seconds
        velocity = GetComponent<Rigidbody2D>().linearVelocity;
        if (Math.Abs(velocity.x) > 0.1f)
        {
            // walking left/right
            timer += Time.deltaTime;
            if(timer > 0.3f)
            {
                timer -= 0.3f;
                r.sprite = (r.sprite == s[1]) ? s[2] : s[1];
            }
            r.flipX = (velocity.x > 0);
        }
        else if (Math.Abs(velocity.y) > 0.1f)
        {
            // walking up/down
            r.sprite = velocity.y < 0 ? s[3] : s[4];
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                timer -= 0.3f;
                r.flipX = !r.flipX;
            }
        }
        else
        {
            // standing still (mostly)
            r.sprite = s[0];
            r.flipX = (velocity.x > 0);
        }
    }
}
