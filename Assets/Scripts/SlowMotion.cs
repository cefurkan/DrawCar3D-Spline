using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public void StartSlowMotionEffect()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void StopSlowMotionEffect()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02F;
    }
}
