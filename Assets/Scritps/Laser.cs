using UnityEngine;

public class Laser: MonoBehaviour {
    [SerializeField]
    private float _speed = 10.0f;
    [SerializeField]
    private bool _isMovinDown;


    void Start() {
    }
    void Update() {

          Debug.Log("Update: " + _isMovinDown);
        if (_isMovinDown == false) {
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
        _isMovinDown = true;
        Debug.Log("AssignEnemyLaser: " + _isMovinDown);
    }
}
