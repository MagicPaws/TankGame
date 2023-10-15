using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class PerkDB : MonoBehaviour {

		public List<Perk> perkList=new List<Perk>();
	
		public static PerkDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_Perk", typeof(GameObject)) as GameObject;
			return obj.GetComponent<PerkDB>();
		}
		
		public static List<Perk> Load1(){
			return LoadDB1().perkList;
		}
		
		public static List<Perk> LoadClone1(){
			PerkDB instance=LoadDB1();
			List<Perk> list=new List<Perk>();
			for(int i=0; i<instance.perkList.Count; i++){
				list.Add(instance.perkList[i].Clone());
			}
			return list;
		}
		
	}
	
}
