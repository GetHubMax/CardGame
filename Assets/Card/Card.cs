using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
// Serializable

[ExecuteInEditMode]
public class Card: MonoBehaviour {
	public CardBase cbase = new CardBase();

	//[Serializable]
	// Use this for initialization
	void Start () {
			
		gameObject.GetComponentInChildren<Text> ().text = cbase.cardName+" "+cbase.rules;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void CardName(string cardName){
		cbase.cardName = cardName;
	}

	public string CardName(){
		return cbase.cardName;
	}

	public void CardType(string cardType){
		cbase.cardType = cardType;
	}

	public void Cost(string cost){
		cbase.cost = cost;
	}

	public void Rules(string rules){
		cbase.rules = rules;
	}

	public CardBase getCardBase(){
		return cbase;
	}



}
[Serializable]
public class CardBase{
	public string cardName;

	public string cardType;

	public string cost;

	public string rules;

	public CardBase(){}

}