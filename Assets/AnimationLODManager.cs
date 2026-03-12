using System.Collections.Generic;
using UnityEngine;

public class AnimationLODManager : MonoBehaviour
{
    private static AnimationLODManager s_instance;
    public static AnimationLODManager Instance {  get { return s_instance; } }


    [SerializeField] float _cameraDistance = 10f;
    [SerializeField] float _timer = 0.2f;

    private List<AnimationLOD> _animationList = new List<AnimationLOD>();

    public float CameraDistance { get { return _cameraDistance; } }
    public float Timer { get { return _timer; } }


    void Awake()
    {
        s_instance = this;
    }

    public void AddAnimationLOD(AnimationLOD animationLOD)
    {
        _animationList.Add(animationLOD);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var item in _animationList)
            {
                item.enabled = !item.enabled;
                item.GetComponent<Animator>().enabled = !item.enabled;
            }
        }
    }

}
