using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseCollision : MonoBehaviour {

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionLayer;
    public CollisionInfo collisionInfo;
    [HideInInspector]
    public Collider2D objCollider;

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

    public void Move(Vector3 vel, Vector2 input)
    {
        UpdateRaycastOrigins();
        collisionInfo.Reset();

        if (vel.x != 0)
            HorizontalCollisions(ref vel);

        if (vel.y != 0)
            VerticalCollisions(ref vel);

        transform.Translate(vel);
    }

    protected abstract void HorizontalCollisions(ref Vector3 vel);

    protected abstract void VerticalCollisions(ref Vector3 vel);

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
