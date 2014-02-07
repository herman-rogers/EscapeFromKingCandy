using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour {

	float cacheShields;
	public float hitTimerLength = 2.0f;
	public float regenTimerLength = 1.5f;
	public float playerShieldIncrementer = 0.5f;
	float hitTimer;
	float regenTimer;
	void Start( ){
		cacheShields = GlobalGameProperties.playerShields;
	}

	void Update( ){
		if ( GlobalGameProperties.playerShields <= 0.0f ){
			StartCoroutine( StartDeathAnimation( ) );
		}

		if ( GlobalGameProperties.playerShields < cacheShields ){
			ResetHitTimer( );
			cacheShields = GlobalGameProperties.playerShields;
		}
		if ( GlobalGameProperties.playerShields > cacheShields ){
			cacheShields = GlobalGameProperties.playerShields;
		}

		if ( Time.time > hitTimer && Time.time > regenTimer && GlobalGameProperties.playerShields < 100.0f ){
			RegenerateShields( );
		}

		transform.position = new Vector3( Mathf.Clamp( transform.localPosition.x, -8.2f, 7.8f ),
			                              Mathf.Clamp( transform.localPosition.y, -3.5f, 5.0f ),
			                              transform.position.z );

	}

	IEnumerator StartDeathAnimation( ){
		Animator[] shipExplosions = GetComponentsInChildren< Animator >( );
		foreach( Animator explosionAnimation in shipExplosions ){
			explosionAnimation.enabled = true;
		}
		yield return new WaitForSeconds( 3.0f );
		GlobalGameProperties.endGame = true;
		Destroy( gameObject );
	}

	void ResetHitTimer( ){
		hitTimer = Time.time + hitTimerLength;
	}

	void RegenerateShields( ){
		regenTimer = Time.time + regenTimerLength;
		GlobalGameProperties.playerShields += playerShieldIncrementer;
	}
}
