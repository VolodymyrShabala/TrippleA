using UnityEngine;

public class LifeTime : MonoBehaviour {
    [SerializeField]
    private float lifeLength = 30;
    private float lifeTime = 0;

    private void Update() {
        lifeTime += Time.fixedDeltaTime;
        if(lifeTime >= lifeLength) {
            Destroy(gameObject);
        }
    }
}