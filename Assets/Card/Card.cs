using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Serializable

[ExecuteInEditMode]
public class Card: MonoBehaviour {
	public CardBase cbase;

	//[Serializable]
	// Use this for initialization
	void Start () {
			
		gameObject.GetComponentInChildren<Text> ().text = cbase.cardName+" "+cbase.rules;
	}

	// Update is called once per frame
	void Update () {
	
	}
	public void CardBase(CardBase cbase){
		this.cbase = cbase;
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

	public CardBase CardBase(){
		return cbase;
	}

	public void Amount(int amount){
		cbase.amount = amount;
	}


}
