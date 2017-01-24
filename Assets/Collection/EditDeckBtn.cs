using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EditDeckBtn : MonoBehaviour {
	private string deckName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EditDeck(){
		GameObject.Find ("CollectionMaster").SendMessage ("EditDeck", deckName);
		GameObject.Find ("CollectionMaster").SendMessage ("SetDeckBnt", gameObject);
	}

	public void SetName(string deckName){
		this.deckName = deckName;
		gameObject.GetComponentInChildren<Text> ().text = deckName;
	}

	public string Name(){
		return deckName;
	}


}
