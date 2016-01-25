using UnityEngine;

[RequireComponent(typeof(PlayerCollision))]
public class PlayerMotor : MonoBehaviour {

    //public float maxJumpHeight = 4; // max units we can jump
    //public float minJumpHeight = 1; // min units we can jump
    //public float timeToJumpApex = .3f; // how long in seconds it takes for us to reach the top of our jump
    public float hMoveSpeed = 8, vMoveSpeed = 2;
    public float movementSmoothing = .115f;
    [HideInInspector] public Vector3 velocity;

    //private float gravity;
    //private float maxJumpVelocity, minJumpVelocity;
    //private float accelerationTimeAirborn = .2f;
    //private float accelerationTimeGrounded = .05f;
    private float velocityXSmoothing, velocityYSmoothing;
    private PlayerCollision collision;

    void Start() {
        collision = GetComponent<PlayerCollision>();

        //gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update() {
        /*
        if (collision.collisionInfo.above || collision.collisionInfo.below) {
            velocity.y = 0;
        }
        */

        if (collision.enabled) {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            /*
            if (Input.GetKeyDown(KeyCode.Space) && collision.collisionInfo.below) {
                velocity.y = maxJumpVelocity;
            }
            if (Input.GetKeyUp(KeyCode.Space)) {
                if (velocity.y > minJumpVelocity)
                    velocity.y = minJumpVelocity;
            }
            */

            float targetVelX = input.x * hMoveSpeed;
            float targetVelY = input.y * vMoveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelX, ref velocityXSmoothing, movementSmoothing);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelY, ref velocityYSmoothing, movementSmoothing);
            //velocity.y += gravity * Time.deltaTime;

            collision.Move(velocity * Time.deltaTime, input);
        }
        else {
            velocity.x = 0;
            //velocity.y += gravity * Time.deltaTime;
            velocity.y = 0;
            collision.Move(velocity * Time.deltaTime, new Vector2(0, 0));
        }
    }

}
