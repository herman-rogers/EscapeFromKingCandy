using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

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
