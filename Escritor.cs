using System;
using System.IO;
using Godot;

public partial class Escritor:Node2D{
    public void GeraCsv(String data){
        string filePath = ".\\arquivo.csv";
        try{
            // Verifica se o arquivo já existe
            bool fileExists = File.Exists(filePath);

            // Usa StreamWriter para abrir o arquivo no modo append
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                // Se o arquivo não existe, escreve o cabeçalho (opcional)
                if (!fileExists){
                    writer.WriteLine("ID,Pixels Internos,Pixels Borda,Pixels Internos (env. conv),Pixels Borda (env. conv),Perimetro,Diametro Maior,Diametro Menor");
                }

                // Escreve a string no arquivo
                writer.WriteLine(data);
            }

            GD.Print("Dados gravados com sucesso no arquivo CSV.");
        }catch (Exception ex){
            GD.Print("Erro ao gravar no arquivo CSV: " + ex.Message);
        }

    }
}