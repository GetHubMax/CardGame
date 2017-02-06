using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;




	[Serializable]
	public class Deck:SyncListStruct<CardBase>
	{
		//public Dictionary<CardBase,int> deck = new Dictionary<CardBase,int>();
		[SerializeField]
		private string name;
		[SerializeField]
		private string path;
		[SerializeField]
		private  int id;
		[SerializeField]
		private int total=0;




		public Deck (string name, int id){
			this.name = name;
			this.id = id;
		}

		public Deck (string name, string path,int id){
			this.name = name;
			this.id = id;
			this.path = path;
		}

		public void AddCard(CardBase card){
			for (int i = 0; i < Count; i++) {
				CardBase c = GetItem (i);
				if (c == card) {
					c.amount++;
					total++;
					return;
				}
			}

			card.amount++;
			Add (card);
			total++;
		}


	
		public void SubCard(CardBase card){
			for (int i = 0; i < Count; i++) {
				CardBase c = GetItem (i);
				if (c == card) {
					c.amount--;
					total--;
					if (c.amount <= 0) {
						Remove (c);
					}
					return;
				}
			}


		}

			


		public int Total(){
			return total;
		}




		public string Name(){
			return name;
		}

		public void SetName(string name){
			this.name = name;
			
		}

		public int Id(){
			return id;
		}



		public void Path(string name, string path){
			this.path = path;
			this.name = name;
		}

		public string Path(){
			return path;
		}


		public bool CanDraw(){
			return Count > 0;
		}


		public CardBase Draw(){

			int index = UnityEngine.Random.Range(0,Count);
			CardBase rt = GetItem (index);
			rt.amount--;
			if (rt.amount <= 0) {
				Remove (rt);
			}
			return rt;


		}



	}

	[Serializable]
	public struct CardBase{
		public string cardName;

		public string cardType;

		public string cost;

		public string rules;

		public int amount;

		public static bool operator ==(CardBase x, CardBase y) 
		{
			return x.cardName.Equals(y.cardName);
		}

		public static bool operator !=(CardBase x, CardBase y) 
		{
			return !x.cardName.Equals(y.cardName);
		}




		public override int GetHashCode(){
			return cardName.GetHashCode ();

		}

		public override bool  Equals(System.Object obj){
			if(obj is CardBase){
				return true;
			}
			CardBase cBase = (CardBase) obj;
			return cBase.cardName == this.cardName;
		}



}
		



