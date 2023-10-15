using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class WeaponDB : MonoBehaviour {
		
		//private static bool initiated=false;
		//private static List<Weapon> allweaponList=new List<Weapon>();	//for storing all effect during runtime
		
		public static WeaponDB instance;
		public static void Init(){
			if(instance!=null) return;
			instance=LoadDB1();
		}
		public static Weapon GetPrefab1(int ID){ Init();
			for(int i=0; i<instance.weaponList.Count; i++){
				if(instance.weaponList[i].ID==ID) return instance.weaponList[i];
			}
			return null;
		}
		
		
		
		public List<Weapon> weaponList=new List<Weapon>();
		
		public static WeaponDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_Weapon", typeof(GameObject)) as GameObject;
			return obj.GetComponent<WeaponDB>();
		}
		
		public static List<Weapon> Load1(){
			return LoadDB1().weaponList;
		}
		
	}
	
}
