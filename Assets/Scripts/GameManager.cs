using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameLevelState {
    START_LEVEL,
    FIRST_LEVEL,
    SECOND_LEVEL,
    THIRD_LEVEL,
    FOURTH_LEVEL,
    FIFTH_LEVEL,
    END_LEVEL
}

public class GameManager : MonoBehaviour {

    public GameObject platformMiddle;
    public GameObject platformLeft;
    public GameObject platformRight;
    public GameObject mainPlayer;
    public GameObject endGameScene;
    public GameObject endGameLabel;
    public GameObject levelRotator;
    public GameObject textRoot;
    public float gameTime;
    private GameLevelState currentGameState = GameLevelState.START_LEVEL;
    private float rotationTimer = 0.0f;
    private float rotationTimeLimit = 20.0f;
    private float timer;
    private List<Text> listOfLabels;

    void Start( ) {
        Text[ ] getLabels = textRoot.GetComponentsInChildren<Text>( true );

        listOfLabels = new List<Text>( );
        foreach ( Text label in getLabels ) {
            if ( label.tag == "JellyHit" ) {
                listOfLabels.Add( label );
            }
        }
        ToggleEnabledGameLabel( false );
    }

    private IEnumerator PlatformRotator( int rotationTime, float rotationIncrementor ) {
        float rotation = rotationIncrementor;
        Vector3 rotationAccumulator = new Vector3( 0, 0, rotationIncrementor );

        for ( int i = 0; i < rotationTime; i++ ) {
            yield return new WaitForSeconds( 0.0009f );
            rotation += 0.0001f;
            Vector3 vectorToTarget = rotationAccumulator - levelRotator.transform.position;
            float angle = Mathf.Atan2( vectorToTarget.y, vectorToTarget.x ) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis( angle, ( rotationIncrementor * Vector3.forward ) );
            levelRotator.transform.rotation = Quaternion.Slerp( levelRotator.transform.rotation, angleAxis, Time.deltaTime );
        }
    }

    int negativeToggle = -1;
    void Update( ) {
        gameTime = Time.timeSinceLevelLoad;
        rotationTimer += Time.deltaTime;

        if ( GlobalGameProperties.endGame ) {
            StartCoroutine( DisplayEndOfGame( ) );
        }

        if ( GlobalGameProperties.showRandomMessage && Time.time > timer ) {
            int getListIndex = Random.Range( 0, 3 );
            if ( skipDuplicateHappyTexts != getListIndex ) {
                skipDuplicateHappyTexts = getListIndex;
                StartCoroutine( DisplayHappyText( getListIndex ) );
            }
        }

        if ( currentGameState != GameLevelState.START_LEVEL && rotationTimer > rotationTimeLimit ) {
            int platformRotation = 10;
            int rotationDirection = 1;
            platformRotation = Random.Range( 50, 100 );
            negativeToggle = negativeToggle * -1;
            rotationDirection = negativeToggle;
            rotationTimer = 0.0f;
            StartCoroutine( PlatformRotator( platformRotation, rotationDirection ) );
        }

        if ( gameTime > 30.0f && gameTime <= 60.0f && currentGameState == GameLevelState.START_LEVEL ) {
            currentGameState = GameLevelState.FIRST_LEVEL;
            platformLeft.GetComponent<PlatformManager>( ).dropRate = 60;
        }
        if ( gameTime > 60.0f && gameTime <= 90.0f && currentGameState == GameLevelState.FIRST_LEVEL ) {
            currentGameState = GameLevelState.SECOND_LEVEL;
            platformRight.GetComponent<PlatformManager>( ).dropRate = 70;
        }
        if ( gameTime > 90.0f && gameTime <= 130.0f && currentGameState == GameLevelState.SECOND_LEVEL ) {
            currentGameState = GameLevelState.THIRD_LEVEL;
        }
        if ( gameTime > 130.0f && gameTime <= 190.0f && currentGameState == GameLevelState.THIRD_LEVEL ) {
            currentGameState = GameLevelState.THIRD_LEVEL;
            mainPlayer.GetComponent<MainPlayer>( ).regenTimerLength = 1.0f;
            platformRight.GetComponent<PlatformManager>( ).dropRate = 60;
        }
        if ( gameTime > 190.0f && gameTime <= 220.0f && currentGameState == GameLevelState.FOURTH_LEVEL ) {
            currentGameState = GameLevelState.FOURTH_LEVEL;
            platformRight.GetComponent<PlatformManager>( ).dropRate = 50;
        }
        if ( gameTime > 220.0f && gameTime <= 320.0f && currentGameState == GameLevelState.FIFTH_LEVEL ) {
            currentGameState = GameLevelState.FIFTH_LEVEL;
            platformRight.GetComponent<PlatformManager>( ).dropRate = 50;
        }
        if ( GlobalGameProperties.kingCandyShip <= 0 ) {
            currentGameState = GameLevelState.END_LEVEL;
            GlobalGameProperties.endGame = false;
            GlobalGameProperties.playerShields = 100.0f;
            Application.LoadLevel( "EndGame" );
        }
    }

    private int skipDuplicateHappyTexts = -1;

    IEnumerator DisplayHappyText( int index ) {
        skipDuplicateHappyTexts = index;
        Text[ ] arrayOfLabels = listOfLabels.ToArray( );
        arrayOfLabels[ index ].gameObject.SetActive( true );
        GlobalGameProperties.showRandomMessage = false;
        timer = Time.time + 2.0f;
        yield return new WaitForSeconds( 1.5f );
        ToggleEnabledGameLabel( false );
    }

    IEnumerator DisplayEndOfGame( ) {
        //NGUITools.SetActive( endGameLabel, true );
        //NGUITools.SetActive( endGameScene, true );
        yield return new WaitForSeconds( 10.0f );
        GlobalGameProperties.endGame = false;
        GlobalGameProperties.playerShields = 100.0f;
        Application.LoadLevel( "FirstScene" );
    }

    void ToggleEnabledGameLabel( bool disable ) {
        foreach ( Text happyLabel in listOfLabels ) {
            if ( happyLabel.tag == "JellyHit" ) {
                happyLabel.gameObject.SetActive( disable );
            }
        }
    }
}
