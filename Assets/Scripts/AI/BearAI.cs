using UnityEngine;
using System.Collections;

public class BearAI : MonoBehaviour
{
	public float attackProximityDistanceX = 2;
	public float attackProximityDistanceY = 1;

	private float movementProximityDistance = 30;
	private GameObject _player;
	private Attack _attack;
	private bool seenPlayer = false;
	private bool swinging = false;

	private Movement _movement;
	public float stopDistanceX = 1;
	public float stopDistanceY = 0;
	private bool followingPlayer = false;
	private BaseCollision collision;

	private void Start()
	{
		_movement = GetComponent<Movement>();
		_player = GameObject.Find("Player");
	}

	void Update(){
		if (followingPlayer && !swinging) {
			//Check for obstacles in path
			RaycastHit2D hit = Physics2D.Raycast(transform.position, _player.transform.position, 2, LayerMask.GetMask("Environment", "Enemy"));
			if(hit && (hit.collider.tag == "Obstacle" || (hit.collider.tag == "Enemy" && hit.collider.gameObject != gameObject))){
				if (_player.transform.position.y >= transform.position.y){
					MoveOrStopTowards (new Vector2 (transform.position.x + Random.value*2, transform.position.y + 2));
				} else {
					MoveOrStopTowards (new Vector2 (transform.position.x + Random.value*2, transform.position.y - 2));
				}
			}
			else
				MoveOrStopTowards (_player.transform.position);

			/*if (timeToCheckForObstacles > 50) {
				RaycastHit2D hit = Physics2D.Raycast(transform.position, _player.transform.position, Mathf.Abs(_player.transform.position.x - transform.position.x), LayerMask.GetMask("Environment"));
				if (hit) {
					if (hit.collider.tag == "Obstacle") {
						followingPlayer = false;
						if (_player.transform.position.y >= transform.position.y)
							MoveOrStopTowards (new Vector2 (hit.transform.position.x, transform.position.y + 3));
						else
							MoveOrStopTowards (new Vector2 (hit.transform.position.x, transform.position.y - 3));
						followingPlayer = true;
					}
				}

			}
			timeToCheckForObstacles++;
			*/
		}
	}

	void Awake()
	{
		_attack = GetComponent<Attack>();
		_player = GameObject.Find("Player");
	}

	void OnEnable()
	{
		StartCoroutine("DoCheck");
	}

	void OnDisable()
	{
		StopCoroutine("DoCheck");
	}

	private bool AttackProximityCheck()
	{
		if (Mathf.Abs(_player.transform.position.x - transform.position.x) < attackProximityDistanceX)
		if (Mathf.Abs(_player.transform.position.y - transform.position.y) < attackProximityDistanceY)
			return true;

		return false;
	}

	private bool MovementProximityCheck()
	{
		if (Mathf.Abs (_player.transform.position.x - transform.position.x) < movementProximityDistance)
			return true;
		else
			return false;
	}

	IEnumerator DoCheck()
	{
		while (true)
		{
			if (seenPlayer) {
				followingPlayer = true;
				if(AttackProximityCheck()){
					if (Random.value > 0.25) {
						_attack.LightAttack ();
						swinging = true;
						yield return new WaitForSeconds(0.5f);
						swinging = false;
					} else {
						_attack.HeavyAttack ();
						swinging = true;
						yield return new WaitForSeconds(1.5f);
						swinging = false;
					}
				}
			} else {
				if (MovementProximityCheck ())
					seenPlayer = true;
			}

			yield return new WaitForSeconds(0.5f);
		}
	}

	private void MoveOrStopTowards(Vector2 position)
	{
		Vector3 deltaPosition = position - new Vector2(transform.position.x, transform.position.y);

		if (Mathf.Abs(deltaPosition.x) <= stopDistanceX)
			deltaPosition.x = 0;

		if (Mathf.Abs(deltaPosition.y) <= stopDistanceY)
			deltaPosition.y = 0;

		_movement.Move(deltaPosition);
	}

}
