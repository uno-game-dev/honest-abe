using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCollision : MonoBehaviour {

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionLayer;
    public CollisionInfo collisionInfo;
    [HideInInspector]
    public BoxCollider2D boxCollider;
    [HideInInspector]
    public Vector2 playerInput;

    private float horizontalRaySpacing, verticalRaySpacing;
    private RaycastOrigins raycastOrigins;

    const float skinWidth = .015f;

    private struct RaycastOrigins {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    public struct CollisionInfo {
        public bool above, below, left, right;

        public void Reset() {
            above = below = left = right = false;
        }
    }

    public virtual void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start() {
        CalculateRaySpacing();
    }

    public void Move(Vector3 vel, Vector2 input) {
        UpdateRaycastOrigins();
        collisionInfo.Reset();
        playerInput = input;

        if (vel.x != 0)
            HorizontalCollisions(ref vel);

        if (vel.y != 0)
            VerticalCollisions(ref vel);

        transform.Translate(vel);
    }

    void VerticalCollisions(ref Vector3 vel) {
        float directionY = Mathf.Sign(vel.y);
        float rayLength = Mathf.Abs(vel.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) {
                vel.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisionInfo.below = directionY == -1;
                collisionInfo.above = directionY == 1;
            }
        }
    }

    void HorizontalCollisions(ref Vector3 vel) {
        float directionX = Mathf.Sign(vel.x);
        float rayLength = Mathf.Abs(vel.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) {
                vel.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisionInfo.left = directionX == -1;
                collisionInfo.right = directionX == 1;
            }
        }
    }

    void UpdateRaycastOrigins() {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

}
