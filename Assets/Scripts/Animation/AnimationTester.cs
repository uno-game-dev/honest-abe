using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnimationTester : MonoBehaviour
{
    public RuntimeAnimatorController runtimeAnimatorController;
    public GameObject[] models;
    public float rotationMultiplier = 2;
    public float scaleMultiplier = 0.5f;
    private Animator animator;
    private int currentAnimationId;
    private int currentModelId = -1;
    private Quaternion localRotationOnStart;
    private Vector3 localScaleOnStart;
    private Text modelText;
    private Text animationText;
    public List<AnimationGroup> animationClips;
    private int baseLayer = 0;

    // Use this for initialization
    void Start()
    {
        localRotationOnStart = transform.localRotation;
        localScaleOnStart = transform.localScale;
        modelText = GameObject.Find("ModelName").GetComponent<Text>();
        animationText = GameObject.Find("AnimationName").GetComponent<Text>();
        NextModel();
    }

    public void NextModel()
    {
        if (models.Length <= 0)
            return;

        currentModelId = (currentModelId + 1) % models.Length;

        DestroyPreviousModel();
        LoadCurrentModel();
    }

    public void PreviousModel()
    {
        if (models.Length <= 0)
            return;

        currentModelId = (currentModelId - 1) % models.Length;
        currentModelId = currentModelId < 0 ? models.Length - 1 : currentModelId;

        DestroyPreviousModel();
        LoadCurrentModel();
    }

    private void LoadCurrentModel()
    {
        modelText.text = models[currentModelId].name;
        GameObject model = Instantiate(models[currentModelId]);
        model.transform.SetParent(transform, false);
        animator = model.GetComponent<Animator>();
        animator.runtimeAnimatorController = runtimeAnimatorController;
        animator.applyRootMotion = false;
        StartAnimation();
    }

    private void DestroyPreviousModel()
    {
        if (transform.childCount > 0)
        {
            GameObject toBeDestroyed = transform.GetChild(0).gameObject;
            toBeDestroyed.transform.SetParent(null);
            Destroy(toBeDestroyed);
        }
    }

    public void StartAnimation()
    {
        if (!animator)
            return;

        var animationClip = animationClips[currentAnimationId];
        animator.CrossFade(animationClip.name, 0.1f, baseLayer, 0);
        animationText.text = string.Format("<{0}> {1}: {2}", animationClip.substate, animationClip.name, animationClip.animationClip.name);
    }

    public void NextAnimation()
    {
        if (animationClips.Count <= 0)
            return;

        currentAnimationId = (currentAnimationId + 1) % animationClips.Count;
        StartAnimation();
    }

    public void PreviousAnimation()
    {
        if (animationClips.Count <= 0)
            return;

        currentAnimationId = (currentAnimationId - 1) % animationClips.Count;
        currentAnimationId = currentAnimationId < 0 ? animationClips.Count - 1 : currentAnimationId;
        StartAnimation();
    }

    public void ResetTransform()
    {
        transform.localRotation = localRotationOnStart;
        transform.localScale = localScaleOnStart;
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
            transform.Rotate(Vector3.down, Input.GetAxis("Horizontal") * rotationMultiplier);

        if (Input.GetAxis("Vertical") != 0 && transform.localScale.x >= 0.1f)
            transform.localScale += Input.GetAxis("Vertical") * scaleMultiplier * Vector3.one;

        if (transform.localScale.x < 0.1f)
            transform.localScale = Vector3.one * 0.1f;
    }
}
