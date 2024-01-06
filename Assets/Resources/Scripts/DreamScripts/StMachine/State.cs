using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public abstract class State 
{
    public virtual void Enter()
    { 
    
    
    }

    public virtual void Exit()
    {
    }


    public virtual void Update()
    { 
    
    
    }

    public virtual void WakeUp()
    {
        SceneManager.LoadScene("Room");
    }


}


public class IdleState : State
{
    private Player Player;

    public IdleState(Player PointClick)
    {
        this.Player = PointClick;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Вошел в состояние покоя");
        
       
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Вышел из состояние покоя");
    }

}


public class JumpState : State
{
    private Player Player;
    private float speed = 100f;
    private float Jumpspeed=0;
    bool wasJumped = false;
    float height = 2;
    Vector3 startPos;
    
    public JumpState(Player PointClick)
    {
        this.Player = PointClick;
    }
    public override void Enter()
    {
        base.Enter();
        startPos = Player.transform.position;
        Player._animator.SetTrigger("WasJumped");
        Debug.Log("Вошел в состояние прыжка");
         
    }
    public override void Update()
    {

        base.Update();
        if ((int)Player._charController.velocity.y == (int)(Vector3.zero).y && !wasJumped)
        {
            wasJumped = true;
            speed = 1;
        }
       
        Jumpspeed += (speed) * Time.deltaTime;

        if (Player.transform.position.y > (startPos.y + height))
        {
            Jumpspeed = 0;
            speed = 0;
          
        }
        Player._charController.Move((new Vector3(Player._dynamikJoyst.Horizontal * Player.moveSpeed * Time.deltaTime, Jumpspeed, Player._dynamikJoyst.Vertical * Player.moveSpeed * Time.deltaTime)));
       
        if ((Player.transform.position.y)== (startPos.y)) Player._stMach.ChangeState(new IdleState(Player));
        Debug.Log("Прыгаем прыгаем");
    
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Вышел из состояние прыжка");
    }


}


public class RunState : State
{
   private Player Player;
 

    public RunState(Player PointClick)
    { 
    this.Player = PointClick;
    }
    public override void Enter()
    {
        base.Enter();
     
         
        Player._animator.SetFloat("Speed",10);
         
 
    }

    public override void Exit()
    {
        base.Exit();
      
        Player._animator.SetFloat("Speed", 0);

    }

    public override void Update()
    {
        
        base.Update();

        Debug.Log("РРР");
        Player._charController.Move((new Vector3(Player._dynamikJoyst.Horizontal *Player.moveSpeed * Time.deltaTime,0, Player._dynamikJoyst.Vertical * Player.moveSpeed * Time.deltaTime)));

        //Player.isGrounded = IsGroundOrNot();

        //Player.offsetdown = Player.isGrounded ? 0 : -2 * Time.deltaTime;

        RotatePlayer(Player._dynamikJoyst.Direction);
 

    }
    //private bool IsGroundOrNot()
    //{

    //    RaycastHit hit;

    //    Ray RayFall = new Ray(Player.transform.position, Vector3.down);


    //    if (Physics.Raycast(RayFall, out hit, 0.78f))
    //    {
    //        Debug.DrawLine(Player.transform.position, hit.point, Color.red);

    //        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //        {
    //            return true;

    //        }

    //        else { return false; }

    //    }


    //    return false;
    //}

    private  void RotatePlayer(Vector2 _joystickRotattion)
    {
        Player.gameObject.transform.rotation =Quaternion.Lerp(Player.transform.rotation, Quaternion.LookRotation(new Vector3 (_joystickRotattion.x, 0, _joystickRotattion.y)),2f);
    
    }


}

public class SleepState : State
{
    Player _player;
    private Bed _bed;
    private CancellationTokenSource _CancelSource = new CancellationTokenSource();
    CancellationToken _cancelToken;



    public SleepState(Player player,Bed bed)
    {
        _player = player;
        _bed = bed;
    }
    public override async void Enter()
    {
         _cancelToken = _CancelSource.Token;
        _bed.CreateGui(_bed.transform.gameObject);
        _player.transform.position = new Vector3(_bed.transform.position.x,0, _bed.transform.position.z);
        _player.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
        _player._animator.SetTrigger("Sleep");
        



        await LieOnBed(4000);
        base.Enter();
        Debug.Log("Вошли в сон");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Вышли из сна");
        _bed.DestroyGui();

    }

    private async Task LieOnBed(int sec)
    {
        _player._charController.enabled = false;
        _player._charController.Move(Vector3.one);
        _player._dynamikJoyst.enabled = false;
        while (!_cancelToken.IsCancellationRequested)
        {
            await Task.Delay(sec);
            SceneManager.LoadScene("BufferZone");
            _CancelSource.Cancel();

        }
        _CancelSource.Dispose();

    }



}

public class OpenCloseDoorState : State
{
    private Player _player;

    private Door _door = null;

    public OpenCloseDoorState(Player player)
    {
        _player = player;
        _door = _player._interactableObjectDoor as Door;
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Вошли в отрытие/двери");
        HingeJoint _hingeJoint = _door.GetComponent<HingeJoint>(); // открываем двери
        _hingeJoint.useMotor = !_door.isOpen? true : false;
         _door.isOpen = !_door.isOpen;
        _player._dynamikJoyst.enabled = false;
        
        _player._animator.SetTrigger("ChangeDoor");


    }
    public override  void Exit()
    {
        Debug.Log("Вышли из состояния открыть двери");
 

    }

    public override void Update()
    {
        base.Update();
    }
}

public class ReadState : State
{
    private Player _player;
    private Book _book;
    public ReadState(Player player)
    {
        _player = player;
      
    }
    public override void Enter()
    {

       
        Debug.Log("Вошли в состояние чтения дневника");
        _book = _player._interactableObjectBook as Book;
        _book.Open();
      
        base.Enter();
     
    }

    public override void Exit()
    {
        _book.Close();
        Debug.Log("Вышли из состояния чтения дневника");
        base.Exit(); 
    }

}

public class TakeItemState : State
{

    private Player _player;
 
    public TakeItemState(Player player)
    {
        _player = player;

    }
    public override void Enter()
    {

        _player._animator.SetTrigger("TakeItem");
        Debug.Log("Вошли в состояние взятия предмета");
        _player._dynamikJoyst.enabled = false;

        base.Enter();

    }

    public override void Exit()
    {
        _player._dynamikJoyst.enabled = true ;
        Debug.Log("Вышли из состояния взятия предмета");
        base.Exit();
    }


}
public class NotifyUserState : State
{
    private Player _player;
    public NotifyUserState(Player _player)
    { 
      this._player = _player;
    }
    public  override void Enter()
    {
        Debug.Log("Вошли воповещение пользователя");
        base.Enter();
    }

    public override void Exit()
    {
        Debug.Log("Вышли в из оповещение пользователя");
        base.Exit();
    }

    

    public override void Update()
    {
        Debug.Log("В состоянии оповещения пользователя!");
        base.Update();
    }

}

public class DieState : State
{
    private Rigidbody [] allrigidbodys; 

    private Player _player;
    public DieState(Player player)
    {
        _player = player;
    }
    public override void Enter()
    {
        base.Enter();
        _player._animator.enabled = false;
        allrigidbodys = _player.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody b in allrigidbodys) b.velocity = Vector3.zero;
        Exit();
    }

    public override void Exit()
    {
        base.Exit();
       _player.Invoke("RestartGame", 4f);

    }
 



}