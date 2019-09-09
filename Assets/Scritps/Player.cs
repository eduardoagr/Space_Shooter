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
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private float _speed = 8f;
    private float _fireSpeed = 0.25f;
    private float _canFire = -1f;
    private int _lives = 3;
    private bool isTripleShotActive = false;
    private bool isSpeedBostActive = false;
    private bool isShieldsActive = false;
    private int _score;
    // Because I do not have a collision, I canon grab the object, by accessing "other"
    private Spawn_Manager Spawn_Manager = null;
    private UI_Manager UI_Manager = null;

    // Start is called before the first frame update
    void Start() {
        transform.position = new Vector3(0, 0, 0);

        Spawn_Manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        UI_Manager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        _audioSource = GetComponent<AudioSource>() ?? null;
            
       _audioSource.clip = _laserClip;
    }
    // Update is called once per frame
    void Update() {
        PlayerMovement();
        Fire();
        PlayerBouds();
    }

    private void Fire() {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire) {
            if (isTripleShotActive) {
                GameObject tripleShot = Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                tripleShot.transform.parent = _tripleShotContainer.transform;
            }
            _canFire = Time.time + _fireSpeed;
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _audioSource.Play();
        }
    }

    private void PlayerMovement() {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0f) * _speed * Time.deltaTime);
    }

    private void PlayerBouds() {
        if (transform.position.y > 5.95f) {
            transform.position = new Vector3(transform.position.x, 5.95f, 0f);
        }
        else if (transform.position.y < -3.98f) {
            transform.position = new Vector3(transform.position.x, -3.98f, 0f);
        }
        if (transform.position.x > 9.2f) {
            transform.position = new Vector3(9.2f, transform.position.y, 0f);
        }
        else if (transform.position.x < -9.3f) {
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
        }
        else if (_lives == 1) {
            _rigthWing.SetActive(true);
        }

        UI_Manager?.UpdateLives(_lives);

        if (_lives < 1) {
            Destroy(this.gameObject);
            // I need to tell the Spawn_Manager, to stop
            Spawn_Manager?.isPlayerDead(true);
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
        UI_Manager?.UpdateText(_score);
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
