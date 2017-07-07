using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GODeck : MonoBehaviour {

	public  Deck currentDeck ;
	private Dictionary<string, int> states = new Dictionary<string,int> ();
	private List<GameObject> cards = new List<GameObject> ();
	private int count = 0;
	// Use this for initialization

	public string Name(){
	
		return currentDeck.Name ();
	}

	void Start () {
		if (currentDeck != null) {
			this.LoadDeck (currentDeck);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadDeck(Deck deck){
		currentDeck = deck;
		count = deck.Total();

		Debug.Log ("Deck");


		foreach(CardBase c in currentDeck.deck){
			GameObject g = (GameObject)Instantiate (Resources.Load (c.cardName));
			g.transform.transform.parent = this.transform;
			cards.Add (g);
			states.Add (c.cardName, c.amount);
		}
	}

	/*********************EDIT DECK************************************/

	//NO USED
	public void AddCard(GameObject card){
		count++;

		currentDeck.AddCard (card.GetComponent<Card> ().cbase);
		if (cards.Contains (card)) {
			states [card.name]++;

		} else {
			GameObject g = Instantiate (card);
			Debug.Log (g.transform);
			g.transform.Translate(new  Vector3(-100000,-100000,1000));
			Debug.Log (g.transform);
			cards.Add (g);
			states.Add (g.name, 1);

		}

	}

	public void AddCard(CardBase card){
		count++;

		currentDeck.AddCard (card);

		if (states.ContainsKey(card.cardName)) {
			states [card.cardName]++;

		} else {
			
			GameObject g = (GameObject)Instantiate (Resources.Load ("CardsP0/"+card.cardName));
			Debug.Log (g.transform.position);
			g.transform.Translate(new  Vector3(-100000,-100000,1000));
			Debug.Log (g.transform.position);
			cards.Add (g);
			states.Add (card.cardName, 1);
		}

	}


	public void SubCard(GameObject card){
		count--;
		currentDeck.SubCard (card.GetComponent<Card> ().cbase);
		if (cards.Contains (card)) {
			states [card.name]--;

		} else {
			
			cards.Remove (card);
			states.Remove (card.name);

		}

	}

	public void SubCard(CardBase card){
		count--;
		currentDeck.SubCard (card);
		if (states.ContainsKey(card.cardName)) {
			states [card.cardName]--;

		} else {


			states.Remove (card.cardName);

		}

	}

	public int Total(){
		return currentDeck.Total ();
	
	}

	/*********************IN PLAY************************************/

	public bool CanDraw(){
		return count > 0;
	}

	public GameObject Draw(){
		GameObject rt = null;
		if (count <= 0) {
			return rt;
		}


		int i = (int)Random.Range (0, cards.Count);
		while (states [cards [i].name] <= 0) {
			i = Random.Range (0, cards.Count);
		}

		if (states [cards [i].name] > 1) {
			rt = Instantiate (cards [i]);
		
		} else {
			rt = cards [i];
		
		}
		count--;
		states[cards [i].name]--;
		return rt;

	}

	public List<CardBase> CardBase(){
		return currentDeck.deck;
	}

	public int Count(){
		return count;
	}

}
