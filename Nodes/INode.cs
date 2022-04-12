using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public interface INode
    {
        public string GetName();
        public int GetCapacity();
        public NodeType GetNodeType();
        public string ProcessMessage(string message);
        public int GetCapacityLeft();
    }
}