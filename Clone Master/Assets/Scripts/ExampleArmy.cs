using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FSG.MeshAnimator;
public class ExampleArmy : MonoBehaviour {
    private FormationBase _formation;

    public static ExampleArmy Instance;
    public FormationBase Formation {
        get {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;
    public float _unitSpeed = 2;

    public List<GameObject> _spawnedUnits = new List<GameObject>();
    [HideInInspector]
    public List<Vector3> _points = new List<Vector3>();

    public Transform _parent;
    public bool isThisInstance;


    private void Awake() 
    {
        if (isThisInstance) { Instance = this; }
    }

    private void Update() {
        SetFormation();
        _unitSpeed = Mathf.Clamp(_unitSpeed, 0f, 20);
    }

    private void SetFormation() {
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count) {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count) {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++) {
            _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], _unitSpeed * Time.deltaTime);
        }
    }

    public void Spawn(IEnumerable<Vector3> points) {
        foreach (var pos in points) {
            var unit = Instantiate(_unitPrefab, transform.position+pos, _unitPrefab.transform.rotation, _parent);
            unit.GetComponent<MeshAnimatorBase>().Play("Idle");

            _spawnedUnits.Add(unit);

            
            


        }
    }

    public void Kill(int num) {
        for (var i = 0; i < num; i++) {
            var unit = _spawnedUnits.Last();
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}