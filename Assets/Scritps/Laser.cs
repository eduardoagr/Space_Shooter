using UnityEngine;

public class Laser: MonoBehaviour {
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private bool isEnemyLaser;

      // This line is only yo avoid errors
    private const string ENEMY = "Enemy", LASER = "Laser", PLAYER = "Player";

    void Start() {
    }
    void Update() {

          Debug.Log("Update: " + isEnemyLaser);
        if (isEnemyLaser == false) {
            MoveUp();
        }
        else {
            MoveDown();
            Debug.Log("Is getting hit");
        }
    }

    private void MoveUp() {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8.61f && this.gameObject != null) {

            Destroy(this.gameObject);
        }
    }

    private void MoveDown() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.61f && this.gameObject != null) {

            Destroy(this.gameObject);
        }
    }

    internal void AssignEnemyLaser(){
        isEnemyLaser = true;
    }

      private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == PLAYER && isEnemyLaser == true) {

            Player player = other.GetComponent<Player>() ?? null;

            player.TakeDamage();
        }
         else if (other.tag == ENEMY && isEnemyLaser != true) {

            Debug.Log("Enemy laser, friendly fire");

            return;
          
        }
    }
}
