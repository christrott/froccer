using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPool : MonoBehaviour {
    public int maxPoolSize;
    public GameObject poolItem;

    private List<GameObject> pool;

	// Use this for initialization
	void Start () {
        pool = new List<GameObject>();
        while (pool.Count < maxPoolSize)
        {
            GameObject car = Instantiate(poolItem);
            car.SetActive(false);
            pool.Add(car);
        }
	}
	
	public GameObject GetNext()
    {
        if (pool.Count > 0)
        {
            GameObject found = pool[0];
            found.SetActive(true);
            pool.Remove(found);
            return found;
        } else
        {
            return null;
        }
    }

    public void Recycle(GameObject usedObject)
    {
        usedObject.SetActive(false);
        pool.Add(usedObject);
    }
}
