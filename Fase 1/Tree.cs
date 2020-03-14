using System;
using System.Collections.Generic;
using System.Linq;



namespace Fase_1
{
    class Tree
    {
        static int values = 1;
        Dictionary<int, List<int>> Follow = new Dictionary<int, List<int>>();


        public ExpressionNode PrefixToBinaryTree(string expression)
        {
            Stack<string> Operation = new Stack<string>();
            Stack<ExpressionNode> Data = new Stack<ExpressionNode>();
            Stack<ExpressionNode> LastRegister = new Stack<ExpressionNode>();
            Stack<ExpressionNode> opAfter = new Stack<ExpressionNode>();
            string chunk = "";
            string current = "";

            for (int i = 0; i < expression.Length; i++)
            {
                current = expression.Substring(i, 1);

                if (isDigit(current))
                {
                    chunk += current;
                    continue;
                }

                if (isOperator(current) && current != ")" && current != "(")
                {
                    if (i + 1 < expression.Length && i + 2 < expression.Length)
                    { //la siguiente es una palabra

                        if (current == "." && (expression.Substring(i + 1, 1) == ".") && expression.Substring(i + 2, 1) != ".")
                        {
                            //dato actual en i + 1 es una palabra
                            chunk += current;
                            continue;
                        }
                    }
                    if (i + 1 < expression.Length)
                    {
                        if (expression.Substring(i + 1, 1) == "Æ")
                        {
                            chunk += current;
                            ExpressionNode node = new ExpressionNode();
                            node.dato = chunk;
                            chunk = "";
                            Data.Push(node);
                            i++;
                            continue;
                        }
                    }

                    if (chunk != "")
                    {
                        ExpressionNode node = new ExpressionNode();
                        node.dato = chunk;
                        chunk = "";
                        Data.Push(node);
                    }

                    bool canOperate = true;
                    Operation.Push(current);


                    //tiene que verificar que si puede operar, de lo contrario continua agregando

                    while (canOperate == true)
                    {
                        canOperate = false;

                        if (Operation.Count > 0)
                        {
                            //validacion especial de | intermedio
                            if (Operation.Peek() == "|")
                            {
                                string temp = Operation.Pop();

                                if (Operation.Count > 0)
                                {

                                    if (Operation.Peek() == "." && Data.Count > 1)
                                    {
                                        ExpressionNode op = new ExpressionNode();
                                        op.dato = Operation.Pop();

                                        ExpressionNode node1 = Data.Pop();
                                        ExpressionNode node2 = Data.Pop();

                                        op.Left = node2;
                                        op.Right = node1;

                                        Data.Push(op);
                                        Operation.Push(temp);

                                        //se saca el operador y se guarda para validar si se puede operar la
                                        //siguiente expr y luego concatenar con |
                                        opAfter.Push(Data.Pop());
                                    }
                                    else if (Data.Count > 1)
                                    {
                                        ExpressionNode op = new ExpressionNode();
                                        op.dato = temp;

                                        ExpressionNode node1 = Data.Pop();
                                        ExpressionNode node2 = Data.Pop();

                                        op.Left = node2;
                                        op.Right = node1;

                                        Data.Push(op);
                                    }

                                    else
                                    {
                                        Operation.Push(current);
                                    }
                                }
                                else
                                {
                                    if (Operation.Count > 0 && Data.Count > 1)
                                    {
                                        if (Operation.Peek() == "|")
                                        {
                                            opAfter.Push(Data.Pop());
                                        }

                                    }
                                    Operation.Push(temp);
                                }

                            }
                            if (Operation.Peek() == ".")
                            {//necesita minimo 2 datos para operar
                                if (Data.Count > 1)
                                {
                                    canOperate = true;
                                }

                                if (canOperate)
                                {
                                    ExpressionNode op = new ExpressionNode();
                                    op.dato = Operation.Pop();

                                    ExpressionNode node1 = Data.Pop();
                                    ExpressionNode node2 = Data.Pop();

                                    op.Left = node2;
                                    op.Right = node1;

                                    Data.Push(op);
                                }
                            }

                            if (Operation.Peek() == "*" || Operation.Peek() == "+" || Operation.Peek() == "?")
                            {
                                if (Data.Count > 0)
                                {
                                    canOperate = true;
                                }

                                if (canOperate)
                                {
                                    ExpressionNode op = new ExpressionNode();
                                    op.dato = Operation.Pop();

                                    op.Left = Data.Pop();

                                    Data.Push(op);
                                }
                            }
                        }

                    }

                }

                if (current == ")" && Operation.Count > 0)
                {
                    if (chunk != "")
                    {
                        ExpressionNode node = new ExpressionNode();
                        node.dato = chunk;
                        chunk = "";
                        Data.Push(node);
                    }


                    while (Operation.Peek() != "(" && Operation.Count > 1)
                    {

                        if (Operation.Peek() == ".")
                        {

                            ExpressionNode data = new ExpressionNode();
                            data.dato = Operation.Pop();

                            if (Data.Count >= 2)
                            {
                                ExpressionNode nodea = Data.Pop();
                                ExpressionNode nodeb = Data.Pop();

                                data.Left = nodeb;
                                data.Right = nodea;

                                Data.Push(data);
                            }
                            else if (Data.Count == 1 && opAfter.Count == 1)
                            {
                                ExpressionNode nodea = Data.Pop();
                                ExpressionNode nodeb = opAfter.Pop();

                                data.Left = nodeb;
                                data.Right = nodea;

                                Data.Push(data);
                            }
                            else if (Data.Count == 1 && LastRegister.Count > 1)
                            {
                                ExpressionNode nodea = Data.Pop();
                                ExpressionNode nodeb = LastRegister.Pop();

                                data.Left = nodeb;
                                data.Right = nodea;

                                Data.Push(data);
                            }
                            continue;
                        }

                        if (Operation.Peek() == "*" || Operation.Peek() == "+" || Operation.Peek() == "?")
                        {
                            ExpressionNode data = new ExpressionNode();
                            data.dato = Operation.Pop();

                            ExpressionNode nodea = Data.Pop();

                            data.Left = nodea;
                            Data.Push(data);
                            continue;
                        }

                        if (Operation.Peek() == "|")
                        {// se trae un dato de regAnterior
                            ExpressionNode data = new ExpressionNode();
                            data.dato = Operation.Pop();

                            if (Data.Count == 2)
                            {
                                ExpressionNode nodea = Data.Pop();
                                ExpressionNode nodeb = Data.Pop();

                                data.Left = nodeb;
                                data.Right = nodea;

                            }
                            else
                            {
                                ExpressionNode nodea = opAfter.Pop();
                                ExpressionNode nodeb = Data.Pop();

                                data.Left = nodea;
                                data.Right = nodeb;

                            }



                            Data.Push(data);
                            continue;
                        }

                        Operation.Pop();
                    }

                    if (Operation.Count > 0)
                    {
                        if (Operation.Peek() == "(")
                        {
                            Operation.Pop();
                        }
                    }

                    if (Operation.Count > 0)
                    {
                        if (Operation.Peek() == "|")
                        {// se trae un dato de regAnterior
                         //p
                            ExpressionNode data = new ExpressionNode();
                            data.dato = Operation.Pop();

                            ExpressionNode nodea = LastRegister.Pop();
                            ExpressionNode nodeb = Data.Pop();

                            data.Left = nodea;
                            data.Right = nodeb;

                            Data.Push(data);
                        }

                    }


                }


                if (current == "(")
                {

                    if (Data.Count != 0 && Operation.Count == 1)
                    {
                        LastRegister.Push(Data.Pop());
                    }
                    if (LastRegister.Count == 2)
                    {
                        ExpressionNode opr = new ExpressionNode();
                        opr.dato = Operation.Pop();

                        ExpressionNode node1 = LastRegister.Pop();
                        ExpressionNode node2 = LastRegister.Pop();

                        opr.Left = node2;
                        opr.Right = node1;

                        LastRegister.Push(opr);
                    }
                    else
                    {
                        if (Data.Count > 0)
                        {
                            LastRegister.Push(Data.Pop());

                        }
                        Operation.Push(current);
                    }

                    continue;
                }





            }

            while (Operation.Count != 0)
            {
                if (chunk != "")
                {
                    ExpressionNode node = new ExpressionNode();
                    node.dato = chunk;
                    chunk = "";
                    Data.Push(node);
                }

                bool canOperate = false;

                if (Operation.Peek() == "|" || Operation.Peek() == ".")
                {//necesita minimo 2 Data para operar
                    if (Data.Count > 1)
                    {
                        canOperate = true;
                    }

                    if (canOperate)
                    {
                        ExpressionNode op = new ExpressionNode();
                        op.dato = Operation.Pop();

                        ExpressionNode node1 = Data.Pop();
                        ExpressionNode node2 = Data.Pop();

                        op.Left = node2;
                        op.Right = node1;

                        Data.Push(op);
                    }
                }

                if (Operation.Count > 0)
                {
                    if (Operation.Peek() == "*" || Operation.Peek() == "+" || Operation.Peek() == "?")
                    {
                        if (Data.Count > 0)
                        {
                            canOperate = true;
                        }

                        if (canOperate)
                        {
                            ExpressionNode op = new ExpressionNode();
                            op.dato = Operation.Pop();

                            op.Left = Data.Pop();

                            Data.Push(op);
                        }
                    }
                }



            }

            return Data.Pop();
        }

