using UnityEngine;
using System.Collections;

public class EnemyWeapons : PlayerWeapons {

    public override void OnCollisionEnter2D ( Collision2D col ){
		if ( col.collider.name.Contains( "PlayerCandyShip" ) ){
			StartCoroutine( LaserExplosion( ) );
			GlobalGameProperties.playerShields -= 5.0f;
		}
	}

}
