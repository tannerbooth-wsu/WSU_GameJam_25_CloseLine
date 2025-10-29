using System;
using TMPro;
using UnityEngine;

public class DualPlayerPhysicsController : MonoBehaviour
{
    [Header("Player References")]
    public Rigidbody2D player1;
    public Rigidbody2D player2;

    [Header("Movement Settings")]
    public float moveForce = 10f;
    public float maxSpeed = 5f;

    [Header("Weight Settings")]
    [Range(0.1f, 5f)] public float player1Weight = 1f;
    [Range(0.1f, 5f)] public float player2Weight = 1f;

    [Header("Jump Drag Settings")]
    public float normalLinearDrag = 15f;
    public float normalAngularDrag = 2f;
    public float jumpLinearDrag = 0f;
    public float jumpAngularDrag = 0f;

    public float snapForce = 50f;
    public float jumpDuration = 1f;

    private bool isJumping = false;

    void FixedUpdate()
    {
        if (!isJumping)
        {
            ApplyMovement(player1, KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, player1Weight);
            ApplyMovement(player2, KeyCode.I, KeyCode.K, KeyCode.J, KeyCode.L, player2Weight);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }

    void ApplyMovement(Rigidbody2D rb, KeyCode up, KeyCode down, KeyCode left, KeyCode right, float weight)
    {
        Vector2 input = new Vector2(
            (Input.GetKey(right) ? 1 : Input.GetKey(left) ? -1 : 0),

            (Input.GetKey(up) ? 1 : Input.GetKey(down) ? -1 : 0)
        );
        if (input.sqrMagnitude > 0.01f)
        {
            Vector2 desiredForce = input.normalized * moveForce / weight;
            rb.AddForce(desiredForce);
        }

        // Clamp speed for control
        if (rb.linearVelocity.magnitude > maxSpeed)
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;

    }
    
    System.Collections.IEnumerator Jump()
    {
        isJumping = true;

        Debug.Log("Jumping: ");

        // Remove drag
        player1.linearDamping = jumpLinearDrag;
        player1.angularDamping = jumpAngularDrag;
        player2.linearDamping = jumpLinearDrag;
        player2.angularDamping = jumpAngularDrag;

        // Snap force toward each other
        Vector2 dir = (player2.position - player1.position).normalized;
        player1.AddForce(dir * snapForce);
        player2.AddForce(-dir * snapForce);
        // Wait
        yield return new WaitForSeconds(jumpDuration);

        // Restore normal damping
        player1.linearDamping = normalLinearDrag;
        player1.angularDamping = normalAngularDrag;
        player2.linearDamping = normalLinearDrag;
        player2.angularDamping = normalAngularDrag;

        isJumping = false;
    }
}
