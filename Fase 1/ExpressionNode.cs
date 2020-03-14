using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase_1
{
    class ExpressionNode
    {
        public List<int> First;
        public List<int> Last;
        public List<int> Follow;
        public ExpressionNode parent;
        public string dato;
        public ExpressionNode Right;
        public ExpressionNode Left;
        public bool isNullable;

        public ExpressionNode()
        {
            First = new List<int>();
            Last = new List<int>();
            Follow = new List<int>();
            parent = null;
            Right = null;
            Left = null;
            dato = "";
            isNullable = false;
        }
    }
}
