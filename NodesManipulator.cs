using System;

namespace Lab1
{
    public abstract class Decorator : INode
    {
        protected INode component;
        public Decorator(INode node)
        {
            component = node;
        }
        public abstract int GetCapacity();

        public abstract int GetCapacityLeft();

        public string GetName()
        {
            return  component.GetName();
        }

        public abstract NodeType GetNodeType();

        public abstract string ProcessMessage(string message);
    }

    public class NodeBroken : Decorator
    {
        private readonly string _message;

        public NodeBroken(INode node, string message) : base(node)
        {
            _message = message;
        }

        public override int GetCapacity() => 0;
        public override string ProcessMessage(string message) => _message;
        public override NodeType GetNodeType() => NodeType.Broken;
        public override int GetCapacityLeft() => 0;
    }
    public class NodeSmart : Decorator
    {
        public NodeSmart(INode node) : base(node) { }
        public override int GetCapacity() => base.component.GetCapacity();
        public override NodeType GetNodeType() => NodeType.Smart;
        public override int GetCapacityLeft() => base.component.GetCapacityLeft();
        public override string ProcessMessage(string message)
        {
            if (base.component.GetCapacityLeft() == 0)
            {
                return "Capacity reached, no new messages will be processed";
            }
            else
                return base.component.ProcessMessage(message);
        }
    }

    public class NodeEncrypt : Decorator
    {
        protected int offset;
        protected int max;
        private int calls;
        public NodeEncrypt(INode node, int off, int _max) : base(node) 
        {
            calls = 0;
            offset = off;
            max = _max;
        }
        public override int GetCapacity() => base.component.GetCapacity();
        public override NodeType GetNodeType() => NodeType.Encryption;
        public override int GetCapacityLeft() => base.component.GetCapacity() - calls;
        public override string ProcessMessage(string message)
        {
            char[] encmess = message.ToCharArray();
            for(int i = 0; i < message.Length; i++)
            {
                char letter = encmess[i];

                letter = (char)(letter + offset);

                if (letter > 'z')
                {
                    letter = (char)(letter - 26);
                }

                encmess[i] = letter;
            }
            if (calls > max)
            {
                return "Node auto-destroyed";
            }
            else
                calls++;
            return new string(encmess);
        }
    }
    public class NodeDecrypt : Decorator
    {
        protected int offset;
        public NodeDecrypt(INode node, int off) : base(node)
        {
            offset = 26 - off;
        }
        public override int GetCapacity() => base.component.GetCapacity();
        public override NodeType GetNodeType() => NodeType.Decryption;
        public override int GetCapacityLeft() => base.component.GetCapacity();
        public override string ProcessMessage(string message)
        {
            char[] encmess = message.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                char letter = encmess[i];

                letter = (char)(letter + offset);

                if (letter > 'z')
                {
                    letter = (char)(letter - 26);
                }

                encmess[i] = letter;
            }
            return new string(encmess);
        }
    }

    public static class NodesManipulator
    {
        public static INode SetNodeToBroken(INode node, string message)
        {
            //TODO
            return new NodeBroken(node, message);
        }
        
        public static INode MakeNodeSmart(INode node)
        {
            //TODO
            return new NodeSmart(node);
        }
        
        public static INode EncryptMessages(INode node, int offset, int maxCalls)
        {
            //TODO
            return new NodeEncrypt(node, offset, maxCalls);
        }
        
        public static INode DecryptMessages(INode node, int offset)
        {
            //TODO
            return new NodeDecrypt(node, offset);
        }
    }
}