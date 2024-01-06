using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {
	public float speed = 3.0f;
    private float Speed;

    
	public float obstacleRange = 5.0f;
	
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;
    private float FireRate = 1f;
	
	private bool _alive;
	
	void Start() {
		_alive = true;
         Speed = speed ;
        StartCoroutine(WanderingCoroutine());
      
        
	}
   

	public  IEnumerator WanderingCoroutine()
	{
        while (true)
        {
            if (_alive)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);

                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.SphereCast(ray, 1f, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if (hitObject.gameObject.CompareTag("Player"))
                    {
                        if (_fireball == null)
                        {
                            _fireball = Instantiate(fireballPrefab) as GameObject;
                            _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                            _fireball.transform.rotation = transform.rotation;
                            speed = 0;
                            yield return new WaitForSeconds(FireRate);
                        }
                    }
                    else if (hit.distance < obstacleRange)
                    {
                        float angle = Random.Range(-110, 110);

                        transform.Rotate(0, angle, 0);
                    }

                    else if (!hitObject.gameObject.CompareTag("Player"))
                    {
                        speed = Speed;
                     
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }



    }

	 
	public void SetAlive(bool alive) {
		_alive = alive;
	}
}
