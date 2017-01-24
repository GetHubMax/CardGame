using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardInDeck : MonoBehaviour {
	string cardName;
	CardBase card;
	int amount;
	private bool update=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (update) {
			SetChild ();
			update = false;
		}

	}

	public void SetCard(CardBase card, int amount=1){
		this.card = card;
		this.cardName = card.cardName;
		this.amount = amount;
		SetChild ();
	}



	public void SetAmout(int i){
		this.amount = i;
		SetChild ();
	}


	void SetChild(){
		Transform[] child = gameObject.GetComponentsInChildren<Transform> ();
		child [0].gameObject.GetComponentInChildren<Text> ().text = cardName +" x"+amount;

	
	}

	public void Add(){
		GameObject.Find ("CollectionMaster").SendMessage ("AddCard", card);
		update = true;
	}

	public void Sub(){
		GameObject.Find ("CollectionMaster").SendMessage ("SubCard", card);
		update = true;
	}

}
