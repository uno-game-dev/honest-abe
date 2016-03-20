using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationTester : MonoBehaviour
{
    public RuntimeAnimatorController runtimeAnimatorController;
    public GameObject[] models;
    public int numberOfAnimations = 2;
    public float rotationMultiplier = 2;
    public float scaleMultiplier = 0.5f;
    private Animator _animator;
    private int _currentAnimationId;
    private int _currentModelId = -1;
    private Quaternion _localRotationOnStart;
    private Vector3 _localScaleOnStart;
    private Text _modelText;
    private Text _animationText;

    // Use this for initialization
    void Start()
    {
        _localRotationOnStart = transform.localRotation;
        _localScaleOnStart = transform.localScale;
        _modelText = GameObject.Find("ModelName").GetComponent<Text>();
        _animationText = GameObject.Find("AnimationName").GetComponent<Text>();
        NextModel();
    }

    public void NextModel()
    {
        if (models.Length <= 0)
            return;

        _currentModelId = (_currentModelId + 1) % models.Length;

        DestroyPreviousModel();
        LoadCurrentModel();
    }

    public void PreviousModel()
    {
        if (models.Length <= 0)
            return;

        _currentModelId = (_currentModelId - 1) % models.Length;

        DestroyPreviousModel();
        LoadCurrentModel();
    }

    private void LoadCurrentModel()
    {
        _modelText.text = models[_currentModelId].name;
        GameObject model = Instantiate(models[_currentModelId]);
        model.transform.SetParent(transform, false);
        _animator = model.GetComponent<Animator>();
        _animator.runtimeAnimatorController = runtimeAnimatorController;
        _animator.applyRootMotion = false;
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
        if (!_animator)
            return;

        _animator.SetInteger("Animation", _currentAnimationId);
        _animator.SetTrigger("ChangeAnimation");
        _animationText.text = _animator.runtimeAnimatorController.animationClips[_currentAnimationId].name;
    }

    public void NextAnimation()
    {
        if (numberOfAnimations <= 0)
            return;

        _currentAnimationId = (_currentAnimationId + 1) % numberOfAnimations;
        StartAnimation();
    }

    public void PreviousAnimation()
    {
        if (numberOfAnimations <= 0)
            return;

        _currentAnimationId = (_currentAnimationId - 1) % numberOfAnimations;
        StartAnimation();
    }

    public void ResetTransform()
    {
        transform.localRotation = _localRotationOnStart;
        transform.localScale = _localScaleOnStart;
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
