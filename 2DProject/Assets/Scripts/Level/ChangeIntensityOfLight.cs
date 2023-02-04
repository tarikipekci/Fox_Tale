using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ChangeIntensityOfLight : MonoBehaviour
{
    public GameObject block;
    public Transform blockLeft;
    public Transform blockRight;
    public new Light2D light;

    private void Update()
    {
        var position = PlayerController.instance.transform.position;
        var blockLeftDistance = Vector2.Distance(position, blockLeft.position);
        var blockRightDistance = Vector2.Distance(position, blockRight.position);

        if (blockLeftDistance < blockRightDistance)
        {
            light.intensity = 1;
        }
        else if (blockRightDistance < blockLeftDistance && PlayerController.instance.transform.position.y >= block.transform.position.y)
        {
            light.intensity = 0.3f;
        }
    }
}