        private bool isDigit(string dato)
        {
            try
            {
                string operators = "*|+.()?~";

                if (operators.Contains(dato))
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool isOperator(string op)
        {
            string operators = "*|+.?()~";

            if (operators.Contains(op))
            {
                return true;
            }

            return false;
        }

        //se comienza a trabajar los First,Last y Follow
        public ExpressionNode assignRules(ExpressionNode root)
        {
            if (root.Right == null && root.Left == null)
            {
                root.First.Add(values);
                root.Last.Add(values);
                values++;
            }
            else
            {
                assignRules(root.Left);

                if (root.dato == "*")
                {
                    root.isNullable = true;
                }
                if (root.dato == "+")
                {
                    if (root.Left.isNullable == false)
                    {
                        root.isNullable = false;
                    }
                    else
                    {
                        root.isNullable = true;
                    }
                }
                if (root.dato == "?")
                {
                    root.isNullable = true;
                }

                if (root.Right != null)
                {
                    assignRules(root.Right);
                }


                if (root.dato == ".")
                {
                    if (root.Left != null && root.Right != null)
                    {
                        if (root.Left.isNullable == true && root.Right.isNullable == true)
                        {
                            root.isNullable = true;
                        }
                        else
                        {
                            root.isNullable = false;
                        }
                    }
                }

                if (root.dato == "|")
                {
                    if (root.Left != null && root.Right != null)
                    {
                        if (root.Left.isNullable == true || root.Right.isNullable == true)
                        {
                            root.isNullable = true;
                        }
                        else
                        {
                            root.isNullable = false;
                        }
                    }
                }
            }

            return root;
        }

        public ExpressionNode FirstAndLast(ExpressionNode root)
        {
            if (root.Left != null)
            {
                FirstAndLast(root.Left);
            }

            if (root.Right != null)
            {
                FirstAndLast(root.Right);
            }

            if (root.Left == null && root.Right == null)
            {
                return root;
            }

            if (root.dato == "|" && root.Right != null && root.Left != null)
            {
                List<int> c1 = root.Left.First;
                List<int> c2 = root.Right.First;

                for (int i = 0; i < c1.Count; i++)
                {
                    root.First.Add(c1.ElementAt(i));
                }

                for (int i = 0; i < c2.Count; i++)
                {
                    if (!root.First.Contains(c2.ElementAt(i)))
                    {
                        root.First.Add(c2.ElementAt(i));
                    }
                    else
                    {
                        continue;
                    }

                }

                //tomamos ahora los last

                c1 = root.Left.Last;
                c2 = root.Right.Last;

                for (int i = 0; i < c1.Count; i++)
                {
                    root.Last.Add(c1.ElementAt(i));
                }

                for (int i = 0; i < c2.Count; i++)
                {
                    if (!root.Last.Contains(c2.ElementAt(i)))
                    {
                        root.Last.Add(c2.ElementAt(i));
                    }
                    else
                    {
                        continue;
                    }

                }
            }

            if (root.dato == "." && root.Right != null && root.Left != null)
            {
                //c1 es nullable
                if (root.Left.isNullable == true)
                {
                    List<int> c1 = root.Left.First;
                    List<int> c2 = root.Right.First;

                    for (int i = 0; i < c1.Count; i++)
                    {
                        root.First.Add(c1.ElementAt(i));
                    }

                    for (int i = 0; i < c2.Count; i++)
                    {
                        if (!root.First.Contains(c2.ElementAt(i)))
                        {
                            root.First.Add(c2.ElementAt(i));
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                else
                {
                    root.First = root.Left.First;
                }

                if (root.Right.isNullable == true)
                {
                    List<int> c1 = root.Left.Last;
                    List<int> c2 = root.Right.Last;

                    for (int i = 0; i < c1.Count; i++)
                    {
                        root.Last.Add(c1.ElementAt(i));
                    }

                    for (int i = 0; i < c2.Count; i++)
                    {
                        if (!root.Last.Contains(c2.ElementAt(i)))
                        {
                            root.Last.Add(c2.ElementAt(i));
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                else
                {
                    root.Last = root.Right.Last;
                }
            }

            if (root.dato == "*" && root.Left != null)
            {
                root.First = root.Left.First;
                root.Last = root.Left.Last;
            }

            if (root.dato == "+" && root.Left != null)
            {
                root.First = root.Left.First;
                root.Last = root.Left.Last;
            }

            if (root.dato == "?" && root.Left != null)
            {
                root.First = root.Left.First;
                root.Last = root.Left.Last;
            }

            return root;
        }

        public Dictionary<int, List<int>> Follows(ExpressionNode root)
        {
            for (int i = 1; i < values; i++)
            {
                List<int> followpos = new List<int>();
                Follow.Add(i, followpos);
            }

            return Follow;
        }

        public Dictionary<int, List<int>> FollowTable(ExpressionNode root, Dictionary<int, List<int>> followPos)
        {

            if (root.Left == null && root.Right == null)
            {
                return followPos;
            }

            if (root.Left != null)
            {
                followPos = FollowTable(root.Left, followPos);
            }
            if (root.Right != null)
            {
                followPos = FollowTable(root.Right, followPos);
            }

            if (root.dato == ".")
            {
                List<int> firstc2 = root.Right.First;
                List<int> lastc1 = root.Left.Last;

                for (int i = 0; i < lastc1.Count; i++)
                {
                    if (followPos[lastc1.ElementAt(i)].Count != 0)
                    {

                        for (int j = 0; j < firstc2.Count; j++)
                        {
                            //si el nodo contiene algun elemento de la lista, entonces no lo agregara
                            if (followPos[lastc1.ElementAt(i)].Contains(firstc2.ElementAt(j)))
                            {

                                continue;
                            }
                            else
                            {

                                followPos[lastc1.ElementAt(i)].Add(firstc2.ElementAt(j));
                            }
                        }
                    }
                    else
                    {//si no tiene nada, entonces agregara


                        followPos[lastc1.ElementAt(i)] = firstc2;
                    }

                }
            }

            if ((root.dato == "*" || root.dato == "+" || root.dato == "?") && root.Left != null)
            {
                List<int> first1 = root.Left.First;
                List<int> last1 = root.Left.Last;

                for (int i = 0; i < last1.Count; i++)
                {
                    if (followPos.ElementAt(last1.ElementAt(i)).Value.Count != 0)
                    {
                        for (int j = 0; j < last1.Count; j++)
                        {
                            //si el nodo contiene algun elemento de la lista, entonces no lo agregara
                            if (followPos.ElementAt(last1.ElementAt(i)).Value.Contains(first1.ElementAt(j)))
                            {
                                continue;
                            }
                            else
                            {
                                followPos[last1.ElementAt(i)].Add(first1.ElementAt(j));
                            }
                        }
                    }
                    else
                    {//si no tiene nada, entonces agregara
                        //followPos.Add(lastc1.ElementAt(i), firstc1);
                        followPos[last1.ElementAt(i)] = first1;
                    }
                }

            }
            return followPos;
        }

        public Dictionary<int, string> ObtainLeafs(ExpressionNode root, Dictionary<int, string> Data)
        {
            if (root.Left == null && root.Right == null)
            {
                Data.Add(root.First[0], root.dato);
            }

            if (root.Left != null)
            {
                Data = ObtainLeafs(root.Left, Data);
            }

            if (root.Right != null)
            {
                Data = ObtainLeafs(root.Right, Data);
            }

            return Data;
        }
    }
}
