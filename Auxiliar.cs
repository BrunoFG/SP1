using System;
using System.Collections.Generic;
using System.Linq;
public class Auxiliar
{

    public (int, int) MedeDiametro(int[,] tabela)
    {
        List<int> listaDeDiametros = new();
        int temp = 0;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (tabela[i, j] != 0)
                {
                    temp++;
                }
                else if (temp != 0)
                {
                    listaDeDiametros.Add(temp);
                    temp = 0;
                }
            }
            if (temp != 0)
            {
                listaDeDiametros.Add(temp);
                temp = 0;
            }
        }
        temp = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (tabela[j, i] != 0)
                {
                    temp++;
                }
                else if (temp != 0)
                {
                    listaDeDiametros.Add(temp);
                    temp = 0;
                }
            }
            if (temp != 0)
            {
                listaDeDiametros.Add(temp);
                temp = 0;
            }
        }
        return (listaDeDiametros.Max(), listaDeDiametros.Min());
    }
}