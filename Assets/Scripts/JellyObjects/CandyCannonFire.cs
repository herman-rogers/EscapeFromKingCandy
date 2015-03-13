using UnityEngine;
using System.Collections;

public class CandyCannonFire : PlayerWeapons {

	override public void DestroyProjectile( ){
		Destroy( gameObject, 3.0f );
	}

	override public void LaunchWeapon( ){
		transform.Translate( Vector3.left * playerWeaponSpeed * Time.deltaTime );
	}

	override public void OnTriggerEnter2D ( Collider2D col ){
		if ( col.tag == "Player" ){
			GetComponent<AudioSource>().Play( );
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
