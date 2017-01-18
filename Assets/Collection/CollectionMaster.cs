using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Application;

public class CollectionMaster : MonoBehaviour {

	public GameObject deckBtn;
	public GameObject selectDeckPage;
	public GameObject editDeckPage;
	public int deckLimit=10;

	Dictionary<int, Deck> decks = new Dictionary<int, Deck>();

	private int deckId = 0;
	private Deck currentDeck;
	private List<GameObject> deckBtns = new List<GameObject>();
	// Use this for initialization
	void Start () {
		editDeckPage.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void LoadDecks(){
		foreach (KeyValuePair<int, Deck> pair in decks) {
			GameObject go =Instantiate (deckBtn);
			go.transform.SetParent (selectDeckPage.transform,false);
			go.transform.Translate (new Vector3 (0, -50, 0));
			go.GetComponentInChildren<Text> ().text = pair.Value.Name();
			go.GetComponent<EditDeckBtn> ().SetId (pair.Key);
			deckBtn = go;
		}





	}

	void EditDeck(int index){
		selectDeckPage.SetActive (false);
		editDeckPage.SetActive (true);
		GameObject.Find ("DeckName").GetComponent<InputField> ().text = decks [index].Name ();
		currentDeck = decks [index];

	}



	public void CreateDeck(){
		if (decks.Count>=deckLimit ) {
			return;
		}

		decks.Add (deckId, new Deck ("My deck",deckId));

		GameObject go =Instantiate (deckBtn);
		go.transform.SetParent (selectDeckPage.transform,false);
		go.transform.Translate (new Vector3 (0, -40, 0));
		go.GetComponentInChildren<Text>().text="My deck";
		go.GetComponent<EditDeckBtn> ().SetId (deckId);
		deckBtns.Add (go);
		deckBtn = go;
		deckId++;

	}


	public void RenameDeck(GameObject name){
		currentDeck.SetName(name.GetComponent<InputField>().text);

	}

	public void Back(){
		editDeckPage.SetActive (false);
		selectDeckPage.SetActive (true);
		int cid = currentDeck.Id (); 
		foreach (GameObject go in deckBtns) {
			int id = go.GetComponent<EditDeckBtn> ().Id ();

			if(id==cid){
				go.GetComponentInChildren<Text>().text = currentDeck.Name ();
			}
			
		}



	}

}
