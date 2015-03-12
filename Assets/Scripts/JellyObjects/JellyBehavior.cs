using UnityEngine;
using System.Collections;

public class JellyBehavior : MonoBehaviour {
	public Sprite changeSpriteColor;
	Sprite getCurrentJellySprite;
	float collisionTimer;

	private void OnTriggerEnter2D( Collider2D col ){
        if ( col.name.Contains( "CandyGrenade" ) ) {
			gameObject.tag = "JellyHit";
		}
        if ( col.name.Contains( "CandyLaser" ) && gameObject.name.Contains( "blue" ) ) {
			ChangePlatformColor( );
		}
        if ( col.name.Contains( "EnemyCandy" ) && gameObject.name.Contains( "red" ) ) {
			StartCoroutine( DestroyEnemyShip( col.GetComponent<Collider>().gameObject ) );
		}
        if ( col.name.Contains( "PlayerCandyShip" ) && Time.time > collisionTimer ) {
			collisionTimer = Time.time + 5.0f;
			GlobalGameProperties.playerShields -= 1.0f;
		}
	}

	private void ChangePlatformColor( ){
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
