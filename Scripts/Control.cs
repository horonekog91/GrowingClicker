
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;


namespace GrowingCliker {

    public class Control : UdonSharpBehaviour
    {

        public GameObject target;
        public Text count;
        public Text height;
        public Text weight;
        public float defaultHeight;
        public float defaultWeight;

        int _count;
        float _height;
        float _weight;
        Animator animator;

        void Start()
        {
            _count = 1; // 倍率
            _height = defaultHeight;
            _weight = defaultWeight;

            // アニメーター取得
            animator = target.GetComponent<Animator>();

            // テキスト初期化
            ViewText();
        }

        void Update() {
            CalcScale();
        }


        // 倍率リセット
        // 子オブジェクトのインタラクトで呼び出すため、publicにしておく
        public void Reset(){
            _count = 1;
            _height = defaultHeight * _count;
            _weight = defaultWeight * Mathf.Pow(_count,3);
            target.transform.localScale = new Vector3(1,1,1);

            ViewText();

            if(animator != null){
                animator.SetFloat("scale", _count);
            }
        }


        // 倍率増加
        // 子オブジェクトのインタラクトで呼び出すため、publicにしておく
        public void CountUp(){
            _count++;
            _height = defaultHeight * _count;
            _weight = defaultWeight * Mathf.Pow(_count,3);

            ViewText();

            if(animator != null){
                animator.SetFloat("scale", _count);
            }

        }

        // 倍率減少
        // 子オブジェクトのインタラクトで呼び出すため、publicにしておく
        public void CountDown(){
            // 縮小モードに切り替え
//            _isMode = 1;
            if(_count <= 1) return; // ただし、count が1以下にはならないように

            _count--;
            _height = defaultHeight * _count;
            _weight = defaultWeight * Mathf.Pow(_count,3);

            ViewText();

            if(animator != null){
                animator.SetFloat("scale", _count);
            }
        }


        // 計算処理
        void CalcScale(){

            // count と scale の差
            float diff;
            diff = _count - target.transform.localScale.x;

            // 1フレームごとに足していく数値
            // 差があるほどスピードアップ
            float speed;
            Vector3 v3;
            speed = diff / 50;
            v3 = new Vector3(speed, speed, speed);

            target.transform.localScale += v3;
        }

        // 表示処理
        void ViewText(){
            // 身長
            if(_height >= 100000){
                // 100000cmを超えている場合キロメートルに
                height.text = (_height / 100 / 1000).ToString() + "km";
            }
            else if(_height >= 1000){
                // 100cmを超えている場合メートルに
                height.text = (_height / 100).ToString() + "m";
            }
            else {
                // それ以外はセンチで
                height.text = _height.ToString() + "cm";
            }

            // 体重
            if(_weight >= 1000){
                // 1000を超えている場合はトンに
                weight.text = (_weight / 1000).ToString() + "t";
            } else {
                // それ以外はキログラムで
                weight.text = _weight.ToString() + "kg";
            }

            count.text = _count.ToString();
        }

    }

}