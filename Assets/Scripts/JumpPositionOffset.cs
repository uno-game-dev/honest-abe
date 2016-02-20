﻿using UnityEngine;
using System.Collections;

public class JumpPositionOffset : MonoBehaviour
{
    public float offset;
    public bool isShadow;
    private float previousOffset;
    private Movement jump;

    // Use this for initialization
    void Awake()
    {
        jump = GetComponentInParent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = isShadow && !jump.isGrounded ? 0 : jump.simulatedHeight;
        if (previousOffset != offset)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y -= previousOffset;
            localPosition.y += offset;
            transform.localPosition = localPosition;
            previousOffset = offset;
        }
    }
}
