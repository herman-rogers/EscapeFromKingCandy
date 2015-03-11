using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject disabledScreen;
    public GameObject enabledScreen;

    public void ToggleScene( ) {
        disabledScreen.SetActive( true );
        enabledScreen.SetActive( false );
    }

	public void LoadNextScene( ){
		Application.LoadLevel( Application.loadedLevel + 1 );
	}

	public void ExitGame( ){
		Application.Quit( );
	}

	public void LoadMenu( ){
		Application.LoadLevel( "FirstScene" );
	}
}
