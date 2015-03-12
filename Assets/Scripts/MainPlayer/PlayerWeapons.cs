using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {

    public float playerWeaponSpeed = 10.0f;

	void Start( ){
		InvokeRepeating( "LaunchWeapon", Time.deltaTime, 0.009f );
		DestroyProjectile( );
	}

	virtual public void DestroyProjectile( ){
		Destroy( gameObject, 1.0f );
	}

	virtual public void LaunchWeapon( ){
		transform.Translate( Vector3.up * playerWeaponSpeed * Time.deltaTime );
	}

	virtual public void OnTriggerEnter2D( Collider2D col ){
		if ( col.name == "jelly_blue" || col.name == "jelly_red" ){
			StartCoroutine( LaserExplosion( ) );
		}
	}

	public IEnumerator LaserExplosion( ){
		CancelInvoke( "LaunchWeapon" );
		GetComponent< CircleCollider2D >( ).enabled = false;
		GetComponent< Animator >( ).enabled = true;
		yield return new WaitForSeconds( 1.0f );
		DestroyObject( gameObject );
	}
}
