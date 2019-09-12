using System.Collections;

using UnityEngine;

public class Player: MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab = null; // This will eliminate errors
    [SerializeField]
    private GameObject _tripleShotPrefab = null;
    [SerializeField]
    private GameObject _shieldsVisualizer = null;
    [SerializeField]
    private GameObject _tripleShotContainer = null;
    [SerializeField]
    private GameObject _leftWing, _rigthWing;
    [SerializeField]
    private GameObject _explosonPrefab;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private bool _isPlayerOne;
    [SerializeField]
    private bool _isPlayerTwo;
    private float _speed = 8f;
    private float _fireSpeed = 0.25f;
    private float _canFire = -1f;
    private int _lives = 3;
    private bool isTripleShotActive;
    private bool isSpeedBostActive;
    private bool isShieldsActive;
    private int _score;
    // Because I do not have a collision, I canon grab the object, by accessing "other"
    private Spawn_Manager spawn_Manager = null;
    private UI_Manager uI_Manager = null;
    private GameManager gameManager = null;
    private Animator animator;

    const string CANVAS = "Canvas", SPAWNMANAGER = "Spawn_Manager", GAMEMANAGER = "Game_Manager";

    void Awake() {
        spawn_Manager = GameObject.Find(SPAWNMANAGER).GetComponent<Spawn_Manager>() ?? null;
        uI_Manager = GameObject.Find(CANVAS).GetComponent<UI_Manager>() ?? null;
        gameManager = GameObject.Find(GAMEMANAGER).GetComponent<GameManager>() ?? null;
        _audioSource = GetComponent<AudioSource>() ?? null;
        animator = GetComponent<Animator>() ?? null;
        _audioSource.clip = _laserClip;

        if (!gameManager.CoopMode()) {
            transform.position = new Vector3(0, 0, 0);
        }
    }
    // Update is called once per frame
    void Update() {
        Movement();
        Fire();
        PlayerBouds();
    }

    private void Fire() {
        if (_isPlayerOne) {
            if (Input.GetKeyDown(KeyCode.Q) && Time.time > _canFire) {
                if (isTripleShotActive) {
                    GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    tripleShot.transform.parent = _tripleShotContainer.transform;
                }
                _canFire = Time.time + _fireSpeed;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                _audioSource.Play();
            }
        }
        if (_isPlayerTwo) {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) {
                if (isTripleShotActive) {
                    GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                    tripleShot.transform.parent = _tripleShotContainer.transform;
                }
                _canFire = Time.time + _fireSpeed;
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
                _audioSource.Play();
            }
        }
    }

    private void Movement() {

        if (_isPlayerOne) {
            if (Input.GetKeyDown(KeyCode.W)) {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.S)) {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.D)) {
                //Left animation
                animator.SetBool("Right", true);
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                //Right animation
                animator.SetBool("Left", true);
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
              } else {
                animator.SetBool("Left", false);
                animator.SetBool("Right", false);
            }
        }

        if (_isPlayerTwo) {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }
    }

    private void PlayerBouds() {
        if (transform.position.y > 5.95f) {
            transform.position = new Vector3(transform.position.x, 5.95f, 0f);
        } else if (transform.position.y < -3.98f) {
            transform.position = new Vector3(transform.position.x, -3.98f, 0f);
        }
        if (transform.position.x > 9.2f) {
            transform.position = new Vector3(9.2f, transform.position.y, 0f);
        } else if (transform.position.x < -9.3f) {
            transform.position = new Vector3(-9.3f, transform.position.y, 0f);
        }
    }

    internal void TakeDamage() {

        if (isShieldsActive) {
            _lives++;
            isShieldsActive = false;
            _shieldsVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if (_lives == 2) {
            _leftWing.SetActive(true);
        } else if (_lives == 1) {
            _rigthWing.SetActive(true);
        }

        uI_Manager?.UpdateLives(_lives);

        if (_lives < 1) {
            Instantiate(_explosonPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            // I need to tell the Spawn_Manager, to stop
            spawn_Manager.isPlayerDead(true);
        }
    }

    internal void SpeedUp() {

        isSpeedBostActive = true;
        _speed += 2.5f;
        StartCoroutine(SpeedBostConlDown(5f));
    }

    internal void TripleShot() {

        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDowRowtine(5.0f));
    }

    internal void ShieldsActive() {

        isShieldsActive = true;
        _shieldsVisualizer.SetActive(true);
    }

    internal void UpdateScore(int point) {

        _score += point;
        uI_Manager.UpdateText(_score);
    }

    IEnumerator TripleShotPowerDowRowtine(float time) {

        yield return new WaitForSeconds(time);
        isTripleShotActive = false;
    }

    IEnumerator SpeedBostConlDown(float time) {

        yield return new WaitForSeconds(time);
        _speed = _speed - 2.5f;
    }
}
