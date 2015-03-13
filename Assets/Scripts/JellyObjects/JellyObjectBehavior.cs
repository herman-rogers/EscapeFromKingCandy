using UnityEngine;
using System.Collections;

public class JellyObjectBehavior : MonoBehaviour {
    public int numberHitToDestroyPlatform = 3;
    public float jellyPlatformSpeed;
    private float destroyTimer = 0.0f;

    void Start( ) {
        InvokeRepeating( "MovePlatformDown", Time.deltaTime, 0.009f );
    }

    void Update( ) {
        destroyTimer += Time.deltaTime;
        if ( destroyTimer > 20.0f  ) {
            DestroyPlatform( );
        }
    }

    void MovePlatformDown( ) {
        if ( jellyPlatformSpeed > 0 ) {
            transform.Translate( Vector3.down * Time.deltaTime * jellyPlatformSpeed );
            return;
        }
        transform.Translate( Vector3.down * Time.deltaTime );
    }

    void DestroyPlatform( ) {
        Destroy( gameObject );
    }
}
