using UnityEngine;
using System.Collections;
using BehaviourMachine;

[RequireComponent(typeof(BaseCollision))]
[RequireComponent(typeof(Movement))]
public class Grabbable : MonoBehaviour
{
    public enum State { Null, Grabbed, Hit, Thrown, Escape }

    public State state;
    private Animator _animator;
    private Movement _movement;
    private EnemyFollow _movementAI;
    private StateMachine _attackAI;
    private BaseCollision _collision;
    private GameObject _grabbedBy;
    private Transform _previousParent;
    private int _myLayer;
    private CharacterState _characterState;
    private KnockDown _knockDown;
	private Blackboard _blackboard;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _movementAI = GetComponent<EnemyFollow>();
        _attackAI = GetComponent<StateMachine>();

        _collision = GetComponent<BaseCollision>();
        _characterState = GetComponent<CharacterState>();
        _knockDown = GetComponent<KnockDown>();
        _myLayer = gameObject.layer;
		_blackboard = GetComponent<Blackboard> ();
    }

    private void Update()
    {
        if (state == State.Grabbed)
            transform.localPosition = new Vector3(1.5f, 0, 0);
    }

    private void OnEnable()
    {
        _collision.OnCollisionEnter += OnCollision;
    }

    private void OnDisable()
    {
        _collision.OnCollisionEnter -= OnCollision;

        if (_grabbedBy)
            _grabbedBy.GetComponent<Grabber>().Release();
    }

    private void OnCollision(Collider2D collider)
    {
        if (collider.tag == "Grab")
            GetGrabbed(collider.transform.parent.gameObject);
    }

    private void GetGrabbed(GameObject grabbedBy)
    {
        if (state != State.Null)
            return;

        if (!_characterState.CanBeGrabbed())
            return;

        if (gameObject.tag == "Enemy") EventHandler.SendEvent(EventHandler.Events.ENEMY_GRAB);
        state = State.Grabbed;
        _animator.PlayAtSpeed("Grabbed");
        _characterState.SetState(CharacterState.State.Grabbed);
        _movement.Move(Vector2.zero);
        _movement.SetDirection(grabbedBy.GetComponent<Movement>().direction, true);
        if (_movementAI) _movementAI.enabled = false;
        if (_attackAI) _attackAI.enabled = false;

		// AI stuff: Mark this enemy's position around the player as available
		float attackPosition = _blackboard.GetFloatVar("attackPosition");
		if (attackPosition != -1) {
			string positionVar = "pos" + attackPosition;
			GlobalBlackboard.Instance.GetBoolVar (positionVar).Value = false;
			_blackboard.GetFloatVar ("attackPosition").Value = -1;
		}

        grabbedBy.GetComponent<Grabber>().Hold(gameObject);

        _previousParent = transform.parent;
        transform.SetParent(grabbedBy.transform, true);
        _grabbedBy = grabbedBy;

        gameObject.layer = grabbedBy.layer;
        _collision.AddCollisionLayer("Enemy");

        if (GetComponentInChildren<AttackArea>())
            GetComponentInChildren<AttackArea>().gameObject.SetActive(false);
    }

    public void Release()
    {
        if (state == State.Null)
            return;

        state = State.Null;
        _characterState.SetState(CharacterState.State.Idle);
        if (_movementAI) _movementAI.enabled = true;
        if (_attackAI) _attackAI.enabled = true;
        transform.SetParent(_previousParent);
        gameObject.layer = _myLayer;
        _collision.RemoveCollisionLayer("Enemy");

        if (_grabbedBy)
            _movement.SetDirection(_grabbedBy.GetComponent<Movement>().direction);
        _movement.FlipDirection();

        _grabbedBy = null;
    }

    public void Throw()
    {
        if (state == State.Null)
            return;

        if (!_knockDown)
        {
            Release();
            return;
        }

        state = State.Null;
        if (_movementAI) _movementAI.enabled = true;
        if (_attackAI) _attackAI.enabled = true;
        transform.SetParent(_previousParent);
        gameObject.layer = _myLayer;
        _collision.RemoveCollisionLayer("Enemy");
        _knockDown.StartKnockDown(_grabbedBy.GetComponent<Movement>().direction == Movement.Direction.Left ? -10 : 10);
    }
}