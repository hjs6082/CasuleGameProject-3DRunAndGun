using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private enum OperationType
    {
        plus,
        minus,
        multiplication,
        division
    }

    [SerializeField] private OperationType gateOperation;
    [SerializeField] private int value;

    [SerializeField] private TextMeshPro operationText;
    [SerializeField] private MeshRenderer forceField;
    [SerializeField] private Material[] operationTypeMaterial;

    private void Awake()
    {
        AssignOperation();
    }
    private void AssignOperation()
    {
        string finalText = "";

        if (gateOperation == OperationType.plus)
            finalText += "+";
        if (gateOperation == OperationType.minus)
            finalText += "-";
        if (gateOperation == OperationType.multiplication)
            finalText += "x";
        if (gateOperation == OperationType.division)
            finalText += "¡À";

        finalText += value.ToString();
        operationText.text = finalText;

        if (gateOperation == OperationType.plus || gateOperation == OperationType.multiplication)
            forceField.material = operationTypeMaterial[0];
        else
            forceField.material = operationTypeMaterial[1];
    }

    public void ExecuteOperation()
    {
        if (gateOperation == OperationType.plus)
            GameEvents.instance.bulletFireRate.Value += value;
        if (gateOperation == OperationType.minus)
        {
            GameEvents.instance.bulletFireRate.Value -= value;
            GameEvents.instance.bulletFireRate.Value = Mathf.Max(1, GameEvents.instance.bulletFireRate.Value);
        }
        if (gateOperation == OperationType.multiplication)
            GameEvents.instance.bulletFireRate.Value *= value;
        if (gateOperation == OperationType.division)
        {
            GameEvents.instance.bulletFireRate.Value /= value;
            GameEvents.instance.bulletFireRate.Value = Mathf.Max(1, GameEvents.instance.bulletFireRate.Value);
        }

        GetComponent<BoxCollider>().enabled = false;
        forceField.gameObject.SetActive(false);
    }
}