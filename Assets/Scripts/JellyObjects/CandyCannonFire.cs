using UnityEngine;
using System.Collections;

public class CandyCannonFire : PlayerWeapons {

	override public void DestroyProjectile( ){
		Destroy( gameObject, 3.0f );
	}

	override public void LaunchWeapon( ){
		transform.Translate( Vector3.left * 5 * Time.deltaTime );
	}

	override public void OnCollisionEnter2D ( Collision2D col ){
		if ( col.collider.name.Contains( "PlayerCandyShip" ) ){
			audio.Play( );
			StartCoroutine( "LaserExplosion" );
			GlobalGameProperties.playerShields -= 2.0f;
		}
	}

	IEnumerator LaserExplosion( ){
		CancelInvoke( "LaunchWeapon" );
		GetComponent< CircleCollider2D >( ).enabled = false;
		GetComponent< Animator >( ).enabled = true;
		yield return new WaitForSeconds( 1.0f );
		DestroyObject( gameObject );
	}
}
