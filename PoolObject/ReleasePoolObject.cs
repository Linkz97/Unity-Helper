using LinkzJ.Games.Ultilities.PoolObject;
using UnityEngine;

public class ReleasePoolObject : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager.PoolType poolType;

    public void OnRelease()
    {
        ObjectPoolManager.ReturnObject(this.gameObject, poolType);
    }
}
