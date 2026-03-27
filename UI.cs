using System.Text;
using UnityEngine;

public class UI : MonoBehaviour
{
    int _panel = 0;

    string _chatInput = "";
    string _chatHistory =
        "Bot: Hello!\n\nTry: temprature monitor with wifi module";
    Vector2 _scroll;

    void OnGUI()
    {
        DrawSidebar();
        DrawContent();
    }

    void DrawSidebar()
    {
        GUI.Box(new Rect(0, 0, 200, Screen.height), "");

        string[] navs =
        {
            "Dashboard",
            "BOM",
            "Chat Assistant"
        };

        for (int i = 0; i < navs.Length; i++)
        {
            if (GUI.Button(new Rect(10, 50 + i * 60, 180, 40), navs[i]))
                _panel = i;
        }
    }

    void DrawContent()
    {
        switch (_panel)
        {
            case 0: PanelDashboard(); break;
            case 1: PanelBOM(); break;
            case 2: PanelChat(); break;
        }
    }

    void PanelDashboard()
    {
        GUI.Label(new Rect(220, 50, 500, 30), "Dashboard", GUI.skin.label);
    }

    void PanelBOM()
    {
        GUI.Label(new Rect(220, 50, 500, 30), "Bill of Materials");

        GUI.Box(new Rect(220, 100, 400, 250), "");

        GUI.Label(new Rect(240, 130, 300, 25), "1. ESP32 / ESP8266");
        GUI.Label(new Rect(240, 160, 300, 25), "2. DHT11 / DHT22 / LM35");
        GUI.Label(new Rect(240, 190, 300, 25), "3. Power Supply");
        GUI.Label(new Rect(240, 220, 300, 25), "4. Breadboard / PCB");
        GUI.Label(new Rect(240, 250, 300, 25), "5. Jumper Wires");
        GUI.Label(new Rect(240, 280, 300, 25), "6. Resistor");
        GUI.Label(new Rect(240, 310, 300, 25), "7. LED");
    }

    void PanelChat()
    {
        GUI.Label(new Rect(220, 50, 500, 30), "Chat Assistant");

        GUI.Box(new Rect(220, 90, 500, 350), "");

        Rect scrollRect = new Rect(230, 100, 480, 250);
        Rect viewRect = new Rect(0, 0, 460, 800);

        _scroll = GUI.BeginScrollView(scrollRect, _scroll, viewRect);

        GUI.Label(new Rect(10, 10, 440, 780), _chatHistory);

        GUI.EndScrollView();

        _chatInput = GUI.TextField(new Rect(230, 370, 350, 30), _chatInput);

        if (GUI.Button(new Rect(600, 370, 100, 30), "Send"))
        {
            SubmitChat();
        }

        if (Event.current.type == EventType.KeyDown &&
            Event.current.keyCode == KeyCode.Return)
        {
            SubmitChat();
        }
    }

    void SubmitChat()
    {
        if (string.IsNullOrWhiteSpace(_chatInput)) return;

        _chatHistory += "\n\nYou: " + _chatInput;
        _chatHistory += "\n\nBot: " + GetResponse(_chatInput);

        _chatInput = "";
    }

    string GetResponse(string input)
    {
        string n = input.ToLower();

        if (n.Contains("temprature monitor with wifi module") ||
            n.Contains("temperature monitor with wifi module"))
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Required components:");
            sb.AppendLine("1. ESP32 or ESP8266");
            sb.AppendLine("2. One sensor (choose one):");
            sb.AppendLine("   - DHT11");
            sb.AppendLine("   - DHT22");
            sb.AppendLine("   - LM35");
            sb.AppendLine("3. Power supply");
            sb.AppendLine("4. Breadboard or PCB");
            sb.AppendLine("5. Jumper wires");
            sb.AppendLine("6. Resistor");
            sb.AppendLine("7. LED");

            return sb.ToString();
        }

        return "Try: temprature monitor with wifi module";
    }
}