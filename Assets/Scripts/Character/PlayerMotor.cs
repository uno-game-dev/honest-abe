using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public float stepInterval = 0.6f;
    public Weapon savedWeapon;
    public float pickupDuration = 2f;
    [HideInInspector]
    public bool isOnItem;

    private GameManager _gameManager;
    private UIManager _uiManager;
    private Attack _playerAttack;
    private BaseCollision _collision;
    private Jump _jump;
    private Movement _movement;
    private PlayerControls _controls;
    private Vector3 _velocity;
    private List<Collider2D> _collidersImOn = new List<Collider2D>();
    private float _stepElapsed = 0.0f;
    private CharacterState _characterState;
	private Grabber _grabber;
    private Animator _animator;

    void Start()
    {
        _movement = GetComponent<Movement>();
        _jump = GetComponent<Jump>();
        _collision = GetComponent<BaseCollision>();
        _collision.OnCollisionEnter += OnCollisionStart;
        _collision.OnCollisionStay += OnCollisionUpdate;
        _collision.OnCollisionExit += OnCollisionEnd;
        _controls = GetComponent<PlayerControls>();
        _playerAttack = GetComponent<Attack>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _characterState = GetComponent<CharacterState>();
		_grabber = GetComponent<Grabber>();
        _animator = GetComponent<Animator>();
        isOnItem = false;
        Initialize();
    }

    void Update()
    {
        // If the game hasn't officially started yet, don't do any update calls
        if (!UIManager.updateActive || CutsceneManager.cutsceneActive) return;

        // Else run the update code
        if (_movement.enabled)
        {
			float movementMod = (_grabber.state == Grabber.State.Hold || _grabber.state == Grabber.State.Punch) ? _grabber.moveSpeedModifier : 1f;
			_velocity = new Vector2(InputManager.GetAxis("Horizontal") * _movement.horizontalMovementSpeed * movementMod,
				InputManager.GetAxis("Vertical") * _movement.vericalMovementSpeed * movementMod );
            _movement.Move(_velocity);

            UpdateStep();
        }
        else
        {
            _velocity.x = 0;
            _velocity.y = 0;
        }
    }

    public void Initialize()
    {
        _uiManager = GameObject.Find("UI").GetComponent<UIManager>();
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
            _controls.ResetHold();
            _collidersImOn.Add(collider);
            isOnItem = true;
        }

        else if (collider.tag == "Perk")
        {
            _collidersImOn.Add(collider);
            _controls.ResetHold();
            _uiManager.perkText.enabled = true;
            _uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
            isOnItem = true;
        }

        else if (collider.tag == "AbeAxe")
        {
            _controls.ResetHold();
            _collidersImOn.Add(collider);
            _uiManager.perkText.enabled = true;
            _uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
            isOnItem = true;
        }
        else if (collider.tag == "OneUseWeapon")
        {
            _controls.ResetHold();
            _collidersImOn.Add(collider);
            isOnItem = true;
        }
    }

    private void OnCollisionUpdate(Collider2D collider)
    {
        if (collider.tag == "Weapon")
        {
            if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked && _playerAttack.emptyHanded)
            {
                if (!collider.gameObject.GetComponent<Weapon>().isEnemyWeapon || this.FindContainsInChildren("Hat_SF"))
                {
                    EventHandler.SendEvent(EventHandler.Events.WEAPON_PICKUP, collider.gameObject);
                    _playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
                    collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
                    StartPickup();
                }
            }
        }
        if (collider.tag == "Perk")
        {
            if (collider.GetComponent<LockedPerk>() && collider.GetComponent<LockedPerk>().isLocked)
                return;

            if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked)
            {
                if ((collider.gameObject.GetComponent<Perk>().category == Perk.PerkCategory.HAT || collider.gameObject.GetComponent<Perk>().category == Perk.PerkCategory.NONE_HAT) && !PerkManager.hatPerkChosen)
                {
                    EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                    collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                    StartHatPickup();
                }
                else if ((collider.gameObject.GetComponent<Perk>().category == Perk.PerkCategory.TRINKET)
                    && (PerkManager.activeTrinketPerk == null))
                {
                    EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                    collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                    SoundPlayer.Play("Trinket Pickup");
                    StartPickup();
                }
            }
        }
        if (collider.tag == "AbeAxe")
        {
            if (collider.GetComponent<LockedPerk>() && collider.GetComponent<LockedPerk>().isLocked)
                return;

            if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked && _playerAttack.emptyHanded)
            {
                EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                _playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
                collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                _playerAttack.emptyHanded = false;
                SoundPlayer.Play("Axe Pickup");
                StartPickup();
            }
        }
        if (collider.tag == "OneUseWeapon")
        {
            if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked)
            {
                savedWeapon = gameObject.GetComponent<Attack>().weapon;
                if (savedWeapon.name != "Player")
                {
                    savedWeapon.transform.gameObject.SetActive(false);
                }
                _playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
                collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
                _playerAttack.emptyHanded = false;
                StartPickup();
            }
        }
        if (collider.tag == "Enemy")
        {
            //If enemy has a weapon to steal
            if ((collider.GetComponent<ShootAttack>() != null) && (_playerAttack.emptyHanded))
            {
                EventHandler.SendEvent(EventHandler.Events.ENEMY_CLOSE_TO_STEAL_WEAPON, collider.gameObject);
            }
        }

        _controls.heldComplete = false;
    }

    private void StartPickup()
    {
        _movement.SetState(Movement.State.Null);
        _animator.TransitionPlay("Pickup");
        _characterState.SetState(CharacterState.State.Pickup);
        Invoke("FinishPickup", pickupDuration);
    }

    private void StartHatPickup()
    {
        _movement.SetState(Movement.State.Null);
        _animator.TransitionPlay("Pickup Hat");
        _characterState.SetState(CharacterState.State.Pickup);
        Invoke("FinishPickup", pickupDuration);
    }

    private void OnCollisionEnd(Collider2D collider)
    {
        if (_collidersImOn.Contains(collider))
            _collidersImOn.Remove(collider);

        if (collider)
            if (collider.GetComponent<Perk>())
                if (_uiManager.perkText)
                    _uiManager.perkText.enabled = false;

        _controls.ResetHold();
        _controls.justClicked = false;

        if (collider != null && (collider.tag == "Perk" || collider.tag == "Weapon" || collider.tag == "AbeAxe" || collider.tag == "OneUseWeapon"))
            isOnItem = false;
    }

    private void UpdateStep()
    {
        if (_movement.state == Movement.State.Walk &&
                (_jump.state == Jump.State.Null) &&
                (_stepElapsed < stepInterval)
            )
        {
            _stepElapsed += Time.deltaTime;
        }

        if (stepInterval < _stepElapsed)
        {
            EventHandler.SendEvent(EventHandler.Events.STEP);
            _stepElapsed = 0.0f;
        }
    }

    private void FinishPickup()
    {
        if (_characterState.state == CharacterState.State.Pickup)
            _characterState.SetState(CharacterState.State.Idle);
    }
}