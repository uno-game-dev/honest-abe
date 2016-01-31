using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Enemy Follow
/// 
/// This Script moves the attached GameObject towards one of the following targets:
/// - Mouse
/// - Player
/// - TargetGameObject (specified by User)
/// 
/// Author: Edward Thomas Garcia
/// </summary>
public class EnemyFollow : MonoBehaviour
{
    /// <summary>Maximum Horizontal Movement Speed for GameObject</summary>
    public float horizontalMoveSpeed = 6;

    /// <summary>Maximum Vertical Movement Speed for GameObject</summary>
    public float verticalMoveSpeed = 4;

    /// <summary>Enumeration of the different Target Types</summary>
    public enum TargetType { Null, Player, Mouse, TargetGameObject }

    /// <summary>Specifies what kind of target this GameObject will follow</summary>
    public TargetType targetType = TargetType.Null;

    /// <summary>The target that is followed if targetType is TargetGameObject</summary>
    public GameObject target;

    /// <summary>The distance in an axis x or y where the GameObject starts slowing down from Maximum Speed</summary>
    public float stoppingDistance = 2;

    /// <summary>The player GameObject when targetType is Player</summary>
    private GameObject _player;

    private Rigidbody2D _rigidbody;

    public Vector3 velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>Update is called once per frame</summary>
    private void Update()
    {
        if (targetType == TargetType.Mouse)
            FollowMouse();
        else if (targetType == TargetType.Player)
            FollowPlayer();
        else if (targetType == TargetType.TargetGameObject)
            FollowTargetGameObject();
    }

    /// <summary>Moves GameObject towards Mouse</summary>
    private void FollowMouse()
    {
        MoveTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /// <summary>Moves GameObject towards Player</summary>
    private void FollowPlayer()
    {
        if (!_player)
            _player = GameObject.Find("Player");

        if (_player)
            MoveTowards(_player.transform.position);
    }

    /// <summary>Moves GameObject towards targetGameObject</summary>
    private void FollowTargetGameObject()
    {
        if (target)
            MoveTowards(target.transform.position);
    }

    /// <summary>Moves GameObject towards a position</summary>
    private void MoveTowards(Vector2 position)
    {
        Vector3 targetVelocity = position - new Vector2(transform.position.x, transform.position.y);

        if (Mathf.Abs(targetVelocity.x) > stoppingDistance)
            targetVelocity.x = Mathf.Sign(targetVelocity.x) * horizontalMoveSpeed;
        if (Mathf.Abs(targetVelocity.y) > stoppingDistance)
            targetVelocity.y = Mathf.Sign(targetVelocity.y) * verticalMoveSpeed;

        velocity = targetVelocity;
        _rigidbody.velocity = targetVelocity;
    }
}
