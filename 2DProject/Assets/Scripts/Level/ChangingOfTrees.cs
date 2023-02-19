using Level;
using UnityEngine;

public class ChangingOfTrees : MonoBehaviour
{
    [Header("Components")] public SpriteRenderer sp;

    [Header("Sprite")] public Sprite treeSprite;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!(BossController.instance.hitPoints <= 0)) return;
        if (!(GlobalLightController.instance.lightingCounter > 0)) return;
        if (!(GlobalLightController.instance.globalLight.intensity ! <= 1)) return;
        if (sp.sprite == treeSprite) return;
        sp.sprite = treeSprite;
        sp.color = Color.white;
        gameObject.transform.position += new Vector3(0f, -4.03f, 0f);
    }
}