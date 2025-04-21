using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics{
    Dictionary<string, float> stats;

    public static class SetMarkers{
        // Character Markers
        public static string JumpHeight = "JumpHeight";
        public static string MaxSpeed = "MaxSpeed";

        //Item Markers
        public static string Durability = "Durability";
        public static string Range = "Range";
        public static string Damage = "Damage";
        public static string AttackSpeed = "AttackSpeed";

        //Shared Markers
        public static string Protection = "Protection";
        public static string BonusHealth = "BonusHealth";
        public static string Health = "Health";
    }

    public Statistics(params string[] initKeys){
        stats = new Dictionary<string, float>();
        foreach (string key in initKeys){
            Add(key, 0);
        }
    }
    /**
    * Default returns 0
    */
    public float Get(string key){
        float val = 0;
        try{
            stats.TryGetValue(key, out val);
        }catch {
            
        }
        return val;
    }

    public bool Add(string key, float val){
        return stats.TryAdd(key, val);
    }

    public void Put(string key, float val){
        if (Contains(key)){
            stats[key] = val;
        }else{
            stats.Add(key, val);
        }
    }

    public bool Remove(string key){
        return stats.Remove(key);
    }

    public bool Contains(string key){
        return stats.ContainsKey(key);
    }
}
