using System;
using System.Collections.Generic;
using UnityEngine;

namespace Application
{
	public class Deck
	{
		Dictionary<GameObject,int> deck = new Dictionary<GameObject,int>();
		string name;

		public Deck (string name){
			this.name = name;
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



	}
}

