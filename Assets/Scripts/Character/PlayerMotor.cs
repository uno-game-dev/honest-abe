using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
	public float stepInterval = 0.6f;

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
		Initialize();
	}

	void Update()
	{
		// If the game hasn't officially started yet, don't do any update calls
		if (!UIManager.updateActive) return;

		// Else run the update code
		if (_movement.enabled)
		{
			_velocity = new Vector2(MobileInput.GetAxis("Horizontal") * _movement.horizontalMovementSpeed,
				MobileInput.GetAxis("Vertical") * _movement.vericalMovementSpeed);
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
		}

		else if (collider.tag == "Perk")
		{
			_collidersImOn.Add(collider);
			_controls.ResetHold();
			_uiManager.perkText.enabled = true;
			_uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
		}

		else if (collider.tag == "AbeAxe")
		{
			_controls.ResetHold();
			_collidersImOn.Add(collider);
			_uiManager.perkText.enabled = true;
			_uiManager.perkText.text = collider.GetComponent<Perk>().perkDesc;
		}
	}

	private void OnCollisionUpdate(Collider2D collider)
	{
		if (collider.tag == "Weapon")
		{
			if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked && _playerAttack.emptyHanded)
			{
				EventHandler.SendEvent(EventHandler.Events.WEAPON_PICKUP, collider.gameObject);
				_playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
				collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
			}
		}
        if (collider.tag == "Perk")
        {
            if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked)
            {
                if (collider.gameObject.GetComponent<Perk>().category == Perk.PerkCategory.HAT && PerkManager.activeHatPerk == null)
                {
                    EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                    collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                    if (!_gameManager.perkChosen)
                    {
                        _gameManager.perkChosen = true;
                        _uiManager.perkText.enabled = false;
                    }
                }
                else if (collider.gameObject.GetComponent<Perk>().category == Perk.PerkCategory.TRINKET && PerkManager.activeTrinketPerk == null)
                {
                    EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
                    collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
                    if (!_gameManager.perkChosen)
                    {
                        _gameManager.perkChosen = true;
                        _uiManager.perkText.enabled = false;
                    }
                }
            }
        }
		if (collider.tag == "AbeAxe")
		{
			if (_controls.heldComplete && _collidersImOn.Contains(collider) && _controls.justClicked && _playerAttack.emptyHanded)
			{
				EventHandler.SendEvent(EventHandler.Events.PERK_PICKUP, collider.gameObject);
				_playerAttack.SetWeapon(collider.gameObject.GetComponent<Weapon>());
				collider.GetComponent<BaseCollision>().AddCollisionLayer("Enemy");
				collider.transform.gameObject.GetComponent<Perk>().OnCollision(gameObject);
				_playerAttack.emptyHanded = false;

				if (!_gameManager.perkChosen)
				{
					_gameManager.perkChosen = true;
					_uiManager.perkText.enabled = false;
				}
			}
		}
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
}
