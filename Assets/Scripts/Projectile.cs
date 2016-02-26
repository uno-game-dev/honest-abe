using UnityEngine;
using System.Collections;
using System;

public class Projectile : MonoBehaviour
{
    public enum State { Null, InAir, OnGround}

    private static float gravity = -9.81f;
    public float gravityMultiplier = 1;
    public float velocity = 25;
    public float decelerration = 30;
    public float torque = 20;
    public State state;

    void OnEnable()
    {
        state = State.InAir;
    }

    void OnDisable()
    {
        state = State.Null;
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

    private void MoveHorizontal()
    {
        Vector3 position = transform.position;
        position.x += velocity * Time.deltaTime;
        transform.position = position;

        transform.Rotate(0, torque, 0);
        //transform.Rotate(0, 0, torque); // But need to change pivot point

        velocity -= decelerration * Time.deltaTime;
        velocity = Math.Max(velocity, 0);

        if (velocity == 0)
            state = State.OnGround;
    }

    private void MoveVertical()
    {
    }
}
