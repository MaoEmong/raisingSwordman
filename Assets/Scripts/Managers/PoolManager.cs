using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ǯ�Ŵ���
public class PoolManager
{
	// ������Ʈ Ǯ
	public class Pool
	{
		// ���� ������Ʈ
		public GameObject OriginPoolObject;
		// ������ �θ�Transform
		public Transform Root;
		// ������Ʈ ������ stack
		Stack<GameObject> _poolStack = new Stack<GameObject>();

		// �ʱ�ȭ(���� ������Ʈ, �̸� ����� �� ����)
		public void Init(GameObject origin, int count = 3)
		{
			OriginPoolObject = origin;
			Root = new GameObject().transform;
			Root.name = $"{origin.name}_Root";

			for (int i = 0; i < count; i++)
				Push(Create());
		}
		// ���� ������Ʈ�� ���纻 ����
		GameObject Create()
		{
			GameObject go = Object.Instantiate(OriginPoolObject);
			go.name = OriginPoolObject.name;
			return go;
		}
		// ������ �־�α�
		public void Push(GameObject go)
		{
			if (go == null)
				return;

			go.transform.parent = Root;
			go.SetActive(false);
			
			_poolStack.Push(go);
		}
		// ������ ��������
		public GameObject Pop(Transform _root)
		{
			GameObject go;

			// �̸� ������ �����Ͱ� ���� �Ҹ����� ��� �߰�����
			if(_poolStack.Count > 0)
				go = _poolStack.Pop();
			else
				go = Create();

			go.SetActive(true);

			if (_root == null)
				go.transform.parent = Managers.Scene.CurScene.transform;

			go.transform.parent = _root;

			return go;
		}
	}

	// Dictionary<������Ʈ�̸�,������Ǯ>
	Dictionary<string, Pool> _pools = new Dictionary<string, Pool>();
	// ������ Ǯ�� �θ�
	Transform _root;
	// Ȱ��ȭ�� �������� �θ�
	Transform _ableRoot;
	

	public void Init()
	{
		if(_root == null)
		{
			_root = new GameObject { name = "Pool_Root" }.transform;
			Object.DontDestroyOnLoad(_root);
		}
		if(_ableRoot == null)
		{
			_ableRoot = new GameObject { name = "Able_Pool_Root" }.transform;
			Object.DontDestroyOnLoad(_ableRoot);
		}
	}

	// ������ Ǯ ����(���� ������Ʈ�� �������� ���� �ÿ���)
	public void CreatePool(GameObject origin, int count = 3)
	{
		if (_pools.ContainsKey(origin.name) == false)
		{
			Pool pool = new Pool();
			pool.Init(origin, count);
			pool.Root.parent = _root;

			_pools.Add(origin.name, pool);
		}
	}
	// ������ ����ֱ�
	public void Push(GameObject go)
	{
		string name = go.name;
		// ����ִ� �������� Ǯ�� �������� �ʴٸ� �ش� �����ʹ� ����
		if(_pools.ContainsKey(name) == false)
		{
			GameObject.Destroy(go);
			return;
		}

		_pools[name].Push(go);
	}
	// ������ ��������
	public GameObject Pop(GameObject go, Transform root = null)
	{
		// �ش��ϴ� ������Ʈ�� �����ϴ� Ǯ�� ���ٸ� ����
		if (_pools.ContainsKey(go.name) == false)
			CreatePool(go);

		Transform Root = root;

		if (Root == null)
			Root = _ableRoot;

		return _pools[go.name].Pop(Root);
	}

	// Ȱ��ȭ, ��Ȱ��ȭ�� ������Ʈ Ǯ ����
	public void Clear()
	{
		foreach(Transform t in _root)
			GameObject.Destroy(t.gameObject);
		foreach(Transform t in _ableRoot)
			GameObject.Destroy(t.gameObject);

		_pools.Clear();
	}
}
