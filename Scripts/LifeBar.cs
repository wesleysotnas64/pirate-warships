using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public Transform ShipTransform;
    public Transform ChildLifeBar;
    public Vector3   FullLifeColor;
    public Vector3   HalfLifeColor;
    public Vector3   EmptyLifeColor;
    public Vector3   CurrentLifeColor;
    public float     yOffset;
    public SpriteRenderer ChildSprite;

    private void Awake()
    {
        FullLifeColor  = new Vector3(0, 1, 0);
        HalfLifeColor  = new Vector3(1, 1, 0);
        EmptyLifeColor = new Vector3(1, 0, 0);
        yOffset = 0.7f;
        ChildLifeBar = gameObject.transform.GetChild(0);
        ChildSprite  = ChildLifeBar.gameObject.GetComponent<SpriteRenderer>();
        UpdateColor(1);
    }

    private void Update()
    {
        if(ChildLifeBar != null)
        {
           transform.position = new Vector3
            (
                ShipTransform.position.x,
                ShipTransform.position.y + yOffset,
                ShipTransform.position.z
            );
        }
    }

    public void UpdateScale(float scale)
    {
        ChildLifeBar.localScale = new Vector3(scale, 1, 1);
        UpdateColor(scale);
    }

    private void UpdateColor(float interpolation)
    {
        if (interpolation > 0.5f)
        {
            interpolation    = (interpolation-0.5f)/0.5f;
            CurrentLifeColor = Vector3.Lerp(HalfLifeColor, FullLifeColor, interpolation);
        }
        else
        {
            interpolation    = interpolation / 0.5f;
            CurrentLifeColor = Vector3.Lerp(EmptyLifeColor, HalfLifeColor, interpolation);
        }

        ChildSprite.color = new Color
        (
            CurrentLifeColor.x,
            CurrentLifeColor.y,
            CurrentLifeColor.z,
            1
        );
    }
}
