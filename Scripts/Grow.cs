
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;



public class Grow : UdonSharpBehaviour
{

    GameObject root;
    UdonBehaviour script;

    void Start()
    {
        root = transform.root.gameObject;
        script = (UdonBehaviour)root.GetComponent(typeof(UdonBehaviour));
    }
    

    public override void Interact()
    {
        script.SendCustomEvent("CountUp");
    }
    
    private void OnMouseDown() {
        VRCPlayerApi localPlayer = Networking.LocalPlayer;

        // プレイヤーがいない ＝ エディターで起動した場合はクリック操作もできるようにする
        if (localPlayer == null) script.SendCustomEvent("CountUp");
    }

}
