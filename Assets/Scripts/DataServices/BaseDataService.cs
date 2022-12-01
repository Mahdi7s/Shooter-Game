using System;
using System.Collections.Generic;

public class BaseDataService<T>  where T : new()
{
    protected bool IsOffline = false;

    /// <summary>
    /// This field is for data cache
    /// </summary>
    protected Dictionary<string, IEnumerable<T>> Cache = new Dictionary<string, IEnumerable<T>>();
    protected Dictionary<string, Tuple<bool, List<Action<object>>>> FetchQueue = new Dictionary<string, Tuple<bool, List<Action<object>>>>();
  //  protected LocalDataService<T> LocalDataService;//= new LocalDataService<T>("PrimeSQLLite.db");

    protected virtual string TableName { get { return typeof(T).Name.Replace("Statistic", ""); } }
    //#if(DeviceId=="wdfsdfgsfgfgfgsfggsfgsggssgs")
    // protected const string HostName =  "http://188.165.57.215:9090/";//
    /*#else"http://localhost:55212/";

    #endif*/
    //protected const string HostName = StaticValues.HostName + ":9090/";//"http://localhost:55213/";//
    protected virtual string ControllerName { get { return typeof(T).Name.Replace("Model", "").Replace("Statistic", ""); } }
    #region Onlin
    /// <summary>
    /// This method removes a row from a specific table with the row id.
    /// </summary>
    /// <param name="id">Row Id</param>
    /// <param name="callback">If the return value of the function is greater than zero, the result is successful, otherwise it was unsuccessful </param>
    public virtual void Delete(long id, Action<int> callback)
    {
       
    }
    /// <summary>
    /// This function adds one row to a particular table and returns the id of that row
    /// </summary>
    /// <param name="entry">The row model you want to add</param>
    /// <param name="callback">If the return value of the function is greater than zero, the result is successful, otherwise it was unsuccessful</param>
    public virtual void Insert(T entry, Action<long> callback)
    {
       
    }
    /// <summary>
    ///  This function adds one row to a particular table and returns the id of that row
    /// </summary>
    /// <param name="callback">If the return value of the function is greater than zero, the result is successful, otherwise it was unsuccessful</param>
    /// <param name="columns">key(string)=Column Name,Value(object)=Value Of Culomn</param>
    public virtual void Insert(Action<long> callback, params KeyValuePair<string, object>[] columns)
    {

       
    }
    /// <summary>
    /// Get a row from a specific table with the same line id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callback"></param>
    public virtual void Get(long id, Action<T> callback)
    {
      

    }
    /// <summary>
    /// Get a list of rows of a table with a specific query without Parameters
    /// </summary>
    /// <param name="sql">SQL Query</param>
    /// <param name="callback"></param>
    public virtual void GetQueryTable(string sql, Action<IEnumerable<T>> callback)
    {

    }
    /// <summary>
    ///  Get a list of rows of a table with a specific query with Parameters
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="sql">SQL Query</param>
    /// <param name="parameters">Key(string)=Parameter Name,Value(object)=Paramater Value</param>
    public virtual void QueryTableWithParam(Action<IEnumerable<T>> callback, string sql, params KeyValuePair<string, object>[] parameters)
    {
      
    }
    /// <summary>
    /// Get a list of rows of a table with a specific conjunction with Parameters
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="conjunction">conjunction of query that write after where </param>
    /// <param name="parameters">Key(string)=Parameter Name,Value(object)=Paramater Value</param>
    /// <param name="columns">columns Names</param>
    public virtual void QueryTableWithParam(Action<IEnumerable<T>> callback, string conjunction = null, Dictionary<string, object> parameters = null, params string[] columns)
    {
      

    }

    /// <summary>
    /// example for culomn =new{}
    /// </summary>  
    /// <param name="id"></param>
    /// <param name="callback"></param>
    public virtual void Update(long id, T entry, Action<int> callback)
    {
       
    }
    public virtual void ExecuteQueryWithParameters(string sql, Action<int> callback, params KeyValuePair<string, object>[] parameters)
    {
       
     
    }
    public virtual void Update(long id, Action<int> callback, params KeyValuePair<string, object>[] columns)
    {
      
    }
   
  
  
 
    public void GetAll(Action<IEnumerable<T>> callback)
    {
      
    }
   
    #endregion
 

}