using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    //Controls the Monster Movement Behaviours
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _MonsterWalks, _MonsterLooksAround;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maximumRotationInterval;
    private bool _reachedXMaxPos;
    private bool _chasingCharacter;
    private float _xMaxPos = 70;
    private float _xMinPos = -10;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

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
            if (!_chasingCharacter)
            {
                CheckLocationChangeDirection();
            }
            float spd = Mathf.Abs(_movementSpeed);
            _anim.SetFloat("RunningSpeed", spd);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _chasingCharacter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _chasingCharacter = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Character Caught!");
            //Trigger the Death of the Player
            //Trigger Attack Animation
            _MonsterWalks = false; //Stops the monster Walking (or it will just push the player).
        }
    }
}
