using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class ExtensionFunctions
{
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
}
