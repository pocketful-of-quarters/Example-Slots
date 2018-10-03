using System.Collections.Generic;
using CSFramework;
using UnityEngine;
//using Firebase;

namespace Elona.Slot {
	/// <summary>
	/// An example custom Symbol class inherited from Symbol 
	/// </summary>
	public class ElosSymbol : Symbol {
		//[Header("Elos")]
		//public List<string> talksJP;
		//public List<string> talksEN;

		//public string GetRandomTalk() {
		//	if (Lang.isJP) return talksJP.Count == 0 ? "Mew mew?" : talksJP[Random.Range(0, talksJP.Count)];
		//	return talksEN.Count == 0 ? "Mew mew?" : talksEN[Random.Range(0, talksEN.Count)];
		//}

        private void Awake()
        {
            Dictionary<string, object> defaults = new Dictionary<string, object>();

            // These are the values that are used if we haven't fetched data from the
            // service yet, or if we ask for values that the service doesn't have:
            //Firebase.RemoteConfig.ConfigValue matchTypeFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("match_type");
            //Firebase.RemoteConfig.ConfigValue payTypeFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("pay_type");
            //Firebase.RemoteConfig.ConfigValue frequencyFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("frequency");
            //Firebase.RemoteConfig.ConfigValue minCountPerReelFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("minCountPerReel");

            //defaults.Add("match_type", MatchType.Normal);
            //defaults.Add("pay_type", PayType.Normal);
            //defaults.Add("pays", "0,0,0,0,0");
            //defaults.Add("frequency", 50);
            //defaults.Add("minCountPerReel", 0);

            //Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
        }  
	}
}