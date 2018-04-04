using UnityEngine;
using Vuforia;

namespace Assets.Scripts
{
    public sealed class VuforiaManager : MonoBehaviour
    {
        [HideInInspector] public Camera arCamera;
        [HideInInspector] public ImageTargetBehaviour backgroundImageTargetBehaviour;

        void Awake()
        {
            arCamera = FindObjectOfType<Camera>();
            // since there is one image target we can use find object of type of
            backgroundImageTargetBehaviour = FindObjectOfType<ImageTargetBehaviour>();
        }
    }
}