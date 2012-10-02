using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CCIFMS
{
  public class ThreadSafeList<T> : INotifyPropertyChanged
  {
    //private readonly Object _syncRoot = new object();
    #region private member
    private List<T> _inner_list = null;
    #endregion
    
    #region properties
    public int Count
    {
      get
      {
        return _inner_list.Count;
      }
    }
    #endregion

    #region construtor
    public ThreadSafeList()
    {
      this._inner_list = new List<T>();
    }
    #endregion

    #region public method
    public void Add(T item)
    {
      lock (this._inner_list)
      {
        _inner_list.Add(item);
        NotifyPropertyChanged("Count");
      }
    }
    public void Remove(T item)
    {
      lock (_inner_list)
      {
        _inner_list.Remove(item);
        NotifyPropertyChanged("Count");
      }
    }
    public bool Contains(T Item)
    {
      return _inner_list.Contains(Item);
    }
    public void Clear()
    {
      _inner_list.Clear();
    }

    #endregion
    
    #region INotifyPropertyChanged implement
    public event PropertyChangedEventHandler PropertyChanged;
    private void NotifyPropertyChanged(string name)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
      }
    }
    #endregion

  }
}
