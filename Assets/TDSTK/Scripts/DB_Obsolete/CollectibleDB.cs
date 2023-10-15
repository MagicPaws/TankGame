using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class CollectibleDB : MonoBehaviour {
		
		public List<Collectible> collectibleList=new List<Collectible>();
		
		public static CollectibleDB instance;
		public static CollectibleDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_Collectible", typeof(GameObject)) as GameObject;
			return obj.GetComponent<CollectibleDB>();
		}
		
		public static List<Collectible> Load1(){
			return LoadDB1().collectibleList;
		}
		
	}
	
}
