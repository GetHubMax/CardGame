using UnityEngine;
using UnityEngine.UI;
using System.Collections;
[ExecuteInEditMode]
public class Card : MonoBehaviour {
	public string cardName;
	public string cardType;
	public string cost;
	public string rules;

	// Use this for initialization
	void Start () {
			
		GetComponentInChildren<Text> ().text = cardName+" "+rules;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void CardName(string cardName){
		this.cardName = cardName;
	}

	public string CardName(){
		return cardName;
	}

	public void CardType(string cardType){
		this.cardType = cardType;
	}

	public void Cost(string cost){
		this.cost = cost;
	}

	public void Rules(string rules){
		this.rules = rules;
	}


}
