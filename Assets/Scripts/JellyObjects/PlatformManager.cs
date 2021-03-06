﻿using UnityEngine;
using System.Collections;

public class PlatformManager : MonoBehaviour {
	public GameObject bluePlatform;
	public GameObject redPlatform;
	public float spawnRate = 2.0f;
	public int dropRate;
	public int platformPosition;
	public bool dropPlatforms;
	JellyTurretBehavior getJellyTurretReference;
	GameObject getPlatformReference;
	float platformTimer = 0.0f;

	void Update( ){
		if ( Time.time > platformTimer && dropPlatforms ){
			DropPlatform( );
		}
	}

	void DropPlatform( ){
		platformTimer = Time.time + spawnRate;
		GlobalGameProperties.GetRandomColors( );
		string[] arrayOfRandomColors = GlobalGameProperties.getRandomColor.ToArray( );

		if ( arrayOfRandomColors[platformPosition] == "blue" ){
		    getPlatformReference = ( GameObject )Instantiate( bluePlatform, transform.position, transform.rotation );
            getPlatformReference.transform.parent = this.transform;
		}
		if ( arrayOfRandomColors[platformPosition] == "red" ){
			getPlatformReference = ( GameObject )Instantiate( redPlatform, transform.position, transform.rotation );
            getPlatformReference.transform.parent = this.transform;
		}
		if ( getPlatformReference != null ){
			getJellyTurretReference = getPlatformReference.GetComponentInChildren< JellyTurretBehavior >( );
			SetCannonActiveRandomly( );
		}
	}

	void SetCannonActiveRandomly( ){
		float randomTurretActivation = Random.Range( 0.0f, 100.0f );
		if ( randomTurretActivation > dropRate ){
			CatchNullReference( true );
			InvertCandyCannonRotation( );
		} else {
			CatchNullReference( false );
		}
	}

	void CatchNullReference( bool setActive ){
		try{
			getJellyTurretReference.gameObject.SetActive( setActive );
		} catch ( System.NullReferenceException ){
		}
	}

	void InvertCandyCannonRotation( ){
		if ( getJellyTurretReference != null ){
            if ( gameObject.tag == "JellyHit" ){
    			GameObject candyCane = getJellyTurretReference.GetComponentInChildren< SpriteRenderer >( ).gameObject;
	    		getJellyTurretReference.gameObject.transform.Rotate( 0,180,0 );
		    	getJellyTurretReference.gameObject.transform.position = new Vector3( getJellyTurretReference.transform.position.x, 
			                                                         getJellyTurretReference.transform.position.y,
			                                                         -2 );
			    candyCane.transform.localPosition = new Vector3( candyCane.transform.localPosition.x, 
			                                           candyCane.transform.localPosition.y,
			                                           -2.0f );
            }
		}
	}
}
