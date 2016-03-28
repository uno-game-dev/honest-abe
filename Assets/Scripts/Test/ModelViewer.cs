using UnityEngine;
using System.Collections;
using System;

/// <summary> Creates 4 Models to view from all angles </summary>
public class ModelViewer : MonoBehaviour
{
    public enum InputType { Null, Mouse, Touch }

    public GameObject[] models;
    private int modelIndex;
    private Vector3 _localPosition;
    private Quaternion _localRotation;
    public float CameraDragMultiplier = 0.004f;

    private GameObject topModel;
    private GameObject bottomModel;
    private GameObject leftModel;
    private GameObject rightModel;
    private GameObject frontModel;
    private GameObject backModel;
    private Vector3 _startPosition;
    private InputType _inputType;
    private Vector3 _startCameraPosition;

    // Use this for initialization
    void Start()
    {
        _localPosition = transform.localPosition;
        _localRotation = transform.localRotation;
        LoadCurrentModel();
    }

    public void NextModel()
    {
        if (models.Length <= 0)
            return;

        modelIndex = (modelIndex + 1) % models.Length;
        DestroyPreviousModels();
        LoadCurrentModel();
    }

    private void DestroyPreviousModels()
    {
        if (topModel)
            Destroy(topModel);
        if (bottomModel)
            Destroy(bottomModel);
        if (leftModel)
            Destroy(leftModel);
        if (rightModel)
            Destroy(rightModel);
        if (frontModel)
            Destroy(frontModel);
        if (backModel)
            Destroy(backModel);

        topModel = null;
        bottomModel = null;
        leftModel = null;
        rightModel = null;
        frontModel = null;
        backModel = null;
    }

    private void LoadCurrentModel()
    {
        topModel = Instantiate(models[modelIndex]);
        bottomModel = Instantiate(models[modelIndex]);
        leftModel = Instantiate(models[modelIndex]);
        rightModel = Instantiate(models[modelIndex]);
        frontModel = Instantiate(models[modelIndex]);
        backModel = Instantiate(models[modelIndex]);

        topModel.transform.SetParent(transform, false);
        bottomModel.transform.SetParent(transform, false);
        leftModel.transform.SetParent(transform, false);
        rightModel.transform.SetParent(transform, false);
        frontModel.transform.SetParent(transform, false);
        backModel.transform.SetParent(transform, false);

        topModel.transform.Translate(0, 0.5f*3, 0);
        bottomModel.transform.Translate(1.2f*3, 0.5f*3, 0);
        leftModel.transform.Translate(2.4f*3, 0, 0);
        rightModel.transform.Translate(3.6f*3, 0, 0);
        frontModel.transform.Translate(4.8f*3, 0, 0);
        backModel.transform.Translate(6.0f*3, 0, 0);

        topModel.transform.Rotate(-90, 0, 0);
        bottomModel.transform.Rotate(90, 0, 0);
        leftModel.transform.Rotate(0, -90, 0);
        rightModel.transform.Rotate(0, 90, 0);
        frontModel.transform.Rotate(0, 180, 0);
        backModel.transform.Rotate(0, 0, 0);
    }

    private void Update()
    {
        if (_inputType == InputType.Null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                _startCameraPosition = Camera.main.transform.position;
                _inputType = InputType.Mouse;
            }
            else if (Input.touchCount > 0)
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _startPosition = Input.touches[0].position;
                    _startCameraPosition = Camera.main.transform.position;
                    _inputType = InputType.Touch;
                }
        }
        else if (_inputType == InputType.Mouse)
        {
            if (Input.GetMouseButtonUp(0))
                _inputType = InputType.Null;
            else
                ShiftCamera(Input.mousePosition);
        }
        else if (_inputType == InputType.Touch)
        {
            Vector3 touchPosition = Input.touches[0].position;
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                _inputType = InputType.Null;
            else
                ShiftCamera(Input.touches[0].position);
        }
    }

    private void ShiftCamera(Vector3 mousePosition)
    {
        Vector3 position = Camera.main.transform.position;
        position.x = _startCameraPosition.x + (_startPosition.x - mousePosition.x) * CameraDragMultiplier;
        position.x = Mathf.Clamp(position.x, 0, 6*3);
        Camera.main.transform.position = position;
    }
}