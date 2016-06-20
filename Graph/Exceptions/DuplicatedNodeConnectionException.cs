using System;

namespace Graph.Exceptions
{
    /// <summary>
    /// Node duplicated in Graph informations
    /// </summary> 
    [Serializable]
    public class DuplicatedNodeConnectionException: Exception
    {
        public DuplicatedNodeConnectionException(string nodeDuplicated)
            : base(String.Format("The node '{0}' is duplicated.", nodeDuplicated))
        {
        }
   
    }
}