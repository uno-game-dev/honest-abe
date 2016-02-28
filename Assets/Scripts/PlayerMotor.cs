using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;
    private Movement movement;
    private BaseCollision collision;
    private PlayerControls controls;
    private Attack playerAttack;
    private List<Collider2D> collidersImOn = new List<Collider2D>();
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
            collidersImOn.Add(collider);
        }

        else if (collider.tag == "Perk")
        {
            collidersImOn.Add(collider);
            controls.ResetHold();

            if (!GameManager.perkChosen)
            {
                uiManager.perkText.enabled = true;
                uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
            }
        }

        else if (collider.tag == "AbeAxe")
        {
            controls.ResetHold();
            collidersImOn.Add(collider);

            if (!GameManager.perkChosen)
            {
                uiManager.perkText.enabled = true;
                uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
            }
        }
    }

    private void OnCollisionUpdate(Collider2D collider)
    {
        if (collider.tag == "Weapon")
            if (controls.heldComplete && collidersImOn.Contains(collider) && controls.justClicked && playerAttack.emptyHanded)
            {
                GetComponent<Attack>().SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
            }

        if (collider.tag == "Perk")
            if (controls.heldComplete && collidersImOn.Contains(collider) && controls.justClicked)
            {
                collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);

                if (!GameManager.perkChosen)
                {
                    GameManager.perkChosen = true;
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().lockRightEdge = false;
                    uiManager.perkText.enabled = false;
                }
            }

        if (collider.tag == "AbeAxe")
            if (controls.heldComplete && collidersImOn.Contains(collider) && controls.justClicked && playerAttack.emptyHanded)
            {
                playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
                collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                playerAttack.emptyHanded = false;

                if (!GameManager.perkChosen)
                {
                    GameManager.perkChosen = true;
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().lockRightEdge = false;
                    uiManager.perkText.enabled = false;
                }
            }
    }

    private void OnCollisionEnd(Collider2D collider)
    {
        if (collidersImOn.Contains(collider))
            collidersImOn.Remove(collider);

        if (collider)
            if (collider.GetComponent<Perk>())
                if (uiManager.perkText)
                    uiManager.perkText.enabled = false;

        controls.ResetHold();
        controls.justClicked = false;
    }
}