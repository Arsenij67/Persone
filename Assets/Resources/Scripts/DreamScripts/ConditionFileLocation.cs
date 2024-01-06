using System.IO;

public class ConditionFileLocation : ConditionData
{

    public override void CheckData(string path)
    {
        inputField.text = "";
        base.CheckData(path);
        if (string.IsNullOrEmpty(path)) SetFalse();

        else
        {
            inputField.text = Path.GetFileName(path);
            SetTrue();
        }
    }
}
