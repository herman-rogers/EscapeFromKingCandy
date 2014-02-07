using UnityEngine;
using System.Collections;

public class JellyTurretBehavior : MonoBehaviour {

	public GameObject candyAmmunition;
	public float      turretFireRate = 0.5f;
	float             cannonCooldownTimer;

	void Update( ){
		if ( Time.time > cannonCooldownTimer ){
			CannonFire( );
		}
	}
	void CannonFire( ){
		audio.Play( );
		cannonCooldownTimer = Time.time + turretFireRate;
		Instantiate( candyAmmunition, this.transform.position, transform.rotation );
	}
}
