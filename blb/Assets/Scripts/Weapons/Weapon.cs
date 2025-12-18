using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Boomerang;
    public GameObject BombSpawner;
    public GameObject Gun;
    public GameObject LazerSpawner;
    public GameObject ZoneSpawner;

    public int SwordLevel;
    public int BoomerangLevel;
    public int BombSpawnerLevel;
    public int GunLevel;
    public int LazerSpawnerLevel;
    public int ZoneSpawnerLevel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMovements player = GetComponent<PlayerMovements>();

        if (SwordLevel >= 1 && Sword != null)
        {
            GameObject sword = Instantiate(Sword, transform);
            sword.transform.localPosition = Vector3.right;
            sword.GetComponent<Sword>().level = SwordLevel;
            sword.GetComponent<Sword>().player = player;
        }

        if (BoomerangLevel >= 1 && Boomerang != null)
        {
            GameObject boomerang = Instantiate(Boomerang, transform);
            boomerang.transform.localPosition = Vector3.right;
            boomerang.GetComponent<Boomerang>().level = BoomerangLevel;
            boomerang.GetComponent<Boomerang>().player = player;
        }

        if (BombSpawnerLevel >= 1 && BombSpawner != null)
        {
            GameObject bombspawner = Instantiate(BombSpawner, transform);
            bombspawner.transform.localPosition = Vector3.right;
            bombspawner.GetComponent<BombSpawner>().level = BombSpawnerLevel;
            bombspawner.GetComponent<BombSpawner>().player = player;
        }

        if (GunLevel >= 1 && Gun != null)
        {
            GameObject gun = Instantiate(Gun, transform);
            gun.transform.localPosition = Vector3.right;
            Gun gunScript = gun.GetComponent<Gun>();
            gunScript.level = GunLevel;
            gunScript.player = player;
        }



        if (LazerSpawnerLevel >= 1 && LazerSpawner != null)
        {
            GameObject lazerSpawner = Instantiate(LazerSpawner, transform);
            lazerSpawner.transform.localPosition = Vector3.right;
            lazerSpawner.GetComponent<LazerSpawner>().level = LazerSpawnerLevel;
            lazerSpawner.GetComponent<LazerSpawner>().player = player;
        }

        if (ZoneSpawnerLevel >= 1 && ZoneSpawner != null)
        {
            GameObject zone = Instantiate(ZoneSpawner, transform);
            zone.transform.localPosition = Vector3.right;
            zone.GetComponent<ZoneSpawner>().level = ZoneSpawnerLevel;
            zone.GetComponent<ZoneSpawner>().player = player;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);
    }
}
