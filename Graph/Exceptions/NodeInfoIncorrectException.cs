using System;

namespace Graph.Exceptions
{
    /// <summary>
    /// Malformed Node information
    /// </summary>
    [Serializable]
    public class NodeInfoIncorrectException: Exception
    {
        public NodeInfoIncorrectException(string incorrectInfo)
            :base(String.Format("The node information '{0}' is incorrect.", incorrectInfo))
        {
        }
    }
}