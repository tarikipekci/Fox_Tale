using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/tarikipek.ci/");
    }

    public void OpenLinkedIn()
    {
        Application.OpenURL("https://www.linkedin.com/in/tar%C4%B1k-ipek%C3%A7i-514a261bb/");
    }

    public void OpenYoutubeChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCMFL7ZBkfEbkPP51SiscuwA");
    }
}