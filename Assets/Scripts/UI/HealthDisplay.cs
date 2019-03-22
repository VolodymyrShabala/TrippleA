using UnityEngine;

public class HealthDisplay : MonoBehaviour {
    public void IncreaseHealth() {
        for(int i = 0; i < transform.childCount; ++i) {
            if(!transform.GetChild(i).gameObject.activeInHierarchy) {
                transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void DecreaseHealth() {
        for(int i = transform.childCount - 1; i > 0; --i) {
            if(transform.GetChild(i).gameObject.activeInHierarchy) {
                transform.GetChild(i).gameObject.SetActive(false);
                break;
            }
        }
    }
}