using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeDelta : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private AttributeText attributeText;
    [SerializeField] private float floatSpeed = 0.5f;
    [SerializeField] private float lifetime = 1f;

    private float life = 0.01f;

    public void AssignAttributes(Vector3 attributes)
    {
        attributeText.UpdateAttributeTexts(attributes, Vector3.zero);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * Time.deltaTime * floatSpeed;

        // fade out the color
        attributeText.Loyalty.color = new Color(attributeText.Loyalty.color.r, attributeText.Loyalty.color.g, attributeText.Loyalty.color.b, lifetime - life);
        attributeText.Might.color = new Color(attributeText.Might.color.r, attributeText.Might.color.g, attributeText.Might.color.b, lifetime - life);
        attributeText.Influence.color = new Color(attributeText.Influence.color.r, attributeText.Influence.color.g, attributeText.Influence.color.b, lifetime - life);

        life += Time.deltaTime;

        // destroy if it dies
        if (life >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
