using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class UnitAIDB : MonoBehaviour {
		
		public List<UnitAI> unitList=new List<UnitAI>();
		
		public static UnitAIDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_UnitAI", typeof(GameObject)) as GameObject;
			return obj.GetComponent<UnitAIDB>();
		}
		
		public static List<UnitAI> Load1(){
			return LoadDB1().unitList;
		}
		
		
	}
	
}
