using System;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private Animator _animator;
    private GameObject _grabbedBy;
    private BaseCollision _collision;
    private Movement _movement;
    private EnemyFollow _movementAI;
    private InfantryAI _attackAI;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _movementAI = GetComponent<EnemyFollow>();
        _attackAI = GetComponent<InfantryAI>();
        _collision = GetComponent<BaseCollision>();
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
        {
            GameObject grabbedBy = hit.collider.transform.parent.gameObject;
            GetGrabbed(grabbedBy);
            grabbedBy.GetComponent<Attack>().FinishGrab(this);
            transform.parent = _grabbedBy.transform;
        }
    }

    private void Update()
    {
        _collision.Tick();
        if (_grabbedBy)
            transform.localPosition = new Vector3(1.5f, 0, 0);
    }

    public void GetGrabbed(GameObject grabbedBy)
    {
        _movement.SetDirection(grabbedBy.GetComponent<Movement>().direction, true);
        _movementAI.enabled = false;
        _attackAI.enabled = false;
        _animator.SetBool("Grabbed", true);
        _grabbedBy = grabbedBy;        
    }

    public void Release()
    {
        _movementAI.enabled = true;
        _attackAI.enabled = true;
        _animator.SetBool("Grabbed", false);
        transform.parent = null;

        if (_grabbedBy)
            _movement.SetDirection(_grabbedBy.GetComponent<Movement>().direction);
        _movement.Flip();
        _grabbedBy = null;
    }
}
