using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseCollision : MonoBehaviour {

    public enum State { Null, CollisionStart, CollisionStay, CollisionEnd }

    private Vector2 velocity;
    public Vector2 Velocity {
        get { return velocity; }
    }

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionLayer;
    public CollisionInfo collisionInfo;
    public State state;
    [HideInInspector] public Collider2D _collider;

    public delegate void CollisionHandler(RaycastHit2D hit);
    public event CollisionHandler OnCollisionStay = delegate { };
    public event CollisionHandler OnCollisionEnter = delegate { };
    public event CollisionHandler OnCollisionExit = delegate { };

    private float horizontalRaySpacing, verticalRaySpacing;
    private RaycastOrigins raycastOrigins;
    private Dictionary<Collider2D, State> _collisionStates = new Dictionary<Collider2D, State>();
    private Dictionary<Collider2D, > _collisionStates = new Dictionary<Collider2D, State>();

    private const float skinWidth = .015f;

    private struct RaycastOrigins
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

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        CalculateRaySpacing();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    private void OnDisable()
    {
        state = State.Null;
    }

    private void Update()
    {
        Tick();
        LaunchCollisionEvents();
    }

    public void Tick() {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 1, collisionLayer);

        if (hitCollider != null) {
            UpdateRaycastOrigins();
            collisionInfo.Reset();

            Vector3 vel;

            vel = new Vector3(skinWidth, 0, 0);
            HorizontalCollisions(ref vel);

            vel = new Vector3(-skinWidth, 0, 0);
            HorizontalCollisions(ref vel);

            vel = new Vector3(0, skinWidth, 0);
            VerticalCollisions(ref vel);

            vel = new Vector3(0, -skinWidth, 0);
            VerticalCollisions(ref vel);
        }
    }

    public void Move(Vector3 vel) {
        velocity = vel;

        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 1, collisionLayer);

        if (hitCollider != null) {
            UpdateRaycastOrigins();
            collisionInfo.Reset();
            if (vel.x != 0)
                HorizontalCollisions(ref vel);

            if (vel.y != 0)
                VerticalCollisions(ref vel);
        }

        transform.Translate(vel);
    }

    private void HorizontalCollisions(ref Vector3 vel) {
        float directionX = Mathf.Sign(vel.x);
        float rayLength = Mathf.Abs(vel.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit) {
                if (!hit.collider.isTrigger)
                    vel.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisionInfo.left = directionX == -1;
                collisionInfo.right = directionX == 1;

                LaunchCollisionEvents(hit);
            }
        }
    }

    private void VerticalCollisions(ref Vector3 vel) {
        float directionY = Mathf.Sign(vel.y);
        float rayLength = Mathf.Abs(vel.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit) {
                if (!hit.collider.isTrigger)
                    vel.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisionInfo.below = directionY == -1;
                collisionInfo.above = directionY == 1;

                LaunchCollisionEvents(hit);
            }
        }
    }

    private void UpdateRaycastOrigins()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private bool HasCollided()
    {
        return collisionInfo.above || collisionInfo.below || collisionInfo.left || collisionInfo.right;
    }

    private void LaunchCollisionEvents(RaycastHit2D hit)
    {
        State state = GetCollisionState(hit);
        if (HasCollided())
        {
            if (state == State.Null)
            {
                OnCollisionEnter(hit);
                state = State.CollisionStart;
            }
            else if (state == State.CollisionStart)
                state = State.CollisionStay;

            OnCollisionStay(hit);
        }
        else
        {
            if (state == State.CollisionStay)
            {
                OnCollisionExit(hit);
                state = State.CollisionEnd;
            }
            else if (state == State.CollisionEnd)
                state = State.Null;
        }
    }

    private State GetCollisionState(RaycastHit2D hit)
    {
        if (!_collisionStates.ContainsKey(hit.collider))
            _collisionStates.Add(hit.collider, State.Null);
    }
}
