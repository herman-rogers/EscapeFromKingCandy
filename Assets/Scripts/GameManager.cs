using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject platformMiddle;
	public GameObject platformLeft;
	public GameObject platformRight;
	public GameObject shipLauncher1;
	public GameObject shipLauncher2;
	public GameObject shipLauncher3;
	public GameObject mainPlayer;
	public GameObject endGameScene;
	public GameObject endGameLabel;
    public GameObject levelRotator;
	public GameObject textRoot;
	public float gameTime;
    private Quaternion originalRotation;
    private Quaternion firstLevelRotation = new Quaternion(  );
    private List< Text > listOfLabels;
	private float timer;

    public float levelRotationTest = 0;

	void Start( ){
        Text[ ] getLabels = textRoot.GetComponentsInChildren<Text>( true );
        originalRotation = levelRotator.transform.rotation;

        firstLevelRotation = new Quaternion( originalRotation.x, originalRotation.y, levelRotationTest, originalRotation.w );

        listOfLabels = new List< Text >( );
        foreach ( Text label in getLabels ) {
            if ( label.tag == "JellyHit" ) {
                listOfLabels.Add( label );
            }
        }
		GlobalGameProperties.currentLevelReached = 7;
		ToggleEnabledGameLabel( false );
	}
	
	void Update( ){
		gameTime = Time.timeSinceLevelLoad;
		if ( GlobalGameProperties.endGame ){
			StartCoroutine( DisplayEndOfGame( ) );
		}

        levelRotator.transform.rotation = firstLevelRotation;

		if( GlobalGameProperties.showRandomMessage && Time.time > timer ){
            int getListIndex = Random.Range( 0, 3 );
            if ( skipDuplicateHappyTexts != getListIndex ) {
                skipDuplicateHappyTexts = getListIndex;
                StartCoroutine( DisplayHappyText( getListIndex ) );
            }
		}
		if ( gameTime > 30.0f && gameTime <= 60.0f ){
			GlobalGameProperties.currentLevelReached = 6;
			platformLeft.GetComponent< PlatformManager >( ).dropRate = 60;
		}
		if ( gameTime > 60.0f && gameTime <= 90.0f ){
			GlobalGameProperties.currentLevelReached = 5;
			platformRight.GetComponent< PlatformManager >( ).dropRate = 70;
		}
		if ( gameTime > 90.0f && gameTime <= 130.0f ){
			GlobalGameProperties.currentLevelReached = 4;
			shipLauncher1.SetActive( true );
		}
		if ( gameTime > 130.0f && gameTime <= 190.0f ){
			mainPlayer.GetComponent< MainPlayer >( ).regenTimerLength = 1.0f;
			GlobalGameProperties.currentLevelReached = 3;
			platformRight.GetComponent< PlatformManager >( ).dropRate = 60;
			shipLauncher2.SetActive( true );
		}
		if ( gameTime > 190.0f && gameTime <= 220.0f ){
			GlobalGameProperties.currentLevelReached = 2;
			platformRight.GetComponent< PlatformManager >( ).dropRate = 50;
			shipLauncher2.SetActive( true );
		}
		if ( gameTime > 220.0f && gameTime <= 320.0f ){
			GlobalGameProperties.currentLevelReached = 1;
			platformRight.GetComponent< PlatformManager >( ).dropRate = 50;
			shipLauncher2.SetActive( true );
		}
		if ( gameTime > 320.0f ){
			GlobalGameProperties.endGame = false;
			GlobalGameProperties.playerShields = 100.0f;
			GlobalGameProperties.currentLevelReached = 7;
			Application.LoadLevel( "EndGame" );
		}
	}

    private int skipDuplicateHappyTexts = -1;

	IEnumerator DisplayHappyText( int index ){
        skipDuplicateHappyTexts = index;
        Text[] arrayOfLabels = listOfLabels.ToArray( );
        arrayOfLabels[ index ].gameObject.SetActive( true );
		GlobalGameProperties.showRandomMessage = false;
		timer = Time.time + 2.0f;
		yield return new WaitForSeconds( 1.5f );
		ToggleEnabledGameLabel( false );
	}

	IEnumerator DisplayEndOfGame( ){
        //NGUITools.SetActive( endGameLabel, true );
        //NGUITools.SetActive( endGameScene, true );
		yield return new WaitForSeconds( 10.0f );
		GlobalGameProperties.endGame = false;
		GlobalGameProperties.playerShields = 100.0f;
		GlobalGameProperties.currentLevelReached = 7;
		Application.LoadLevel( "FirstScene" );
	}

	void ToggleEnabledGameLabel( bool disable ){
        foreach ( Text happyLabel in listOfLabels ) {
            if ( happyLabel.tag == "JellyHit" ) {
                happyLabel.gameObject.SetActive( disable );
            }
        }
	}
}
