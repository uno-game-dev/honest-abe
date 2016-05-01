using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public enum State { Null, InAir, OnGround }
    public enum SpinDirection { Clockwise, Counterclockwise, DoNotSpin }

    private static float gravity = -9.81f;
    public float gravityMultiplier = 1;
    public float sign = 1;
    public float velocity = 25;
    public float decelerration = 30;
    public float torque = 20;
    public SpinDirection spinDirection;
    public State state;
    private BaseCollision _collision;
    private float _startXPos;
    private float _endXPos;
	private int _damage;
    private int _throwDamage;
    private int _distance;
    private Transform _child;

    void Awake()
    {
        _collision = this.GetOrAddComponent<BaseCollision>();
        _child = transform.GetChild(0);
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
        SoundPlayer.Play("Axe Throw Hits Target");
        if (collider.GetComponent<Stun>())
            collider.GetComponent<Stun>().GetStunned();
        if (collider.GetComponent<Damage>())
        {
            _distance = Math.Abs((int)(_startXPos - _endXPos));
            _throwDamage = 10 - (_distance / 2);
            if (_throwDamage < 0)
                _throwDamage = 0;
			Debug.Log("Projectile Damge: " + (_damage + _throwDamage));
			collider.GetComponent<Damage>().ExecuteDamage( (_throwDamage + _damage), collider);
        }
    }

	public void StartProjectile(float velocity = 25, int baseDamage = 0)
    {
        if (state == State.InAir)
            return;
		
        _startXPos = transform.position.x;
		_damage = baseDamage;
		state = State.InAir;
        sign = Mathf.Sign(velocity);
        this.velocity = Mathf.Abs(velocity);
        enabled = true;
    }

    private void MoveHorizontal()
    {
        transform.Translate(sign * velocity * Time.deltaTime, 0, 0);
        _child.RotateAround(transform.position + new Vector3(0, 0.5f), Vector3.back, GetSpinDirection() * torque);

        velocity -= decelerration * Time.deltaTime;
        velocity = Math.Max(velocity, 0);

        if (velocity == 0)
        {
            state = State.OnGround;
            enabled = false;
        }
    }

    private void MoveVertical()
    {
    }

    private float GetSpinDirection()
    {
        if (spinDirection == SpinDirection.Clockwise)
            return 1;
        else if (spinDirection == SpinDirection.Counterclockwise)
            return -1;
        else
            return 0;
    }
}
