using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This resorts combat for all characters. */

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	public float attackRate = 1f;
	public event System.Action OnAttack;
	public Transform healthBarPos;
	public CharacterStats myStats;
	public CharacterStats enemyStats;

	private float attackCountdown = 0f;

	void Start () {
		myStats = GetComponent<CharacterStats>();
		HealthUIManager.instance.Create (healthBarPos, myStats);
	}

	void Update () {
		attackCountdown -= Time.deltaTime;
	}

	public void Attack (CharacterStats enemyStats) {
		if (attackCountdown <= 0f) {
			this.enemyStats = enemyStats;
			attackCountdown = 1f / attackRate;

			StartCoroutine(DoDamage(enemyStats, .6f));

			if (OnAttack != null) {
				OnAttack ();
			}
		}
	}


	IEnumerator DoDamage(CharacterStats stats, float delay) {
		// print ("Start");
		yield return new WaitForSeconds (delay);
	
		Debug.Log ("[CharacterCombat] " + transform.name + " swings for " + myStats.damage.GetValue () + " damage");
		enemyStats.TakeDamage (myStats.damage.GetValue ());
	}


}
