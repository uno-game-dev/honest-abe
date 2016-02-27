using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;
    private Movement movement;
    private BaseCollision collision;
    private PlayerControls controls;
    private Attack playerAttack;
    private bool onWeapon;

    private UIManager uiManager;

    void Start()
    {
        movement = GetComponent<Movement>();
        collision = GetComponent<BaseCollision>();
        collision.OnCollisionEnter += OnCollisionStart;
        collision.OnCollisionStay += OnCollisionUpdate;
        collision.OnCollisionExit += OnCollisionEnd;
        controls = GetComponent<PlayerControls>();
        playerAttack = GetComponent<Attack>();
        uiManager = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    void Update()
    {
        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive) return;

        // Else run the update code
        if (movement.enabled)
        {
            if (!collision.collisionInfo.above && !collision.collisionInfo.below && !collision.collisionInfo.right && !collision.collisionInfo.left)
            {
                uiManager.perkText.enabled = false;
            }

            velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 100, Input.GetAxisRaw("Vertical") * 100);
            movement.Move(velocity);
        }
        else
        {
            velocity.x = 0;
            velocity.y = 0;
        }
    }

    private void OnCollisionStart(Collider2D collider)
    {
        if (collider.tag == "Item")
            collider.transform.gameObject.GetComponent<Item>().OnCollision(gameObject);

        else if (collider.tag == "Weapon")
        {
            controls.ResetHold();
            onWeapon = true;
        }

        else if (hit.collider.tag == "Perk")
        {
            if (!justCollided)
            {
                controls.ResetHold();
                justCollided = true;
            }

            if (justCollided && !GameManager.perkChosen)
            {
                uiManager.perkText.enabled = true;
                uiManager.perkText.text = hit.collider.GetComponent<Perk>().perkDesc;
            }

            if (controls.heldComplete && justCollided && controls.justClicked)
            {
                hit.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);

                if (!GameManager.perkChosen)
                {
                    GameManager.perkChosen = true;
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().lockRightEdge = false;
                    uiManager.perkText.enabled = false;
                }
            }
        }

        else if (hit.collider.tag == "AbeAxe")
        {
            if (!justCollided)
            {
                controls.ResetHold();
                justCollided = true;
            }

            if (justCollided && !GameManager.perkChosen)
            {
                uiManager.perkText.enabled = true;
                uiManager.perkText.text = hit.collider.GetComponent<Perk>().perkDesc;
            }

            if (controls.heldComplete && justCollided && controls.justClicked && playerAttack.emptyHanded)
            {
                playerAttack.SetWeapon(hit.collider.gameObject.GetComponent<Weapon>());
                hit.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                playerAttack.emptyHanded = false;

                if (!GameManager.perkChosen)
                {
                    GameManager.perkChosen = true;
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().lockRightEdge = false;
                    uiManager.perkText.enabled = false;
                }
            }
        }
    }

    private void OnCollisionUpdate(Collider2D collider)
    {
        if (collider.tag != "Weapon")
            return;

        if (controls.heldComplete && onWeapon && controls.justClicked)
            GetComponent<Attack>().SetWeapon(collider.gameObject.GetComponent<Weapon>());
    }

    private void OnCollisionEnd(Collider2D collider)
    {
        if (collider && collider.tag != "Weapon")
            return;

        controls.ResetHold();
        controls.justClicked = false;
        onWeapon = false;
    }
}
}