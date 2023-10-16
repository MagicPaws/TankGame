using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "AbilityDB", menuName = "TDSTK_DB/AbilityDB", order = 1)]
	public class Ability_DB : ScriptableObject {
		
		public List<Ability> abilityList=new List<Ability>();
		
		public static Ability_DB instance;
		public static Ability_DB LoadDB(){
			return Resources.Load("DB_TDSTK/AbilityDB", typeof(Ability_DB)) as Ability_DB;
		}
		
		public static void Init(){
			if(instance!=null) return;
			instance=LoadDB();
		}
		public static Ability CloneItem(int ID){ Init();
			for(int i=0; i<instance.abilityList.Count; i++){
				if(instance.abilityList[i].ID==ID) return instance.abilityList[i].Clone();
			}
			return null;
		}
		
		public static List<Ability> Load(){ Init(); return instance.abilityList; }
		
		public static List<Ability> LoadClone(){ Init();
			List<Ability> newList=new List<Ability>();
			for(int i=0; i<instance.abilityList.Count; i++) newList.Add(instance.abilityList[i].Clone());
			return newList;
		}
		
		
		//[HideInInspector] 
		[Space(10)] public bool copied=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copied) return;
			
			instance.copied=true;
			instance.abilityList=AbilityDB.Load1();
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copied; }
		
	}
	
}
