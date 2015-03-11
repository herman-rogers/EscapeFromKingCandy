using UnityEngine;
using System.Collections;

public class GameFontManager : MonoBehaviour {

    //public UILabel playerShieldsLabel;
    //public UILabel playerOuterHullLevel;
    //public UILabel playerShieldBoost;
	bool           toggleShieldBoost = true;

	void Update ( ){
        //playerShieldsLabel.text = "Shields: " + GlobalGameProperties.playerShields;
        //playerOuterHullLevel.text = "Hull Breach: " + GlobalGameProperties.currentLevelReached + "/ 7";
		if ( toggleShieldBoost && GlobalGameProperties.currentLevelReached == 3 ){
			StartCoroutine( DisplayShieldBoost( ) );
		}
	}

	IEnumerator DisplayShieldBoost( ){
		toggleShieldBoost = false;
        //playerShieldBoost.gameObject.SetActive( true );
		GlobalGameProperties.playerShields += 20.0f;
		yield return new WaitForSeconds( 3.0f );
        //playerShieldBoost.gameObject.SetActive( false );
	}
}
