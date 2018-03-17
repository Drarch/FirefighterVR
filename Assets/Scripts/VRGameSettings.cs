using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRGameSettings : MonoBehaviour {
    [SerializeField]
    [Range(0.5f, 3.0f)]
    public float m_RenderScale = 1.0f;  //The render scale. Higher numbers = better quality, but trades performance

    void Start()
    {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = m_RenderScale;
    }
}

