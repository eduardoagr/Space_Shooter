using UnityEngine;

public class Enemy: MonoBehaviour {
    [SerializeField]
    private float _speed = 4f;
    private Animator _anmin;
    private bool isAlive = true;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _enemyLaser;
    private float _fireRate = 3.0f;
    private float _canFire = -1;
    

    // This line is only yo avoid errors
    private const string ENEMY = "Enemy", LASER = "Laser", PLAYER = "Player";

    // I need to access the player, because I don't have reference to it when the laser hit an enemy, I need to find it 
    private Player player = null;

    void Start() {

        player = GameObject.Find(PLAYER).GetComponent<Player>();
        _anmin = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>() ?? null;
    }
    // Update is called once per frame
    void Update() {
        Movement();
        Fire();
    }

    private void Fire() {
        if (Time.time > _canFire) {

            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject Obj = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            // because we have 2 lasers. we have to get each separate one
            Laser[] lasers = Obj.GetComponentsInChildren<Laser>() ?? null;

            var length = lasers.Length;

            for (int i = 0; i < length; ++i) {

                lasers[i].AssignEnemyLaser();

            }
            //Debug.Break();
        }
    }

    internal void Movement() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime, 0);

        if (transform.position.y < -8.5f) {

            float randomX = Random.Range(-8.90f, 8.90f);
            transform.position = new Vector3(randomX, 8f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == PLAYER) {
            player?.TakeDamage();
            _anmin?.SetTrigger("Drstroy_Enemy");
            _speed = 0;
             _audioSource.Play();
            Destroy(this.gameObject, 2.3f);
        }


        else if (other.tag == LASER) {
            Destroy(other.gameObject);
              _audioSource.Play();
            _anmin?.SetTrigger("Drstroy_Enemy");
            _speed = 0;
            if (isAlive == true) {
                isAlive = false;
                 _audioSource.Play();
                Destroy(this.gameObject, 2.3f);
                GetComponent<PolygonCollider2D>().enabled = false;
                if (isAlive == false) {
                    player?.UpdateScore(2);
                }
            }
        }
    }
}
