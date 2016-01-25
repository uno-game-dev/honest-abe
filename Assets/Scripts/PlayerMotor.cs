using UnityEngine;

[RequireComponent(typeof(BaseCollision))]
public class PlayerMotor : MonoBehaviour {

    public float hMoveSpeed = 8, vMoveSpeed = 2;
    public float movementSmoothing = .115f;

    private Vector3 velocity;
    private float velocityXSmoothing, velocityYSmoothing;
    private PlayerCollision collision;

    void Start() {
        collision = GetComponent<PlayerCollision>();
    }

    void Update() {
        if (collision.enabled) {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            float targetVelX = input.x * hMoveSpeed;
            float targetVelY = input.y * vMoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, movementSmoothing);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelY, ref velocityYSmoothing, movementSmoothing);

            collision.Move(velocity * Time.deltaTime, input);
        }
        else {
            velocity.x = 0;
            velocity.y = 0;
            collision.Move(velocity * Time.deltaTime, new Vector2(0, 0));
        }
    }

}
