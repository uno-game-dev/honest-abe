using UnityEngine;
using System.Collections;

public class BloodDripper : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    public GameObject trackedGameObject;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (trackedGameObject.activeSelf && particleSystem.isStopped)
            particleSystem.Play();
        else if (!trackedGameObject.activeSelf && particleSystem.isPlaying)
            particleSystem.Stop();
    }
}
