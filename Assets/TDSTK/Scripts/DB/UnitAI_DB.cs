using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "UnitAIDB", menuName = "TDSTK_DB/UnitAIDB", order = 1)]
	public class UnitAI_DB : ScriptableObject {
		
		[HideInInspector] public List<GameObject> objList=new List<GameObject>();
		public List<UnitAI> unitList=new List<UnitAI>();
		
		public static UnitAI_DB instance;
		public static UnitAI_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			
			#if UNITY_2018_3_OR_NEWER
			instance.FillObjectList();
			#endif
			
			return instance;
		}
		
		
		public static UnitAI_DB LoadDB(){
			return Resources.Load("DB_TDSTK/UnitAIDB", typeof(UnitAI_DB)) as UnitAI_DB;
		}
		
		public static List<UnitAI> Load(){ 
			Init();
			VerifyList();
			return instance.unitList;
		}
		
		public static bool VerifyList(bool hasUnitRemoved=false){	Init();
			#if UNITY_2018_3_OR_NEWER
			for(int i=0; i<instance.unitList.Count; i++){
				if(instance.unitList[i]!=null){
					if(instance.objList.Count>i)
						instance.objList[i]=instance.unitList[i].gameObject;
					else
						instance.objList.Add(instance.unitList[i].gameObject);
					continue;
				}
				
				if(i<instance.objList.Count && instance.objList[i]!=null){
					UnitAI item=instance.objList[i].GetComponent<UnitAI>();
					if(item!=null){
						instance.unitList[i]=item;
						continue;
					}
				}
				
				instance.unitList.RemoveAt(i);	i-=1;		hasUnitRemoved=true;
			}
			
			while(instance.objList.Count>instance.unitList.Count){
				instance.objList.RemoveAt(instance.objList.Count-1);
			}
			#else
			for(int i=0; i<instance.unitList.Count; i++){
				if(instance.unitList[i]==null){ instance.unitList.RemoveAt(i);	i-=1;	hasUnitRemoved=true; }
			}
			#endif
			
			return hasUnitRemoved;
		}
		
		
		public void FillObjectList(){
			objList=new List<GameObject>();
			for(int i=0; i<unitList.Count; i++) objList.Add(unitList[i].gameObject);
		}
		
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.unitList=new List<UnitAI>( UnitAIDB.Load1() );
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
	}
	
}
