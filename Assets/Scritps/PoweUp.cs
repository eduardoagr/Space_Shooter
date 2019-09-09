using UnityEngine;

public class PoweUp: MonoBehaviour {
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private int _powerUpsID = 0;
    [SerializeField]
    private AudioClip _powerClip;

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5.77f) {

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        switch (other.tag) {
            case "Player":
                switch (_powerUpsID) {
                    case 0:
                        other.GetComponent<Player>()?.TripleShot();
                        break;
                    case 1:
                        other.GetComponent<Player>()?.SpeedUp();
                        break;
                    case 2:
                        other.GetComponent<Player>()?.ShieldsActive();
                        break;
                    default:
                        break;
                }
                AudioSource.PlayClipAtPoint(_powerClip, Camera.main.transform.position);
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
