using System;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public GameObject hit;
    private BaseCollision _collision;
    private ChainAttack _chainAttack;
    private bool _updateChainAttack;
    private bool alreadyCollided;

    private void Awake()
    {
        _collision = GetComponent<BaseCollision>();
        _chainAttack = GetComponentInParent<ChainAttack>();
        _collision.OnCollision += OnCollision;
    }

    private void OnEnable()
    {
        //_collision.OnCollision += OnCollision;
        _updateChainAttack = true;
    }

    private void OnDisable()
    {
        //_collision.OnCollision -= OnCollision;
        if (!hit && _chainAttack)
            _chainAttack.Miss();
        hit = null;
        alreadyCollided = false;
    }

    private void Update()
    {
        _collision.Tick();
    }

    private void OnCollision(RaycastHit2D hit)
    {
        if (!alreadyCollided) {
            if (transform.parent.gameObject.tag == "Player") {
                Debug.Log("perked");
                PerkManager.PerformPerkEffects();
            }

            if (_updateChainAttack && _chainAttack) {
                _chainAttack.Hit();
                _updateChainAttack = false;
            }
            this.hit = hit.collider.gameObject;
            alreadyCollided = true;
        }
    }
}
