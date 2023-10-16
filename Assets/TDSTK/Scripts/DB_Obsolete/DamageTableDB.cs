using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDSTK;

namespace TDSTK {

	public class DamageTableDB : MonoBehaviour {
		public List<ArmorType> armorTypeList=new List<ArmorType>();
		public List<DamageType> damageTypeList=new List<DamageType>();
		
		public static DamageTableDB instance;
		public static DamageTableDB LoadDB1(){
			GameObject obj=Resources.Load("DB_TDSTK/Obsolete/DB_DamageTable", typeof(GameObject)) as GameObject;
			return obj.GetComponent<DamageTableDB>();
		}
		
		public static void Init(){
			if(instance!=null) return;
			instance=LoadDB1();
		}
		
		public static List<ArmorType> GetArmorTypeList1(){ Init();
			return instance.armorTypeList;
		}
		public static List<DamageType> GetDamageTypeList1(){ Init();
			return instance.damageTypeList;
		}
		
	}
	

}