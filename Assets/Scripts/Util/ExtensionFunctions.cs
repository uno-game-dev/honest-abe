using UnityEngine;

public static class ExtensionFunctions
{
    public enum EaseType
    {
        EaseInOut,
        EaseOut,
        EaseIn,
        Linear
    }

    private static System.Random random = new System.Random();

    public static T GetOrAddComponent<T>(this Component component) where T : Component
    {
        T comp = component.GetComponent<T>();
        if (!comp)
            comp = component.gameObject.AddComponent<T>();
        return comp;
    }

    public static GameObject FindInChildren(this Component component, string name)
    {
        for (int i = 0; i < component.transform.childCount; i++)
        {
            GameObject child = component.transform.GetChild(i).gameObject;
            if (name == child.name)
                return child;
        }
        return null;
    }

    public static GameObject FindContainsInChildren(this Component component, string name)
    {
        for (int i = 0; i < component.transform.childCount; i++)
        {
            GameObject child = component.transform.GetChild(i).gameObject;
            if (child.name.Contains(name))
                return child;
        }

        for (int i = 0; i < component.transform.childCount; i++)
        {
            GameObject child = component.transform.GetChild(i).gameObject;
            GameObject childsChild = child.transform.FindContainsInChildren(name);
            if (childsChild)
                return childsChild;
        }

        return null;
    }

    public static T FindComponent<T>(this Component component) where T : Component
    {
        for (int i = 0; i < component.gameObject.GetComponents<Component>().Length; i++)
        {
            Component child = component.gameObject.GetComponents<Component>()[i];
            if (child is T)
                return (child as T);
        }
        return null;
    }

    public static AnimationClip GetAnimationClip(this Animator animator, string clipName)
    {
        foreach (var animationClip in animator.runtimeAnimatorController.animationClips)
            if (animationClip.name == clipName)
                return animationClip;

        return null;
    }

    public static int Random(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public static GameObject[] FindGameObjectsWithLayer(int layer)
    {
        var goArray = GameObject.FindObjectsOfType<GameObject>();

        var goList = new System.Collections.Generic.List<GameObject>();
        for (var i = 0; i < goArray.Length; i++)
            if (goArray[i].layer == layer)
                goList.Add(goArray[i]);

        return goList.ToArray();
    }

    public static float ClipLength(this Animator animator, string clipName)
    {
        AnimationClip clip = animator.GetAnimationClip(clipName);
        if (clip)
            return clip.length;

        return -1;
    }

    public static void TransitionPlay(this Animator animator, string stateName, float transitionSeconds = 0.1f, float normalizedTime = 0)
    {
        float stateLength = animator.GetCurrentAnimatorStateInfo(0).length;
        float transitionDuration = transitionSeconds / stateLength;
        animator.CrossFade(stateName, transitionDuration, -1, normalizedTime);
    }

    public static float EaseFromTo(float start, float end, float value, EaseType type = EaseType.EaseInOut)
    {
        switch (type)
        {
            case EaseType.EaseInOut:
                return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));

            case EaseType.EaseOut:
                return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));

            case EaseType.EaseIn:
                return Mathf.Lerp(start, end, 1.0f - Mathf.Cos(value * Mathf.PI * 0.5f));

            default:
                return Mathf.Lerp(start, end, value);
        }
    }
}
