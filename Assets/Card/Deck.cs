using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application
{
	public class Deck
	{
		Dictionary<GameObject,int> deck = new Dictionary<GameObject,int>();
		string name;
		private  int id;

		public Deck (string name, int id){
			this.name = name;
			this.id = id;
		}

		public void Add(GameObject card){
			if (deck.ContainsKey (card)) {
				deck [card] = deck [card] + 1;
			} else {
				deck.Add (card, 1);
			}
				
		}

		public List<GameObject> GetCards(){
			return new List<GameObject>(deck.Keys);

		}




		public int CardAmout(GameObject card){
			if (deck.ContainsKey (card)) {
				return deck [card];
			
			} else {
			
				return 0;
			}				
		
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


	}
}

