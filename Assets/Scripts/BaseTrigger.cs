using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseTrigger : MonoBehaviour
{
    public delegate void CollisionHandler(Collider2D collider);
    public event CollisionHandler TriggerEnter = delegate { };
    public event CollisionHandler TriggerExit = delegate { };
    public event CollisionHandler TriggerStay = delegate { };

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionLayer;
    public CollisionInfo collisionInfo;
    [HideInInspector]
    public Collider2D objCollider;
    [HideInInspector]
    public Vector2 playerInput;

    protected float horizontalRaySpacing, verticalRaySpacing;
    protected RaycastOrigins raycastOrigins;

    protected const float skinWidth = .015f;

    protected struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;

        public void Reset()
        {
            above = below = left = right = false;
        }
    }

    public virtual void Awake()
    {
        objCollider = GetComponent<Collider2D>();
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    public void Update()
    {
        Move(Vector3.zero, Vector2.zero);
    }

    private void OnTriggerEnter2D(Collider2D collider) { TriggerEnter(collider); }
    private void OnTriggerExit2D(Collider2D collider) { TriggerExit(collider); }
    private void OnTriggerStay2D(Collider2D collider) { TriggerStay(collider); }

    public virtual void Move(Vector3 vel, Vector2 input)
    {
        playerInput = input;

        UpdateRaycastOrigins();
        collisionInfo.Reset();

        transform.Translate(vel);
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = objCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = objCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

}
