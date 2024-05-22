using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private void Awake()
    {
        // Disable the default cursor
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        // Make the custom sprite follow the cursor
        if (Camera.main is not null)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos;
        }
    }
}
