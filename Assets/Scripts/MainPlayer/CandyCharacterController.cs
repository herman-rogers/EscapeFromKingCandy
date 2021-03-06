﻿using UnityEngine;
using System.Collections;

public class CandyCharacterController : MonoBehaviour {
	public GameObject candyLaser;
	public GameObject candyGrenade;
	public GameObject candyBarrelThree;
    public float characterSpeed = 10;
	private GameObject candyBarrelOne;
    private GameObject candyBarrelTwo;
    private float nextFire;
    private bool toggleFiringPosition;
    private Vector3 cachedShipTransform;

    private void Start( ) {
        cachedShipTransform = this.transform.position;
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

    private void Update( ) {
		MovePlayerVertical( );
		MovePlayerHorizontal( );
		PlayerHasFiredCandyGun( );
	}

    private void MovePlayerVertical( ) {
        if ( transform.localPosition.y <= -550.0f || transform.localPosition.y >= 550.0f ) {
            transform.position = cachedShipTransform;
        }
        transform.Translate( Vector3.up * Input.GetAxis( "Vertical" ) * characterSpeed * Time.deltaTime );
	}

    private void MovePlayerHorizontal( ) {
        if ( transform.localPosition.x <= -600.0f || transform.localPosition.x >= 600.0f ) {
            transform.position = cachedShipTransform;
        }
        transform.Translate( Vector3.right * Input.GetAxis( "Horizontal" ) * characterSpeed * Time.deltaTime );
	}

    private void PlayerHasFiredCandyGun( ) {
		if ( Input.GetButton( "Fire1" ) && Time.time >= nextFire ){
			FireCandyLaser( );
		}
		if ( Input.GetButton( "Fire2" ) && Time.time >= nextFire ){
			FireCandyLaser( );
		}
	}

    private void FireCandyLaser( ) {
		nextFire = Time.time + 0.1f;
		if ( toggleFiringPosition ){
			Instantiate( candyLaser, candyBarrelOne.transform.position, transform.rotation );
			toggleFiringPosition = false;
		}
		else if ( !toggleFiringPosition ){
			Instantiate( candyLaser, candyBarrelTwo.transform.position, transform.rotation );
			toggleFiringPosition = true;
		}
	}

    private void FireCandyGrenade( ) {
		nextFire = Time.time + 1.0f;
		Instantiate( candyGrenade, candyBarrelThree.transform.position, transform.rotation );
	}
}
