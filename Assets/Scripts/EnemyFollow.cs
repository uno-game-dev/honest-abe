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
[RequireComponent(typeof(BaseCollision))]
public class EnemyFollow : MonoBehaviour
{
    /// <summary>Enumeration of the different Target Types</summary>
    public enum TargetType { Null, Player, Mouse, TargetGameObject }

    /// <summary>Specifies what kind of target this GameObject will follow</summary>
    public TargetType targetType = TargetType.Null;

    /// <summary>The target that is followed if targetType is TargetGameObject</summary>
    public GameObject target;

    /// <summary>The distance in an axis x or y where the GameObject starts slowing down from Maximum Speed</summary>
    public float stopDistanceX = 2;
    public float stopDistanceY = 0;

    /// <summary>The player GameObject when targetType is Player</summary>
    private GameObject _player;

    /// <summary>The Movement Engine</summary>
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
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
        MoveOrStopTowards(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /// <summary>Moves GameObject towards Player</summary>
    private void FollowPlayer()
    {
        if (!_player)
            _player = GameObject.Find("Player");

        if (_player)
            MoveOrStopTowards(_player.transform.position);
    }

    /// <summary>Moves GameObject towards targetGameObject</summary>
    private void FollowTargetGameObject()
    {
        if (target)
            MoveOrStopTowards(target.transform.position);
    }

    /// <summary>Moves GameObject towards a position</summary>
    private void MoveOrStopTowards(Vector2 position)
    {
        Vector3 deltaPosition = position - new Vector2(transform.position.x, transform.position.y);

        if (Mathf.Abs(deltaPosition.x) <= stopDistanceX)
            deltaPosition.x = 0;

        if (Mathf.Abs(deltaPosition.y) <= stopDistanceY)
            deltaPosition.y = 0;

        _movement.Move(deltaPosition);
    }
}
