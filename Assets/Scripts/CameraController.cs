using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;

	public float scrollSpeed = 5f;
	public float minY = 10f;
	public float maxY = 80f;


    void Start()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        /*if (GameManager.GameIsOver)
		{
			this.enabled = false;
			return;
		}*/

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
		{
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
		{
			transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
		{
			transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
		{
			transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");

		Vector3 posA = transform.position;

        //pos.y = Mathf.Lerp(pos.y, pos.y - scroll * 10000, 10f *Time.deltaTime);

        posA -= new Vector3(0, scroll * scrollSpeed * 100, 0);

        //Vector3.SmoothDamp(pos, pos - new Vector3(0, pos.y - scroll * 100, 0), ref tt, 1f);

		posA.y = Mathf.Clamp(posA.y, minY, maxY);

        if (transform.position != posA)
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, posA.y, Time.deltaTime * scrollSpeed), transform.position.z);

		//transform.position = pos;

	}
}
