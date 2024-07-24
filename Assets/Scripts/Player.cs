using DefaultNamespace;
using DefaultNamespace.Events;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : Entity
{ 
    private int actionPoints = 4;
    private int maxActionPoints;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private HealthUI healthUI;
    [Header("Events")]
    [SerializeField] private OnPetalSelectionListener onPetalSelectionListener;
    [SerializeField] private OnPetalUnSelectionListener onPetalUnSelectionListener;
    private void Start()
    {
        maxActionPoints = actionPoints;
        onPetalSelectionListener.Response.AddListener(UseActionPoint);
        onPetalUnSelectionListener.Response.AddListener(GainActionPoint);
        _attributes = new EntityAttribute(entitySo.Attribute.Name, entitySo.Attribute.Health);
    }
    
    
    private void UseActionPoint(int cost)
    {
        if (actionPoints < cost)
        {
            Debug.Log("Not enough action points");
            throw new System.Exception("Not enough action points");
        }
        actionPoints -= cost;
        Debug.Log("Action points: " + actionPoints);
        playerUI.UpdateActionPoints(actionPoints);
    }
    
    private void GainActionPoint(int amount)
    {
        actionPoints += amount;
        if (actionPoints > maxActionPoints)
        {
            actionPoints = maxActionPoints;
        }
        playerUI.UpdateActionPoints(actionPoints);
    }
    
    public override void addArmor(int amount)
    {
        _attributes.armor += amount;
        playerUI.UpdateArmor(_attributes.armor);
    }
    
    public override void loseHP(int amount)
    {
        if (_attributes.armor > 0)
        {
            _attributes.armor -= amount;
            playerUI.UpdateArmor(_attributes.armor);
            return;
        }
        _attributes.Health -= amount;
        healthUI.UpdateHealth(_attributes.Health, entitySo.Attribute.Health);
    }
    
    private void OnDisable()
    {
        onPetalSelectionListener.Response.RemoveListener(UseActionPoint);
        onPetalUnSelectionListener.Response.RemoveListener(GainActionPoint);
    }
}