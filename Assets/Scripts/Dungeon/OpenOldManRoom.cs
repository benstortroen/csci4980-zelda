using UnityEngine;

public class OpenOldManRoom : MonoBehaviour
{
    PushBlock pushBlock;
    [SerializeField] GameObject doorTile;
    [SerializeField] Sprite doorSprite;

    void Start()
    {
        pushBlock = GetComponent<PushBlock>();
        doorTile.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    void Update()
    {
        if (pushBlock.isPushed())
        {
            doorTile.GetComponent<BoxCollider2D>().isTrigger = true;
            doorTile.GetComponent<SpriteRenderer>().sprite = doorSprite;
        }

        GetComponent<OpenOldManRoom>().enabled = false;
    }
}
