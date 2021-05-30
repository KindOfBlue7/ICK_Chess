using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureController : MonoBehaviour
{
    public string Type;
    public string CurrentPosition;
    public GameController Controller;
    public GameObject Board;
    public GameObject Pointer;
    public Rigidbody m_Rigidbody;

    private List<string> PossibleMoves;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool Clicked;
    public bool isMoving;
    public bool Active;
    private float ZPosition;
    private Camera MainCamera;
    private string startTile;
    private string lastTile;
    private GameObject Cursor;

    // Start is called before the first frame update
    void Start()
    {
        startTile = "e4";
        MainCamera = Controller.MainCamera;
        isMoving = false;
        Clicked = false;
        Active = false;
        PossibleMoves = new List<string>();
        ZPosition = MainCamera.WorldToScreenPoint(transform.position).z;
        ChangePosition(CurrentPosition);
        //controllerScript cur = Cursor.GetComponent<PointerController>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isMoving)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZPosition);
            transform.position = MainCamera.ScreenToWorldPoint(position + new Vector3(offset.x, 0));
            Vector3 check_position = MainCamera.ScreenToWorldPoint(position + new Vector3(offset.x, 0));
            check_position.y = 0;

            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    MeshCollider TileCollider = Controller.Tiles[i, j].transform.GetChild(0).gameObject.GetComponent<MeshCollider>();
                    if (TileCollider.bounds.Contains(check_position)){
                        lastTile = Controller.Tiles[i, j].name;
                        break;
                    }
                }
            }
        }
        */

        if (Active)
        {
            if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
            {
                isMoving = true;
                print("aa");
            }
            
        }

        if (isMoving)
        {
            if (Pointer.GetComponent<PointerController>().CurrentPosition != CurrentPosition)
            {
                if (Input.GetKeyDown("return") || Input.GetMouseButtonDown(0))
                {
                    ChangePosition(Pointer.GetComponent<PointerController>().CurrentPosition);
                    isMoving = false;
                    Clicked = false;
                    print("bb");
                }
            }
            
        }
    }

    void OnTriggerEnter()
    {
        Active = true;
    }

    void OnTriggerExit()
    {
        Active = false;
    }

    void FindPossibleMoves()
    {

    }
    /*
    void OnMouseDown()
    {
        startTile = CurrentPosition;
        BeginDragging();
    }

    void OnMouseUp()
    {
        EndDragging();
        isMoving = false;
    }

    void BeginDragging()
    {
        isMoving = true;
        offset = MainCamera.WorldToScreenPoint(transform.position) - Input.mousePosition;
    }

    void EndDragging()
    {
        isMoving = false;
        Vector3 position = MainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ZPosition) + new Vector3(offset.x, 0));
        position.y = 0;
        MeshCollider BoardCollider = Board.transform.GetChild(0).gameObject.GetComponent<MeshCollider>();

        if (BoardCollider.bounds.Contains(position)){
            ChangePosition(lastTile);
        } else
        {
            ChangePosition(startTile);
        }
    }
    */
    void ChangePosition(string TileName)
	{
        List<int> indices = Controller.tile_to_index(TileName);
        if (indices[0] > 8 || indices[0] < 0 || indices[1] > 8 || indices[1] < 0)
        {
            Debug.Log("Wrong tile name");
        }
        GameObject tmp = Board.transform.Find("Plane").gameObject;
        CurrentPosition = TileName;
        GameObject Tile = Controller.Tiles[indices[0], indices[1]];
        transform.Translate(Tile.transform.position-transform.position);
        FindPossibleMoves();
	}
}
