using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fase_1
{
    class Automata
    {
        //Introducir el Registro al arbol
        //recorrerlo para introducir las reglas
        public ExpressionNode CreateTree(string regex)
        {
            Tree tree = new Tree();
            ExpressionNode root = tree.PrefixToBinaryTree(regex);

            return root;

        }

        /// <summary>
        /// Create the Finite Deterministic Automaton base in a tree
        /// nodes, a list of followpos and mathematical set states
        /// </summary>
        /// <param name="root"></param>
        /// <param name="nodos"></param>
        /// <param name="followpos"></param>
        /// <returns></returns>
        public Dictionary<string, string> CreateAutomata(ExpressionNode root, Dictionary<int, string> nodes, Dictionary<int, List<int>> followpos)
        {
            State state = new State();
            List<string> node_values = new List<string>();
            bool EndFollow = false;
            int state_num = 0;
            int count = 0;
            int current_state = 0;
            state.StateSet.Add("q" + state_num.ToString(), root.First);
            state_num++;

            Dictionary<string, string> transicion_valor = new Dictionary<string, string>();

            //se inicia obteniendo los nodos y se valuan todos los datos

            node_values = ObtainNodeValues(nodes);
            List<int> temp_followpos = state.StateSet.ElementAt(count).Value;
            List<int> FollowInsert = new List<int>();
            current_state = -1;

            while (!EndFollow)
            {
                //no se ha terminado de analizar todos los conjuntos
                //se inicia analizado el conjunto perteneciente a q0
                temp_followpos = state.StateSet.ElementAt(count).Value;
                FollowInsert = new List<int>();
                current_state++;
                count++;

                for (int i = 0; i < node_values.Count; i++)
                {
                    //se toman los datos y se analizar para ver si se pueden hacer transiciones de estados
                    //se obtiene cada nodo del followpos y se evalua si el nodo contiene el dato a evaluar del listado de nodos
                    //si si, se crea un nuevo conjunto con el followpos o concatenacion del followpos


                    for (int j = 0; j < nodes.Count; j++)
                    {
                        //obtenemos el dato del nodo
                        string dato = nodes.ElementAt(j).Value;

                        //verificamos si es igual al dato buscado, si es asi, se verifica tambien que el dato este en el conjunto de
                        //followpos

                        if (node_values.ElementAt(i) == dato)
                        {
                            //existe un dato similar
                            if (temp_followpos.Contains(j + 1))
                            {   //existe dentro del conjunto

                                if (true)
                                {

                                    for (int l = 0; l < nodes.Count; l++)
                                    {

                                        if (nodes.ElementAt(l).Value == dato && temp_followpos.Contains(l + 1))
                                        {

                                            if (!FollowInsert.Contains(l + 1))
                                            {
                                                FollowInsert.Add(nodes.ElementAt(l).Key);
                                            }

                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }

                                }


                                //verificamos que el conjunto no exista dentro de los estados validos
                                bool canInsert = false;
                                List<int> setState = new List<int>();

                                if (FollowInsert.Count != 0)
                                {
                                    for (int n = 0; n < FollowInsert.Count; n++)
                                    {
                                        //tomar los followpos de los nodos determinados anteriormente e insertarlos en tookedFollowPos
                                        if (!setState.Contains(FollowInsert.ElementAt(n)))
                                        {
                                            for (int x = 0; x < followpos.Count; x++)
                                            {
                                                if (FollowInsert.ElementAt(n) == followpos.ElementAt(x).Key)
                                                {
                                                    foreach (int item in followpos.ElementAt(x).Value)
                                                    {
                                                        if (!setState.Contains(item))
                                                        {
                                                            setState.Add(item);
                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }

                                for (int k = 0; k < state.StateSet.Values.Count; k++)
                                {
                                    if (state.StateSet.ElementAt(k).Value.OrderBy(x => x).SequenceEqual(setState.OrderBy(m => m)))
                                    {
                                        canInsert = false;
                                        break;
                                    }
                                    else
                                    {
                                        canInsert = true;
                                    }
                                }

                                if (canInsert)
                                {
                                    state.StateSet.Add("q" + state_num, setState);
                                    state_num++;
                                }


                                string name = name = "q" + current_state.ToString() + " / " + node_values.ElementAt(i).ToString();

                                //buscamos la transicion que corresponde al estado (o sea, obtener la llave asociada a tal followpos)
                                for (int k = 0; k < state.StateSet.Count; k++)
                                {
                                    if (state.StateSet.ElementAt(k).Value.OrderBy(x => x).SequenceEqual(setState.OrderBy(m => m)))
                                    {
                                        if (transicion_valor.ContainsKey(name))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            if (state.StateSet.ElementAt(k).Value.Contains(nodes.Count))
                                            {
                                                transicion_valor.Add(name, "#" + state.StateSet.ElementAt(k).Key);
                                            }
                                            else
                                            {
                                                transicion_valor.Add(name, state.StateSet.ElementAt(k).Key);
                                            }
                                        }
                                    }
                                }

                                if (transicion_valor.Count != 0 && transicion_valor.ContainsKey(name) == false)
                                {   //significa que no se encontro ninguna transicion y se pone un default

                                    transicion_valor.Add(name, "");
                                }

                                FollowInsert.Clear();
                            }

                        }
                    }

                }

                if (current_state + 1 == state.StateSet.Count)
                {
                    EndFollow = true;
                }
            }


            return transicion_valor;
        }

        private List<string> ObtainNodeValues(Dictionary<int, string> nodos)
        {
            List<string> node_values = new List<string>();

            for (int i = 0; i < nodos.Count; i++)
            {
                if (node_values.Count == 0)
                {
                    node_values.Add(nodos.Values.ElementAt(i));
                }
                if (!node_values.Contains(nodos.ElementAt(i).Value) && nodos.ElementAt(i).Value != "#")
                {
                    node_values.Add((nodos.Values.ElementAt(i)));
                }
            }

            return node_values;
        }

    }
}
