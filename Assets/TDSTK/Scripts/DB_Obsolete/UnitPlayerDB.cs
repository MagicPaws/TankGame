using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class UnitPlayerDB : MonoBehaviour {
		
		public List<UnitPlayer> unitList=new List<UnitPlayer>();
		
		public static UnitPlayerDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_UnitPlayer", typeof(GameObject)) as GameObject;
			return obj.GetComponent<UnitPlayerDB>();
		}
		
		public static List<UnitPlayer> Load1(){
			return LoadDB1().unitList;
		}
		
	}
	
}
