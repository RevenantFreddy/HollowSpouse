using Modding;
using UnityEngine;
using System.IO;
using System.Diagnostics;
namespace HollowSpouse
{
    public class HollowSpouse : Mod, IGlobalSettings<GlobalSettings>, ITogglableMod
    {

        private float falltimer = 0f;

        public bool ToggleButtonInsideMenu => true;

        public static GlobalSettings GS { get; set; } = new GlobalSettings();

        new public string GetName() => "HollowSpouse";

        public override string GetVersion() => "0.9.5";

        public Process processTemp;
        public override void Initialize()
        {
            if (Process.GetProcessesByName("lovespouse").Length == 0)
            {
                Process.Start(Application.dataPath + "/Managed/Mods/HollowSpouse/" + "lovespouse.exe");
                
            }
            if(File.Exists(Application.dataPath + "/Managed/Mods/HollowSpouse/" + "vib.dat"))
            {
                File.Delete(Application.dataPath + "/Managed/Mods/HollowSpouse/" + "vib.dat");
            }
            ModHooks.HeroUpdateHook += HeroUpdateHook;
        }

        private void HeroUpdateHook()
        {
            if (Process.GetProcessesByName("lovespouse").Length == 0)
            {
                Process.Start(Application.dataPath + "/Managed/Mods/HollowSpouse/" + "lovespouse.exe");

            }
            File.WriteAllText(Application.dataPath + "/Managed/Mods/HollowSpouse/" + "vib.dat","SHAKE " + ((int)(9 * (1 - ((float)GameObject.Find("Knight").GetComponent<HeroController>().playerData.health / (float)GameObject.Find("Knight").GetComponent<HeroController>().playerData.maxHealth)))).ToString() + " 0.1");
        }


        public void Unload()
        {
            processTemp.Kill();
            ModHooks.HeroUpdateHook -= HeroUpdateHook;
            Log("Mod unloaded");
        }

        public void OnLoadGlobal(GlobalSettings s) => GS = s;

        public GlobalSettings OnSaveGlobal() => GS;
    }

    public class GlobalSettings
    {

    }
}

/*Falltimer max amounts:
 * Resting Grounds drop: 4.036
 * King's Pass drop: 4.086
 * Crossroads drop: 4.270
 * Cliffs leftside drop: 6.822
 * CoT elevator drop: 8.289
 * Abyss full drop: 11.000
 * 
 * Built in BIG_FALL_TIME = 1.1
 */