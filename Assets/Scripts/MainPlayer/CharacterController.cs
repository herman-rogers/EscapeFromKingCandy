using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour {
	public GameObject candyLaser;
	public GameObject candyGrenade;
	public GameObject candyBarrelThree;
	GameObject        candyBarrelOne;
	GameObject        candyBarrelTwo;
	float             nextFire;
	bool              toggleFiringPosition;

	void Start( ){
		Transform[] getBarrelReferences = this.gameObject.GetComponentsInChildren< Transform >( );
		foreach( Transform gunBarrel in getBarrelReferences ){
			if ( gunBarrel.gameObject.name == "candybarrel1" ){
				candyBarrelOne = gunBarrel.gameObject;
			}
			if ( gunBarrel.gameObject.name == "candybarrel2" ){
				candyBarrelTwo = gunBarrel.gameObject;
			}
		}
	}

	void Update( ){
		MovePlayerVertical( );
		MovePlayerHorizontal( );
		PlayerHasFiredCandyGun( );
	}

	void MovePlayerVertical( ){
		this.transform.Translate( Vector3.up * Input.GetAxis( "Vertical" ) * 10 * Time.deltaTime );
	}

	void MovePlayerHorizontal( ){
		this.transform.Translate( Vector3.right * Input.GetAxis( "Horizontal" ) * 10 * Time.deltaTime );
	}

	void PlayerHasFiredCandyGun( ){
		if ( Input.GetButton( "Fire1" ) && Time.time >= nextFire ){
			FireCandyLaser( );
		}
		if ( Input.GetButton( "Fire2" ) && Time.time >= nextFire ){
			FireCandyGrenade( );
		}
	}

	void FireCandyLaser( ){
		nextFire = Time.time + 0.1f;
		audio.Play( );
		if ( toggleFiringPosition ){
			Instantiate( candyLaser, candyBarrelOne.transform.position, transform.rotation );
			toggleFiringPosition = false;
		}
		else if ( !toggleFiringPosition ){
			Instantiate( candyLaser, candyBarrelTwo.transform.position, transform.rotation );
			toggleFiringPosition = true;
		}
	}

	void FireCandyGrenade( ){
		nextFire = Time.time + 1.0f;
		audio.Play( );
		Instantiate( candyGrenade, candyBarrelThree.transform.position, transform.rotation );
	}
}
