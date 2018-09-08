using System.Collections;
using UnityEngine;

public class C_CameraMove : MonoBehaviour {

    private void Awake()
    {
        setupCamera();
    }

    // 화면 해상도
    private void setupCamera()
    {
        Screen.SetResolution(1280, 800, false);
    }
    
}
