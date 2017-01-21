using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Application;

public class CollectionMaster : MonoBehaviour {

	public GameObject deckBtn;
	public GameObject selectDeckPage;
	public GameObject editDeckPage;
	public GameObject cardInDeck;
	public int deckLimit=4;

	private Dictionary<int, Deck> decks = new Dictionary<int, Deck>();
	private Dictionary<string, GameObject> cardButtons = new Dictionary<string, GameObject> ();
	private List<GameObject> cards = new List<GameObject>();

	private int deckId = 0;
	private Deck currentDeck;
	private List<GameObject> deckBtns = new List<GameObject>();
	private int mode = SELECTMODE;

	const int EDITMODE = 0;
	const int SELECTMODE = 1;

	// Use this for initialization
	void Start () {
		editDeckPage.SetActive (false);
		LoadCards ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (Input.GetMouseButtonDown (0)) {
			
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint (Input.mousePosition),Vector2.zero,Mathf.Infinity);
			if (hit.collider != null && mode==EDITMODE) {
				Debug.Log (hit.collider.gameObject.name);
				AddCard (hit.collider.gameObject);
			}
							

		}
	}


	void LoadCards(){
		Object[] cards=Resources.LoadAll ("CardsP0");
		int x = 0;
		int y = 0;
		int down = 5;
		int across = 5;
		foreach (GameObject go in cards) {
			GameObject gcard =Instantiate ((GameObject) go);
			this.cards.Add (gcard);
			if (x >= deckLimit) {
				x = 0;
				y++;
			}

			gcard.transform.Translate (new Vector3 (x*across-5, down*y-2, 0));
			x++;
		}
	
	
	
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
		mode = EDITMODE;
		cardButtons = new Dictionary<string, GameObject> ();
		foreach(KeyValuePair<GameObject,int > pair in currentDeck.deck){
			AddCard (pair.Key,pair.Value);

		}

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
		mode = SELECTMODE;


	}
	public void AddCard(GameObject gCard){
		currentDeck.Add (gCard,1);
		Card card = gCard.GetComponent<Card> ();


		if (!cardButtons.ContainsKey (card.CardName())) {
			GameObject button = Instantiate (cardInDeck);	
			button.transform.SetParent (editDeckPage.transform , false);
			Vector3 down = new Vector3(0,currentDeck.Count ()*-25,0);
			button.transform.Translate (down);
			button.GetComponent<CardInDeck> ().SetCard (gCard, currentDeck.deck[gCard]);
			cardButtons.Add (card.CardName (), button);
		}else{
			cardButtons [card.CardName ()].GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard]);	

		}


	}

	public void AddCard(GameObject gCard, int amount){
		currentDeck.Add (gCard,amount);
		Card card = gCard.GetComponent<Card> ();


		if (!cardButtons.ContainsKey (card.CardName())) {
			GameObject button = Instantiate (cardInDeck);	
			Vector3 down = new Vector3(0,currentDeck.Count ()*-25,0);
			button.transform.Translate (down);
			button.GetComponent<CardInDeck> ().SetCard (gCard, currentDeck.deck[gCard]);
			button.transform.SetParent (editDeckPage.transform , false);
			cardButtons.Add (card.CardName (), button);

		}
			cardButtons [card.CardName ()].GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard]);	



	}


	public void SubCard(GameObject gCard){
		GameObject tmp = cardButtons [gCard.GetComponent<Card> ().CardName ()];
		currentDeck.deck [gCard] = currentDeck.deck [gCard] - 1;
		tmp.GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard]);	
		if (currentDeck.deck [gCard] <= 0) {
			
			cardButtons.Remove (gCard.GetComponent<Card>().CardName());
			Destroy (tmp);
		}



	}

	private int more=10;
	private int pageNumber=1; 

	public void Next(){
		 int limit = cards.Count / (deckLimit*2)	;
		Debug.Log (pageNumber +"/"+limit);
		if (pageNumber < limit) {
			Camera.main.transform.Translate (new Vector3 (0, more, 0));
			pageNumber++;
		}
	}


	public void Prev(){
		 int limit = cards.Count / (deckLimit*2);
		Debug.Log (pageNumber +"/"+limit);
		if (pageNumber > 1) {
			Camera.main.transform.Translate (new Vector3 (0, -more, 0));
			pageNumber--;
		}
	}
}
