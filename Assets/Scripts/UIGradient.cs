using UnityEngine;
using UnityEngine.UI;

public class UIGradient : BaseMeshEffect
{
    public enum Direction
    {
        Vertical,
        Horizontal,
        Diagonal
    }

    [Header("Gradient")]
    [SerializeField] private Direction direction = Direction.Vertical;
    [SerializeField] private Color colorA = new Color(0.05f, 0.08f, 0.18f, 1f);
    [SerializeField] private Color colorB = new Color(0.10f, 0.45f, 0.85f, 1f);

    public override void ModifyMesh(VertexHelper vertexHelper)
    {
        if (!IsActive() || vertexHelper.currentVertCount == 0)
        {
            return;
        }

        UIVertex vertex = new UIVertex();

        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;

        for (int i = 0; i < vertexHelper.currentVertCount; i++)
        {
            vertexHelper.PopulateUIVertex(ref vertex, i);
            minX = Mathf.Min(minX, vertex.position.x);
            maxX = Mathf.Max(maxX, vertex.position.x);
            minY = Mathf.Min(minY, vertex.position.y);
            maxY = Mathf.Max(maxY, vertex.position.y);
        }

        float width = Mathf.Max(0.001f, maxX - minX);
        float height = Mathf.Max(0.001f, maxY - minY);

        for (int i = 0; i < vertexHelper.currentVertCount; i++)
        {
            vertexHelper.PopulateUIVertex(ref vertex, i);

            float t;

            if (direction == Direction.Horizontal)
            {
                t = (vertex.position.x - minX) / width;
            }
            else if (direction == Direction.Diagonal)
            {
                float tx = (vertex.position.x - minX) / width;
                float ty = (vertex.position.y - minY) / height;
                t = (tx + ty) * 0.5f;
            }
            else
            {
                t = (vertex.position.y - minY) / height;
            }

            vertex.color = Color.Lerp(colorA, colorB, t);
            vertexHelper.SetUIVertex(vertex, i);
        }
    }
}
