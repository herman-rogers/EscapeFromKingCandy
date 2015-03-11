using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    private static MusicManager _instance;

    private void Awake( ) {
        if ( !_instance ) {
            _instance = this;
            DontDestroyOnLoad( this );
            return;
        }
        if ( this != _instance ) {
            Destroy( this.gameObject );
        }
    }

    public static MusicManager instance {
        get {
            if ( !_instance ) {
                _instance = GameObject.FindObjectOfType<MusicManager>( );
                DontDestroyOnLoad( _instance.gameObject );
            }
            return _instance;
        }
    }
}