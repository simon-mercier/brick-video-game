using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class MakeMap : MonoBehaviour
{
    public Sprite map;

    private DetectGameOver detectGameOver;

    [Header("TILES\n")]
    [SerializeField] private static GameObject defTile;
    [SerializeField] private static GameObject endTile;
    [SerializeField] private static GameObject startTile;
    [SerializeField] private static GameObject cpTile;
    [SerializeField] private static GameObject newTile;
    [SerializeField] private static GameObject cpTempTile;
    [SerializeField] private static GameObject newTempTile;
    [SerializeField] private static GameObject qsTile;
    [SerializeField] private static GameObject colliderBrick;
    [SerializeField] private static GameObject flashTile;
    [SerializeField] private static GameObject[] arrow;

    [Header("BRICK\n")]
    [SerializeField] public GameObject brick;

    private GameObject _brick;

    private Texture2D myTexture;

    private Color thisPixel;

    //Normal Tiles
    public List<GameObject> Tiles { get; } = new List<GameObject>();

    //StartPos
    public List<GameObject> StartTilePos { get; } = new List<GameObject>();

    //EndPos
    public GameObject EndTilePos { get; set; }

    //NewTiles
    public List<GameObject> HiddenTiles { get; } = new List<GameObject>();

    public List<GameObject> HiddenTileActivators { get; } = new List<GameObject>();

    public List<GameObject> HiddenTilesColliders { get; } = new List<GameObject>();

    //NewTempTiles
    public List<GameObject> HiddenTempTiles { get; } = new List<GameObject>();

    public List<GameObject> HiddenTempTileActivators { get; } = new List<GameObject>();

    public List<GameObject> HiddenTempTilesColliders { get; } = new List<GameObject>();


    //QuickSand
    public List<GameObject> QuickSandColliders { get; } = new List<GameObject>();
    public List<GameObject> QuickSandTiles { get; } = new List<GameObject>();

    public List<Vector3> QuickSandTilesReference { get; } = new List<Vector3>();

    public List<GameObject> QuickSandTempColliders { get; } = new List<GameObject>();

    //Transparent Tiles
    public List<GameObject> TransparentTiles { get; } = new List<GameObject>();

    //FlashTiles
    public List<GameObject> FlashTiles { get; } = new List<GameObject>();

    public List<GameObject> FlashTilesColliders { get; } = new List<GameObject>();

    public static MakeMap Instance { get; set; }

    //Arrows
    public static List<GameObject> arrows = new List<GameObject>();

    private GameObject levelFailScreen;

    private int trueMapWidth = 1;
    private int trueMapHeight = 1;
    private static bool makingLevel = false;
    private static int arrowCounter = 0;
    private bool canPlaceTransparentTile;

    void Awake()
    {
        levelFailScreen = GameObject.Find("UI").transform.GetChild(4).gameObject;
        levelFailScreen.SetActive(false);
        detectGameOver = GetComponent<DetectGameOver>();

        Instance = this;
    }
    public void CreateMap(Sprite[] sprites, int level)
    {
        for(int i = 1; i <= sprites.Length; i++)
        {
            for (int row = 0; row < map.rect.width; row++)
            {
                for (int col = 0; col < map.rect.height; col++)
                {
                    myTexture = map.texture;
                    thisPixel = myTexture.GetPixel(row, col);
                    if (thisPixel.a != 0 && row + 1 > trueMapWidth)
                    {
                        trueMapWidth = row + 1;
                    }

                    if (thisPixel.a != 0 && col + 1 > trueMapHeight)
                    {
                        trueMapHeight = col + 1;

                    }
                    if (ArrowDetector())
                    {
                        switch (level)
                        {
                            case 1:
                                arrowCounter = 0;
                                break;
                            case 30:
                                arrowCounter = 1;
                                break;
                            case 44:
                                arrowCounter = 2;
                                break;
                            case 56:
                                arrowCounter = 3;
                                break;
                            case 70:
                                arrowCounter = 4;
                                break;
                        }
                        arrows.Add(Instantiate(arrow[arrowCounter], new Vector3(row, 1, col), Quaternion.identity));

                        arrows[arrows.Count - 1].transform.rotation = arrow[arrowCounter].transform.rotation;
                        arrowCounter++;
                    }
                    //tiles
                    if (MatchColor(0, 1, 0, 1))
                    {
                        Tiles.Add(Instantiate(defTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        
                    }
                    //EndTile
                    else if (MatchColor(0, 0, 1, .2f))
                    {
                        EndTilePos = Instantiate(endTile, new Vector3(row, -1.5f, col), Quaternion.identity);
                        
                    }
                    //StartTile
                    else if (MatchColor(1, 0, 0, .2f))
                    {
                        StartTilePos.Add(Instantiate(startTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        
                    }
                    //hiddenTileActivators
                    else if (MatchColor(1, 1, 0, .2f))
                    {
                        HiddenTileActivators.Add(Instantiate(cpTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        
                    }
                    //hiddenTiles
                    else if (MatchColor(0, 1, 1, .2f))
                    {
                        HiddenTiles.Add(Instantiate(newTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        SetTilesActiveState(false, HiddenTiles);

                        GameObject tile;
                        HiddenTilesColliders.Add(tile = Instantiate(colliderBrick, new Vector3(row, -1.5f, col), Quaternion.identity));
                        tile.name = "hiddenTileCollider";
                        tile.SetActive(true);
                    }
                    //hiddenTempTile
                    else if (MatchColor(1, 1, 1, .2f))
                    {
                        HiddenTempTiles.Add(Instantiate(newTempTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        SetTilesActiveState(false, HiddenTempTiles);

                        GameObject tile;
                        HiddenTempTilesColliders.Add(tile = Instantiate(colliderBrick, new Vector3(row, -1.5f, col), Quaternion.identity));
                        tile.name = "hiddenTempTileCollider";
                        tile.SetActive(true);
                       
                    }
                    //hiddenTempTileActivators
                    else if (MatchColor(1, 0, 1, .2f))
                    {
                        HiddenTempTileActivators.Add(Instantiate(cpTempTile, new Vector3(row, -1.5f, col), Quaternion.identity));
              
                    }
                    //qsTilesReference
                    else if (MatchColor(0, 0, 0, .2f))
                    {
                        QuickSandTilesReference.Add(new Vector3(row, -1.5f, col));
                        
                    }

                    //flashTiles
                    else if (MatchColor(1, 0, 0.5f, .2f, 0.1f))
                    {
                        FlashTiles.Add(Instantiate(flashTile, new Vector3(row, -1.5f, col), Quaternion.identity));
                        SetTilesActiveState(false, FlashTiles);

                        GameObject tile;
                        FlashTilesColliders.Add(tile = Instantiate(colliderBrick, new Vector3(row, -1.5f, col), Quaternion.identity));
                        tile.SetActive(true);
                        canPlaceTransparentTile = true;
                    }
                    //Transparent
                    else
                    {
                        if (TouchesAnotherTile(row, col, myTexture))
                        {
                            TransparentTiles.Add(Instantiate(colliderBrick, new Vector3(row, -1.5f, col), Quaternion.identity));
                        }
                    }
                }
            }
        }

        
        //startTilePos.Add(Instantiate(startTile, new Vector3(0, -1.5f, 0), Quaternion.identity));
        SetCameraPos();
        InvokeRepeating("DoFlashTileEffect", 2, 3);
        CreateQuickSand();
        CreateBrick();
        SetParentToLevel();
    }

    private bool TouchesAnotherTile(int x, int y, Texture2D myTexture)
    {
        return (myTexture.GetPixel(x, y + 1).a > 0.1f || myTexture.GetPixel(x, y - 1).a > 0.1f || myTexture.GetPixel(x + 1, y).a > 0.1f || myTexture.GetPixel(x - 1, y).a > 0.1f);
    }

    private void SetCameraPos()
    {
        Camera.main.transform.position = new Vector3((trueMapWidth / 2f) -0.5f, 18 + trueMapHeight, -15 - trueMapHeight);
    }

    private void CreateBrick()
    {
        Camera.main.transform.position = new Vector3((trueMapWidth / 2f) - 0.5f, 18 + trueMapHeight, -15 - trueMapHeight);

        _brick = Instantiate(brick, new Vector3(0, 3, 0), Quaternion.identity) as GameObject;
        _brick.SetActive(true);
    }

    private void CreateQuickSand()
    {
        for (var i = 0; i < QuickSandTilesReference.Count; i++)
        {
            GameObject Collider;
            QuickSandColliders.Add(Collider = Instantiate(colliderBrick, QuickSandTilesReference[i], Quaternion.identity));
            Collider.name = "qsCollider";
            Collider.GetComponent<QuickSandDetector>().IsQs = true;

            QuickSandTiles.Add(Instantiate(qsTile, QuickSandTilesReference[i], Quaternion.identity));

            GameObject TempCollider;
            QuickSandTempColliders.Add(TempCollider = Instantiate(colliderBrick, QuickSandTilesReference[i], Quaternion.identity));
            TempCollider.name = "qsColliderTemp";
            TempCollider.SetActive(false);
        }
    }

    private void DestroyQuickSand()
    {
        QuickSandTiles.ForEach(Destroy);
        QuickSandTiles.Clear();
        QuickSandColliders.ForEach(Destroy);
        QuickSandColliders.Clear();
        QuickSandTempColliders.ForEach(Destroy);
        QuickSandTempColliders.Clear();
    }

    public void SetParentToLevel()
    {
        _brick.transform.SetParent(gameObject.transform, false);

        Tiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        StartTilePos.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTileActivators.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTilesColliders.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTempTiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTempTileActivators.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        HiddenTempTilesColliders.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        QuickSandColliders.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        QuickSandTiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        QuickSandTempColliders.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        TransparentTiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        FlashTiles.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        FlashTilesColliders.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        arrows.ForEach(a => a.transform.SetParent(gameObject.transform, false));

        EndTilePos.transform.SetParent(gameObject.transform, false);
    }

    public static void SetTilesActiveState(bool state, List<GameObject> tileType, int i)
    {
        tileType[i].SetActive(state);
    }

    IEnumerator SetTilesActiveState(float timeSec, bool state, List<GameObject> tileType, int i)
    {
        yield return new WaitForSeconds(timeSec);
        tileType[i].SetActive(state);
    }

    public static void SetTilesActiveState(bool state, List<GameObject> tileType)
    {
        tileType.ForEach((GameObject Tile) => { Tile.SetActive(state); });
    }

    public static void SetTilesActiveState(List<GameObject> tileType)
    {
        tileType.ForEach((GameObject Tile) => { Tile.SetActive(!Tile.activeSelf); });
    }

    public static void SetTilesKinematicState(bool state, List<GameObject> tileType, int i)
    {
        tileType[i].GetComponent<Rigidbody>().isKinematic = state;
    }

    public static void SetTilesKinematicState(bool state, List<GameObject> tileType)
    {
        tileType.ForEach((GameObject Tile) => { Tile.GetComponent<Rigidbody>().isKinematic = state; });
    }

    public static void SetTimerForTile(float timeSec)
    {
        Instance.StartCoroutine(Instance.ResetTempTiles(timeSec));
    }

    IEnumerator ResetTempTiles(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SetTilesActiveState(false, HiddenTempTiles);
    }

    private void DoFlashTileEffect()
    {
        SetTilesActiveState(FlashTiles);
    }

    private bool ArrowDetector()
    {
        return thisPixel.a >= 0.4f && thisPixel.a <= 0.6f;
    }

    private bool MatchColor(float r, float g, float b, float a)
    {
        return thisPixel.r == r && thisPixel.g == g && thisPixel.b == b && thisPixel.a >= a;
    }
    private bool MatchColor(float r, float g, float b, float a, float increment)
    {
        return thisPixel.r <= r + increment && thisPixel.r >= r - increment && thisPixel.g <= g + increment && thisPixel.g >= g - increment && thisPixel.b <= b + increment && thisPixel.b >= b - increment && thisPixel.a >= a;
    }

    public void DestroyLevel()
    {
        BrickManager.IsUp = true;
        BrickManager.LastDir = Dir.NONE;

        detectGameOver.CanPressCheckPoint = true;

        BrickManager.CanPlaySound = true;
        try
        {
            Instance.levelFailScreen.SetActive(false);
            Destroy(Instance.gameObject);
            Destroy(Instance._brick);
        }
        catch (Exception e) { Debug.Log(e); }
        Destroy(EndTilePos);

        StartTilePos.ForEach(Destroy);
        StartTilePos.Clear();

        Tiles.ForEach(Destroy);
        Tiles.Clear();

        HiddenTiles.ForEach(Destroy);
        HiddenTiles.Clear();

        HiddenTileActivators.ForEach(Destroy);
        HiddenTileActivators.Clear();

        HiddenTilesColliders.ForEach(Destroy);
        HiddenTilesColliders.Clear();

        HiddenTempTiles.ForEach(Destroy);
        HiddenTempTiles.Clear();

        HiddenTempTileActivators.ForEach(Destroy);
        HiddenTempTileActivators.Clear();

        HiddenTempTilesColliders.ForEach(Destroy);
        HiddenTempTilesColliders.Clear();

        QuickSandTiles.ForEach(Destroy);
        QuickSandTiles.Clear();

        QuickSandColliders.ForEach(Destroy);
        QuickSandColliders.Clear();

        QuickSandTempColliders.ForEach(Destroy);
        QuickSandTempColliders.Clear();

        QuickSandTilesReference.Clear();

        TransparentTiles.ForEach(Destroy);
        TransparentTiles.Clear();

        FlashTilesColliders.ForEach(Destroy);
        FlashTilesColliders.Clear();

        FlashTiles.ForEach(Destroy);
        FlashTiles.Clear();

        arrows.ForEach(Destroy);
        arrows.Clear();
        try
        {
            Instance.CancelInvoke("QSTilesFall");
        }
        catch { }

        BrickManager.CanMove = true;

    }

    public static void RestartLevel()
    {
        if (BrickManager.CanPlaySound && AudioManager.SoundOn)
        {
            AudioManager.Play(Sounds.Die);
            BrickManager.CanPlaySound = false;
        }
        Instance.levelFailScreen.SetActive(true);
        Instance.levelFailScreen.transform.GetChild(2).GetComponent<RectTransform>().localPosition = new Vector2(0, Instance.levelFailScreen.transform.GetChild(2).GetComponent<RectTransform>().localPosition.y);
    }

    public void Restart()
    {
        BrickManager.CanPlaySound = true;
        levelFailScreen.SetActive(false);
 
        detectGameOver.FirstTimeInLoop = true;
        try
        {
            _brick.GetComponent<Rigidbody>().isKinematic = true;

            try
            {
                CancelInvoke("QSTilesFall");
            }
            catch { }

            Instance._brick.transform.rotation = Quaternion.identity;

            BrickManager.IsUp = true;

            BrickManager.LastDir = Dir.NONE;

            Instance._brick.transform.position = new Vector3(0, 3, 0);

            Instance._brick.GetComponent<Rigidbody>().isKinematic = false;

            detectGameOver.CanPressCheckPoint = true;

            SetTilesActiveState(false, QuickSandTempColliders);

            HiddenTiles.ForEach((GameObject obj) => { obj.SetActive(false); });

            HiddenTempTiles.ForEach((GameObject obj) => { obj.SetActive(false); });

            Tiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            HiddenTempTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            HiddenTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            QuickSandTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            HiddenTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            HiddenTileActivators.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            HiddenTempTileActivators.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);

            arrows.ForEach(a => a.SetActive(true));

            Invoke("CanMoveLater", .3f);

            DestroyQuickSand();

            CreateQuickSand();

            SetParentToLevel();

            QuickSandDetector.AlreadyTriggered = false;
        }
        catch { }
    }

    private void CanMoveLater()
    {
        BrickManager.CanMove = true;
    }
}