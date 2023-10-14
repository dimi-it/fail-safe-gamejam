using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private float _timeRange;
    private ProjectileMain _projectileMain;
    private GameObject _owner;

    private void Start()
    {
        _projectileMain = this.GetComponent<ProjectileMain>();
        _owner = _projectileMain.Owner;
        this.transform.parent = _owner.transform;
        Invoke(nameof(OnTimerElapsed), _timeRange);
    }

    private void Update()
    {

        transform.RotateAround(_owner.transform.position, Vector3.up, 180 /( _timeRange * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetInstanceID() == _owner.GetInstanceID())
        {
            return;
        }
        if (otherObj.CompareTag("Player"))
        {
            CollideOnPlayer(otherObj);
        }
    }

    private void CollideOnPlayer(GameObject player)
    {
        player.GetComponent<CharacterHealt>().DecreaseLife(_projectileMain.Damage);
        Destroy(this.gameObject);
    }

    private void OnTimerElapsed()
    {
        Destroy(this.gameObject);
    }
}
