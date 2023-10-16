using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDSTK{

	//~ public class UnitTracker : MonoBehaviour {

		//~ public List<Unit> unitList=new List<Unit>();
		
		//~ public static void AddUnit(Unit unit){
			//~ if(unit.gameObject.layer!=9) return;
			//~ unitList.Add(unit);
		//~ }
		//~ public static void RemoveUnit(Unit unit){
			//~ unitList.Remove(unit);
		//~ }
		
		
		//~ public static Unit GetNearestUnit(Vector3 pos){
			//~ int nearestIdx=-1; 	float nearest=Mathf.Infinity;
			//~ for(int i=0; i<instance.unitList.Count; i++){
				//~ float dist=Vector3.Distance(pos, instance.unitList[i].GetPos());
				//~ if(dist<nearest){
					//~ nearest=dist;
					//~ nearestIdx=i;
				//~ }
			//~ }
			//~ return nearestIdx>=0 ? instance.unitList[nearestIdx] : null ;
		//~ }
		
		
		
		//~ private static UnitTracker instance;
		
		//~ public static void Init(){
			//~ if(instance==null) return;
			
			//~ GameObject obj=new GameObject("UnitTracker");
			//~ instance=obj.AddComponent<UnitTracker>();
		//~ }
		
		//~ void OnDestroy(){
			//~ instance=null;
		//~ }
		
	//~ }

}
