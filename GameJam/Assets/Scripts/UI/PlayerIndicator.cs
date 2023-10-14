using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    public Transform Unit { get; set; }
    Transform camTransform;
    CharacterMain characterMain;

    public Vector3 offset;
    private void OnEnable()
    {
        CharacterHealt.OnDeath += CharacterHealt_OnDeath;
    }
    private void OnDisable()
    {
        CharacterHealt.OnDeath -= CharacterHealt_OnDeath;
    }
    private void CharacterHealt_OnDeath(int id)
    {
        if (characterMain.ID == id)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
        characterMain = Unit.gameObject.GetComponent<CharacterMain>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camTransform.transform.position);
        transform.position = Unit.position + offset;
    }
}
