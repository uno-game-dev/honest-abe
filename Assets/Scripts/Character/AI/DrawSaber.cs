using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class DrawSaber : ConditionNode {

	private Attack attack;
	private GameObject leeSaber;

	// Use this for initialization
	public override void Start () {
		attack = self.GetComponent<Attack> ();
		attack.weapon.gameObject.SetActive (false); // disable the pistol

		// enable the saber
		self.GetComponent<Transform> ().FindContainsInChildren ("Saber").SetActive (true);
		leeSaber = self.GetComponent<Transform> ().FindContainsInChildren ("Saber");
		attack.SetWeapon (leeSaber.GetComponent<Weapon>());
	}
	
	// Update is called once per frame
	public override Status Update () {
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
