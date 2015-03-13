using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameFontManager : MonoBehaviour {

    public Text playerShieldsLabel;
    public Text enemyCandyShip;
    public Text playerShieldBoost;

    void Update( ) {
        playerShieldsLabel.text = "HP: " + GlobalGameProperties.playerShields;
        enemyCandyShip.text = "King Candy Ship: " + GlobalGameProperties.kingCandyShip;
    }

    IEnumerator DisplayShieldBoost( ) {
        GlobalGameProperties.playerShields += 20.0f;
        yield return new WaitForSeconds( 3.0f );
    }
}
