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
		public void StashAll()
		{
			while (_activeList.Count > 0)
			{
				if (null == _activeList.First.Value)
				{
					_activeList.RemoveFirst();
				}
				else
				{
					StashUnusedObject(_activeList.First.Value);
				}
			}
		}
		public void StashUnusedObject(T element)
		{
			if (null == element)
			{
				throw new ArgumentNullException("Passed null object to object pool for stashing");
			}
			if (_stashedObjectStack.Contains(element))
			{
				throw new ArgumentException("Chosen object already stashed");
			}
			if (!_activeList.Contains(element))
			{
				throw new ArgumentException("Chosen object is not part of this pool");
			}
			element.gameObject.SetActive(false);
			_activeList.Remove(element);
			_stashedObjectStack.Push(element);
		}

		public virtual void DestroyAllPoolObjects()
		{
			foreach (T activeElement in _activeList)
			{
				if (activeElement != null)
				{
					UnityEngine.Object.Destroy(activeElement.gameObject);
				}
			}
			_activeList.Clear();
			while(_stashedObjectStack.Count>0)
			{
				T pop = _stashedObjectStack.Pop();
				if (null != pop)
				{
					UnityEngine.Object.Destroy(pop.gameObject);
				}
			}
		}
	}
}