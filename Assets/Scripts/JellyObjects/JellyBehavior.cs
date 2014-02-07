using UnityEngine;
using System.Collections;

public class JellyBehavior : MonoBehaviour {
	public Sprite changeSpriteColor;
	Sprite        getCurrentJellySprite;
	float         collisionTimer;

	void OnCollisionEnter2D( Collision2D col ){
		if ( col.collider.name.Contains( "CandyGrenade" ) ){
			gameObject.tag = "JellyHit";
		}
		if ( col.collider.name.Contains( "CandyLaser" ) && gameObject.name.Contains( "blue" ) ){
			ChangePlatformColor( );
		}
		if ( col.collider.name.Contains( "EnemyCandy" ) && gameObject.name.Contains( "red" ) ){
			StartCoroutine( DestroyEnemyShip( col.collider.gameObject ) );
		}
		if( col.collider.name.Contains( "PlayerCandyShip" ) && Time.time > collisionTimer ){
			collisionTimer = Time.time + 5.0f;
			GlobalGameProperties.playerShields -= 1.0f;
		}
	}

	void ChangePlatformColor( ){
		gameObject.tag = "JellyHit";
		gameObject.GetComponent< SpriteRenderer >( ).sprite = changeSpriteColor;
	}

	IEnumerator DestroyEnemyShip( GameObject enemyShip ){
		Animator[] shipParts = enemyShip.GetComponentsInChildren< Animator >( );
		foreach( Animator animation in shipParts ){
			animation.enabled = true;
		}
		yield return new WaitForSeconds( 0.5f );
		GlobalGameProperties.showRandomMessage = true;
		Destroy( enemyShip );
	}
}
