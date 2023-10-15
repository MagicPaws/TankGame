using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	[CreateAssetMenu(fileName = "DamageTableDB", menuName = "TDSTK_DB/DamageTableDB", order = 1)]
	public class DamageTable_DB : ScriptableObject {
		
		public List<ArmorType> armorTypeList=new List<ArmorType>();
		public List<DamageType> damageTypeList=new List<DamageType>();
		
		public static DamageTable_DB instance;
		public static DamageTable_DB LoadDB(){
			return Resources.Load("DB_TDSTK/DamageTableDB", typeof(DamageTable_DB)) as DamageTable_DB;
		}
		
		public static void Init(){
			if(instance!=null) return;
			instance=LoadDB();
		}
		
		
		//[HideInInspector] 
		[Space(10)] public bool copied=false;
		public static void CopyFromOldDB(){		Init();
			if(instance.copied) return;
			
			instance.copied=true;
			instance.armorTypeList=DamageTableDB.GetArmorTypeList1();
			instance.damageTypeList=DamageTableDB.GetDamageTypeList1();
		}
		public static bool UpdatedToPost_2018_3(){ Init(); return instance.copied; }
		
	}
	
}
