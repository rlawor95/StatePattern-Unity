using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AObject : MonoBehaviour
{
    private State<AObject> myState; // 현재 상태를 참조할 변수.

    private void Start()
    {
        myState = new AState();
    }

    private void Update()
    {
        State<AObject> nowState = myState.InputHandle(this);
        myState.action(this);

        if (!nowState.Equals(myState)) //상태가 바뀐 경우
        {
            myState.Exit(this);
            myState = nowState;
        }
    }


    public class AState : State<AObject>
    {
        float time = 0;

        public override State<AObject> InputHandle(AObject t)
        {
            if (time > 2.0f)
                return new BState();
            return this;
        }
        public override void Enter(AObject t)
        {
            time = 0;
            base.Enter(t);
        }

        public override void Update(AObject t)
        {
            Debug.Log("A State Update ");
            time += Time.deltaTime;
            base.Update(t);
        }

        public override void Exit(AObject t)
        {
            Debug.Log("A Exit");
            base.Exit(t);
        }
    }

    public class BState : State<AObject>
    {
        float time = 0;
        public override State<AObject> InputHandle(AObject t)
        {
            if (time > 2.0f)
                return new CState();
            return this;
        }

        public override void Enter(AObject t)
        {
            Debug.Log("B State Enter");
            base.Enter(t);
        }

        public override void Update(AObject t)
        {
            Debug.Log("B State Update");
            time += Time.deltaTime;
            base.Update(t);
        }

        public override void Exit(AObject t)
        {
            Debug.Log("B Exit");
            base.Exit(t);
        }
    }

    public class CState : State<AObject>
    {
        float time = 0;
        public override State<AObject> InputHandle(AObject t)
        {
            if (time > 2.0f)
                return new AState();
            return this;
        }

        public override void Enter(AObject t)
        {
            time = 0;
            Debug.Log("C State Enter");
            base.Enter(t);
        }

        public override void Update(AObject t)
        {
            Debug.Log("C State Update");
            time += Time.deltaTime;
            base.Update(t);
        }

        public override void Exit(AObject t)
        {
            Debug.Log("C Exit");
            base.Exit(t);
        }
    }
}
