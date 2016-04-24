using UnityEngine;

public class PropDepth : MonoBehaviour {

    private Transform _playerTransform, _transform;
    private Vector3 _underPlayer, _overPlayer;

	void Start () {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _transform = GetComponent<Transform>();

        _underPlayer = new Vector3(_transform.position.x, _transform.position.y, -40f);
        _overPlayer = new Vector3(_transform.position.x, _transform.position.y, 1f);
    }
	
	void Update () {
        if (_playerTransform.position.y > _transform.position.y)
            _transform.position = _underPlayer;
        else
            _transform.position = _overPlayer;
	}
}
