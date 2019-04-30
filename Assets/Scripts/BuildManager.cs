using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static BuildManager instance;

    

    void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    private GameObject turretToBuild;

    public GameObject standartTurretPref;

    public GameObject missileTurretPref;

    void Start()
    {
        turretToBuild = standartTurretPref;
    }

    public void SetStandartTurretToBuild()
    {
        turretToBuild = standartTurretPref;
    }

    public void SetMissileTurretToBuild()
    {
        turretToBuild = missileTurretPref;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
