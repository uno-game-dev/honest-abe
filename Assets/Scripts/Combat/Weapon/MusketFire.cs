using UnityEngine;
using System.Collections;

public class MusketFire : MonoBehaviour
{
    public ParticleSystem[] particleSystems;

    public void Fire()
    {
        foreach (var particleSystem in particleSystems)
        {
            GameObject instance = Instantiate(particleSystem.gameObject);
            instance.GetComponent<ParticleSystem>().Play();
            instance.transform.position = particleSystem.transform.position;
            if (GetCharacter(transform))
                if (GetCharacter(transform).transform.localScale.x < 0)
                    if (instance.name.Contains("Flash"))
                        instance.transform.Rotate(0, 180, 0);

            Destroy(instance, 2);
        }
    }

    public GameObject GetCharacter(Transform transform)
    {
        if (transform.GetComponent<CharacterState>())
            return transform.gameObject;
        else if (transform.parent)
            return GetCharacter(transform.parent);
        else
            return null;
    }
}
