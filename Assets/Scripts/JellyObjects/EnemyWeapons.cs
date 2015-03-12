using UnityEngine;
using System.Collections;

public class EnemyWeapons : PlayerWeapons {

    public override void OnTriggerEnter2D ( Collider2D col ){
		if ( col.name.Contains( "PlayerCandyShip" ) ){
			StartCoroutine( LaserExplosion( ) );
			GlobalGameProperties.playerShields -= 5.0f;
		}
	}

}
