using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���ҽ� ���� �Ŵ���
public class ResourceManager
{
	// ���� ��ο� �����ϴ� ���̴� ��������
   public T Load<T>(string path) where T : Object
	{
		return Resources.Load<T>(path);
	}
	// ������ ��ο� �����ϴ� ������Ʈ ����
	public GameObject Instantiate(string path, Transform parent = null)
	{
		GameObject prefab = Load<GameObject>($"Prefabs/{path}");
		if(prefab == null)
		{
			Debug.Log($"Failed Load Prefab : Prefabs/{path}");
			return null;
		}

		return Object.Instantiate(prefab, parent);
	}
	// ������Ʈ �ı�
	public void Destroy(GameObject go)
	{
		if (go == null)
			return;

		Object.Destroy(go);
	}
}
