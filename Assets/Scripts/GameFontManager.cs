using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameFontManager : UnityObserver {

    public Text playerShieldsLabel;
    public Text enemyCandyShip;
    public GameObject playerHurt;
    public const string PLAYER_HURT = "PLAYER_HURT";

    public override void OnNotify( Object sender, EventArguments e ) {
        if ( e.eventMessage == PLAYER_HURT ) {
            StartCoroutine( DisplayerHurtImage( ) );
        }
    }

    void Update( ) {
        if ( GlobalGameProperties.playerShields < 0 ) {
            playerShieldsLabel.text = "HP: " + 0;
        }
        playerShieldsLabel.text = "HP: " + GlobalGameProperties.playerShields;
        enemyCandyShip.text = "King Candy's Ship: " + GlobalGameProperties.kingCandyShip;
    }

    IEnumerator DisplayerHurtImage( ) {
        playerHurt.SetActive( true );
        yield return new WaitForSeconds( 0.09f );
        playerHurt.SetActive( false );
    }

}
