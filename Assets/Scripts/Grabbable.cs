using UnityEngine;
using System.Collections;

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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _movementAI = GetComponent<EnemyFollow>();
        _attackAI = GetComponent<InfantryAI>();
        _collision = GetComponent<BaseCollision>();
        _characterState = GetComponent<CharacterState>();
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

        state = State.Grabbed;
        _animator.SetBool("Grabbed", true);
        _characterState.SetState(CharacterState.State.Grabbed);
        _movement.Move(Vector2.zero);
        _movement.SetDirection(grabbedBy.GetComponent<Movement>().direction, true);
        _movementAI.enabled = false;
        _attackAI.enabled = false;

        grabbedBy.GetComponent<Grabber>().Hold(gameObject);

        _previousParent = transform.parent;
        transform.parent = grabbedBy.transform;
        _grabbedBy = grabbedBy;

        gameObject.layer = grabbedBy.layer;
        GetComponent<BaseCollision>().collisionLayer = GetComponent<BaseCollision>().collisionLayer | (1 << LayerMask.NameToLayer("Enemy"));
    }

    public void Release()
    {
        if (state == State.Null)
            return;

        state = State.Null;
        _animator.SetBool("Grabbed", false);
        _characterState.SetState(CharacterState.State.Idle);
        _movementAI.enabled = true;
        _attackAI.enabled = true;
        transform.parent = _previousParent;
        gameObject.layer = _myLayer;
        GetComponent<BaseCollision>().collisionLayer ^= (1 << LayerMask.NameToLayer("Enemy"));

        if (_grabbedBy)
            _movement.SetDirection(_grabbedBy.GetComponent<Movement>().direction);
        _movement.FlipDirection();

        _grabbedBy = null;
    }
}
