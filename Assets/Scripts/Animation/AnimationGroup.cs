using UnityEngine;
using System;

[Serializable]
public class AnimationGroup {
    public string substate;
    public string name;
    public AnimationClip animationClip;
    public AnimatorClipInfo animatorClipInfo;
    public AnimatorStateInfo animatorStateInfo;
}