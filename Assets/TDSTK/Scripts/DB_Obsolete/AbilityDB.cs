using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class AbilityDB : MonoBehaviour {
		
		public List<Ability> abilityList=new List<Ability>();
		
		public static AbilityDB instance;
		public static AbilityDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_Ability", typeof(GameObject)) as GameObject;
			return obj.GetComponent<AbilityDB>();
		}
		
		public static void Init(){
			if(instance!=null) return;
			instance=LoadDB1();
		}
		public static Ability CloneItem1(int ID){ Init();
			for(int i=0; i<instance.abilityList.Count; i++){
				if(instance.abilityList[i].ID==ID) return instance.abilityList[i].Clone();
			}
			return null;
		}
		
		public static List<Ability> Load1(){ Init(); return instance.abilityList; }
		
		public static List<Ability> LoadClone1(){ Init();
			List<Ability> newList=new List<Ability>();
			for(int i=0; i<instance.abilityList.Count; i++) newList.Add(instance.abilityList[i].Clone());
			return newList;
		}
		
	}
	
}
