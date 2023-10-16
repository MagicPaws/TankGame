using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "EffectDB", menuName = "TDSTK_DB/EffectDB", order = 1)]
	public class Effect_DB : ScriptableObject {
		
		public List<Effect> effectList=new List<Effect>();
		
		//called during game initiation to assign correct index to each corresponding effect ID
		public static int GetEffectIndex(int ID){	Init();
			for(int i=0; i<instance.effectList.Count; i++){ if(instance.effectList[i].ID==ID) return i; }
			return -1;
		}
		//called during runtime when an effect is actually needed (item now only store effect ID and index);
		public static Effect CloneItem(int idx){		Init();
			if(idx>0 && idx<instance.effectList.Count) return instance.effectList[idx].Clone();
			return null;
		}
		
		
		public static Effect_DB instance;
		public static Effect_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			return instance;
		}
		
		public static Effect_DB LoadDB(){
			return Resources.Load("DB_TDSTK/EffectDB", typeof(Effect_DB)) as Effect_DB;
		}
		
		public static List<Effect> Load(){ Init(); return instance.effectList; }
		
		public static List<Effect> LoadClone(){	Init();
			List<Effect> list=new List<Effect>();
			for(int i=0; i<instance.effectList.Count; i++) list.Add(instance.effectList[i].Clone());
			return list;
		}
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.effectList=new List<Effect>( EffectDB.LoadClone1() );
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
	}
	
}
