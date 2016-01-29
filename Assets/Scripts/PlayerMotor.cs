using UnityEngine;

[RequireComponent(typeof(BaseCollision))]
public class PlayerMotor : MonoBehaviour {

    public float hMoveSpeed = 8, vMoveSpeed = 6;
    public float movementSmoothing = .115f;

    private Vector3 velocity;
    private float velocityXSmoothing, velocityYSmoothing;
    private BaseCollision collision;

    void Start() {
        collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;
    }

    void Update() {
        if (collision.enabled) {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            float targetVelX = input.x * hMoveSpeed;
            float targetVelY = input.y * vMoveSpeed;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, movementSmoothing);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelY, ref velocityYSmoothing, movementSmoothing);

            collision.Move(velocity * Time.deltaTime);
        }
        else {
            velocity.x = 0;
            velocity.y = 0;
            collision.Move(velocity * Time.deltaTime);
        }
    }

    private void OnCollision(RaycastHit2D hit) {
        if (hit.collider.tag == "Enemy") {
            Debug.Log("AWAY FROM ME");
        }
    }
    

}
