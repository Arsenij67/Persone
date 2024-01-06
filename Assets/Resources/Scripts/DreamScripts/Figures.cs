using UnityEngine;
using Button = UnityEngine.UI.Button;

public class Figures : MonoBehaviour
{

    private Button thisButton;

    internal bool isPressed = false;

    private void Awake()
    {
        thisButton = GetComponent<Button>();

        thisButton.onClick.AddListener(ChangeColorOfButton);
    }


    private void ChangeColorOfButton()
    {
        isPressed = true;

        var parent = transform.parent;

        Figures[] _butFig = parent.GetComponentsInChildren<Figures>();

        foreach (var fig in _butFig)
        {
            if (fig != this)
                fig.isPressed = false;
        }
      
        Cell.ButtonClicked.image.color = thisButton.image.color;


    }
}
   
 