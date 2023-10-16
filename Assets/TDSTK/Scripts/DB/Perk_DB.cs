using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "PerkDB", menuName = "TDSTK_DB/PerkDB", order = 1)]
	public class Perk_DB : ScriptableObject {
		
		public List<Perk> perkList=new List<Perk>();
		
		public static Perk_DB instance;
		public static Perk_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			return instance;
		}
		
		public static Perk_DB LoadDB(){
			return Resources.Load("DB_TDSTK/PerkDB", typeof(Perk_DB)) as Perk_DB;
		}
		
		public static List<Perk> Load(){ Init(); return instance.perkList; }
		
		public static List<Perk> LoadClone(){	Init();
			List<Perk> list=new List<Perk>();
			for(int i=0; i<instance.perkList.Count; i++) list.Add(instance.perkList[i].Clone());
			return list;
		}
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.perkList=new List<Perk>( PerkDB.LoadClone1() );
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
	}
	
}
