using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    public Color hColor;

    private Color startColor;

    private GameObject turret;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        if (ShopManager.instance.IsEnoughToBuy(turretToBuild.GetComponent<TurretScript>().Price))
        {
            ShopManager.instance.BuyLogic(turretToBuild.GetComponent<TurretScript>().Price);
            turret = (GameObject)Instantiate(turretToBuild, transform.position + new Vector3(0, .5f, 0), transform.rotation);
        }
        
        

    }

    void OnMouseEnter()
    {
        if (turret != null) return;
        rend.material.color = hColor;
        this.transform.localScale *= 1.1f;
    }


    void OnMouseExit()
    {
        if (turret != null) return;
        rend.material.color = startColor;
        this.transform.localScale /= 1.1f;
    }

}
