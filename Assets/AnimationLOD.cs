using UnityEngine;

public class AnimationLOD : MonoBehaviour
{
    private Animator _animator;
    private float _timer;
    private SkinnedMeshRenderer _skinned;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _skinned = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    void Start()
    {
        //여러 객체가 한번에 Animator.Update()되는 것을 분산하기 위해 Timer에 랜덤한 초기값 설정
        _timer = Random.Range(0f, AnimationLODManager.Instance.Timer);
        AnimationLODManager.Instance.AddAnimationLOD(this);
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Camera.main.transform.position, transform.position);

        if (dist < AnimationLODManager.Instance.CameraDistance)
        { // 가까우면 매 프레임 (부드럽게)
            _animator.Update(Time.deltaTime);

            if (_quality != Quality.Bone4)
            {
                _quality = Quality.Bone4;
                _skinned.quality = SkinQuality.Bone4;
            }
        }
        else
        { // 멀면 특정 시간마다 업데이트 (뚝뚝 끊기게)
            _timer += Time.deltaTime;
            if (_timer >= AnimationLODManager.Instance.Timer)
            { // 약 10 FPS 효과

                if (_quality != Quality.Bone1)
                {
                    _quality = Quality.Bone1;
                    _skinned.quality = SkinQuality.Bone1;
                }


                _animator.Update(_timer);
                _timer = 0;
            }
        }
    }

    private Quality _quality = Quality.Bone4;
    private enum Quality
    {
        Bone1,
        Bone4
    }

    

}
