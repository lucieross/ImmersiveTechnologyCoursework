using UnityEngine;



public class WireTask3D : MonoBehaviour

{

    private GameObject currentStartNode;

    private int successfulConnections = 0;

    public int totalWires = 3;

    public float wireThickness = 0.02f; // Thin for VR 



    public void OnNodeClick(GameObject clickedNode)

    {

        if (currentStartNode == null)

        {

            currentStartNode = clickedNode;

            // Visual feedback: Make it glow or change color 

            currentStartNode.GetComponent<Renderer>().material.color *= 1.5f;

        }

        else

        {

            // Compare colors of the Materials 

            Color startColor = currentStartNode.GetComponent<Renderer>().material.color;

            Color endColor = clickedNode.GetComponent<Renderer>().material.color;



            if (currentStartNode != clickedNode && ColorsMatch(startColor, endColor))

            {

                Create3DWire(currentStartNode.transform.position, clickedNode.transform.position, startColor);



                // Disable to prevent clicking again 

                currentStartNode.tag = "Untagged";

                clickedNode.tag = "Untagged";



                successfulConnections++;

                if (successfulConnections >= totalWires) Debug.Log("Puzzle Solved!");

            }

            currentStartNode = null;

        }

    }



    bool ColorsMatch(Color a, Color b)

    {

        // Simple check to see if colors are roughly the same 

        return Vector4.Distance(a, b) < 0.1f;

    }



    void Create3DWire(Vector3 start, Vector3 end, Color color)

    {

        GameObject lineObj = new GameObject("Wire");

        LineRenderer lr = lineObj.AddComponent<LineRenderer>();



        // Setup the look of the wire 

        lr.material = new Material(Shader.Find("Sprites/Default"));

        lr.startColor = color;

        lr.endColor = color;

        lr.startWidth = wireThickness;

        lr.endWidth = wireThickness;



        // Set positions 

        lr.positionCount = 2;

        lr.SetPosition(0, start);

        lr.SetPosition(1, end);

    }

}