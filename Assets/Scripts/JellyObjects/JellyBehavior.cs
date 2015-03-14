using UnityEngine;
using System.Collections;

public class JellyBehavior : MonoBehaviour {
    public Sprite changeSpriteColor;
    Sprite getCurrentJellySprite;
    float collisionTimer;

    private void OnTriggerEnter2D( Collider2D col ) {
        if ( col.name.Contains( "CandyGrenade" ) ) {
            gameObject.tag = "JellyHit";
        }
        if ( col.name.Contains( "CandyLaser" ) && gameObject.name.Contains( "blue" ) ) {
            DestroyBlueCandy( );
        }
        if ( col.name.Contains( "PlayerCandyShip" ) && Time.time > collisionTimer ) {
            collisionTimer = Time.time + 5.0f;
            GlobalGameProperties.playerShields -= 1.0f;
            Subject.Notify( GameFontManager.PLAYER_HURT );
            GetComponent<AudioSource>( ).Play( );
        }
    }

    private void DestroyBlueCandy( ) {
        gameObject.tag = "JellyHit";
        GlobalGameProperties.kingCandyShip -= 1.0f;
        StartCoroutine( StartDestroyAnimation( ) );
    }

    IEnumerator StartDestroyAnimation( ) {
        GetComponent<BoxCollider2D>( ).enabled = false;
        yield return new WaitForSeconds( 0.1f );
        GetComponent<Animator>( ).enabled = true;
        GlobalGameProperties.showRandomMessage = true;
        yield return new WaitForSeconds( 0.2f );
        Destroy( gameObject );
    }

}
