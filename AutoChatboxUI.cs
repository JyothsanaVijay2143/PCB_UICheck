using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AutoChatboxUI : MonoBehaviour
{
    private Canvas canvas;
    private GameObject chatPanel;
    private Text chatOutput;
    private InputField userInput;
    private Button sendButton;
    private ScrollRect scrollRect;
    private RectTransform contentRect;

    private void Start()
    {
        CreateUI();
        AppendBotMessage("Hello! Type: temprature monitor with wifi module");
    }

    private void CreateUI()
    {
        // Create Canvas
        GameObject canvasObj = new GameObject("AutoChatCanvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObj.AddComponent<GraphicRaycaster>();

        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        // Create EventSystem if missing
        if (FindFirstObjectByType<EventSystem>() == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<StandaloneInputModule>();
        }

        // Main chat panel
        chatPanel = CreateUIObject("ChatPanel", canvasObj.transform);
        Image panelImage = chatPanel.AddComponent<Image>();
        panelImage.color = new Color(0.08f, 0.08f, 0.08f, 0.88f);

        RectTransform panelRect = chatPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0f, 0f);
        panelRect.anchorMax = new Vector2(0f, 0f);
        panelRect.pivot = new Vector2(0f, 0f);
        panelRect.sizeDelta = new Vector2(420f, 600f);
        panelRect.anchoredPosition = new Vector2(20f, 20f);

        // Title
        GameObject titleObj = CreateUIObject("Title", chatPanel.transform);
        Text titleText = titleObj.AddComponent<Text>();
        titleText.text = "Component Assistant";
        titleText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        titleText.fontSize = 24;
        titleText.fontStyle = FontStyle.Bold;
        titleText.color = Color.white;
        titleText.alignment = TextAnchor.MiddleCenter;

        RectTransform titleRect = titleObj.GetComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0f, 1f);
        titleRect.anchorMax = new Vector2(1f, 1f);
        titleRect.pivot = new Vector2(0.5f, 1f);
        titleRect.sizeDelta = new Vector2(0f, 50f);
        titleRect.anchoredPosition = Vector2.zero;

        // Scroll view
        GameObject scrollViewObj = CreateUIObject("ScrollView", chatPanel.transform);
        Image scrollBg = scrollViewObj.AddComponent<Image>();
        scrollBg.color = new Color(0.14f, 0.14f, 0.14f, 0.95f);

        scrollRect = scrollViewObj.AddComponent<ScrollRect>();
        RectTransform scrollViewRect = scrollViewObj.GetComponent<RectTransform>();
        scrollViewRect.anchorMin = new Vector2(0f, 0f);
        scrollViewRect.anchorMax = new Vector2(1f, 1f);
        scrollViewRect.offsetMin = new Vector2(15f, 80f);
        scrollViewRect.offsetMax = new Vector2(-15f, -60f);

        // Viewport
        GameObject viewportObj = CreateUIObject("Viewport", scrollViewObj.transform);
        Image viewportImage = viewportObj.AddComponent<Image>();
        viewportImage.color = new Color(1f, 1f, 1f, 0.01f);

        Mask viewportMask = viewportObj.AddComponent<Mask>();
        viewportMask.showMaskGraphic = false;

        RectTransform viewportRect = viewportObj.GetComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.offsetMin = Vector2.zero;
        viewportRect.offsetMax = Vector2.zero;

        // Content
        GameObject contentObj = CreateUIObject("Content", viewportObj.transform);
        contentRect = contentObj.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0f, 1f);
        contentRect.anchorMax = new Vector2(1f, 1f);
        contentRect.pivot = new Vector2(0.5f, 1f);
        contentRect.sizeDelta = new Vector2(0f, 1200f);
        contentRect.anchoredPosition = Vector2.zero;

        // Chat output text
        GameObject outputObj = CreateUIObject("ChatOutput", contentObj.transform);
        chatOutput = outputObj.AddComponent<Text>();
        chatOutput.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        chatOutput.fontSize = 20;
        chatOutput.color = Color.white;
        chatOutput.alignment = TextAnchor.UpperLeft;
        chatOutput.horizontalOverflow = HorizontalWrapMode.Wrap;
        chatOutput.verticalOverflow = VerticalWrapMode.Overflow;
        chatOutput.supportRichText = true;
        chatOutput.text = "";

        RectTransform outputRect = outputObj.GetComponent<RectTransform>();
        outputRect.anchorMin = new Vector2(0f, 1f);
        outputRect.anchorMax = new Vector2(1f, 1f);
        outputRect.pivot = new Vector2(0.5f, 1f);
        outputRect.offsetMin = new Vector2(10f, -1180f);
        outputRect.offsetMax = new Vector2(-10f, 0f);

        scrollRect.viewport = viewportRect;
        scrollRect.content = contentRect;
        scrollRect.horizontal = false;
        scrollRect.vertical = true;

        // Input field background
        GameObject inputObj = CreateUIObject("UserInput", chatPanel.transform);
        Image inputImage = inputObj.AddComponent<Image>();
        inputImage.color = Color.white;

        userInput = inputObj.AddComponent<InputField>();

        RectTransform inputRect = inputObj.GetComponent<RectTransform>();
        inputRect.anchorMin = new Vector2(0f, 0f);
        inputRect.anchorMax = new Vector2(0f, 0f);
        inputRect.pivot = new Vector2(0f, 0f);
        inputRect.sizeDelta = new Vector2(280f, 45f);
        inputRect.anchoredPosition = new Vector2(15f, 15f);

        // Input text
        GameObject inputTextObj = CreateUIObject("Text", inputObj.transform);
        Text inputText = inputTextObj.AddComponent<Text>();
        inputText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        inputText.fontSize = 18;
        inputText.color = Color.black;
        inputText.alignment = TextAnchor.MiddleLeft;
        inputText.supportRichText = false;

        RectTransform inputTextRect = inputTextObj.GetComponent<RectTransform>();
        inputTextRect.anchorMin = Vector2.zero;
        inputTextRect.anchorMax = Vector2.one;
        inputTextRect.offsetMin = new Vector2(10f, 6f);
        inputTextRect.offsetMax = new Vector2(-10f, -7f);

        // Placeholder
        GameObject placeholderObj = CreateUIObject("Placeholder", inputObj.transform);
        Text placeholderText = placeholderObj.AddComponent<Text>();
        placeholderText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        placeholderText.fontSize = 18;
        placeholderText.text = "Type here...";
        placeholderText.fontStyle = FontStyle.Italic;
        placeholderText.color = new Color(0.5f, 0.5f, 0.5f, 0.85f);
        placeholderText.alignment = TextAnchor.MiddleLeft;

        RectTransform placeholderRect = placeholderObj.GetComponent<RectTransform>();
        placeholderRect.anchorMin = Vector2.zero;
        placeholderRect.anchorMax = Vector2.one;
        placeholderRect.offsetMin = new Vector2(10f, 6f);
        placeholderRect.offsetMax = new Vector2(-10f, -7f);

        userInput.textComponent = inputText;
        userInput.placeholder = placeholderText;
        userInput.lineType = InputField.LineType.SingleLine;

        // Send button
        GameObject buttonObj = CreateUIObject("SendButton", chatPanel.transform);
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.5f, 1f, 1f);

        sendButton = buttonObj.AddComponent<Button>();
        sendButton.targetGraphic = buttonImage;

        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0f, 0f);
        buttonRect.anchorMax = new Vector2(0f, 0f);
        buttonRect.pivot = new Vector2(0f, 0f);
        buttonRect.sizeDelta = new Vector2(100f, 45f);
        buttonRect.anchoredPosition = new Vector2(300f, 15f);

        // Button text
        GameObject buttonTextObj = CreateUIObject("Text", buttonObj.transform);
        Text buttonText = buttonTextObj.AddComponent<Text>();
        buttonText.text = "Send";
        buttonText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        buttonText.fontSize = 20;
        buttonText.color = Color.white;
        buttonText.alignment = TextAnchor.MiddleCenter;

        RectTransform buttonTextRect = buttonTextObj.GetComponent<RectTransform>();
        buttonTextRect.anchorMin = Vector2.zero;
        buttonTextRect.anchorMax = Vector2.one;
        buttonTextRect.offsetMin = Vector2.zero;
        buttonTextRect.offsetMax = Vector2.zero;

        // Button click
        sendButton.onClick.AddListener(HandleSend);
    }

    private GameObject CreateUIObject(string objectName, Transform parent)
    {
        GameObject obj = new GameObject(objectName);
        obj.transform.SetParent(parent, false);
        obj.AddComponent<RectTransform>();
        return obj;
    }

    private void HandleSend()
    {
        if (userInput == null)
            return;

        string input = userInput.text.Trim();

        if (string.IsNullOrEmpty(input))
            return;

        AppendUserMessage(input);
        string response = GetResponse(input);
        AppendBotMessage(response);

        userInput.text = "";
        userInput.ActivateInputField();

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    private string GetResponse(string input)
    {
        string normalized = input.ToLower().Trim();

        if (normalized == "temprature monitor with wifi module" ||
            normalized == "temperature monitor with wifi module")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Required components:");
            sb.AppendLine();
            sb.AppendLine("1. ESP32 or ESP8266");
            sb.AppendLine("2. One temperature sensor (choose any one):");
            sb.AppendLine("   - DHT11");
            sb.AppendLine("   - DHT22");
            sb.AppendLine("   - LM35");
            sb.AppendLine("3. Power supply (5V USB or battery)");
            sb.AppendLine("4. Breadboard or PCB");
            sb.AppendLine("5. Jumper wires");
            sb.AppendLine("6. Resistor");
            sb.AppendLine("7. LED indicator");
            sb.AppendLine();
            sb.AppendLine("OLED is excluded.");
            return sb.ToString();
        }

        return "I only recognize this query: temprature monitor with wifi module";
    }

    private void AppendUserMessage(string message)
    {
        chatOutput.text += "\n\n<color=cyan><b>You:</b></color> " + message;
    }

    private void AppendBotMessage(string message)
    {
        chatOutput.text += "\n\n<color=lime><b>Bot:</b></color> " + message;
    }
}