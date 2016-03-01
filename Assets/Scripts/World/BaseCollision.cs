using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/**
 * BaseCollision is the class that all objects that will need to trigger a collision with other objects will need.
 * Objects that will only receive collisions and not do the triggering will NOT need BaseCollision.
 * In order for an object to use BaseCollision, it must have a Collider2D - any 2D collider will work.
 **/ 

[RequireComponent(typeof(Collider2D))]
public class BaseCollision : MonoBehaviour
{

    /*
     * The state of the current collision
     * Null - no collision
     * CollisionStart - the first frame the collision happened
     * CollisionStay - each frame the collision is happening after the start
     * CollisionEnd - the final frame the collision happened
     */ 
    public enum State { Null, CollisionStart, CollisionStay, CollisionEnd }

    // The velocity of the parent object
    private Vector2 velocity;
    public Vector2 Velocity
    {
        get { return velocity; }
    }

    // The number of horizontal rays that the object will use to detect collisions
    public int horizontalRayCount = 4;

    // The number of vertical rays that the object will use to detect collisions
    public int verticalRayCount = 4;

    // The LayerMask that will be used to check for collisions against
    public LayerMask collisionLayer;

    // Our instance of an internal struct used to get if a collision is happening on any of the cardinal directions of the object
    public CollisionInfo collisionInfo;

    // The total number of collisions that this object is currently taking part in
    public int numberOfCollisions;

    /*
     * The delegates used to "fill in the blanks" for what events will be fired at different stages of the collision
     * OnCollisionStay - what happens each frame of the collision
     * OnCollisionEnter - what happens the first frame the collision happens
     * OnCollisionExit - what happens the final frame the collision happens
     */ 
    public delegate void CollisionHandler(Collider2D collider);
    public event CollisionHandler OnCollisionStay = delegate { };
    public event CollisionHandler OnCollisionEnter = delegate { };
    public event CollisionHandler OnCollisionExit = delegate { };

    /*
     * Horizontal Ray Spacing and Vertical Ray Spacing determines how far apart each ray of the raycasts are placed
     * They are not manually set and are instead calculated based on how many horizontal / vertical rays there are
     * This ensures that collisions are being checked for evenly across the object, minimizing the chance of missing a collision
     */ 
    private float horizontalRaySpacing, verticalRaySpacing;

    // A reference to the Collider2D on the object
    private Collider2D _collider;

    /*
     * The spot on the object that we will start casting rays from
     * This is primarily for moving objects so that we only check for collisions in the direction we're moving
     * This makes the checks more efficient since we don't have to worry about triggering collisions in the direction
     *     we are not currently moving in
     */
    private RaycastOrigins raycastOrigins;

    // A list of all of the current collisions with the object and the state those collisions are in
    public Dictionary<Collider2D, State> _currentCollisions = new Dictionary<Collider2D, State>();

    // Essentially, the size of the rays being cast
    private const float skinWidth = .015f;

    // Struct associated with raycastOrigins
    private struct RaycastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }

    // Struct associated with collisionInfo
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
        // On start, calculate the spacing between the rays based on how many rays are being cast
        CalculateRaySpacing();
    }

    void OnDrawGizmos()
    {
        // Draw a circle around the object in the editor for debugging
        Gizmos.DrawWireSphere(transform.position, 1);
    }

    void Update()
    {
        // Get an array of all of the objects currently being collided with
        Vector2 pointA = new Vector2(transform.position.x, transform.position.y) + _collider.offset - (Vector2)_collider.bounds.size / 2;
        Vector2 pointB = new Vector2(transform.position.x, transform.position.y) + _collider.offset + (Vector2)_collider.bounds.size / 2;
        Collider2D[] currentCollisions = Physics2D.OverlapAreaAll(pointA, pointB, collisionLayer);

        // Go through each collider that we're colliding with and run the AddCollision method using each collider
        foreach (Collider2D collider in currentCollisions)
            AddCollision(collider);

        // Go through each collider that we're colliding with and run the OnCollisionStay method using each collider
        foreach (Collider2D collider in _currentCollisions.Keys)
            if (collider)
                OnCollisionStay(collider);

        // Go through each collider that we're colliding with and run the RemoveCollision method using each collider
        for (int i = _currentCollisions.Keys.Count - 1; i >= 0; i--)
        {
            Collider2D collider = _currentCollisions.Keys.ElementAt(i);
            if (collider)
                if (!currentCollisions.Contains(collider))
                    RemoveCollision(collider);
        }
    }

    void OnDisable()
    {
        // If we disable the BaseCollision component, remove all current collisions
        for (int i = _currentCollisions.Keys.Count - 1; i >= 0; i--)
            RemoveCollision(_currentCollisions.Keys.ElementAt(i));
    }

    public void Move(Vector3 vel)
    {
        velocity = vel;

        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 1, collisionLayer);

        if (hitCollider != null)
        {
            UpdateRaycastOrigins();
            collisionInfo.Reset();
            if (vel.x != 0)
                HorizontalCollisions(ref vel);

            if (vel.y != 0)
                VerticalCollisions(ref vel);
        }

        transform.Translate(vel);
    }

    private void HorizontalCollisions(ref Vector3 vel)
    {
        float directionX = Mathf.Sign(vel.x);
        float rayLength = Mathf.Abs(vel.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                if (!hit.collider.isTrigger)
                    vel.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
                collisionInfo.left = directionX == -1;
                collisionInfo.right = directionX == 1;
            }
        }
    }

    private void VerticalCollisions(ref Vector3 vel)
    {
        float directionY = Mathf.Sign(vel.y);
        float rayLength = Mathf.Abs(vel.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + vel.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionLayer);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                if (!hit.collider.isTrigger)
                    vel.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisionInfo.below = directionY == -1;
                collisionInfo.above = directionY == 1;
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

    private void AddCollision(Collider2D collider)
    {
        if (collider == _collider) return;

        if (!_currentCollisions.ContainsKey(collider))
        {
            _currentCollisions.Add(collider, State.CollisionStart);
            OnCollisionEnter(collider);
            numberOfCollisions++;
        }
    }

    private void RemoveCollision(Collider2D collider)
    {
        if (collider == _collider) return;

        if (_currentCollisions.ContainsKey(collider))
        {
            _currentCollisions[collider] = State.CollisionEnd;
            OnCollisionExit(collider);
            _currentCollisions.Remove(collider);
            numberOfCollisions--;
        }
    }

    public void AddCollisionLayer(string name)
    {
        collisionLayer |= (1 << LayerMask.NameToLayer(name));
    }

    public void RemoveCollisionLayer(string name)
    {
        collisionLayer &= ~(1 << LayerMask.NameToLayer(name));
    }
}
