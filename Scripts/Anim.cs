
using System.Collections;

using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace GrowingCliker {

    public class Anim : UdonSharpBehaviour
    {
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update() {
            animator.SetFloat("scale", transform.localScale.x);
        }
    }

}
