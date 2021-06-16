using System;
using UnityEngine;
using System.Collections.Generic;

namespace Match_Invaders.Logic
{
	public class GenericObjectPool<T> where T : MonoBehaviour
	{
		private readonly Stack<T> _stashedObjectStack = new Stack<T>();
		private readonly LinkedList<T> _activeList = new LinkedList<T>(); // faster removal of nodes
		public T Prototype;
		public int ActiveObjectsCount => _activeList.Count;

		public GenericObjectPool(T proto) => Prototype = proto;
		public T ProvideObject(Transform parent, Vector3 position, bool shouldBeEnabled)
		{
			if (_stashedObjectStack.Count > 0)
			{
				T obj = _stashedObjectStack.Pop();
				obj.transform.parent = parent;
				obj.transform.position = position;
				obj.gameObject.SetActive(shouldBeEnabled);
				_activeList.AddLast(obj);
				return obj;
			}
			else
			{
				T obj = UnityEngine.Object.Instantiate(Prototype.gameObject, position, Prototype.transform.rotation, parent).GetComponent<T>();
				_activeList.AddLast(obj);
				return obj;
			}
		}

		public void StashUnusedObject(T obj)
		{
			if (null == obj)
			{
				throw new ArgumentNullException("Passed null object to object pool for stashing");
			}
			if (_stashedObjectStack.Contains(obj))
			{
				throw new ArgumentException("Chosen object already stashed");
			}
			if (!_activeList.Contains(obj))
			{
				throw new ArgumentException("Chosen object is not part of this pool");
			}
			obj.gameObject.SetActive(false);
			_activeList.Remove(obj);
			_stashedObjectStack.Push(obj);
		}
	}
}