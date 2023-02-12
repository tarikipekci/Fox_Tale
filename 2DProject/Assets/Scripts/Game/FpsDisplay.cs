using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class FpsDisplay : MonoBehaviour
    {
        public float timer, refresh, avgFramerate;
        public string display = "{0} FPS";
        public Text fpsDisplay;
        
        private void Awake()
        {
            fpsDisplay.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F2) && fpsDisplay.enabled == false)
            {
                fpsDisplay.enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.F2) && fpsDisplay.enabled)
            {
                fpsDisplay.enabled = false;
            }
            
            var timeLapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timeLapse;
            if (timer <= 0) avgFramerate = (int)(1f / timeLapse);
            timer -= timeLapse;
            // ReSharper disable once SpecifyACultureInStringConversionExplicitly
            fpsDisplay.text = string.Format(display, avgFramerate.ToString());
        }
    }
}