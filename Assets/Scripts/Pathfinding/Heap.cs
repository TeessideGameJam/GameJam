using UnityEngine;
using System.Collections;
using System;

public class Heap<T> where T : IHItem<T>
{
    T[] items;
    int cItemCount;

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public void Add(T item)
    {
        item.HIndex = cItemCount;
        items[cItemCount] = item;
        UpSort(item);
        cItemCount++;
    }

    public T RemoveFirst()
    {
        T first = items[0];
        cItemCount--;
        items[0] = items[cItemCount];
        items[0].HIndex = 0;
        DownSort(items[0]);
        return first;
    }

    public void ItemUpdate(T item)
    {
        UpSort(item);

    }

    public int Count
    {
        get
        {
            return cItemCount;
        }
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HIndex], item);
    }

    void DownSort(T item)
    {
        while (true)
        {
            int cIndexL = (item.HIndex * 2) + 1;
            int cIndexR = (item.HIndex * 2) + 2;
            int swapIndex = 0;

            if (cIndexL < cItemCount)
            {
                swapIndex = cIndexL;

                if (cIndexR < cItemCount)
                {
                    if (items[cIndexL].CompareTo(items[cIndexR]) < 0)
                    {
                        swapIndex = cIndexR;
                    }
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }

    void UpSort(T item)
    {
        int pIndex = (item.HIndex - 1) / 2;

        while (true)
        {
            T pItem = items[pIndex];
            if (item.CompareTo(pItem) > 0)
            {
                Swap(item, pItem);
            }
            else
            {
                break;
            }
            pIndex = (item.HIndex - 1) / 2;
        }
    }

    void Swap(T a, T b)
    {
        items[a.HIndex] = b;
        items[b.HIndex] = a;
        int aIndex = a.HIndex;
        a.HIndex = b.HIndex;
        b.HIndex = aIndex;
    }
}

public interface IHItem<T> : IComparable<T>
{
    int HIndex
    {
        get;
        set;
    }
}