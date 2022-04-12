using System;
using System.Collections.Generic;

namespace Lab1
{
    public class Node: INode
    {
        private readonly string _name;

        public string GetName()
        {
            return _name;
        }

        public int GetCapacity()
        {
            return 10;
        }

        public NodeType GetNodeType()
        {
            return NodeType.Ordinary;
        }

        public string ProcessMessage(string message)
        {
            if(_i++<GetCapacity())
                return message;
            return "";
        }

        private int _i;
        public int GetCapacityLeft()
        {
            return GetCapacity() - _i;
        }

        public Node(string name)
        {
            _name = name + " node";
        }
    }
}