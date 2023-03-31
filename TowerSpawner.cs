using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public List<GameObject> towers;
    public List<GameObject> ghosts;
    public GameObject towerIndicator;
    public GameObject towerIndicator2;
    public Grid grid;
    public Camera camera;
    private RaycastHit2D _hit;

    private bool _isPlantingActive;
    private Vector3 _mouseXD;
    private int _spawnID = -1;

    private Vector3 _towerSpawnPosition;

    private void Update()
    {
        if (_isPlantingActive) SetHover();

        if (Input.GetMouseButtonDown(0))
            if (CanSpawn())
            {
                SetSpawnPosition();
                BuildTower();
            }
    }

    private bool TowerExists()
    {
        Vector2 ray = GetMousePosition();
        _hit = Physics2D.Raycast(ray, Vector2.zero);

        return _hit.collider && _hit.collider.name.StartsWith("Tower") && !_hit.collider.name.Contains("Indicator");
    }

    private bool PlaceToBuild()
    {
        Vector2 ray = GetMousePosition();
        _hit = Physics2D.Raycast(ray, Vector2.zero);

        return _hit.collider && _hit.collider.CompareTag("Tile");
    }

    private bool CanSpawn()
    {
        var x1 = _spawnID != -1;
        var x2 = !TowerExists();
        var x3 = PlaceToBuild();

        return x1 && x2 && x3;
    }

    private bool EmptyTile()
    {
        Vector2 ray = GetMousePosition();
        _hit = Physics2D.Raycast(ray, Vector2.zero);

        return _hit.collider && _hit.collider.CompareTag("Tile");
    }

    public void SelectTower(int id)
    {
        _spawnID = id;
        _isPlantingActive = true;
        CreateIndicator();
    }

    private void CreateIndicator()
    {
        towerIndicator = Instantiate(ghosts[_spawnID], GetMousePosition(), Quaternion.identity);
        towerIndicator2 = Instantiate(ghosts[_spawnID], GetMousePosition(), Quaternion.identity);
    }

    public void DeselectTower()
    {
        _isPlantingActive = false;
        _spawnID = -1;
    }

    private void BuildTower()
    {
        Destroy(towerIndicator);
        Destroy(towerIndicator2);
        Instantiate(towers[_spawnID], _towerSpawnPosition, Quaternion.identity);
        DeselectTower();
    }

    private void SetHover()
    {
        if (_isPlantingActive)
        {
            var mousePosition = GetMousePosition();
            var cellPositionDefault = grid.WorldToCell(mousePosition);
            var cellPositionCentered = grid.GetCellCenterWorld(cellPositionDefault);
            var currentPosition = towerIndicator.transform.position;
            mousePosition.z = 0;
            towerIndicator.transform.position = mousePosition;
            if (CanSpawn() && currentPosition != cellPositionCentered)
            {
                towerIndicator2.transform.position = cellPositionCentered;
            }
        }
    }

    private void SetSpawnPosition()
    {
        var mousePosition = GetMousePosition();
        var cellPositionDefault = grid.WorldToCell(mousePosition);
        _towerSpawnPosition = grid.GetCellCenterWorld(cellPositionDefault);
    }

    private Vector3 GetMousePosition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }
}