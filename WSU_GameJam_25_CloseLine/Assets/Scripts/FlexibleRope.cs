using UnityEngine;
using System.Collections.Generic;

public class FlexibleRope : MonoBehaviour
{
    public Rigidbody2D player1;
    public Rigidbody2D player2;
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 10;
    public float segmentLength = 0.3f;
    public float attachRadius = 0.3f; // distance from center to attach point (player radius)

    private List<Rigidbody2D> segments = new List<Rigidbody2D>();
    private HingeJoint2D joint1, joint2;
    private LineRenderer line;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segmentCount + 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;

        Rigidbody2D prevBody = player1;

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject seg = Instantiate(ropeSegmentPrefab, transform);
            seg.transform.position = Vector3.Lerp(player1.position, player2.position, (float)i / segmentCount);

            Rigidbody2D segRb = seg.GetComponent<Rigidbody2D>();
            HingeJoint2D joint = seg.GetComponent<HingeJoint2D>();
            joint.connectedBody = prevBody;
            joint.autoConfigureConnectedAnchor = false;
            joint.anchor = new Vector2(0, 0);
            joint.connectedAnchor = new Vector2(0, 0);

            segments.Add(segRb);
            prevBody = segRb;
        }

        // connect last segment to player2
        joint2 = player2.gameObject.AddComponent<HingeJoint2D>();
        joint2.connectedBody = segments[segments.Count - 1];
        joint2.autoConfigureConnectedAnchor = false;
        joint2.anchor = Vector2.zero;
        joint2.connectedAnchor = Vector2.zero;

        // Also create a joint from player1 to first segment (for dynamic rotation)
        joint1 = player1.gameObject.AddComponent<HingeJoint2D>();
        joint1.connectedBody = segments[0];
        joint1.autoConfigureConnectedAnchor = false;
        joint1.anchor = Vector2.zero;
        joint1.connectedAnchor = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // draw rope line
        line.SetPosition(0, player1.position);
        for (int i = 0; i < segments.Count; i++)
            line.SetPosition(i + 1, segments[i].position);
        line.SetPosition(segments.Count + 1, player2.position);

        UpdateDynamicAnchors();
    }

    void UpdateDynamicAnchors()
    {
        Vector2 dir1 = (segments[0].position - player1.position).normalized;

        Vector2 dir2 = (segments[segments.Count - 1].position - player2.position).normalized;

        // Smooth anchor movement
        joint1.anchor = Vector2.Lerp(joint1.anchor, dir1 * attachRadius, Time.deltaTime * 15f);
        joint2.anchor = Vector2.Lerp(joint2.anchor, dir2 * attachRadius, Time.deltaTime * 15f);
    }

}
