using UnityEngine;
using System.Collections;

public class JellyObjectBehavior : MonoBehaviour {
	public int         numberHitToDestroyPlatform = 3;
	Animator[]         listOfJellyPlatforms;
	int                counter;
	
	void Start( ){
		listOfJellyPlatforms = gameObject.GetComponentsInChildren< Animator >( );
		InvokeRepeating( "MovePlatformDown", Time.deltaTime, 0.009f );
	}
	
    void Update( ){
		UpdatePlatformStatus( );
		if ( counter >= numberHitToDestroyPlatform && gameObject.name.Contains( "Blue" ) ){
			StartPlatformDeath( );
			return;
		}
		if ( counter >= numberHitToDestroyPlatform && gameObject.name.Contains( "Red" ) ){
			StartPlatformExplosion( );
		}
		if ( transform.position.y < -20.0f ){
			DestroyPlatform( );
		}
	}

	void MovePlatformDown( ){
	    transform.Translate( Vector3.down * Time.deltaTime );
	}

	void UpdatePlatformStatus( ){
		counter = 0;
		foreach( Animator jellyPlatform in listOfJellyPlatforms ){
			if ( jellyPlatform.gameObject.tag == "JellyHit" ){
				counter++;
			}
		}
	}

	void StartPlatformDeath( ){
		StartCoroutine( "StartDestroyAnimation" );
	}

	void StartPlatformExplosion( ){
		foreach( Animator jellyPlatform in listOfJellyPlatforms ){
			jellyPlatform.gameObject.GetComponent< Rigidbody2D >( ).isKinematic = false;
		}
	}

	IEnumerator StartDestroyAnimation( ){
	    foreach( Animator jellyPlatform in listOfJellyPlatforms ){
			jellyPlatform.GetComponent< BoxCollider2D >( ).enabled = false;
			yield return new WaitForSeconds( 0.1f );
			jellyPlatform.enabled = true;
			GlobalGameProperties.showRandomMessage = true;
		}
		yield return new WaitForSeconds( 0.1f );
		Destroy( gameObject );
	}

	void DestroyPlatform( ){
		Destroy( gameObject );
	}
}
