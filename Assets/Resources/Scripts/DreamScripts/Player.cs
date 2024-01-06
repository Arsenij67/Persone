using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IObserver<Vector2>
{
	[SerializeField] private Transform target;
	public float moveSpeed = 3.0f;
	internal StateMachine _stMach;

	public  Image _infoUserPanel;

	internal ControllerColliderHit _contact;
	internal CharacterController _charController;

	internal Animator _animator;
	[SerializeField] private Button _buttWakeUp;
 

	public DynamicJoystick _dynamikJoyst;

	internal InteractGUI _interactableObjectDoor;
	internal InteractGUI _interactableObjectBed;
	internal InteractGUI _interactableObjectBook;

	internal RaycastHit hit;



	public void Awake()
	{
		 
		_stMach = new StateMachine();
		_stMach.Initialize(new IdleState(this));
		_animator = GetComponent<Animator>();
		_charController = GetComponent<CharacterController>();
		_buttWakeUp?.onClick.AddListener(_stMach.CurrentState.WakeUp);

	}


	public void Update()
    {
		_stMach.CurrentState.Update();
		 
		Ray ray = new Ray(transform.position, transform.forward);

		bool isLight = Physics.SphereCast(ray, 0.2f, out hit, 1f);

		InteractWithDoor(isLight, hit);
		InteractWithBed(isLight, hit);
		InteractWithBook(isLight, hit);

	}

	public void InteractWithDoor(bool isLight, RaycastHit hit) // метод для взаимодействия с дверью 
	{

      
	 
        if (isLight)
		{


			if (hit.collider.CompareTag("Door") && hit.collider.GetComponent<Door>().OpenFirstPeace)
			{
		

				_interactableObjectDoor = hit.collider.gameObject.GetComponent<Door>();

				_interactableObjectDoor.CreateGui(hit.collider.gameObject);



			}
			else if (!hit.collider.CompareTag("Door") && _interactableObjectDoor != null && _interactableObjectDoor.OpenFirstPeace == false)
			{
				_interactableObjectDoor.OpenFirstPeace = true;
				_interactableObjectDoor.DestroyGui();

			}

			Debug.DrawLine(transform.position, hit.point, Color.green);

		}

	}
	public void InteractWithBed(bool isLight, RaycastHit hit)
	{

		if (isLight)
		{


			if (hit.collider.CompareTag("Bed") && hit.collider.GetComponent<Bed>().OpenFirstPeace)
			{
				_interactableObjectBed = hit.transform.GetComponent<Bed>();
				_interactableObjectBed.CreateGui(hit.collider.gameObject);


			}

			else if (!hit.collider.CompareTag("Bed") && _interactableObjectBed != null && _interactableObjectBed.OpenFirstPeace == false)
			{
				_interactableObjectBed.OpenFirstPeace = true;
				_interactableObjectBed.DestroyGui();

			}
		}


	} // метод взаимодействия с короватью
	public void InteractWithBook(bool isLight, RaycastHit hit) // метод взаимодействия с книгой
	{

		if (isLight)
		{


			if (hit.collider.CompareTag("Book") && hit.collider.GetComponentInChildren<Book>().OpenFirstPeace)
			{
				_interactableObjectBook = hit.collider.GetComponentInChildren<Book>();
				_interactableObjectBook.CreateGui(hit.collider.gameObject);


			}

			else if (!hit.collider.CompareTag("Book") && _interactableObjectBook != null && _interactableObjectBook.OpenFirstPeace == false)
			{
				_interactableObjectBook.OpenFirstPeace = true;
				_interactableObjectBook.DestroyGui();

			}
		}


	}

	public void OnOpenDoor()// выход из закрывания- открывания двери
	{
		_stMach.CurrentState.Exit();
        _dynamikJoyst.enabled = true;
    }
	public virtual void OnOpenBook() // открытие меню после анимации поднятия предмета
	{
		_stMach.ChangeState(new ReadState(this));
	}

	public void JumpPlayer()
	{
		if (_stMach.CurrentState.GetType() == typeof(JumpState)) return;
		_stMach.ChangeState(new JumpState(this));// метод для вызова прыжка
	}

	

    public void UpdateInfo(Vector2 info)
    {
		_stMach.ChangeState(new NotifyUserState(this));
    }
	 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
		if (hit.transform.tag.Equals("DieFloor")) 
		{
			 Destroy(hit.gameObject);
			_stMach.ChangeState(new DieState(this));
		}
    }
    private void RestartGame() => SceneManager.LoadScene("Room");


}