using UnityEngine;
using UnityEngine.Pool;
public class NumberImagePool : MonoBehaviour
{
    [SerializeField] public GameObject digitPrefab;
    public IObjectPool<GameObject> pool { get; private set; }

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(digitPrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 50
        );
        WarmUpPool(20);
    }
    
    private void WarmUpPool(int count)
    {
        var tempArray = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            tempArray[i] = pool.Get();
        }

        for (int i = 0; i < count; i++)
        {
            pool.Release(tempArray[i]);
        }
    }

    public GameObject Get()
    {
        return pool.Get();
    }

    public void Release(GameObject obj)
    {
        pool.Release(obj);
    }
}