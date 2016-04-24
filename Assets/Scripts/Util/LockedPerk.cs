using UnityEngine;
using System.Collections;
using System;

public class LockedPerk : MonoBehaviour
{
    public bool isLocked;
    public GameObject lockIcon;

    public void Lock()
    {
        isLocked = true;
        for (int i = transform.childCount - 1; i >= 0; i--)
            Destroy(transform.GetChild(i).gameObject);
        GameObject lo = Instantiate(lockIcon);
        lo.transform.SetParent(transform, false);
    }
}
