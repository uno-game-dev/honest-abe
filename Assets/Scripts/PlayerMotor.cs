using UnityEngine;

[RequireComponent(typeof(BaseCollision))]
public class PlayerMotor : MonoBehaviour
{

    public float hMoveSpeed = 8, vMoveSpeed = 6;
    public float movementSmoothing = .115f;

    private Vector3 velocity;
    private float velocityXSmoothing, velocityYSmoothing;
    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {

        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive) return;

        // Else run the update code
        if (movement.enabled)
        {
            velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 100, Input.GetAxisRaw("Vertical") * 100);
            movement.Move(velocity);
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;
        }
    }
}
