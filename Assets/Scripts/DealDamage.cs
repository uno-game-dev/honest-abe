using UnityEngine;
using System.Collections;

public class DealDamage : MonoBehaviour {

    private BaseCollision collision;

	void Start () {
	    collision = GetComponent<BaseCollision>();
        collision.OnCollision += OnCollision;
	}

    void Update() {
        collision.Move(Vector2.zero);
    }

    private void OnCollision(RaycastHit2D hit) {
        if (hit.collider.tag == "Player") {
            Debug.Log("Player took 10 damage");
        }
    }
	
}
