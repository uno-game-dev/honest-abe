using UnityEngine;
using System.Collections;

public class PickupHat : MonoBehaviour
{
    public enum HatType { Regular, Bear, StickyFingers }

    public GameObject abesHat;
    public Material abesRegularHat;
    public Material abesBearHat;
    public Material abesStickFingersHat;
    private Renderer abesHatRenderer;

    private void Start()
    {
        abesHat = this.FindContainsInChildren("Hat");
        if (abesHat)
            abesHatRenderer = abesHat.GetComponent<Renderer>();
    }

    public void SetHat(HatType hatType)
    {
        abesHat.SetActive(true);
        if (hatType == HatType.Bear)
            abesHatRenderer.material = abesBearHat;
        else if (hatType == HatType.StickyFingers)
            abesHatRenderer.material = abesStickFingersHat;
        else // if (hatType == HatType.Regular)
            abesHatRenderer.material = abesRegularHat;
    }

    public void RemoveHat()
    {
        abesHat.SetActive(false);
    }
}