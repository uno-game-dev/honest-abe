using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;
    private Movement movement;
    private BaseCollision collision;
    private PlayerControls controls;
    private bool onWeapon;

    void Start()
    {
        movement = GetComponent<Movement>();
        collision = GetComponent<BaseCollision>();
        collision.OnCollisionEnter += OnCollisionEnter;
        collision.OnCollisionStay += OnCollisionStay;
        collision.OnCollisionExit += OnCollisionExit;
        controls = GetComponent<PlayerControls>();
    }

    void Update()
    {
        // If the game hasn't officially started yet, don't do any update calls
        //if (!UIManager.updateActive) return;

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

    private void OnCollisionEnter(Collider2D collider)
    {
        if (collider.tag == "Item")
        {
            collider.transform.gameObject.GetComponent<Item>().OnCollision(gameObject);
        }

        else if (collider.tag == "Weapon")
        {
            controls.ResetHold();
            onWeapon = true;
        }
    }

    private void OnCollisionStay(Collider2D collider)
    {
        if (collider.tag != "Weapon")
            return;

        if (controls.heldComplete && onWeapon && controls.justClicked)
            GetComponent<Attack>().SetWeapon(collider.gameObject.GetComponent<Weapon>());
    }

    private void OnCollisionExit(Collider2D collider)
    {
        if (collider && collider.tag != "Weapon")
            return;

        controls.ResetHold();
        controls.justClicked = false;
        onWeapon = false;
    }
}