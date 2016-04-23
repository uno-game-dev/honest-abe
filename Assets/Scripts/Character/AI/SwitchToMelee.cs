using UnityEngine;
using System.Collections;
using BehaviourMachine;

public class SwitchToMelee : ConditionNode {

	private GameObject musket;
	private Attack attack;

	// Use this for initialization
	public override void Start () {
		attack = self.GetComponent<Attack> ();
		// set weapon's attack type to Jab
		musket = self.GetComponent<Transform> ().FindContainsInChildren ("Musket");
		musket.GetComponent<Weapon>().attackType = Weapon.AttackType.Jab;
		attack.SetWeapon (musket.GetComponent<Weapon> ());
	}
	
	// Update is called once per frame
	public override Status Update () {
		if (onSuccess.id != 0)
			owner.root.SendEvent (onSuccess.id);
		return Status.Success;
	}
}
