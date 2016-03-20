using UnityEngine;
using System.Collections;

public class ModelLoader : MonoBehaviour
{
    public GameObject model;
    public Vector3 localPosition = new Vector3(0, -0.5f, 0);
    public Vector3 localRotation = new Vector3(0, 145, 0);
    public Vector3 localScale = new Vector3(3, 3, 3);
    public GameObject loadedModel;
    public RuntimeAnimatorController runtimeAnimationController;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.avatar = null;
    }

    void OnEnable()
    {
        DestroyLoadedModel(true);

        if (!model)
            return;

        loadedModel = Instantiate(model);
        loadedModel.transform.SetParent(transform);
        loadedModel.name = model.name;
        loadedModel.transform.localPosition = localPosition;
        loadedModel.transform.localEulerAngles = localRotation;
        loadedModel.transform.localScale = localScale;

        Animator modelAnimator = loadedModel.GetComponent<Animator>();
        Avatar avatar = null;
        if (_animator && modelAnimator)
            avatar = modelAnimator.avatar;

        DestroyImmediate(modelAnimator);
        _animator.avatar = avatar;
    }

    void OnDisable()
    {
        DestroyLoadedModel();
    }

    void DestroyLoadedModel(bool destroyImmediately = false)
    {
        if (loadedModel)
        {
            if (destroyImmediately)
                DestroyImmediate(loadedModel);
            else
                Destroy(loadedModel);
        }

        loadedModel = null;
        _animator.avatar = null;
    }
}
