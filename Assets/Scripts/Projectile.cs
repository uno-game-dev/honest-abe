using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    public enum State { Null, InAir, OnGround }

    private static float gravity = -9.81f;
    public float gravityMultiplier = 1;
    public float sign = 1;
    public float velocity = 25;
    public float decelerration = 30;
    public float torque = 20;
    public State state;
    private BaseCollision _collision;

    void Awake()
    {
        _collision = this.GetOrAddComponent<BaseCollision>();
    }

    void OnEnable()
    {
        _collision.OnCollisionEnter += OnCollide;
    }

    void OnDisable()
    {
        state = State.Null;
        _collision.OnCollisionEnter -= OnCollide;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.InAir)
        {
            MoveHorizontal();
            MoveVertical();
        }
    }

    private void OnCollide(Collider2D collider)
    {
        velocity = 0;
        if (collider.GetComponent<Damage>())
            collider.GetComponent<Damage>().ExecuteDamage(collider.gameObject, 30, collider);
        if (collider.GetComponent<Stun>())
            collider.GetComponent<Stun>().GetStunned();
    }

    public void StartProjectile(float velocity = 25)
    {
        if (state == State.InAir)
            return;

        state = State.InAir;
        sign = Mathf.Sign(velocity);
        this.velocity = Mathf.Abs(velocity);
        enabled = true;
    }

    private void MoveHorizontal()
    {
        transform.Translate(sign * velocity * Time.deltaTime, 0, 0);
        //transform.Rotate(0, torque, 0);
        //transform.Rotate(0, 0, torque); // But need to change pivot point

        velocity -= decelerration * Time.deltaTime;
        velocity = Math.Max(velocity, 0);

        if (velocity == 0)
        {
            state = State.OnGround;
            transform.localRotation = Quaternion.identity;
            enabled = false;
        }
    }

    private void MoveVertical()
    {
    }
}
