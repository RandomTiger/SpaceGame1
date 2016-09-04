using System;

public class CyclicBuffer<T>
{
    public CyclicBuffer(int size)
    {
        m_List = new T[size];
    }

    public void Add(T element)
    {
        int newTail = (m_Tail + 1) % m_List.Length;

        if(newTail == m_Head)
        {
            throw new Exception("Out of buffer");
        }

        m_List[m_Tail] = element;
        m_Tail = newTail;
    }

    public T GetFirst()
    {
        if (m_Head == m_Tail)
        {
            return default(T);
        }

        T element = m_List[m_Head];
        m_Head = (m_Head + 1) % m_List.Length;
        return element;
    }

    private T[] m_List;
    int m_Head;
    int m_Tail;
}
