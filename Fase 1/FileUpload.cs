using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Fase_1
{
    class FileUpload
    {

        public static Dictionary<string, List<string>> SETS = new Dictionary<string, List<string>>();
        public static Dictionary<int, string> ACTIONS = new Dictionary<int, string>();
        public static Dictionary<int, string> TOKENS = new Dictionary<int, string>();


        public string ReadFile(string path)
        {
            try
            {
                //TODO: Validar por que no me incluye la R del 'O''R' en el token
                //TODO: ingresar reglas ya al arbol para su construccion
                String FinalExpression = null;
                StreamReader streamReader = new StreamReader(path);

                Dictionary<string, List<string>> actions = new Dictionary<string, List<string>>();

                Dictionary<string, string> tokens = new Dictionary<string, string>();
                int line = 0;
                int columns = 1;
                string Register = "";

                while (!streamReader.EndOfStream)
                {
                    //leemos la line de codigo actual
                    string readline = streamReader.ReadLine();
                    line++;
                    string[] validate = readline.Split();
                    string value = "";
                    bool Valid_Value = false;
                    //validamos si pertenece a algun tipo de dato reservado dentro del archivo

                    if (validate[0] == "SETS" || value == "SETS")
                    {//es un set y se mete a validar SETS hasta que encuentra la palabra TOKENS

                        value = validate[0];


                        while (!streamReader.EndOfStream)
                        {

                            if (value == "SETS" && line == 1)
                            {
                                line++;
                                continue;
                            }

                            if (!streamReader.EndOfStream)
                            {
                                readline = streamReader.ReadLine();
                            }

                            if (readline == "\t" && !streamReader.EndOfStream)
                            {
                                continue;
                            }

                            if (readline == "" && !streamReader.EndOfStream)
                            {
                                continue;
                            }

                            validate = readline.Trim().Split();

                            switch (validate[0])
                            {
                                case "TOKENS":
                                    value = "TOKENS";
                                    break;

                                case "ACTIONS":
                                    FinalExpression = FinalExpression.Remove(FinalExpression.Length - 1, 1);
                                    FinalExpression = FinalExpression + ".#";
                                    value = "ACTIONS";
                                    break;

                                default:
                                    //no contiene ninguna palabra reservada
                                    if (value == "SETS")
                                    {//si la palabra reservada sigue siendo SETS entonces obtiene expresion en datas[1]
                                        string[] datas = new string[2];
                                        string name = "";
                                        columns = 0;

                                        for (int i = 0; i < readline.Length; i++)
                                        {
                                            string separator = readline.Substring(i, 1);

                                            if (separator == "=")
                                            {
                                                datas[0] = name;
                                                datas[1] = readline.Substring(i + 1, (readline.Length - 1) - (i));
                                                break;
                                            }
                                            columns++;
                                            name += readline[i];
                                        }
                                        line++;

                                        //datas[1] contiene la expresion deSetList
                                        List<string> SetList = new List<string>();

                                        int count_separators = 0;

                                        for (int i = 0; i < datas[1].Length; i++)
                                        {
                                            string ACdata = datas[1].Substring(i, 1);

                                            //validar espacios
                                            if (ACdata == " " && Valid_Value == false)
                                            {
                                                columns++;
                                                continue;
                                            }

                                            if (ACdata == "\t" && Valid_Value == false)
                                            {
                                                columns++;
                                                continue;
                                            }
                                            columns++;

                                            if (Valid_Value == false && ACdata == "C")
                                            {//puede que posiblemente sea CHR

                                                while (i < datas[1].Length && datas[1].Substring(i + 1, 1) != "(")
                                                {
                                                    string set = "CHR";
                                                    string integer_init = "";
                                                    string integer_final = "";

                                                    ACdata += datas[1].Substring(i + 1, 1);

                                                    if (set.Contains(ACdata))
                                                    {
                                                        if (ACdata == "CHR")
                                                        {
                                                            if (i + 2 < datas[1].Length)
                                                            {
                                                                i++;
                                                                columns++;
                                                                //se busca el numero contenido dentro del CHR
                                                                integer_init += datas[1].Substring(i + 2, 1);
                                                                i = i + 2;
                                                                columns = columns + 2;

                                                                while (i + 1 < datas[1].Length && datas[1].Substring(i + 1, 1) != ")")
                                                                {
                                                                    integer_init += datas[1].Substring(i + 1, 1);
                                                                    i++;
                                                                    columns++;
                                                                }

                                                                //se trata de parsear el dato, si da error entonces 
                                                                //hay inconsistencia de datas

                                                                try
                                                                {


                                                                    if (i + 2 < datas[1].Length)
                                                                    {
                                                                        if (datas[1].Substring(i + 2, 1) == ".")
                                                                        {//se verifica si el siguiente es un punto y si el i + 2 es diferente de .
                                                                            if (datas[1].Substring(i + 3, 1) == ".")
                                                                            {//posiblemente es un intervalo
                                                                                i = i + 3;
                                                                                columns = columns + 3;
                                                                                ACdata = "";

                                                                                while (i + 1 < datas[1].Length)
                                                                                {
                                                                                    ACdata += datas[1].Substring(i + 1, 1);

                                                                                    if (set.Contains(ACdata))
                                                                                    {
                                                                                        if (ACdata == "CHR")
                                                                                        {
                                                                                            i = i + 3;

                                                                                            while (datas[1].Substring(i, 1) != ")" && i < datas[1].Length)
                                                                                            {
                                                                                                integer_final += datas[1].Substring(i, 1);
                                                                                                i++;
                                                                                                columns++;
                                                                                            }
                                                                                            break;
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            i++;
                                                                                            columns++;
                                                                                        }
                                                                                    }

                                                                                    else
                                                                                    {
                                                                                        throw new Exception();
                                                                                    }


                                                                                }
                                                                            }

                                                                        }
                                                                        else if (datas[1].Substring(i + 2, 1) == "C")
                                                                        {//posible concatenacion de CHR's

                                                                            //se incrementa en uno para librarse del ")"
                                                                            i++;
                                                                            columns++;
                                                                            ACdata = "";
                                                                            string ConvertData = "";

                                                                            while (i + 1 < datas[1].Length)
                                                                            {
                                                                                ACdata += datas[1].Substring(i + 1, 1);
                                                                                i++;
                                                                                columns++;
                                                                                if ("CHR".Contains(ACdata))
                                                                                {
                                                                                    if (ACdata == "CHR")
                                                                                    {
                                                                                        if (i + 2 < datas[1].Length)
                                                                                        {
                                                                                            ConvertData += datas[1].Substring(i + 2, 1);
                                                                                            i = i + 2;
                                                                                            columns = columns + 2;
                                                                                            //se procede a tomar la parte numerica del CHR,
                                                                                            //caso termine por un ), entonces convertira el numero a 
                                                                                            //char y lo agregara al diccionario
                                                                                            while (i + 1 < datas[1].Length && datas[1].Substring
                                                                                                (i + 1, 1) != ")")
                                                                                            {
                                                                                                ConvertData += datas[1].Substring(i + 1, 1);
                                                                                                i++;
                                                                                                columns++;
                                                                                            }

                                                                                            //se agrega alSetList
                                                                                            int num = int.Parse(ConvertData);
                                                                                            SetList.Add(Convert.ToChar(num).ToString());
                                                                                            ConvertData = "";

                                                                                            ACdata = "";
                                                                                            if (i + 1 < datas[1].Length)
                                                                                            {
                                                                                                i = i + 1;
                                                                                                columns++;
                                                                                                if (integer_init != "")
                                                                                                {
                                                                                                    num = int.Parse(integer_init);
                                                                                                    SetList.Add(Convert.ToChar(num).ToString());
                                                                                                    integer_init = "";
                                                                                                }
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                //verificar si el ultimo char es un ")"
                                                                                                if (i + 1 < datas[1].Length)
                                                                                                {
                                                                                                    ACdata = datas[1].Substring(i + 1, 1);

                                                                                                    if (ACdata != ")")
                                                                                                    {
                                                                                                        throw new Exception("Error al parsear CHR");
                                                                                                    }
                                                                                                }
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            throw new Exception();
                                                                                        }
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    throw new Exception("Error en lectura CHR");
                                                                                }
                                                                            }
                                                                            break;
                                                                        }


                                                                    }

                                                                    int a = Convert.ToInt32(integer_init);
                                                                    int b = Convert.ToInt32(integer_final);

                                                                    for (int j = a; j <= b; j++)
                                                                    {
                                                                        SetList.Add(Convert.ToChar(j).ToString());
                                                                    }

                                                                    break;
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    //hay error en datas almacenados
                                                                    throw new Exception("Error en el parse de CHR");
                                                                }

                                                            }
                                                        }
                                                        else
                                                        {
                                                            i++;
                                                            columns++;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                continue;
                                            }

                                            if (Valid_Value == false && ACdata != "'" && ACdata != "+" && ACdata != "\t")
                                            {
                                                //hay un error en el archivo
                                                string pos_err = "Error en lectura archivo. line: " + line.ToString() +
                                                    ", columns: " + columns.ToString();
                                                throw new Exception(pos_err);
                                            }

                                            if (ACdata == "'" && count_separators == 1)
                                            {
                                                //se verifica la siguiente posicion para ver si no contiene algun caracter
                                                //especial que haya que tomar como parte delSetList

                                                if (i + 2 < datas[1].Length)
                                                {
                                                    ACdata = datas[1].Substring(i + 2, 1);
                                                }

                                                if (i + 2 >= datas[1].Length)
                                                {// quiere decir que es el ultimo char enclosure
                                                    Valid_Value = false;
                                                    count_separators = 0;
                                                    continue;
                                                }

                                                //esto quiere decir que el dato intermedio es un caracter de escape '
                                                if (ACdata == "'")
                                                {
                                                    if (i + 1 < datas[1].Length)
                                                    {
                                                        if ((ACdata = datas[1].Substring(i + 1, 1)) == "+")
                                                        {
                                                            Valid_Value = false;
                                                            count_separators = 0;
                                                        }
                                                        else
                                                        {
                                                            Valid_Value = true;
                                                            SetList.Add(ACdata);
                                                            i = i + 2;
                                                            columns = columns + 2;
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //ingresar el dato ' al diccionario
                                                        SetList.Add(ACdata);
                                                    }
                                                }

                                                if (ACdata == ".")
                                                {//se verifica que el caracter anterior sea un punto tambien
                                                    ACdata = datas[1].Substring(i + 1, 1);

                                                    if (ACdata == ".")
                                                    {//es un intervalo
                                                     //se obtiene el ultimo elemento para sacar su char value
                                                        string interval_init = SetList.ElementAt(SetList.Count - 1);
                                                        string interval_final = "";

                                                        if (i + 4 < datas[1].Length)
                                                        {
                                                            //se obtiene el intervalo final
                                                            ACdata = datas[1].Substring(i + 4, 1);
                                                            interval_final = ACdata;
                                                        }
                                                        else
                                                        {
                                                            throw new Exception();
                                                        }

                                                        char init_val = Convert.ToChar(interval_init);
                                                        int init = init_val + 1;

                                                        for (int j = init; j < 256; j++)
                                                        {
                                                            string actual = Convert.ToString((char)j);

                                                            if (actual == interval_final)
                                                            {//se llego al intervalo y termina el ciclo
                                                                i = i + 4;
                                                                columns = columns + 4;
                                                                break;
                                                            }

                                                            SetList.Add(actual);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //se cierra el gap de ingreso de datas y se cambia el estatus de ingreso
                                                    count_separators = 0;
                                                    Valid_Value = false;
                                                }

                                            }

                                            if (Valid_Value == true && ACdata != "'")
                                            {//tiene permitido ingresar al diccionario
                                                string INdata = ACdata;

                                                while (i + 1 < datas[1].Length && datas[1].Substring(i + 1, 1) != "'")
                                                {
                                                    ACdata = datas[1].Substring(i + 1, 1);
                                                    INdata += ACdata;
                                                    i++;
                                                    columns++;
                                                }

                                                SetList.Add(INdata);

                                            }


                                            if (ACdata == "'" && count_separators == 0)
                                            {
                                                //se lleva un contador de separadores de char y se cambia el
                                                //estatus del bool que me permite ingresar si es dato es valido
                                                count_separators++;
                                                Valid_Value = true;
                                            }


                                        }

                                        //se asigna alSetList
                                        SETS.Add(datas[0].Trim(), SetList);

                                    }

                                    if (value == "TOKENS")
                                    {
                                        //verificar si los tokens son validos y agregarlo a unaRegister con un separador |
                                        //luego concatenar el .# y operar en el arbol
                                        string[] datas = new string[2];
                                        string name1 = "";
                                        columns = 0;

                                        for (int i = 0; i < readline.Length; i++)
                                        {
                                            string separador = readline.Substring(i, 1);

                                            if (separador == "=")
                                            {
                                                datas[0] = name1;
                                                datas[1] = readline.Substring(i + 1, (readline.Length - 1) - (i));
                                                break;
                                            }


                                            name1 += readline[i];
                                            columns++;
                                        }


                                        string ACdata = "";
                                        Register = "";
                                        Valid_Value = false;
                                        bool exists = false;
                                        line++;

                                        if (datas[1] == null)
                                        {
                                            continue;
                                        }

                                        for (int i = 0; i < datas[1].Length; i++)
                                        {
                                            exists = false;
                                            ACdata = datas[1].Substring(i, 1);

                                            if (ACdata == "'" && Valid_Value == false)
                                            {
                                                Valid_Value = true;
                                                columns++;
                                                continue;
                                            }

                                            if (ACdata == "(" || ACdata == ")")
                                            {

                                                if (ACdata == ")")
                                                {
                                                    Register = Register.Remove(Register.Length - 1, 1);
                                                    //se concatena para ver si hay un caracter especial fuera
                                                    Register += ACdata;
                                                }
                                                else
                                                {
                                                    if (datas[1].Substring(i - 1, 1) == ".")
                                                    {
                                                        Register += ".";
                                                        continue;
                                                    }
                                                    Register += ACdata;
                                                }

                                                continue;
                                            }

                                            if (ACdata == "'" && Valid_Value == true)
                                            {
                                                if (i + 1 < datas[1].Length && datas[1].Substring(i - 1, 1) == "'")
                                                {
                                                    if (datas[1].Substring(i + 1, 1) == "'")
                                                    {
                                                        for (int j = 0; j < SETS.Count; j++)
                                                        {
                                                            List<string> SetList_temp = SETS.ElementAt(j).Value;

                                                            if (SetList_temp.Contains(ACdata))
                                                            {
                                                                exists = true;
                                                            }
                                                        }

                                                        if (exists)
                                                        {
                                                            //se toma el dato actual como parte de algunSetList y se agrega a laRegister
                                                            Register += ACdata + ".";
                                                            i++;
                                                            columns = columns++;
                                                            Valid_Value = false;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Error en line No: " + line + "," + "columns: " + columns);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Valid_Value = false;
                                                    }
                                                }
                                                else
                                                {
                                                    Valid_Value = false;
                                                    columns++;
                                                    continue;
                                                }

                                            }

                                            if (ACdata == "\"" && Valid_Value == true)
                                            {
                                                if (datas[1].Substring(i + 1, 1) == "'")
                                                {//el " es un dato a validar dentro delSetList

                                                    for (int j = 0; j < SETS.Count; j++)
                                                    {
                                                        List<string> SetList_temp = SETS.ElementAt(j).Value;

                                                        if (SetList_temp.Contains(ACdata))
                                                        {
                                                            exists = true;
                                                        }
                                                    }

                                                    if (exists)
                                                    {
                                                        if (datas[1].Substring(i + 1, 1) == "|")
                                                        {
                                                            Register += ACdata + "|";
                                                            i++;
                                                            columns++;
                                                            Valid_Value = false;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            Register += ACdata + ".";
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Error en line No: " + line + "columns: " + columns);
                                                    }
                                                }
                                            }

                                            if (ACdata == " " || ACdata == "\t")
                                            {
                                                columns++;
                                                continue;
                                            }

                                            if ((ACdata == "|" || ACdata == "*" || ACdata == "?"
                                                || ACdata == "+") && Valid_Value == false)
                                            {//ingreso de nullables especiales a laRegister

                                                if (i + 1 < datas[1].Length)
                                                {
                                                    if (ACdata == "|")
                                                    {//verificamos si el dato anterior de laRegister es una concat

                                                        if (Register.Substring(Register.Length - 1, 1) == ".")
                                                        {
                                                            Register = Register.Remove(Register.Length - 1, 1);
                                                        }
                                                        Register += ACdata;
                                                        continue;
                                                    }
                                                    if (datas[1].Substring(i + 1, 1) == "|" && datas[1].Substring(i + 1, 1) != " ")
                                                    {
                                                        Register += ACdata + "|";
                                                        i++;
                                                        columns++;
                                                        continue;
                                                    }
                                                    if (datas[1].Substring(i + 1, 1) != " ")
                                                    {
                                                        while (datas[1].Substring(i + 1, 1) == " ")
                                                        {
                                                            ACdata = datas[1].Substring(i + 1, 1);
                                                            i++;
                                                            columns++;
                                                        }

                                                        if (datas[1].Substring(i + 1, 1) == "|")
                                                        {
                                                            Register += ACdata + "|";
                                                            i++;
                                                            columns++;
                                                            continue;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (Register[Register.Length - 1] == '.')
                                                        {
                                                            Register = Register.Remove(Register.Length - 1, 1);
                                                        }
                                                        Register += ACdata;
                                                    }
                                                }
                                                else
                                                {//falta agregarle el numeral del final
                                                 //tiene una concat que hay que quitar
                                                    if (Register[Register.Length - 1] == '.')
                                                    {
                                                        Register = Register.Remove(Register.Length - 1, 1);
                                                    }
                                                    Register += ACdata;
                                                }
                                                continue;

                                            }

                                            if ((ACdata == "|" || ACdata == "*" || ACdata == "?"
                                                || ACdata == "+") && Valid_Value == true)
                                            {// es parte de algunSetList
                                                for (int k = 0; k < SETS.Count; k++)
                                                {
                                                    if (SETS.ElementAt(k).Value.Contains(ACdata))
                                                    {
                                                        Register += ACdata + "Æ.";
                                                        exists = true;
                                                        break;
                                                    }
                                                }

                                                if (exists)
                                                {
                                                    continue;
                                                }
                                            }

                                            if (ACdata != "'" && ACdata != "\"" && Valid_Value == false)
                                            {//posible ingreso deSetList completo

                                                if (i + 1 < datas[1].Length)
                                                {
                                                    while (i + 1 < datas[1].Length)
                                                    {
                                                        if (datas[1].Substring(i + 1, 1) != "\t" && datas[1].Substring(i + 1, 1) != " ")
                                                        {
                                                            ACdata += datas[1].Substring(i + 1, 1);
                                                            i++;
                                                            columns++;
                                                        }
                                                        else
                                                        {
                                                            break;
                                                        }

                                                    }
                                                }
                                                //aqui los espacios son los que denotan fin de chunk de texto


                                                if (SETS.ContainsKey(ACdata))
                                                {
                                                    i++;
                                                    columns++;
                                                    if (i + 1 < datas[1].Length && datas[1].Substring(i + 1, 1) == "|")
                                                    {
                                                        Register += ACdata + "|";
                                                        i++;
                                                        columns++;
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        Register += ACdata + ".";
                                                        continue;
                                                    }

                                                }
                                                else
                                                {
                                                    throw new Exception("Error en line No: " + line + "," + "columns: " + columns);
                                                }
                                            }

                                            if (Valid_Value == true && ACdata != "'")
                                            {
                                                string word = ACdata;

                                                while (datas[1].Substring(i + 1, 1) != "'" && i + 1 < datas[1].Length)
                                                {
                                                    ACdata = datas[1].Substring(i + 1, 1);
                                                    word += ACdata;
                                                    i++;
                                                    columns++;
                                                }

                                                if (i + 2 < datas[1].Length)
                                                {
                                                    if (datas[1].Substring(i + 2, 1) == "'")
                                                    {
                                                        for (int j = 0; j < SETS.Count; j++)
                                                        {
                                                            if (SETS.ElementAt(j).Value.Contains(ACdata))
                                                            {
                                                                exists = true;
                                                                break;
                                                            }

                                                        }

                                                        if (exists)
                                                        {
                                                            Register += ACdata + ".";
                                                            columns = columns + 2;
                                                            continue;
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Error en line No: " + line + "," + "columns: " + columns);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        Register += ACdata + ".";
                                                    }

                                                }
                                                else
                                                {//se busca el dato en los SETS
                                                    for (int j = 0; j < SETS.Count; j++)
                                                    {
                                                        if (SETS.ElementAt(j).Value.Contains(ACdata))
                                                        {
                                                            exists = true;
                                                            break;
                                                        }
                                                    }

                                                    if (exists)
                                                    {
                                                        Register += ACdata + ".";
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        throw new Exception("Error en line No: " + line + "," + "columns: " + columns);
                                                    }

                                                }
                                            }
                                        }

                                        if (Register.Substring(Register.Length - 1, 1) == ".")
                                        {
                                            Register = Register.Remove(Register.Length - 1, 1);
                                        }

                                        FinalExpression += "(" + Register + ")" + "|";
                                    }

                                    if (value == "ACTIONS")
                                    {
                                        while (!streamReader.EndOfStream)
                                        {
                                            readline = streamReader.ReadLine();

                                            if (readline.Contains("ERROR") && readline.Contains('='))
                                            {
                                                string[] split = readline.Split('=');

                                                if (split[0].Trim() == "ERROR")
                                                {
                                                    ACTIONS.Add(Convert.ToInt32(split[1].Trim()), "ERROR");
                                                }
                                            }

                                            if (readline.Contains("{"))
                                            {
                                                string nombre_SetList = "";
                                                string[] datas = readline.Split('(');
                                                nombre_SetList = datas[0];

                                                while (!streamReader.EndOfStream && !readline.Contains("}"))
                                                {

                                                    string number1 = "";
                                                    string dato = "";
                                                    bool insert = false;
                                                    readline = streamReader.ReadLine();

                                                    for (int i = 0; i < readline.Length; i++)
                                                    {
                                                        if (readline.Contains("}"))
                                                        {
                                                            break;
                                                        }

                                                        if (readline.Substring(i, 1) == "=" || insert == true)
                                                        {
                                                            insert = true;

                                                            if (readline.Substring(i, 1) == "\t" || readline.Substring(i, 1) == " ")
                                                            {
                                                                continue;
                                                            }
                                                            else
                                                            {
                                                                if (readline.Substring(i, 1) == "\'")
                                                                {
                                                                    i = i + 1;

                                                                    while (readline.Substring(i, 1) != "\'")
                                                                    {
                                                                        try
                                                                        {
                                                                            dato += readline.Substring(i, 1);
                                                                            i = i + 1;
                                                                        }
                                                                        catch (Exception)
                                                                        {

                                                                            throw new Exception("Error parsing de ACTIONS");
                                                                        }

                                                                    }


                                                                    ACTIONS.Add(Convert.ToInt32(number1.Trim()), dato.Trim());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (readline.Substring(i, 1).Trim() == "\t" || readline.Substring(i, 1).Trim() == " ")
                                                            {
                                                                continue;
                                                            }
                                                            number1 += readline.Substring(i, 1).Trim();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                return FinalExpression;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}

