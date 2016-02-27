using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseCollision))]
[RequireComponent(typeof(Movement))]
public class Grabbable : MonoBehaviour
{
    public enum State { Null, Grabbed, Hit, Thrown, Escape }

    public State state;
    private Animator _animator;
    private Movement _movement;
    private EnemyFollow _movementAI;
    private InfantryAI _attackAI;
    private BaseCollision _collision;
    private GameObject _grabbedBy;
    private Transform _previousParent;
    private int _myLayer;
    private CharacterState _characterState;
    private KnockDown _knockDown;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _movementAI = GetComponent<EnemyFollow>();
        _attackAI = GetComponent<InfantryAI>();
        _collision = GetComponent<BaseCollision>();
        _characterState = GetComponent<CharacterState>();
        _knockDown = GetComponent<KnockDown>();
        _myLayer = gameObject.layer;
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

        state = State.Grabbed;
        _animator.SetBool("Grabbed", true);
        _characterState.SetState(CharacterState.State.Grabbed);
        _movement.Move(Vector2.zero);
        _movement.SetDirection(grabbedBy.GetComponent<Movement>().direction, true);
        if (_movementAI) _movementAI.enabled = false;
        if (_attackAI) _attackAI.enabled = false;

        grabbedBy.GetComponent<Grabber>().Hold(gameObject);

        _previousParent = transform.parent;
        transform.parent = grabbedBy.transform;
        _grabbedBy = grabbedBy;

        gameObject.layer = grabbedBy.layer;
        _collision.collisionLayer = _collision.collisionLayer | (1 << LayerMask.NameToLayer("Enemy"));

        if (GetComponentInChildren<AttackArea>())
            GetComponentInChildren<AttackArea>().gameObject.SetActive(false);
    }

    public void Release()
    {
        if (state == State.Null)
            return;

        state = State.Null;
        _animator.SetBool("Grabbed", false);
        _characterState.SetState(CharacterState.State.Idle);
        if (_movementAI) _movementAI.enabled = true;
        if (_attackAI) _attackAI.enabled = true;
        transform.parent = _previousParent;
        gameObject.layer = _myLayer;
        _collision.collisionLayer ^= (1 << LayerMask.NameToLayer("Enemy"));

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
        _animator.SetBool("Grabbed", false);
        if (_movementAI) _movementAI.enabled = true;
        if (_attackAI) _attackAI.enabled = true;
        transform.parent = _previousParent;
        gameObject.layer = _myLayer;
        _collision.collisionLayer ^= (1 << LayerMask.NameToLayer("Enemy"));
        _knockDown.StartKnockDown(_grabbedBy.GetComponent<Movement>().direction == Movement.Direction.Left ? -10 : 10);
    }
}