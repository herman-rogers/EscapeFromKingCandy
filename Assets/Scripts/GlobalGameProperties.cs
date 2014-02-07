using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GlobalGameProperties {

	public static string[]        randomizePlatformColors = new string[]{ "red", "blue" };
	public static List< string >  getRandomColor;
	public static float           playerShields = 100.0f;
	public static int             currentLevelReached;
	public static bool            showRandomMessage;
	public static bool            endGame;

	public static void GetRandomColors( ){
		getRandomColor = new List< string >( );
		for ( int i = 0; i < 3; i++ ){
			int addColorAtPosition = Random.Range( 0, 2 );
			getRandomColor.Add( randomizePlatformColors[addColorAtPosition] );
        }
		if ( !getRandomColor.Contains( "blue" ) ){
			getRandomColor.RemoveAt( 0 );
			getRandomColor.Add( "blue" );
		}
	}
}
