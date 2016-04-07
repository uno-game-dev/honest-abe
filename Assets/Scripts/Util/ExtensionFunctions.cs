using UnityEngine;

public static class ExtensionFunctions
{
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

    public static void PlayAtSpeed(this Animator animator, string stateName, float speed = 1, float normalizedTime = float.NegativeInfinity)
    {
        animator.speed = speed;
        animator.Play(stateName, 0, normalizedTime);
    }

    //public static void PlayInDuration(this Animator animator, string stateName, float duration = 1)
    //{
    //    // TODO: Would be a great helper function
    //}
}
