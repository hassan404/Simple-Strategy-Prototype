using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RowColumnFormationPreset",
    menuName = "Simple RTS/Formations/Row Column Formation Preset", order = 1)]
public class RowColumnFormationPreset : BaseFormationPreset
{
    private enum RowColumnType { Line, Square, Rectangle};
    [SerializeField]
    private RowColumnType rowColumnType = RowColumnType.Square;
    [SerializeField]
    private float interUnitDistance;

    public override List<Vector3> GetFormationOffsets(int count)
    {
        List<Vector3> outList = new List<Vector3>();
        int squareSize = Mathf.CeilToInt(Mathf.Sqrt(count));
        int rows = 0;
        int columns = 0;
        switch (rowColumnType)
        {
            case RowColumnType.Line:
                rows = 1;
                columns = count;

                break;

            case RowColumnType.Square:
                rows = columns = squareSize;

                break;

            case RowColumnType.Rectangle:
                rows = squareSize - 1;
                columns = squareSize + 1;
                if (rows * columns < count)
                    columns += 1;
                break;
        }

        for (int a = 0; a < rows; a++)
        {
            for (int b = 0; b < columns; b++)
            {
                int index = b + a * columns;

                if (index < count)
                {
                    Vector3 nextPos = new Vector3(a, 0f, b) * interUnitDistance;

                    outList.Add(nextPos);
                }
            }
        }

        return outList;
    }
}