using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    //Controls the Monster Movement Behaviours
    [SerializeField] private Animator anim;
    [SerializeField] private bool _MonsterWalks, _MonsterLooksAround;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maximumRotationInterval;
    private bool _reachedXMaxPos;
    private float _xMaxPos = 70;
    private float _xMinPos = -10;

    private void Start()
    {
        if (_MonsterLooksAround)
        {
            StartCoroutine(ChangeRotation(_maximumRotationInterval));
        }
    }

    private void Update()
    {
        if (_MonsterWalks)
        {
            transform.Translate(Vector2.right * Time.deltaTime * _movementSpeed);
            CheckLocationChangeDirection();
            float spd = Mathf.Abs(_movementSpeed);
            anim.SetFloat("RunningSpeed", spd);
        }
    }

    private void CheckLocationChangeDirection()
    {
        if (transform.position.x > _xMaxPos && !_reachedXMaxPos)
        {
            _reachedXMaxPos = true;
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z);

        }
        else if (transform.position.x < _xMinPos && _reachedXMaxPos)
        {
            _reachedXMaxPos = false;
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
        }
    }

    private IEnumerator ChangeRotation(float _aMaxTimeBetweenRotations)
    {
        while (_MonsterLooksAround)
        {
            yield return new WaitForSeconds(Random.Range(3, _aMaxTimeBetweenRotations));
            transform.Rotate(transform.rotation.x, 180, transform.rotation.z);
        }
    }


}
