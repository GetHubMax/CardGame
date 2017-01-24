using System;
using System.Collections.Generic;
using UnityEngine;

namespace MyApplication
{
	[Serializable]
	public class Deck
	{
		public Dictionary<CardBase,int> deck = new Dictionary<CardBase,int>();
		[SerializeField]
		private string name;
		[SerializeField]
		private string path;
		[SerializeField]
		private  int id;

		public Deck (string name, int id){
			this.name = name;
			this.id = id;
		}

		public Deck (string name, string path,int id){
			this.name = name;
			this.id = id;
			this.path = path;
		}

		public void Add(CardBase card){
			if (deck.ContainsKey (card)) {
				deck [card] = deck [card] + 1;
			} else {
				deck.Add (card, 1);
			}
				
		}

		public void Add(CardBase card, int more=1){
			if (deck.ContainsKey (card)) {
				deck [card] = deck [card] + more;
			} else {
				deck.Add (card, more);
			}

		}


		public List<CardBase> GetCards(){
			return new List<CardBase>(deck.Keys);

		}




		public int CardAmout(CardBase card){
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

		public int Count(){
			return deck.Count;
		}
	
	
		public void Path(string name, string path){
			this.path = path;
			this.name = name;
		}

		public string Path(){
			return path;
		}


	}


		

}

