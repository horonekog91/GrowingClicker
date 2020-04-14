
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;


namespace GrowingCliker {

    public class Shrink : UdonSharpBehaviour
    {

        GameObject parent;
        UdonBehaviour script;

        void Start()
        {
            // 親オブジェクトを取得
            parent = transform.parent.gameObject;

            // 親オブジェクトのU#スクリプトを取得
            script = (UdonBehaviour)parent.GetComponent(typeof(UdonBehaviour));
        }
        

        public override void Interact()
        {
            // 親のスクリプトから関数を呼び出し
            script.SendCustomEvent("CountDown");
        }
        
        private void OnMouseDown() {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;

            // プレイヤーがいない ＝ エディターで起動した場合はクリック操作もできるようにする
            if (localPlayer == null) script.SendCustomEvent("CountDown");
        }

    }
}