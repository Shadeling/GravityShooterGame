using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyVision))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(EnemyHealth))]
public class Zombie : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] PatrolPoints;
    [SerializeField] int speed = 3;
    [SerializeField] float damage = 10;
    [SerializeField] float distanceToAttack = 0.5f  ;
    [SerializeField] AudioClip[] audioClips;


    private float _attackCooldown = 2.18f;
    private NavMeshAgent _agent;
    private EnemyVision _enemyVision;
    private bool _hasTarget;
    private Animator _animator;
    private float _time;
    private GameObject _player;
    private int _speedMult = 6;
    private bool _isDead = false;
    private AudioSource _audioSource;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyVision = GetComponent<EnemyVision>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _agent.speed = speed;

        //_enemyVision.PlayerInVision += OnPlayerVision;
        _enemyVision.PlayerFirstTimeInVision += OnFirstVision;
        _enemyVision.PlayerLost += OnPlayerLost;
        GetComponent<EnemyHealth>().onEnemyDie += OnDie;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isDead)
        {
            _time += Time.deltaTime;
            if (!_agent.pathPending && _agent.remainingDistance < 0.5f && !_hasTarget)
            {
                _agent.speed = speed;
                _animator.SetInteger("Speed", speed);
                _animator.SetBool("IsAttacking", false);
                SetPatrolPoint();
                /*if (!_audioSource.isPlaying)
                {
                    _audioSource.clip = audioClips[0];
                }*/
            }

            if (_hasTarget)
            {
                _agent.destination = _player.transform.position;
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < distanceToAttack && _time > _attackCooldown)
                {
                    _animator.SetBool("IsAttacking", true);
                    Attack();
                    _time = 0;
                }
                if (Vector3.Distance(gameObject.transform.position, _player.transform.position) > distanceToAttack && _time > _attackCooldown/3)
                {
                    _agent.speed = speed * _speedMult;
                    _animator.SetBool("IsAttacking", false);
                }

            }
        }
    }

    private void SetPatrolPoint()
    {
        if (PatrolPoints.Length != 0)
        {
            var dest = PatrolPoints[Random.Range(0, PatrolPoints.Length)].position;
            _agent.destination = dest;
        }
    }

    private void OnFirstVision(Collider player)
    {
        _agent.speed = speed * _speedMult;
        _animator.SetInteger("Speed", speed*3);
        _hasTarget = true;
        _player = player.gameObject;
    }

    /*private void OnPlayerVision(Vector3 dest)
    {
        _agent.destination = dest;
    }*/

    private void OnPlayerLost()
    {
        _hasTarget = false;
        _animator.SetBool("IsAttacking", false);
        _agent.speed = speed;
        _animator.SetInteger("Speed", speed);
        SetPatrolPoint();
    }

    private void OnDie()
    {
        _isDead = true;
        _agent.speed = 0;
        _animator.SetBool("IsKilled", true);

        _audioSource.PlayOneShot(audioClips[2]);
    }



    private void Attack()
    {
        _agent.speed = 0;
        _audioSource.PlayOneShot(audioClips[1]);

        if (_player.TryGetComponent<Health>(out var playerHP))
        {
            playerHP.TakeDamage(damage);
        }
        //attack
    }

    public void SetPatrolPoints(Transform[] points)
    {
        PatrolPoints = points;
    }
}
