using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAbility : Ability
{
    // Fields
    public GameObject iceParticlesPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    // Collision enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "IceParticles" &&
            other.gameObject.tag != "TriggerMotherGrave" && other.gameObject.tag != "TriggerOpenGateBeginning" &&
            other.gameObject.tag != "TriggerCloseGateBeginning" && other.gameObject.tag != "TriggerOpenGateEnd" &&
            other.gameObject.tag != "TriggerCloseGateEnd")
        {
            Instantiate(iceParticlesPrefab,
                new Vector3(transform.position.x, 3.7f, transform.position.z),
                Quaternion.identity);
            GameObject.Find("AudioManager").GetComponent<AudioMan>().Play("freeze-sound");
            Destroy(gameObject);
        }
    }
}
