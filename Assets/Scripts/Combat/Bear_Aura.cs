using UnityEngine;
using System.Collections;

public class Bear_Aura : MonoBehaviour {

	public float damage = 1;
	public float stunAmount = 0.2f;
	public float knockbackAmount = 5f;

	 BaseCollision _collision;

	private void Awake()
	{
		_collision = GetComponent<BaseCollision>();
	}

	private void OnEnable()
	{
		_collision.OnCollisionStay += OnCollision;
	}

	private void OnDisable()
	{
		_collision.OnCollisionStay -= OnCollision;
	}

	void OnCollision( Collider2D collider)
	{
		if( gameObject.tag == "Boss" && gameObject.name.Contains("Bear"))
		{
			Debug.Log("It's the bear!.");
			if(collider.gameObject.tag == "Player" )
			{
				Debug.Log("The Bear collided with the player!");
				collider.gameObject.GetComponent<Damage>().ExecuteDamage( damage, _collision.GetComponent<Collider2D>());
				collider.gameObject.GetComponent<Stun>().GetStunned( stunAmount, knockbackAmount, GetComponent<Movement>().direction == Movement.Direction.Right? 1: -1 );
			}
		}
	}
}
