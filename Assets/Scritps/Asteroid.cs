using UnityEngine;

public class Asteroid: MonoBehaviour {

    [SerializeField]
    private float _rotateSpeed = 20f;
    // Update is called once per frame
    [SerializeField]
    private GameObject _explosonPrefab;
    [SerializeField]
    private Spawn_Manager Spawn_Manager;
    
    // This line is only yo avoid errors
    private const string ENEMY = "Enemy", PLAYER = "Player", ASTEROID = "Asteroid", LASER = "Laser", SPAWNMANAGER = "Spawn_Manager";

    void Awake() {
          Spawn_Manager = GameObject.Find(SPAWNMANAGER).GetComponent<Spawn_Manager>() ?? null;
    }
    void Update() {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == ASTEROID) {

               if (transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
        }

        if (other.tag == LASER) {

            Instantiate(_explosonPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0.12f);
            Spawn_Manager.SpawnRoutines();
        }
        else if (other.name == PLAYER) {

            return;
        }

        
    }
}
