using UnityEngine;

namespace Game
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject settingsScreen;
        public GameObject mainMenuGameObject;
        public bool settings;

        private void Awake()
        {
            settings = false;
        }

        public void OpenTheSettings()
        {
            settings = true;

            settingsScreen.SetActive(true);
            mainMenuGameObject.SetActive(false);
        }

        private void Update()
        {
            if (settings)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    mainMenuGameObject.SetActive(true);
                    settings = false;
                    settingsScreen.SetActive(false);
                }
            }
        }
    }
}