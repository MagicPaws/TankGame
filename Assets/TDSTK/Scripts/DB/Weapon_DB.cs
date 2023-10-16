using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "WeaponDB", menuName = "TDSTK_DB/WeaponDB", order = 1)]
	public class Weapon_DB : ScriptableObject {
		
		[HideInInspector] public List<GameObject> objList=new List<GameObject>();
		public List<Weapon> weaponList=new List<Weapon>();
		
		public static Weapon_DB instance;
		public static Weapon_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			
			#if UNITY_2018_3_OR_NEWER
			instance.FillObjectList();
			#endif
			
			return instance;
		}
		
		
		public static Weapon GetPrefab(int ID){ 	Init();
			for(int i=0; i<instance.weaponList.Count; i++){
				if(instance.weaponList[i].ID==ID) return instance.weaponList[i];
			}
			return null;
		}
		
		
		public static Weapon_DB LoadDB(){
			return Resources.Load("DB_TDSTK/WeaponDB", typeof(Weapon_DB)) as Weapon_DB;
		}
		
		public static List<Weapon> Load(){ 
			Init();
			VerifyList();
			return instance.weaponList;
		}
		
		public static bool VerifyList(bool hasItemRemoved=false){	Init();
			#if UNITY_2018_3_OR_NEWER
			for(int i=0; i<instance.weaponList.Count; i++){
				if(instance.weaponList[i]!=null){
					if(instance.objList.Count>i)
						instance.objList[i]=instance.weaponList[i].gameObject;
					else
						instance.objList.Add(instance.weaponList[i].gameObject);
					continue;
				}
				
				if(i<instance.objList.Count && instance.objList[i]!=null){
					Weapon item=instance.objList[i].GetComponent<Weapon>();
					if(item!=null){
						instance.weaponList[i]=item;
						continue;
					}
				}
				
				instance.weaponList.RemoveAt(i);	i-=1;		hasItemRemoved=true;
			}
			
			while(instance.objList.Count>instance.weaponList.Count){
				instance.objList.RemoveAt(instance.objList.Count-1);
			}
			#else
			for(int i=0; i<instance.weaponList.Count; i++){
				if(instance.weaponList[i]==null){ instance.weaponList.RemoveAt(i);	i-=1;	hasItemRemoved=true; }
			}
			#endif
			
			return hasItemRemoved;
		}
		
		
		public void FillObjectList(){
			objList=new List<GameObject>();
			for(int i=0; i<weaponList.Count; i++) objList.Add(weaponList[i].gameObject);
		}
		
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.weaponList=new List<Weapon>( WeaponDB.Load1() );
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
		
	}
	
}
