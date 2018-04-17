using UnityEngine;
using Vuforia;

namespace Assets.Scripts
{
    public sealed class VuforiaManager : MonoBehaviour
    {
        public Camera arCamera;
        [HideInInspector] public ImageTargetBehaviour backgroundImageTargetBehaviour;

        void Awake()
        {
            // since there is one image target we can use find object of type of
            backgroundImageTargetBehaviour = FindObjectOfType<ImageTargetBehaviour>();
        }
    }
}