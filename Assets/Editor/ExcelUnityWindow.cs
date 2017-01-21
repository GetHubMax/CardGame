using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using GemBox.Spreadsheet;
using System.Text;
//namespace AssemblyCSharp

//Works but throws ShowGameBehavior 

namespace Game
{
	//
	public class ExcelUnity:EditorWindow	{


		string file ="path";
		string folder ="Assets";
		GameObject tmp;



		[MenuItem("Window/ExcelUnity")]

		public static void ShowWindow(){
			EditorWindow.GetWindow (typeof(ExcelUnity));

		
		}


		void OnGUI(){
			GUILayout.Label ("Excel Unity");
			file = EditorGUILayout.TextField ("excel file path", file);
			folder = EditorGUILayout.TextField ("output folder", folder);
			tmp = (GameObject)EditorGUILayout.ObjectField (tmp, typeof(GameObject), true);


			if (GUILayout.Button ("Load") ) {
				if (!tmp) {
					Debug.LogError ("Need template object!");
					return;
				}


				import ();


			}


		}


		void import(){
			SpreadsheetInfo.SetLicense ("FREE-LIMITED-KEY");
			List<string> keys = new List<string> ();
			ExcelFile ef= ExcelFile.Load(file);
			//ExcelFile ef= ExcelFile.LoadFromDirectory(file,XlsxLoadOptions.XlsxDefault);
			//StringBuilder sb = new StringBuilder ();


			//first row for names of vaules
			ExcelWorksheet sheet = ef.Worksheets[0];
			//sheet.Calculate();//should work but doesn't
			ExcelCell tmpcell= sheet.Cells["XX100"];

			ExcelRow row = sheet.Rows[0];	

			//get name of 
			foreach (ExcelCell cell in row.AllocatedCells) {

				if (cell.ValueType != CellValueType.Null) {
					keys.Add( cell.StringValue);

				}
			}

			int i = 0;

			int size = keys.Count;
			foreach (ExcelRow rows in sheet.Rows) {
				if (i == 0) {
					i++;
					continue;
				}
				if (rows.Cells [0].Value ==null) {
					break;
							
				}

				int x = 0;
				string name = rows.Cells [0].StringValue;
				Object prefab = PrefabUtility.CreateEmptyPrefab (folder+"/"+name+".prefab");
				//this is the error
				GameObject gb = (GameObject) GameObject.Instantiate<GameObject>((GameObject)tmp);//All the casting to be safe
				foreach(ExcelCell cell in rows.AllocatedCells){
					if (x >= size) {
						break;
					}

					if (cell.Value == null) {
						continue;
					}

					tmpcell.Formula =cell.StringValue;
					tmpcell.Calculate();
					gb.SendMessage (keys[x], tmpcell.Value);
					x++;
				}

				PrefabUtility.ReplacePrefab (gb, prefab, ReplacePrefabOptions.ConnectToPrefab);
				Debug.Log ("prebab: "+name+" done" );
			}
			Debug.Log ("finished excel unity");
		}



		public void Init(){
			//importer = (Importer)FindObjectOfType (typeof(Importer));

		}

	


		public ExcelUnity ()
		{
		}
		


	
	
	}
}

