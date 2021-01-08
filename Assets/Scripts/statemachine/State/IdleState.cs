using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="IdleState", menuName="Unity-FSM/states/Idle", order =1)]
public class IdleState : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 3f;
    float _totalDuration;

    public override void OnEnable() {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }
    public override bool EnterState()
    {
         EnteredState = base.EnterState();
         if (EnteredState){
            Debug.Log("Entered idle state");
            _totalDuration = 0f;
         }
         
        
         
         return EnteredState;
    }
    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Exiting idle state");
        return true;
    }
    
    public override void UpdateState()
    {
        if(EnteredState)
        {
             
             _totalDuration += Time.deltaTime;
             Debug.Log("Updating idle state" + _totalDuration);

             if(_totalDuration >= _idleDuration)
             {
                 _fsm.EnterState(FSMStateType.PATROL);
             }
        }
       
    }
}
