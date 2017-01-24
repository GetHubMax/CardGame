using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MyApplication;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;

public class CollectionMaster : MonoBehaviour {

	public GameObject deckBtn;
	public GameObject selectDeckPage;
	public GameObject editDeckPage;
	public GameObject cardInDeck;
	public GameObject testCard;
	public int deckLimit=4;
	private int deckcount = 0;
	public int rowLimit = 4;

	private Dictionary<string, Deck> decks = new Dictionary<string, Deck>();//The decks
	private Dictionary<string, GameObject> cardButtons = new Dictionary<string, GameObject> ();//
	private List<GameObject> cards = new List<GameObject>();

	private int deckId = 0;
	private Deck currentDeck;
	private GameObject currentDeckBtn;
	private List<GameObject> deckBtns = new List<GameObject>();
	private int mode = SELECTMODE;


	//Needs: change decks to use String as key, key for file name, and buttons store key.

	const int EDITMODE = 0;
	const int SELECTMODE = 1;

	// Use this for initialization
	void Start () {
		editDeckPage.SetActive (false);
		LoadCards ();
		LoadDecks ();
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
			if (x >= rowLimit) {
				x = 0;
				y++;
			}

			gcard.transform.Translate (new Vector3 (x*across-5, down*y-2, 0));
			x++;
		}
	
	
	
	}

		

	void LoadDecks(){
		Debug.Log ("Load deck at "+Application.persistentDataPath+" ...");
		DirectoryInfo levelDirectoryPath = new DirectoryInfo (Application.persistentDataPath);
		FileInfo[] paths = levelDirectoryPath.GetFiles("*.dk",SearchOption.AllDirectories);

		decks = new Dictionary<string, Deck> ();

		foreach (FileInfo ob in paths) {
			Debug.Log (ob.Name);
			Deck dk = Load (ob.FullName);
			decks.Add (dk.Name (), dk);	
		}
		
		//create buttons
		foreach (KeyValuePair<string, Deck> pair in decks) {
			GameObject go =Instantiate (deckBtn);
			go.transform.SetParent (selectDeckPage.transform,false);
			go.transform.Translate (new Vector3 (0, -50, 0));
			//go.GetComponentInChildren<Text> ().text = pair.Value.Name();
			go.GetComponent<EditDeckBtn> ().SetName (pair.Value.Name());
			deckBtn = go;
			deckBtns.Add (deckBtn);
		}

		Debug.Log ("...end");



	}

	void EditDeck(string deckName){
		selectDeckPage.SetActive (false);
		editDeckPage.SetActive (true);
		currentDeck = decks [deckName];
		GameObject.Find ("DeckName").GetComponent<InputField> ().text = currentDeck.Name ();

		mode = EDITMODE;

		foreach(KeyValuePair<CardBase,int > pair in currentDeck.deck){
			//AddCard (pair.Key,pair.Value);
			GameObject button = Instantiate(cardInDeck);
			button.GetComponent<CardInDeck> ().Set (pair.Key.cardName , pair.Value);
			button.transform.SetParent (editDeckPage.transform, false);
			//I need to traslate the cards

		}

	}
	public void SetDeckBnt(GameObject btn){
		currentDeckBtn = btn;

	}


	public void CreateDeck(){
		if (decks.Count>=deckLimit ) {
			Debug.Log ("At Deck limit");
			return;
		}
		int count = 0;
		string post = "";
		string def = "My deck";
		while(decks.ContainsKey(def+post)){
			count++;
			post = " " + count;

		}

		string name = def + post;
		string subname = name.Replace (" ", string.Empty);

		StringBuilder path = new StringBuilder ();
		path.Append (Application.persistentDataPath);
		path.Append("/");
		path.Append(subname);
		path.Append (".dk");
		Deck deck = new Deck (name, path.ToString (), deckId);
		decks.Add (name, deck);

		Save (path.ToString (), deck);

		GameObject go =Instantiate (deckBtn);
		go.transform.SetParent (selectDeckPage.transform,false);
		go.transform.Translate (new Vector3 (0, -40, 0));
		go.GetComponent<EditDeckBtn> ().SetName (name);
		deckBtns.Add (go);
		deckBtn = go;
		deckId++;


		Debug.Log ("created deck");

	}


	public void RenameDeck(GameObject input){
		

		string name = input.GetComponent<InputField> ().text;
		string subname = name.Replace (" ", string.Empty);

		if(decks.ContainsKey(name)){
			Debug.Log ("Name already exit "+name);
		}
		StringBuilder path = new StringBuilder ();
		path.Append (Application.persistentDataPath);
		path.Append("/");
		path.Append(subname);
		path.Append (".dk");

		//Need to change the button

		System.IO.File.Move (currentDeck.Path (), path.ToString ());

		decks.Remove (currentDeck.Name ());


		currentDeck.Path (name,path.ToString ());
		decks.Add (currentDeck.Name (), currentDeck);
	

	}

	public void Back(){
		editDeckPage.SetActive (false);
		selectDeckPage.SetActive (true);
		string cid = currentDeck.Name (); 
		currentDeckBtn.GetComponent<EditDeckBtn> ().SetName (cid);	
		
		foreach (KeyValuePair<string,GameObject> button in cardButtons) {
			Destroy (button.Value);
		}

		cardButtons.Clear();
		Save ();
		mode = SELECTMODE;


	}
	public void AddCard(GameObject gCard){
		currentDeck.Add (gCard.GetComponent<Card>().cbase,1);
		Card card = gCard.GetComponent<Card> ();


		if (!cardButtons.ContainsKey (card.CardName())) {
			GameObject button = Instantiate (cardInDeck);	
			button.transform.SetParent (editDeckPage.transform , false);
			Vector3 down = new Vector3(0,currentDeck.Count ()*-25,0);
			button.transform.Translate (down);
			button.GetComponent<CardInDeck> ().SetCard (gCard, currentDeck.deck[gCard.GetComponent<Card>().cbase]);
			cardButtons.Add (card.CardName (), button);
		}else{
			cardButtons [card.CardName ()].GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard.GetComponent<Card>().cbase]);	

		}


	}

	public void AddCard(GameObject gCard, int amount){
		currentDeck.Add (gCard.GetComponent<Card>().cbase,amount);
		Card card = gCard.GetComponent<Card> ();


		if (!cardButtons.ContainsKey (card.CardName())) {
			GameObject button = Instantiate (cardInDeck);	
			Vector3 down = new Vector3(0,currentDeck.Count ()*-25,0);
			button.transform.Translate (down);
			button.GetComponent<CardInDeck> ().SetCard (gCard, currentDeck.deck[card.cbase]);
			button.transform.SetParent (editDeckPage.transform , false);
			cardButtons.Add (card.CardName (), button);

		}
		cardButtons [card.CardName ()].GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard.GetComponent<Card>().cbase]);	



	}


	public void SubCard(GameObject gCard){
		GameObject tmp = cardButtons [gCard.GetComponent<Card> ().CardName ()];
		currentDeck.deck [gCard.GetComponent<Card>().cbase] = currentDeck.deck [gCard.GetComponent<Card>().cbase] - 1;
		tmp.GetComponent<CardInDeck> ().SetAmout(currentDeck.deck[gCard.GetComponent<Card>().cbase]);	
		if (currentDeck.deck [gCard.GetComponent<Card>().cbase] <= 0) {
			
			cardButtons.Remove (gCard.GetComponent<Card>().CardName());
			Destroy (tmp);
		}



	}

	private int more=10;
	private int pageNumber=1; 

	public void Next(){
		int limit = cards.Count / (rowLimit*2)	;
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

	public void Save(){

		BinaryFormatter bf = new BinaryFormatter ();
	//	if (!File.Exists (bf.ToString ())) {
	//		File.Create (bf.ToString ());
		
	//	}

		//FileStream file = File.Open (str.ToString(), FileMode.Open);//Will need to allow muitable decks
		FileStream file = File.Create (currentDeck.Path());
		bf.Serialize(file, currentDeck);
		file.Close ();
	}

	public void Save(string path, Deck go){

		BinaryFormatter bf = new BinaryFormatter ();
		//	if (!File.Exists (bf.ToString ())) {
		//		File.Create (bf.ToString ());

		//	}

		//FileStream file = File.Open (str.ToString(), FileMode.Open);//Will need to allow muitable decks
		FileStream file = File.Create (path);
		bf.Serialize(file,go);
		file.Close ();
	}

	public Deck Load(string path){
			
		Deck rt = null;
		if (File.Exists (path) ){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path,FileMode.Open);
			rt =(Deck)bf.Deserialize (file) ;
			file.Close ();

		}
		return rt;
	}


	public void TestSaveCard(){

		StringBuilder str = new StringBuilder ();
		str.Append (Application.persistentDataPath);
		str.Append ("/deckcard.cd");


		BinaryFormatter bf = new BinaryFormatter ();
	
		FileStream file = File.Create (str.ToString());
		CardBase cardBase = testCard.GetComponent<Card> ().cbase;
		bf.Serialize(file,cardBase);
		file.Close ();

		//Debug.Log(" Saved "+testCard.GetComponent<Card>().CardName());
		Debug.Log("Saved "+testCard.name);
	}

	public void TestLoadCard(){
		StringBuilder str = new StringBuilder ();
		str.Append (Application.persistentDataPath);
		str.Append ("/deckcard.cd");

		if (File.Exists (str.ToString()) ){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (str.ToString(),FileMode.Open);
			CardBase rt =(CardBase)bf.Deserialize (file) ;
			file.Close ();
			Debug.Log("Loaded "+rt.cardName);

		}
			
	}


	public void TestSaveDeck(){
		StringBuilder str = new StringBuilder ();
		str.Append (Application.persistentDataPath);
		str.Append ("/deck00.dk");


		BinaryFormatter bf = new BinaryFormatter ();

		FileStream file = File.Create (str.ToString());
		bf.Serialize(file,currentDeck);
		file.Close ();

		//Debug.Log(" Saved "+testCard.GetComponent<Card>().CardName());
		Debug.Log("Saved "+currentDeck.Name());
	}

	public void TestLoadDeck(){
		StringBuilder str = new StringBuilder ();
		str.Append (Application.persistentDataPath);
		str.Append ("/deck00.dk");

		if (File.Exists (str.ToString()) ){

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (str.ToString(),FileMode.Open);
			Deck rt =(Deck)bf.Deserialize (file) ;
			file.Close ();
			Debug.Log("Loaded "+rt.Name());

		}
	}





}
