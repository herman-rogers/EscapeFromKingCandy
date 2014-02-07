using UnityEngine;
using System.Collections;

public class EnemyShipBehavior : MonoBehaviour {

	public GameObject gunBarrel;
	public GameObject candyLaser;
	float             nextShot;

	void Start( ){
		InvokeRepeating( "MoveShipRight", Time.deltaTime, 0.009f );
	}

	void Update( ){
		if( transform.position.y < -6.3f ){
			transform.Translate( Vector3.up * Time.deltaTime * 3.0f );
		}
		if ( Time.time > nextShot ){
			FireWeapon( );
		}
	}

	void FireWeapon( ){
		audio.Play( );
		nextShot = Time.time + 1.0f;
		Instantiate( candyLaser, gunBarrel.transform.position, transform.rotation );
	}

	void MoveShipRight( ){
		if ( transform.position.x < 6.0f ){
			transform.Translate( Vector3.right * Time.deltaTime * 0.6f );
		}
		if( transform.position.x > 6.0f ){
		    CancelInvoke( "MoveShipRight" );
			InvokeRepeating( "MoveShipLeft", Time.deltaTime, 0.009f );
		}
	}

	void MoveShipLeft( ){
		if ( transform.position.x >= -4.0f ){
			transform.Translate( Vector3.left * Time.deltaTime * 0.6f );
		}
		if( transform.position.x < -4.0f ){
			CancelInvoke( "MoveShipLeft" );
			InvokeRepeating( "MoveShipRight", Time.deltaTime, 0.009f );
		}
	}
}
