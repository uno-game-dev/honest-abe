using UnityEngine;
using System.Collections;

public class PickupHat : MonoBehaviour
{
    public enum HatType { Regular, Bear, StickyFingers }

    public GameObject abesHat;
    public GameObject abesHand;
    public Material abesRegularHat;
    public Material abesBearHat;
    public Material abesStickFingersHat;
    private Renderer abesHatRenderer;

    private void Start()
    {
        abesHat = this.FindContainsInChildren("Hat");
        abesHand = this.FindContainsInChildren("HumanRigRArmPalm");
        if (abesHat)
            abesHatRenderer = abesHat.GetComponent<Renderer>();
    }

    public void SetHat(HatType hatType, GameObject groundHat, float duration = 1)
    {
        groundHat.transform.SetParent(abesHand.transform);
        groundHat.transform.localPosition = Vector3.zero;
        StartCoroutine(FinishSetHat(hatType, groundHat, duration));
    }

    private IEnumerator FinishSetHat(HatType hatType, GameObject groundHat, float duration)
    {
        yield return new WaitForSeconds(duration);

        groundHat.transform.SetParent(transform);
        groundHat.SetActive(false);

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