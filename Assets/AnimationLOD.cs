using UnityEngine;

public class AnimationLOD : MonoBehaviour
{
    private Animator _animator;
    private float _timer;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        _timer = 0f;
        AnimationLODManager.Instance.AddAnimationLOD(this);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (dist < AnimationLODManager.Instance.CameraDistance)
        { // 가까우면 매 프레임 (부드럽게)
            _animator.Update(Time.deltaTime);
        }
        else
        { // 멀면 특정 시간마다 업데이트 (뚝뚝 끊기게)
            _timer += Time.deltaTime;
            if (_timer >= AnimationLODManager.Instance.Timer)
            { // 약 10 FPS 효과
                _animator.Update(_timer);
                _timer = 0;
            }
        }
    }

}
