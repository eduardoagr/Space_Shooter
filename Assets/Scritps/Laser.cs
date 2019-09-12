using UnityEngine;

public class Laser: MonoBehaviour {
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private bool isEnemyLaser;

    // This line is only yo avoid errors
    private const string ENEMY = "Enemy", PLAYER = "Player", ASTEROID = "Asteroid";

    void Start() {
    }
    void Update() {

        if (isEnemyLaser == false) {
            MoveUp();
        }
        else {
            MoveDown();
        }
    }

    private void MoveUp() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8.61f && this.gameObject != null) {

            Destroy(this.gameObject);

            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void MoveDown() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.61f && this.gameObject != null) {

            Destroy(this.gameObject);

            if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    internal void AssignEnemyLaser() {
        isEnemyLaser = true;
    }

    internal bool LaserCheck() {
        if (isEnemyLaser) {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == PLAYER && isEnemyLaser == true) {

            Player player = other.GetComponent<Player>() ?? null;

            player.TakeDamage();
        }
    }
}
