using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float stepInterval = 0.6f;

    private float stepElapsed = 0.0f;
    private Vector3 velocity;
    private Jump jump;
    private Movement movement;
    private BaseCollision collision;
    private PlayerControls controls;
    private Attack playerAttack;
    private List<Collider2D> collidersImOn = new List<Collider2D>();
    private UIManager uiManager;
    private GameManager _gameManager;

    void Start()
    {
        movement = GetComponent<Movement>();
        jump = GetComponent<Jump>();
        collision = GetComponent<BaseCollision>();
        collision.OnCollisionEnter += OnCollisionStart;
        collision.OnCollisionStay += OnCollisionUpdate;
        collision.OnCollisionExit += OnCollisionEnd;
        controls = GetComponent<PlayerControls>();
        playerAttack = GetComponent<Attack>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    void Update()
    {
        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive) return;

        // Else run the update code
        if (movement.enabled)
        {
            velocity = new Vector2(MobileInput.GetAxis("Horizontal") * movement.horizontalMovementSpeed,
                MobileInput.GetAxis("Vertical") * movement.vericalMovementSpeed);
            movement.Move(velocity);

            UpdateStep();
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
        {
            EventHandler.SendEvent(EventHandler.Events.ITEM_PICKUP, collider.gameObject);
            collider.transform.gameObject.GetComponent<Item>().OnCollision(gameObject);
        }

        else if (collider.tag == "Weapon")
        {
            controls.ResetHold();
            collidersImOn.Add(collider);
        }

        else if (collider.tag == "Perk")
        {
            collidersImOn.Add(collider);
            controls.ResetHold();

            if (!_gameManager.perkChosen)
            {
                uiManager.perkText.enabled = true;
                uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
            }
        }

        else if (collider.tag == "AbeAxe")
        {
            controls.ResetHold();
            collidersImOn.Add(collider);

            if (!_gameManager.perkChosen)
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
                EventHandler.SendEvent(EventHandler.Events.WEAPON_PICKUP, collider.gameObject);
                GetComponent<Attack>().SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
            }

        if (collider.tag == "Perk")
            if (controls.heldComplete && collidersImOn.Contains(collider) && controls.justClicked)
            {
                EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP);
                collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);

                if (!_gameManager.perkChosen)
                {
                    _gameManager.perkChosen = true;
                    uiManager.perkText.enabled = false;
                }
            }

        if (collider.tag == "AbeAxe")
            if (controls.heldComplete && collidersImOn.Contains(collider) && controls.justClicked && playerAttack.emptyHanded)
            {
                EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
                collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                playerAttack.emptyHanded = false;

                if (!_gameManager.perkChosen)
                {
                    _gameManager.perkChosen = true;
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

    private void UpdateStep()
    {
        if (movement.state == Movement.State.Walk &&
                (jump.state == Jump.State.Null) &&
                (stepElapsed < stepInterval)
            )
        {
            stepElapsed += Time.deltaTime;
        }

        if(stepInterval < stepElapsed)
        {
            EventHandler.SendEvent(EventHandler.Events.STEP);
            stepElapsed = 0.0f;
        }
            
    }
}