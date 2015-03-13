using UnityEngine;
using System.Collections;

public class JellyTurretBehavior : MonoBehaviour {

    public GameObject candyAmmunition;
    public float turretFireRate = 0.5f;
    float cannonCooldownTimer;

    private void Update( ) {
        if ( Time.time > cannonCooldownTimer ) {
            CannonFire( );
        }
    }

    private void OnTriggerEnter2D( Collider2D col ) {
        if ( col.name.Contains( "CandyLaser" ) ) {
            Destroy( this.gameObject );
        }
    }

    private void CannonFire( ) {
        GetComponent<AudioSource>( ).Play( );
        cannonCooldownTimer = Time.time + turretFireRate;
        Instantiate( candyAmmunition, this.transform.position, transform.rotation );
    }
}
