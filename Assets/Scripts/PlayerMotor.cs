using UnityEngine;

[RequireComponent(typeof(BaseCollision))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;
    private float velocityXSmoothing, velocityYSmoothing;
    private BaseCollision collision;

    void Start()
    {
        collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;
    }

    void Update()
    {

        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive) return;

        // Else run the update code
        if (collision.enabled)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            float targetVelX = input.x * GlobalSettings.playerMoveSpeedH;
            float targetVelY = input.y * GlobalSettings.playerMoveSpeedV;

            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, GlobalSettings.playerMovementSmoothing);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelY, ref velocityYSmoothing, GlobalSettings.playerMovementSmoothing);

            collision.Move(velocity * Time.deltaTime);
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
    }

}
