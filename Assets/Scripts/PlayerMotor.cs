using UnityEngine;

[RequireComponent(typeof(BaseCollision))]
public class PlayerMotor : MonoBehaviour
{

    public float hMoveSpeed = 8, vMoveSpeed = 6;
    public float movementSmoothing = .115f;

    private Vector3 velocity;
    private float velocityXSmoothing, velocityYSmoothing;
    private BaseCollision collision;
    private PlayerControls controls;

    private bool justCollided = false;

    void Start()
    {
        collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;

        controls = GetComponent<PlayerControls>();
    }

    void Update()
    {

        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive) return;

        // Else run the update code
        if (collision.enabled)
        {

            if (!collision.collisionInfo.above && !collision.collisionInfo.below && !collision.collisionInfo.right && !collision.collisionInfo.left)
                justCollided = false;

            float delta = Time.deltaTime;

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            float targetVelX = input.x * hMoveSpeed;
            float targetVelY = input.y * vMoveSpeed;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, movementSmoothing);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelY, ref velocityYSmoothing, movementSmoothing);

            collision.Move(velocity * delta);
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;
            collision.Tick();
        }
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Item")
        {
            hit.transform.gameObject.GetComponent<Item>().OnCollision(gameObject);
        }

        if (hit.collider.tag == "Weapon") {
            if (!justCollided)
            {
                controls.ResetHold();
                justCollided = true;
            }

            if (controls.heldComplete && justCollided && controls.justClicked)
            {
                hit.transform.gameObject.GetComponent<Weapon>().OnCollision(gameObject);
            }
        }
    }



}
