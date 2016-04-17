using UnityEngine;
using System.Collections;

public class MusketFire : MonoBehaviour {
    public ParticleSystem[] particleSystems;

    public void Fire()
    {
        foreach (var particleSystem in particleSystems)
        {
            GameObject instance = Instantiate(particleSystem.gameObject);
            instance.GetComponent<ParticleSystem>().Play();
            instance.transform.position = particleSystem.transform.position;
            Destroy(instance, 2);
        }
    }
}
