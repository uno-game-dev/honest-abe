using UnityEngine;
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
    private float _startXPos;
    private float _endXPos;
    private int _damage;
    private int _distance;

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
        _endXPos = transform.position.x;
        velocity = 0;
        if (collider.GetComponent<Stun>())
            collider.GetComponent<Stun>().GetStunned();
        if (collider.GetComponent<Damage>())
        {
            _distance = Math.Abs((int)(_startXPos - _endXPos));
            _damage = 10 - (_distance / 2);
            if (_damage < 0)
                _damage = 0;
            Debug.Log("Projectile Damge: " + _damage);
            collider.GetComponent<Damage>().ExecuteDamage(_damage, collider);
        }
    }

    public void StartProjectile(float velocity = 25)
    {
        if (state == State.InAir)
            return;

        _startXPos = transform.position.x;
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
