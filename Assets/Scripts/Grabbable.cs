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

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _movementAI = GetComponent<EnemyFollow>();
        _attackAI = GetComponent<InfantryAI>();
        _collision = GetComponent<BaseCollision>();
    }

    private void Update()
    {
        _collision.Tick();
        if (state == State.Grabbed)
            transform.localPosition = new Vector3(1.5f, 0, 0);
    }

    private void OnEnable()
    {
        _collision.OnCollision += OnCollision;
    }

    private void OnDisable()
    {
        _collision.OnCollision -= OnCollision;
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (hit.collider.tag == "Grab")
            GetGrabbed(hit.collider.transform.parent.gameObject);
    }

    private void GetGrabbed(GameObject grabbedBy)
    {
        if (state != State.Null)
            return;

        state = State.Grabbed;
        _animator.SetBool("Grabbed", true);
        _movement.SetDirection(grabbedBy.GetComponent<Movement>().direction, true);
        _movementAI.enabled = false;
        _attackAI.enabled = false;

        grabbedBy.GetComponent<Grabber>().Hold(gameObject);

        _previousParent = transform.parent;
        transform.parent = grabbedBy.transform;
        _grabbedBy = grabbedBy;
    }

    public void Release()
    {
        if (state == State.Null)
            return;

        state = State.Null;
        _animator.SetBool("Grabbed", false);
        _movementAI.enabled = true;
        _attackAI.enabled = true;
        transform.parent = _previousParent;

        if (_grabbedBy)
            _movement.SetDirection(_grabbedBy.GetComponent<Movement>().direction);
        _movement.Flip();

        Grabber grabber;
        if (grabber = GetComponent<Grabber>())
            grabber.Release();

        _grabbedBy = null;
    }
}
