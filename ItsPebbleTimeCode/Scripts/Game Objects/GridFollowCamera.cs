using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridFramework.Renderers.Rectangular;

public class GridFollowCamera: MonoBehaviour
{
    public Parallelepiped _renderers;
    private Vector3 _fromPosition;
    private Vector3 _toPosition;
    private Vector3 _lastPosition;
    private Camera _cam;
    private const float _buffer = 5f;
    public BoxCollider2D boxCollider;

    public SpawnCollectables spawnCollectables;


    void Awake(){
    }

    public void Start()
    {
        _cam = GetComponent<Camera>();
        _lastPosition = transform.position;

        var camX = _cam.aspect * _cam.orthographicSize + _buffer;
        var camY = _cam.orthographicSize + _buffer;
        var posX = transform.position.x;
        var posY = transform.position.y;

        _renderers.From = new Vector3(posX - camX, posY - camY, 0);
        _renderers.To = new Vector3(posX + camX, posY + camY, 0);

        // Create the initial grid of collectables
        spawnCollectables.CreateGridStart(Mathf.RoundToInt(Mathf.Abs(_renderers.From.x - _renderers.To.x)), Mathf.RoundToInt(Mathf.Abs(_renderers.From.y - _renderers.To.y)), Mathf.RoundToInt(_renderers.From.x), Mathf.RoundToInt(_renderers.From.y));
        boxCollider.size = new Vector2(camX * 2, camY * 2);
        boxCollider.transform.position = new Vector2(_cam.transform.position.x, _cam.transform.position.y);
    }

    public void Update()
    {
        var beyondX = Mathf.Abs(transform.position.x - _lastPosition.x) >= _buffer;
        var beyondY = Mathf.Abs(transform.position.y - _lastPosition.y) >= _buffer;

        if (beyondX || beyondY){
            ResizeGrid();
        }
    }

    private void ResizeGrid(){

        // Move collider before creating the new collectables - so you don't destroy them on collide. Will the collider move or just appear at the new spot???
        boxCollider.transform.position = _cam.transform.position;
        //boxCollider.transform.position = Vector2.Lerp(boxCollider.transform.position, _cam.transform.position, 1f);

        var shift = Vector3.zero;

        for (var i = 0; i < 2; i++)
        {
            shift[i] = transform.position[i] - _lastPosition[i];
        }

        _fromPosition = _renderers.From;
        _toPosition = _renderers.To;

        _renderers.From += shift;
        _renderers.To += shift;

        // Create collectales in the new area of the grid
        if (_renderers.From.y < _fromPosition.y)
        {
            spawnCollectables.CreateGrid(Mathf.RoundToInt(Mathf.Abs(_renderers.From.x - _renderers.To.x)), Mathf.RoundToInt(Mathf.Abs(_renderers.From.y - _fromPosition.y)), Mathf.RoundToInt(_renderers.From.x), Mathf.RoundToInt(_renderers.From.y));
            // Need to delete the collectables on the other side when this happens        
        }

        if (_renderers.From.y > _fromPosition.y)
        {
            spawnCollectables.CreateGrid(Mathf.RoundToInt(Mathf.Abs(_renderers.To.x - _renderers.From.x)), Mathf.RoundToInt(Mathf.Abs(_renderers.To.y - _toPosition.y)), Mathf.RoundToInt(_renderers.From.x), Mathf.RoundToInt(_toPosition.y));
            // Need to delete the collectables on the other side when this happens  
        }

        if (_renderers.From.x < _fromPosition.x)
        {
            spawnCollectables.CreateGrid(Mathf.RoundToInt(Mathf.Abs(_renderers.From.x - _fromPosition.x)), Mathf.RoundToInt(Mathf.Abs(_renderers.From.y - _renderers.To.y)), Mathf.RoundToInt(_renderers.From.x), Mathf.RoundToInt(_renderers.From.y));
            // Need to delete the collectables on the other side when this happens  
        }

        if (_renderers.From.x > _fromPosition.x)
        {
            spawnCollectables.CreateGrid(Mathf.RoundToInt(Mathf.Abs(_renderers.To.x - _toPosition.x)), Mathf.RoundToInt(Mathf.Abs(_renderers.To.y - _renderers.From.y)), Mathf.RoundToInt(_toPosition.x), Mathf.RoundToInt(_renderers.From.y));
            // Need to delete the collectables on the other side when this happens  
        }

        _lastPosition = transform.position;
    }
}
