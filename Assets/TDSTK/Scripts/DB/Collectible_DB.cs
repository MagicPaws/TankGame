using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "CollectibleDB", menuName = "TDSTK_DB/CollectibleDB", order = 1)]
	public class Collectible_DB : ScriptableObject {
		
		[HideInInspector] public List<GameObject> objList=new List<GameObject>();
		public List<Collectible> collectibleList=new List<Collectible>();
		
		public static Collectible_DB instance;
		public static Collectible_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			
			#if UNITY_2018_3_OR_NEWER
			instance.FillObjectList();
			#endif
			
			return instance;
		}
		
		
		public static Collectible_DB LoadDB(){
			return Resources.Load("DB_TDSTK/CollectibleDB", typeof(Collectible_DB)) as Collectible_DB;
		}
		
		public static List<Collectible> Load(){ 
			Init();
			VerifyList();
			return instance.collectibleList;
		}
		
		public static bool VerifyList(bool hasUnitRemoved=false){	Init();
			#if UNITY_2018_3_OR_NEWER
			for(int i=0; i<instance.collectibleList.Count; i++){
				if(instance.collectibleList[i]!=null){
					if(instance.objList.Count>i)
						instance.objList[i]=instance.collectibleList[i].gameObject;
					else
						instance.objList.Add(instance.collectibleList[i].gameObject);
					continue;
				}
				
				if(i<instance.objList.Count && instance.objList[i]!=null){
					Collectible item=instance.objList[i].GetComponent<Collectible>();
					if(item!=null){
						instance.collectibleList[i]=item;
						continue;
					}
				}
				
				instance.collectibleList.RemoveAt(i);	i-=1;		hasUnitRemoved=true;
			}
			
			while(instance.objList.Count>instance.collectibleList.Count){
				instance.objList.RemoveAt(instance.objList.Count-1);
			}
			#else
			for(int i=0; i<instance.collectibleList.Count; i++){
				if(instance.collectibleList[i]==null){ instance.collectibleList.RemoveAt(i);	i-=1;	hasUnitRemoved=true; }
			}
			#endif
			
			return hasUnitRemoved;
		}
		
		
		public void FillObjectList(){
			objList=new List<GameObject>();
			for(int i=0; i<collectibleList.Count; i++) objList.Add(collectibleList[i].gameObject);
		}
		
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.collectibleList=new List<Collectible>( CollectibleDB.Load1() );
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
	}
	
}
