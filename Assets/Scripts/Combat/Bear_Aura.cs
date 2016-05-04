using UnityEngine;
using System.Collections;

public class Bear_Aura : MonoBehaviour {

	public float damage = 6f;
	public float stunAmount = 0.2f;
	public float knockbackAmount = 10f;
	public float damageInterval = 0.2f;

	private float _damageElapsed = 0f;
	private Stun.Direction _dir = Stun.Direction.Left;
	private Stun.Power _pow = Stun.Power.Heavy;


	 BaseCollision _collision;

	private void Awake()
	{
		_collision = GetComponent<BaseCollision>();
		_damageElapsed = damageInterval;
	}

	private void OnEnable()
	{
		_collision.OnCollisionStay += OnCollision;
		_collision.OnCollisionExit += Reset;
	}

	private void OnDisable()
	{
		_collision.OnCollisionStay -= OnCollision;
		_collision.OnCollisionExit -= Reset;
	}

	void OnCollision( Collider2D collider)
	{
		if( gameObject.tag == "Boss" && gameObject.name.Contains("Bear"))
		{
			Debug.Log("It's the bear!.");
			if(collider.gameObject.tag == "Player" )
			{
				Debug.Log("The Bear collided with the player!");
				if( _damageElapsed > damageInterval )
				{
					_damageElapsed =0f;
					float knockbackDirection = (transform.position - collider.transform.position).normalized.x * -1;

					collider.gameObject.GetComponent<Damage>().ExecuteDamage( damage, _collision.GetComponent<Collider2D>());
					collider.gameObject.GetComponent<Stun>().GetStunned( 
						stunAmount, 
						knockbackAmount, 
						knockbackDirection//GetComponent<Movement>().direction == Movement.Direction.Right? 1: -1 
					);

				}
				else
				{
					_damageElapsed += Time.deltaTime;
				}

			}
		}
	}

	void Reset( Collider2D collider)
	{
		_damageElapsed = damageInterval;
	}

}
