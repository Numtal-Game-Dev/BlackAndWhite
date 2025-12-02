using UnityEngine;
using UnityEngine.UI;

public class SkillTreeLines : MonoBehaviour
{
    public GameObject linePrefab;
    public Transform lineContainer;
    public Color lineColor = Color.yellow;
    public float lineWidth = 2f;

    public void DrawLine(RectTransform start, RectTransform end)
    {
        if (start == null || end == null || linePrefab == null || lineContainer == null)
        {
            Debug.LogError("SkillTreeLines: Missing references for drawing a line.");
            return;
        }

        Canvas.ForceUpdateCanvases();
        GameObject lineObj = Instantiate(linePrefab, lineContainer);
        RectTransform lineRect = lineObj.GetComponent<RectTransform>();
        Image lineImage = lineObj.GetComponent<Image>();

        if (lineImage != null)
        {
            lineImage.color = lineColor;
        }

        Canvas canvas = GetComponentInParent<Canvas>(); Camera camera = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : canvas.worldCamera;
        Vector2 localStartPos, localEndPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(lineContainer.GetComponent<RectTransform>(), RectTransformUtility.WorldToScreenPoint(camera, start.position), camera, out localStartPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(lineContainer.GetComponent<RectTransform>(), RectTransformUtility.WorldToScreenPoint(camera, end.position), camera, out localEndPos);

        Vector2 direction = localEndPos - localStartPos;
        float distance = direction.magnitude;

        lineRect.pivot = new Vector2(0.5f, 0.5f);
        lineRect.anchoredPosition = localStartPos + direction / 2f;
        lineRect.sizeDelta = new Vector2(distance, lineWidth);
        lineRect.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    public void ClearLines()
    {
        foreach (Transform child in lineContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
