using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardInDeck : MonoBehaviour {
	string cardName;
	GameObject card;
	int amount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetCard(GameObject card, int amount=1){
		this.card = card;
		this.cardName = card.GetComponent<Card> ().CardName ();
		this.amount = amount;
	}

	public void SetCard(string cardName){
		this.cardName = cardName;
		SetChild ();

	}

	public void SetAmout(int i){
		this.amount = i;
		SetChild ();
	}

	public void Set(string cardName, int i=1){
		this.cardName = cardName;
		this.amount = i;
		SetChild ();
	}

	void SetChild(){
		Transform[] child = gameObject.GetComponentsInChildren<Transform> ();
		child [0].gameObject.GetComponentInChildren<Text> ().text = cardName +" x"+amount;

	
	}

	public void Add(){
		GameObject.Find ("CollectionMaster").SendMessage ("AddCard", card);
	}

	public void Sub(){
		GameObject.Find ("CollectionMaster").SendMessage ("SubCard", card);
	}

}
