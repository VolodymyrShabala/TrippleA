using UnityEngine;

public class CameraShake : MonoBehaviour {

    private static bool shaking = false;
    private static bool shakeForTime = false;
    private static float shakeAmount = 1;
    private static float timeToShake;
    private static Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        if(Time.time > timeToShake && shakeForTime) {
            StopShake();
            shakeForTime = false;
        }

        Shake();
    }

    private void Shake() {
        if(shaking) {
            Vector3 shakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = initialPosition + shakePos;
        } else {
            transform.position = initialPosition;
        }
    }

    public static void StartShake() {
        shaking = true;
    }

    public static void StopShake() {
        shaking = false;
    }

    public static void ShakeFor(float seconds) {
        if(seconds <= 0) {
            return;
        }
        shakeForTime = true;
        StartShake();
        timeToShake = Time.time + seconds;
    }
}