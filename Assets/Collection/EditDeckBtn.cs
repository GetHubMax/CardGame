using UnityEngine;
using System.Collections;

public class EditDeckBtn : MonoBehaviour {
	int id=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EditDeck(){
		GameObject.Find ("CollectionMaster").SendMessage ("EditDeck", id);

	}

	public void SetId(int id){
		this.id = id;
	}


}
