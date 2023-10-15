using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "ProgressionStatsDB", menuName = "TDSTK_DB/ProgressionStatsDB", order = 1)]
	public class ProgressionStats_DB : ScriptableObject {
		
		public LevelProgressionStats stats;
		
		//only used in runtime to load db data to runtime PlayerProgression
		public static void CopyStats(PlayerProgression progress){	Init();
			instance.stats=instance.stats.Clone();
		}
		
		
		public static ProgressionStats_DB instance;
		public static ProgressionStats_DB Init(){
			if(instance!=null) return instance;
			instance=LoadDB();
			return instance;
		}
		
		public static ProgressionStats_DB LoadDB(){
			return Resources.Load("DB_TDSTK/ProgressionStatsDB", typeof(ProgressionStats_DB)) as ProgressionStats_DB;
		}
		
		
		[Space(10)] public bool copiedFromOldDB=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copiedFromOldDB) return;
			instance.copiedFromOldDB=true;
			instance.stats=ProgressionStatsDB.LoadDB1().stats.Clone();
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copiedFromOldDB; }
		
	}
	
}
